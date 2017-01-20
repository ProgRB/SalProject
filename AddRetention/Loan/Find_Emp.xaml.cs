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

namespace Salary.Loan
{
    /// <summary>
    /// Interaction logic for Find_Emp.xaml
    /// </summary>
    public partial class Find_Emp : Window
    {
        private decimal _transfer_id;
        public decimal Transfer_ID { get { return _transfer_id; } }

        private decimal _worker_id;
        public decimal Worker_ID { get { return _worker_id; } }

        private decimal? _from_position;
        public decimal? From_Position { get { return _from_position; } }

        private string _per_num;
        public string Per_Num { get { return _per_num; } }

        private string _last_name;
        public string Last_Name { get { return _last_name; } }

        private string _first_name;
        public string First_Name { get { return _first_name; } }

        private string _middle_name;
        public string Middle_Name { get { return _middle_name; } }

        private string _comb;
        public string Comb { get { return _comb; } }

        private int _sign_Comb;
        public int Sign_Comb { get { return _sign_Comb; } }

        private string _pos_name;
        public string Pos_Name { get { return _pos_name; } }

        private string _subdiv_name;
        public string Subdiv_Name { get { return _subdiv_name; } }

        private string _code_subdiv;
        public string Code_Subdiv { get { return _code_subdiv; } }

        private string _emp_Sex;
        public string Emp_Sex { get { return _emp_Sex; } }

        private DateTime _birth_Date;
        public DateTime Birth_Date { get { return _birth_Date; } }

        private static DataSet _ds = new DataSet();
        private static OracleDataAdapter _daEmp = new OracleDataAdapter();

        DateTime _dateTransfer;
        public Find_Emp(DateTime dateTransfer)
        {
            _daEmp.SelectCommand = new OracleCommand(string.Format(Queries.GetQuery("Loan/SelectEmp_Find_Emp.sql"),
                Connect.SchemaApstaff), Connect.CurConnect);
            _daEmp.SelectCommand.BindByName = true;
            _daEmp.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date);
            _daEmp.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date);

            InitializeComponent();
            _dateTransfer = dateTransfer;
            GetEmp();
            _ds.Tables["EMP"].DefaultView.RowFilter = "";
            dgEmp.ItemsSource = _ds.Tables["EMP"].DefaultView;
            tbLast_Name.Focus();

        }

        private static RoutedUICommand _selectEmp;
        static Find_Emp()
        {
            _ds.Tables.Add("EMP");
            _selectEmp = new RoutedUICommand("Выбрать сотрудника", "SelectEmp", typeof(Find_Emp));
        }
        
        public static RoutedUICommand SelectEmp
        {
            get { return Find_Emp._selectEmp; }
            set { Find_Emp._selectEmp = value; }
        }

        void GetEmp()
        {
            dgEmp.DataContext = null;
            _ds.Tables["EMP"].Clear();
            _daEmp.SelectCommand.Parameters["p_date_begin"].Value = _dateTransfer;
            _daEmp.SelectCommand.Parameters["p_date_end"].Value = _dateTransfer.AddDays(1).AddSeconds(-1);
            _daEmp.Fill(_ds.Tables["EMP"]);
            dgEmp.DataContext = _ds.Tables["EMP"].DefaultView;
        }

        private void Find_TextChanged(object sender, TextChangedEventArgs e)
        {
            string strFilter = "";
            if (tbPer_Num.Text.Trim() != "")
                strFilter = "PER_NUM like '%" + tbPer_Num.Text.Trim() + "%'";
            if (tbLast_Name.Text.Trim() != "")
                strFilter = (strFilter != "" ? strFilter + " and " : "") + "LAST_NAME like '" + tbLast_Name.Text.Trim().ToUpper() + "%'";
            if (tbFirst_Name.Text.Trim() != "")
                strFilter = (strFilter != "" ? strFilter + " and " : "") + "FIRST_NAME like '" + tbFirst_Name.Text.Trim().ToUpper() + "%'";
            if (tbMiddle_Name.Text.Trim() != "")
                strFilter = (strFilter != "" ? strFilter + " and " : "") + "MIDDLE_NAME like '" + tbMiddle_Name.Text.Trim().ToUpper() + "%'";
            _ds.Tables["EMP"].DefaultView.RowFilter = strFilter;
        }

        private void btExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void dgEmp_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Find_Emp.SelectEmp.Execute(null, null);
        }

        private void SelectEmp_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (dgEmp != null && dgEmp.SelectedItem != null)
            {
                e.CanExecute = true;
            }
        }

        private void SelectEmp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataRowView drView = dgEmp.SelectedItem as DataRowView;
            _transfer_id = Convert.ToDecimal(drView["TRANSFER_ID"]);
            _per_num = drView["PER_NUM"].ToString();
            _last_name = drView["LAST_NAME"].ToString();
            _first_name = drView["FIRST_NAME"].ToString();
            _middle_name = drView["MIDDLE_NAME"].ToString();
            _pos_name = drView["POS_NAME"].ToString();
            _subdiv_name = drView["SUBDIV_NAME"].ToString();
            _code_subdiv = drView["CODE_SUBDIV"].ToString();
            _comb = drView["SIGN_COMB"].ToString();
            _emp_Sex = drView["EMP_SEX"].ToString();
            _birth_Date = Convert.ToDateTime(drView["EMP_BIRTH_DATE"]);
            _sign_Comb = Convert.ToInt16(drView["SIGN_COMB"]);
            _worker_id = Convert.ToDecimal(drView["WORKER_ID"]);
            _from_position = drView.Row.Field2<decimal?>("FROM_POSITION");
            this.DialogResult = true;
            this.Close();
        }
    }
}
