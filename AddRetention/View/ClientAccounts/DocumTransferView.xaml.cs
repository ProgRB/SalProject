using EntityGenerator;
using LibrarySalary.Helpers;
using Oracle.DataAccess.Client;
using Salary.Helpers;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Salary.View.ClientAccounts
{
    /// <summary>
    /// Логика взаимодействия для DocumTransferView.xaml
    /// </summary>
    public partial class DocumTransferView : UserControl
    {
        private DocumTransferViewModel _model;

        public DocumTransferView()
        {
            _model = new DocumTransferViewModel();
            InitializeComponent();
            if (Model.TypeCartularySource.Count > 0)
                Model.CurrentTypeCartulary = Model.TypeCartularySource[0].TypeCartularyID;
            Model.LoadDocumTransfer();
            DataContext = Model;
        }

        public DocumTransferViewModel Model
        {
            get
            {
                return _model;
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Model.LoadDocumTransfer();
        }

        private void Add_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        private void Add_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DocumTransferEditor f = new DocumTransferEditor(Model.CurrentTypeCartulary.Value, null);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.LoadDocumTransfer();
            }
        }

        private void Edit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model != null && Model.CurrentDocumTransfer != null;
        }

        private void Edit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DocumTransferEditor f = new DocumTransferEditor(Model.CurrentTypeCartulary.Value, Model.CurrentDocumTransferID);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.LoadDocumTransfer();
            }
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранный документ?", "Удаление документа", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Exception ex = Model.DeleteCurrentDocum();
                if (ex != null)
                    MessageBox.Show(ex.GetFormattedException(), "Ошибка удаления документа");
                else
                    Model.LoadDocumTransfer();
            }
        }
    }

    public class DocumTransferViewModel:NotificationObject
    {
        DataSet ds;
        OracleDataAdapter odaTransferDocum, odaDocumRelation;
        public DocumTransferViewModel()
        {
            ds = new DataSet();
            odaTransferDocum = new OracleDataAdapter(LibrarySalary.Helpers.Queries.GetQueryWithSchema(@"ClientAccountTransfer/SelectDocumentTransferView.sql"), Connect.CurConnect);
            odaTransferDocum.SelectCommand.BindByName = true;
            odaTransferDocum.SelectCommand.Parameters.Add("p_type_cartulary_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaTransferDocum.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, null, ParameterDirection.Input);
            odaTransferDocum.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, null, ParameterDirection.Input);
            odaTransferDocum.TableMappings.Add("Table", "DOCUM_TRANSFER");

            odaDocumRelation = new OracleDataAdapter(LibrarySalary.Helpers.Queries.GetQueryWithSchema(@"ClientAccountTransfer/SelectDocumentTransferRelView.sql"), Connect.CurConnect);
            odaDocumRelation.SelectCommand.BindByName = true;
            odaDocumRelation.SelectCommand.Parameters.Add("p_docum_transfer_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaDocumRelation.TableMappings.Add("Table", "DOCUM_TRANSFER_RELATION");

            OracleDataAdapter odaTypeCart = new OracleDataAdapter(LibrarySalary.Helpers.Queries.GetQueryWithSchema(@"ClientAccountTransfer/SelectCartularyTypeAccess.sql"), Connect.CurConnect);
            odaTypeCart.SelectCommand.BindByName = true;
            odaTypeCart.TableMappings.Add("Table", "TYPE_CARTULARY");
            odaTypeCart.Fill(ds);
        }

        /// <summary>
        /// Источник данных типы реестров
        /// </summary>
        public List<TypeCartulary> TypeCartularySource
        {
            get
            {
                return ds.Tables["TYPE_CARTULARY"].ConvertToEntityList<TypeCartulary>();
            }
        }

        /// <summary>
        /// Адишник текущего выбранного типа реестра
        /// </summary>
        [OracleParameterMapping(ParameterName ="p_type_cartulary_id")]
        public decimal? CurrentTypeCartulary
        {
            get
            {
                return _currentCartularyID;
            }
            set
            {
                _currentCartularyID = value;
                RaisePropertyChanged(() => CurrentTypeCartulary);
            }
        }

        DataView _documTransferView;
        private decimal? _currentCartularyID;
        private DateTime? _dateBegin = DateTime.Today.Trunc("Month"), _dateEnd = DateTime.Today.Trunc("Month").AddMonths(1).AddSeconds(-1);
        private DataRowView _currentDocumTransfer;

        /// <summary>
        /// Дата начала периода фильтра
        /// </summary>
        [OracleParameterMapping(ParameterName ="p_date_begin")]
        public DateTime? DateBegin
        {
            get
            {
                return _dateBegin;
            }
            set
            {
                _dateBegin = value;
                RaisePropertyChanged(() => DateBegin);
            }
        }

        /// <summary>
        /// Дата окончания периода фильтра
        /// </summary>
        [OracleParameterMapping(ParameterName = "p_date_end")]
        public DateTime? DateEnd
        {
            get
            {
                return _dateEnd;
            }
            set
            {
                _dateEnd = value;
                RaisePropertyChanged(() => DateEnd);
            }
        }

        public List<DataRowView> DocumTransferSource
        {
            get
            {
                if (_documTransferView == null)
                    return null;
                else
                    return _documTransferView.OfType<DataRowView>().ToList();
            }
        }

        /// <summary>
        /// Процедура загрузки данных по приказам (документам)
        /// </summary>
        public void LoadDocumTransfer()
        {
            decimal? currentDocID = CurrentDocumTransferID;
            Exception ex =  odaTransferDocum.TryFillWithClear(ds, this);
            if (ex != null)
                MessageBox.Show(ex.GetFormattedException(), "Ошибка загрузки данных");
            else
                if (_documTransferView == null)
                    _documTransferView = new DataView(ds.Tables["DOCUM_TRANSFER"], "", "", DataViewRowState.CurrentRows);
            RaisePropertyChanged(() => DocumTransferSource);
            CurrentDocumTransfer = DocumTransferSource.Where(r => r.Row.Field2<decimal?>("DOCUM_TRANSFER_ID") == currentDocID).FirstOrDefault();
        }

        public DataRowView CurrentDocumTransfer
        {
            get
            {
                return _currentDocumTransfer;
            }
            set
            {
                _currentDocumTransfer = value;
                RaisePropertyChanged(() => CurrentDocumTransfer);
                LoadDocumRelation();
            }
        }

        /// <summary>
        /// Айдишник текущего выбранного документа
        /// </summary>
        [OracleParameterMapping(ParameterName = "p_DOCUM_TRANSFER_ID")]
        public Decimal? CurrentDocumTransferID
        {
            get
            {
                return CurrentDocumTransfer == null ? null : CurrentDocumTransfer.Row.Field2<Decimal?>("DOCUM_TRANSFER_ID");
            }
        }

        DataView _documRelationView;
        /// <summary>
        /// Источик данных для данных документа
        /// </summary>
        public List<DataRowView> DocumTransferRelationSource
        {
            get
            {
                if (_documRelationView == null)
                    return null;
                else
                    return _documRelationView.OfType<DataRowView>().ToList(); 
            }
        }

        /// <summary>
        /// Загрузка данных по документу
        /// </summary>
        public void LoadDocumRelation()
        {
            Exception ex = odaDocumRelation.TryFillWithClear(ds, this);
            if (ex != null)
                MessageBox.Show(ex.GetFormattedException(), "Ошибка загрузки данных по документу");
            else
                if (_documRelationView == null)
                _documRelationView = new DataView(ds.Tables["DOCUM_TRANSFER_RELATION"], "", "", DataViewRowState.CurrentRows);
            RaisePropertyChanged(() => DocumTransferRelationSource);
        }

        /// <summary>
        /// Удаление текущего выбранного элемента
        /// </summary>
        /// <returns></returns>
        internal Exception DeleteCurrentDocum()
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                var DeleteCommand = new OracleCommand(string.Format(@"BEGIN {1}.DOCUM_TRANSFER_DELETE(:p_DOCUM_TRANSFER_ID);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                DeleteCommand.BindByName = true;
                DeleteCommand.Parameters.Add("p_DOCUM_TRANSFER_ID", OracleDbType.Decimal, CurrentDocumTransferID, ParameterDirection.InputOutput);
                DeleteCommand.ExecuteNonQuery();
                tr.Commit();
                return null;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                return ex;
            }
        }
    }
}
