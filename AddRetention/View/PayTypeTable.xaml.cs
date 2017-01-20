using System;
using System.Collections.Generic;
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
using System.Data;
using Oracle.DataAccess.Client;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using LibrarySalary.Helpers;
using Salary.Helpers;
using Salary.Reports;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for PaymentTypeTable.xaml
    /// </summary>
    public partial class PayTypeTable : UserControl, INotifyPropertyChanged
    {
        DataSet ds;
        OracleDataAdapter odaPaymentType, odaRelation, odaProperty;
        OracleCommand cmd_delete;
        OracleCommand cmd_inc;
        static RoutedUICommand _decPayTypePriority = new RoutedUICommand("Уменьшить очередность расчета на 1", Salary.ViewModel.AppCommands.IncPayTypePriority.Name, typeof(PayTypeTable));
        public PayTypeTable()
        {
            InitializeComponent();
            ds = new DataSet();
            cmd_delete = new OracleCommand(string.Format("begin {1}.PAYMENT_TYPE_DELETE(:p_PAYMENT_TYPE_ID);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            cmd_delete.BindByName = true;
            cmd_delete.Parameters.Add("p_payment_type_id", OracleDbType.Decimal, 0, ParameterDirection.Input);
            ds.Tables.Add("PaymentType");
            odaPaymentType = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectPayType.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaPaymentType.SelectCommand.BindByName = true;
            cmd_inc = new OracleCommand(string.Format("begin {1}.IncrPaymentTypePriority(:p_payment_type_id, :p_inc_number); end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            cmd_inc.BindByName = true;
            cmd_inc.Parameters.Add("p_payment_type_id", OracleDbType.Array, ParameterDirection.Input).UdtTypeName = Connect.SchemaApstaff.ToUpper()+".TYPE_TABLE_NUMBER";
            cmd_inc.Parameters.Add("p_inc_number", OracleDbType.Decimal, ParameterDirection.Input);
            odaRelation = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectPaymentCalcRelation.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaRelation.SelectCommand.BindByName=true;
            odaRelation.SelectCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, ParameterDirection.Input);
            odaRelation.TableMappings.Add("Table", "PAYMENT_CALC_RELATION");
            odaProperty = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectPaymentProperties.sql"), Connect.CurConnect); ;
            odaProperty.SelectCommand.BindByName = true;
            odaProperty.SelectCommand.Parameters.Add("p_relation_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaProperty.TableMappings.Add("Table", "PAYMENT_PROPERTY");
            this.DataContext = this;
        }

        /// <summary>
        /// Загрузка данных по видам оплат
        /// </summary>
        private void FillData()
        {
            //ds.Tables["PaymentType"].Rows.Clear();
            odaPaymentType.Fill(ds, "PaymentType");
            if (ds.Tables["PaymentType"].PrimaryKey == null || ds.Tables["PaymentType"].PrimaryKey.Length==0)
                ds.Tables["PaymentType"].PrimaryKey = new DataColumn[] { ds.Tables["PaymentType"].Columns["PAYMENT_TYPE_ID"] };
        }

        /// <summary>
        /// Загрузка методов для вида оплат
        /// </summary>
        private void FillRelation()
        {
            if (ds.Tables.Contains("PAYMENT_CALC_RELATION"))
                ds.Tables["PAYMENT_CALC_RELATION"].Rows.Clear();
            if (CurrentPaymentType != null)
            {
                odaRelation.SelectCommand.Parameters["p_payment_type_id"].Value = CurrentPaymentType["PAYMENT_TYPE_ID"];
                odaRelation.Fill(ds);
            }
        }

        private void FillProperty()
        {
            if(ds.Tables.Contains("PAYMENT_PROPERTY"))
                ds.Tables["PAYMENT_PROPERTY"].Rows.Clear();
            if (CurrentRelation != null)
            {
                odaProperty.SelectCommand.Parameters["p_relation_id"].Value = CurrentRelation["payment_calc_relation_id"];
                odaProperty.Fill(ds);
            }

        }

        DataView paymentSource;
        public DataView PaymentSource
        {
            get
            {
                FillData();
                if (ds != null && ds.Tables.Contains("PaymentType"))
                    paymentSource = new DataView(ds.Tables["PaymentType"], "", "CODE_PAYMENT", DataViewRowState.CurrentRows);
                return paymentSource;
            }
        }

        DataRowView _currentPaymentType;
        public DataRowView CurrentPaymentType
        {
            get
            {
                return _currentPaymentType;
            }
            set
            {
                _currentPaymentType = value;
                OnPropertyChanged("CurrentPaymentType");
                OnPropertyChanged("RelationSource");
                if (RelationSource != null && RelationSource.Count > 0)
                    CurrentRelation = RelationSource[0];
            }
        }

        DataView relationSource;
        public DataView RelationSource
        {
            get
            {
                FillRelation();
                if (ds != null && ds.Tables.Contains("PAYMENT_CALC_RELATION"))
                    relationSource = new DataView(ds.Tables["PAYMENT_CALC_RELATION"], "", "DATE_START_CALC desc", DataViewRowState.CurrentRows);
                return relationSource;
            }
        }

        DataRowView _currentRelation;
        public DataRowView CurrentRelation
        {
            get
            {
                return _currentRelation;
            }
            set
            {
                _currentRelation = value;
                OnPropertyChanged("CurrentRelation");
                OnPropertyChanged("PropertySource");
            }
        }

        DataView propertySource;
        public DataView PropertySource
        {
            get
            {
                FillProperty();
                if (ds != null && ds.Tables.Contains("PAYMENT_PROPERTY"))
                    propertySource = new DataView(ds.Tables["PAYMENT_PROPERTY"], "", "PROPERTY_NAME", DataViewRowState.CurrentRows);
                return propertySource;
            }
        }

        private void DeletePayType_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить шифр оплат?", "",  MessageBoxButton.YesNo, MessageBoxImage.Warning)== MessageBoxResult.Yes)
            {
                try
                {
                    cmd_delete.Parameters["p_payment_type_id"].Value = CurrentPaymentType["PAYMENT_TYPE_ID"];
                    cmd_delete.ExecuteNonQuery();
                    FillData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.GetFormattedException());
                }
            }
        }

        private void AddCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name);
        }

        private void EditCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name) && CurrentPaymentType!=null;
        }

        private void EditPayType_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PayTypeEditor f = new PayTypeEditor(CurrentPaymentType["PAYMENT_TYPE_ID"]);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog()==true)
                FillData();
        }

        private void AddPayType_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PayTypeEditor f = new PayTypeEditor(null);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
                FillData();
        }

        private void InctPayType_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Поднять порядок расчета выбранных шифров оплат на ЕДИНИЦУ?", "ЗП Предприятия", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                cmd_inc.Parameters["p_payment_type_id"].Value = dgPaymentType.SelectedItems.Cast<DataRowView>().Select(p=>(decimal)p["PAYMENT_TYPE_ID"]).ToArray();
                cmd_inc.Parameters["p_inc_number"].Value = -1m;
                try
                {
                    cmd_inc.ExecuteNonQuery();
                    FillData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.GetFormattedException(), "Ошибка выполнения команды");
                }

            }
        }

        private void IncPriotityPayTypeCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name) && dgPaymentType != null && dgPaymentType.SelectedItems.Count>0;
        }

        public static RoutedUICommand DecPayTypePriority 
        {
            get
            {
                return _decPayTypePriority;
            }
        }

        private void DectPayType_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Опустить порядок расчета выбранных шифров оплат на ЕДИНИЦУ?", "ЗП Предприятия", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                cmd_inc.Parameters["p_payment_type_id"].Value = dgPaymentType.SelectedItems.Cast<DataRowView>().Select(p => (decimal)p["PAYMENT_TYPE_ID"]).ToArray();
                cmd_inc.Parameters["p_inc_number"].Value = 1m;
                try
                {
                    cmd_inc.ExecuteNonQuery();
                    FillData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.GetFormattedException(), "Ошибка выполнения команды");
                }

            }
        }

        private void AddCalcRelation_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PayCalcRelationEditor f = new PayCalcRelationEditor(null, CurrentPaymentType["PAYMENT_TYPE_ID"]);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog()==true)
            {
                FillRelation();
            }
        }

        private void EditCalcRelation_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name) && CurrentRelation!=null;
        }

        public decimal? CurrentPaymentTypeID
        {
            get
            {
                if (CurrentPaymentType != null)
                    return CurrentPaymentType.Row.Field2<Decimal?>("PAYMENT_TYPE_ID");
                else return null;
            }
        }
        public decimal? CurrentRelationID
        {
            get
            {
                if (CurrentRelation != null)
                    return CurrentRelation.Row.Field2<Decimal?>("PAYMENT_CALC_RELATION_ID");
                else return null;
            }
        }
        private void EditCalcRelation_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PayCalcRelationEditor f = new PayCalcRelationEditor(CurrentRelationID, e.Parameter==null? CurrentPaymentTypeID:null, e.Parameter==null? null : CurrentRelationID);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                FillRelation();
            }
        }

        OracleCommand cmdDeleteRelation;
        private void DeleteCalcRelation_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранный метод расчета для шифра оплат?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (cmdDeleteRelation == null)
                {
                    cmdDeleteRelation = new OracleCommand(string.Format("begin {0}.PAYMENT_CALC_RELATION_DELETE(:p_calc_relation_id);end;", Connect.SchemaSalary), Connect.CurConnect);
                    cmdDeleteRelation.Parameters.Add("p_calc_relation_id", OracleDbType.Decimal, null, ParameterDirection.Input);
                }
                cmdDeleteRelation.Parameters["p_calc_relation_id"].Value = CurrentRelationID;
                OracleTransaction tr = Connect.CurConnect.BeginTransaction();
                try
                {
                    cmdDeleteRelation.ExecuteNonQuery();
                    tr.Commit();
                    FillRelation();
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    MessageBox.Show(ex.GetFormattedException(), "Ошибка удаления записи");
                }
            }
        }

    
        public event PropertyChangedEventHandler  PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged!=null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private void Print_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Print_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataTable t = new DataTable();
            t = PaymentSource.Table.Rows.OfType<DataRow>().Select(r=> 
                    new {
                            CODE_PAYMENT=r.Field2<string>("CODE_PAYMENT"),
                            TYPE_MESSAGE=r.Field2<string>("NAME_PAYMENT"),
                            TYPE_PAYMENT_TYPE_NAME=r.Field2<string>("TYPE_PAYMENT_TYPE_NAME")
                        }).CopyToDataTable();
            ViewReportWindow.ShowReport(this, "Справочник видов оплат", "Rep_PaymentTypeData.rdlc", t, null);
        }
}
    public class NumberToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == DBNull.Value || value == null || value.ToString() == "0")
                return false;
            else return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Boolean)
                if ((Boolean)value) return 1; else return 0;
            else
                return 0;
        }
    }
}
