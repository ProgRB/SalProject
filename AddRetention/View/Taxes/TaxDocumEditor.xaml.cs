using EntityGenerator;
using LibrarySalary.Helpers;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Salary.Helpers;
using System.Data.Linq.Mapping;

namespace Salary.View.Taxes
{
    /// <summary>
    /// Interaction logic for TaxDocumEditor.xaml
    /// </summary>
    public partial class TaxDocumEditor : Window
    {
        private TaxEmpDocumentModel _model;
        public TaxDocumEditor(decimal? taxDocumId)
        {
            _model = new TaxEmpDocumentModel(Connect.CurConnect, taxDocumId);
            InitializeComponent();
            DataContext = Model;
        }

        static TaxDocumEditor()
        {
            ChooseEmp = new RoutedUICommand("Выбрать сотрудника", "EditTaxCompany", typeof(TaxDocumEditor));
        }

        /// <summary>
        /// Модель представления данных для формы
        /// </summary>
        public TaxEmpDocumentModel Model
        {
            get
            {
                return _model;
            }
        }

        public static RoutedUICommand ChooseEmp { get; private set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ChooseEmp_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ChooseEmp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            EmpFinder f = new EmpFinder();
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.PerNum = f.PerNum;
            }
        }
    }

    /// <summary>
    /// Модель для обработки и обновления данных документа налога на доходы
    /// </summary>
    public partial class TaxEmpDocumentModel : TaxEmpDocum
    {
        DataSet ds;
        OracleDataAdapter odaPerData;
        OracleConnection CurConnect;
        private EntityRelationList<TaxDocumPayment> _paySource;
        private EntityRelationList<TaxDocumDiscount> _discountSource;
        private OracleDataAdapter odaTaxEmpDocum;
        private string _exception;

        public TaxEmpDocumentModel(OracleConnection _connect, decimal? tax_emp_docum_id)
        { 
            CurConnect = _connect;
            ds = new DataSet();

            odaPerData = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Taxes/SelectTaxEmpData.sql"), CurConnect);
            odaPerData.SelectCommand.BindByName = true;
            odaPerData.SelectCommand.Parameters.Add("p_per_num", OracleDbType.Varchar2, string.Empty, ParameterDirection.Input);
            odaPerData.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaPerData.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            odaPerData.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
            odaPerData.TableMappings.Add("Table", "EMP");
            odaPerData.TableMappings.Add("Table1", "PER_DATA");
            odaPerData.TableMappings.Add("Table2", "PASSPORT");

            odaTaxEmpDocum = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Taxes/SelectTaxDocumData.sql"), CurConnect);
            odaTaxEmpDocum.SelectCommand.BindByName = true;
            odaTaxEmpDocum.SelectCommand.Parameters.Add("p_TAX_EMP_DOCUM_ID", OracleDbType.Decimal, tax_emp_docum_id, ParameterDirection.Input);
            odaTaxEmpDocum.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaTaxEmpDocum.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            odaTaxEmpDocum.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
            odaTaxEmpDocum.SelectCommand.Parameters.Add("c4", OracleDbType.RefCursor, ParameterDirection.Output);
            odaTaxEmpDocum.TableMappings.Add("Table", "TAX_EMP_DOCUM");
            odaTaxEmpDocum.TableMappings.Add("Table1", "TAX_COMPANY");
            odaTaxEmpDocum.TableMappings.Add("Table2", "TAX_DOCUM_DISCOUNT");
            odaTaxEmpDocum.TableMappings.Add("Table3", "TAX_DOCUM_PAYMENT");



            try
            {
                odaTaxEmpDocum.Fill(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка получения данных");
                return;
            }

            if (ds.Tables["TAX_EMP_DOCUM"].Rows.Count == 0)
            {
                DataRow r = ds.Tables["TAX_EMP_DOCUM"].NewRow();
                ds.Tables["TAX_EMP_DOCUM"].Rows.Add(r);
            }
            this.DataRow = ds.Tables["TAX_EMP_DOCUM"].Rows[0];
            UpdateEmpData();
        }

        /// <summary>
        /// Список организаций
        /// </summary>
        public List<TaxCompany> TaxCompanySource
        {
            get
            {
                return ds.Tables["TAX_COMPANY"].ConvertToEntityList<TaxCompany>();
            }
        }

        /// <summary>
        /// Перегрузим компанию чтобы обновлялись данные компании
        /// </summary>
        [Column(Name="TAX_COMPANY_ID") ]
        public new decimal? TaxCompanyID
        {
            get
            {
                return base.TaxCompanyID;
            }
            set
            {
                base.TaxCompanyID = value;
                RaisePropertyChanged(() => TaxCompany);
            }
        }

        /// <summary>
        /// Организация выбранная
        /// </summary>
        public TaxCompany TaxCompany
        {
            get
            {
                if (TaxCompanyID == null)
                    return null;
                else
                    return this.GetParentEntity<TaxCompany, decimal?>(()=>TaxCompanyID);
            }
        }

        /// <summary>
        /// Список элементов выплат зарплаты по месяцам
        /// </summary>
        public EntityRelationList<TaxDocumPayment> PaySource
        {
            get
            {
                if (_paySource == null)
                {
                    _paySource = new EntityRelationList<TaxDocumPayment>(ds.Tables["TAX_DOCUM_PAYMENT"].ConvertToEntityList<TaxDocumPayment>()) 
                    { 
                        RelatedEntity = this, 
                        RelationColumn = "TAX_EMP_DOCUM_ID" 
                    };
                    _paySource.AddingNew += _paySource_AddingNew;
                }
                return _paySource;
            }
        }

        private void _paySource_AddingNew(object sender, System.ComponentModel.AddingNewEventArgs e)
        {
            e.NewObject = new TaxDocumPayment()
            {
                DataRow = ds.Tables["TAX_DOCUM_PAYMENT"].Rows.Add(),
                PayDate = new DateTime((this.DocumDate ?? DateTime.Today).Year, 1, 1)
            };
        }

        /// <summary>
        /// Список элементов вычетов по месяцам
        /// </summary>
        public EntityRelationList<TaxDocumDiscount> DiscountSource
        {
            get
            {
                if (_discountSource == null)
                {
                    _discountSource = new EntityRelationList<TaxDocumDiscount>(ds.Tables["TAX_DOCUM_DISCOUNT"].ConvertToEntityList<TaxDocumDiscount>())
                    {
                        RelatedEntity = this,
                        RelationColumn = "TAX_EMP_DOCUM_ID"
                    };
                }
                return _discountSource;
            }
        }

        

        [OracleParameterMapping(ParameterName="p_per_num")]
        public new string PerNum
        {
            get
            {
                return base.PerNum;
            }
            set
            {
                base.PerNum = value;
                UpdateEmpData();
                RaisePropertyChanged(() => Emp);
                RaisePropertyChanged(() => Passport);
                RaisePropertyChanged(() => PerData);
            }
        }

        /// <summary>
        /// Дата документа для формирования отчетности
        /// </summary>
        public new DateTime? DocumDate
        {
            get
            {
                return base.DocumDate;
            }
            set
            {
                if (base.DocumDate.HasValue && value.HasValue)
                { 
                    if (base.DocumDate.Value.Year!=value.Value.Year)
                    {
                        //Если год изменился, то все даты перекидываем в зависимых данных на другой год
                        foreach(var p in  DiscountSource)
                            p.DateDiscount = new DateTime(value.Value.Year, p.DateDiscount.Value.Month, p.DateDiscount.Value.Day);
                        foreach (var p in PaySource)
                            p.PayDate = new DateTime(value.Value.Year, p.PayDate.Value.Month, p.PayDate.Value.Day);
                    }
                }
                base.DocumDate = value;

            }
        }

        /// <summary>
        /// Обновляем персональные данные по табельному номеру
        /// </summary>
        public void UpdateEmpData()
        {
            Exception ex = odaPerData.TryFillWithClear(ds, this);
            if (ex != null)
                DataException = ex.Message;
            else
                DataException = string.Empty;
        }


        /// <summary>
        /// Персональные данны сотрудника
        /// </summary>
        public PerData PerData
        {
            get
            {
                return ds.Tables["PER_DATA"].ConvertToEntityList<PerData>().FirstOrDefault();
            }
        }

        /// <summary>
        /// Данные сотрудника
        /// </summary>
        public EmpAllData Emp
        {
            get
            {
                return ds.Tables["EMP"].ConvertToEntityList<EmpAllData>().FirstOrDefault();
            }
        }

        /// <summary>
        /// Паспортные данные сотрудники
        /// </summary>
        public Passport Passport
        {
            get
            {
                return ds.Tables["PASSPORT"].ConvertToEntityList<Passport>().FirstOrDefault();
            }
        }

        /// <summary>
        /// Всего доход по документу
        /// </summary>
        public decimal? AllPaySum
        {
            get
            {
                return PaySource.Sum(r => r.SumSal);
            }
        }

        /// <summary>
        /// Всего облагаемая база по документу
        /// </summary>
        public decimal? TaxBase
        {
            get
            {
                return PaySource.Sum(r => r.SumSal-(r.SumDisc??0))-(DiscountSource.Sum(r=>r.SumDiscount)??0);
            }
        }

        /// <summary>
        /// Излишне удержанный налог
        /// </summary>
        public decimal? TaxMoreThenCalced
        {
            get
            {
                if (CalcedTax < RetentTax)
                    return RetentTax - CalcedTax;
                else
                    return null;
            }
        }

        /// <summary>
        /// Не удержанный налог по документу
        /// </summary>
        public decimal? TaxLessThenCalced
        {
            get
            {
                if (CalcedTax > RetentTax)
                    return CalcedTax - RetentTax;
                else
                    return null;
            }
        }


        /// <summary>
        /// Ошибка при обновлении данных
        /// </summary>
        public string DataException
        {
            get
            {
                return _exception;
            }
            set
            {
                _exception = value;
                RaisePropertyChanged(() => DataException);
            }
        }
    }
}
