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
using System.Windows.Shapes;
using System.Data;
using Oracle.DataAccess.Client;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for RefSalaryDataEditor.xaml
    /// </summary>
    public partial class RefSalaryDataSelector : Window
    {
        private static RoutedUICommand _selectRow = new RoutedUICommand("Выбрать запись", "SelectRow", typeof(RefSalaryDataSelector));
        DataSet ds = new DataSet();
        private object _selectedValue;
        public RefSalaryDataSelector(object type_ref_salary_id, object selected_id, object transfer_id)
        {
            _selectedValue = selected_id;
            ds = new DataSet();
            OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectRefData.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            a.SelectCommand.Parameters.Add("p_type_ref_salary_id", OracleDbType.Decimal, type_ref_salary_id, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_table_id", OracleDbType.Decimal, selected_id, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, transfer_id, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            a.Fill(ds);
            InitializeComponent();
            string RefColumn = AppDataSet.Tables["TYPE_REF_SALARY"].Select("TYPE_REF_SALARY_ID="+type_ref_salary_id.ToString())[0]["COLUMN_REF"].ToString().ToUpper();
            foreach (DataColumn c in ds.Tables[0].Columns)
                if (c.ColumnName.ToUpper()!=RefColumn)
                {
                    DataGridTextColumn cl = new DataGridTextColumn();
                    cl.Header = c.ColumnName;
                    cl.Binding = new Binding(c.ColumnName);
                    dgRefData.Columns.Add(cl);
                }
            cbTypeRefSalary.DataContext = AppDataSet.Tables["TYPE_REF_SALARY"].DefaultView;
            cbTypeRefSalary.SelectedValue = type_ref_salary_id;
            dgRefData.DataContext = ds.Tables[0].DefaultView;
        }
        public static RoutedUICommand SelectRow
        {
            get
            {
                return _selectRow;
            }
        }

        private void Select_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ds != null && dgRefData != null && dgRefData.SelectedValue != null;
        }

        public object SelectedValue
        {
            get
            {
                return _selectedValue;
            }
            set
            {
                _selectedValue = value;
            }
        }
        private void Select_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
