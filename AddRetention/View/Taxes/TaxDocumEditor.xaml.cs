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
using System.Globalization;
using System.ComponentModel;

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
            EmpFinder f = new EmpFinder(true);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.PerNum = f.PerNum;
            }
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model != null && Model.HasChanges;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Exception ex = Model.Save();
            if (ex != null)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка сохранения");
            }
            else
            {
                if (this.IsLoaded)
                {
                    this.DialogResult = true;
                    Close();
                }
            }
        }
    }

    /// <summary>
    /// Модель для обработки и обновления данных документа налога на доходы
    /// </summary>
    public partial class TaxEmpDocumentModel : TaxEmpDocum, IDataErrorInfo
    {
        DataSet ds;
        OracleDataAdapter odaPerData;
        private EntityRelationList<TaxDocumPayment> _paySource;
        private EntityRelationList<TaxDocumDiscount> _discountSource;
        private OracleDataAdapter odaTaxEmpDocum, odaTax_Docum_Payment, odaTax_Docum_Discount;
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
            odaPerData.SelectCommand.Parameters.Add("c4", OracleDbType.RefCursor, ParameterDirection.Output);
            odaPerData.TableMappings.Add("Table", "EMP");
            odaPerData.TableMappings.Add("Table1", "PER_DATA");
            odaPerData.TableMappings.Add("Table2", "PASSPORT");
            odaPerData.TableMappings.Add("Table3", "ADDRESS");

            odaTaxEmpDocum = GetModelAdapter<TaxEmpDocum>();
            odaTaxEmpDocum.SelectCommand = new OracleCommand(Queries.GetQueryWithSchema(@"Taxes/SelectTaxDocumData.sql"), CurConnect);
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

            odaTax_Docum_Discount = GetModelAdapter<TaxDocumDiscount>();
            odaTax_Docum_Payment = GetModelAdapter<TaxDocumPayment>();

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
                this.DataRow = ds.Tables["TAX_EMP_DOCUM"].Rows[0];
                LockSign = 1;
                DocumDate = new DateTime(DateTime.Today.Year, ((DateTime.Today.Month - 1) / ((int)3)) * 3 + 1, 1).AddDays(-1);
                _isNew = true;
            }
            else
                this.DataRow = ds.Tables["TAX_EMP_DOCUM"].Rows[0];
            UpdateEmpData();
        }

        /// <summary>
        /// Сохранение данных по модели
        /// </summary>
        /// <returns></returns>
        public new Exception Save()
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                if (IsNew && TaxEmpDocumID!=null)
                    TaxEmpDocumID = null;
                odaTaxEmpDocum.Update(new DataRow[] { this.DataRow });
                foreach (var p in PaySource)
                {
                    if (p.TaxDocumPaymentID != null && (IsNew || p.EntityState == DataRowState.Added))
                        p.TaxDocumPaymentID = null;
                    if (p.TaxEmpDocumID == null || p.TaxEmpDocumID != TaxEmpDocumID)
                        p.TaxEmpDocumID = TaxEmpDocumID;
                }
                foreach (var p in DiscountSource)
                {
                    if (p.TaxDocumDiscountID != null && (IsNew || p.EntityState == DataRowState.Added))
                        p.TaxDocumDiscountID = null;
                    if (p.TaxEmpDocumID == null || p.TaxEmpDocumID != TaxEmpDocumID)
                        p.TaxEmpDocumID = TaxEmpDocumID;
                }
                odaTax_Docum_Payment.Update(ds.Tables["TAX_DOCUM_PAYMENT"]);
                odaTax_Docum_Discount.Update(ds.Tables["TAX_DOCUM_DISCOUNT"]);
                tr.Commit();
                return null;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                return ex;
            }
        }

        bool _isNew = false;
        /// <summary>
        /// Является ли документ добавляемым
        /// </summary>
        public bool IsNew
        {
            get
            {
                return _isNew;
            }
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

        public new decimal? TaxPercent
        {
            get
            {
                return base.TaxPercent;
            }
            set
            {
                base.TaxPercent = value;
                CalcedTax = CalcTax();
            }
        }

        public new decimal? CalcedTax
        {
            get
            {
                return base.CalcedTax;
            }
            set
            {
                base.CalcedTax = value;
                RaisePropertyChanged(() => TaxMoreThenCalced);
                RaisePropertyChanged(() => TaxLessThenCalced);
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
                    _paySource.ListChanged += _paySource_ListChanged;
                }
                return _paySource;
            }
        }

        private void _paySource_ListChanged(object sender, ListChangedEventArgs e)
        {
            CalcedTax = CalcTax();
            RaisePropertyChanged(() => AllPaySum);
            RaisePropertyChanged(() => TaxBase);
        }

        private decimal? CalcTax()
        {
            if (this.TaxPercent == 13)
            {
                return Math.Round(((PaySource.Sum(r => r.SumSal - (r.SumDisc??0)) - DiscountSource.Sum(r => r.SumDiscount??0)) * TaxPercent/100) ?? 0, 0, MidpointRounding.AwayFromZero);
            }
            else
            {
                return PaySource.Sum(r=> Math.Round(((r.SumSal - (r.SumDisc??0)) * TaxPercent/100)?? 0, 0, MidpointRounding.AwayFromZero));
            }
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
                    _discountSource.ListChanged += _discountSource_ListChanged;
                }
                return _discountSource;
            }
        }

        private void _discountSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            CalcedTax = CalcTax();
            RaisePropertyChanged(() => TaxBase);
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
                UpdateEmpAddress();
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

        public void UpdateEmpAddress()
        {
            if (ds.Tables["ADDRESS"].Rows.Count > 0)
            {
                Address a = new Address() { DataRow = ds.Tables["ADDRESS"].Rows[0] };
                this.CodeRegion = a.CodeRegion;
                this.CodeCountry = "643";
                this.HomeIndex = a.PostIndex;
                this.District = a.District;
                this.City = a.City;
                this.Locality = a.Locality;
                this.Street = a.Street;
                this.HomeNumber = a.House;
                this.Housing = a.Bulk;
                this.FlatNumber = a.Flat;
            }
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

        public bool HasChanges {
            get
            {
                return ds != null && ds.HasChanges();
            }
        }
    }

    public class MonthToDateConverter : FrameworkElement, IValueConverter
    {

        public static readonly DependencyProperty DateConvertProperty = DependencyProperty.Register("DateConvert", typeof(DateTime?), typeof(MonthToDateConverter),
            new PropertyMetadata(DateTime.Today));

        public DateTime? DateConvert
        {
            get
            {
                return (DateTime?)GetValue(DateConvertProperty);
            }
            set
            {
                SetValue(DateConvertProperty, value);
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value != DependencyProperty.UnsetValue)
            {
                return ((DateTime)value).Month;
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value != DependencyProperty.UnsetValue && DateConvert!=null)
            {
                int k = System.Convert.ToInt32(value);
                if (k >0 && k < 13)
                    return new DateTime(DateConvert.Value.Year, k, 1);
                else
                    return null;
            }
            else
                return null;
        }
    }
}
