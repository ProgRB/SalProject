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
using System.ComponentModel;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for Signes.xaml
    /// </summary>
    public partial class Signes : Window, INotifyPropertyChanged
    {
        private static RoutedUICommand _addSign, _deleteSign, _saveSign, _nextStep;
        private static DataSet ds = new DataSet();
        private static OracleDataAdapter aSign = new OracleDataAdapter();
        private string Code_docum;
        decimal? subdiv_id;
        private int RowCnt = 0;
        private DataTable t;
        private Signes(decimal? Subdiv_id, string DocumentName)
        {
            InitializeComponent();            
            subdiv_id = Subdiv_id;
            Code_docum = DocumentName;
            aSign.SelectCommand = new OracleCommand(string.Format(
                @"select case when DEFAULT_NUMBER is null then 0 else 1 end as FL, sign_doc_id,POS_NAME_SIGN,EMP_NAME,CODE_DOCUM, DEFAULT_NUMBER+0 as default_number,SUBDIV_ID                     
                from {0}.SIGN_DOC
                where upper(TRIM(code_docum))=UPPER(TRIM(:p_code_docum)) and subdiv_id=:p_subdiv_id 
                order by default_number",
                Connect.SchemaApstaff), Connect.CurConnect);
            aSign.SelectCommand.BindByName = true;
            aSign.SelectCommand.Parameters.Add("p_code_docum", OracleDbType.Varchar2).Value = Code_docum;
            aSign.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal).Value = subdiv_id;
            aSign.InsertCommand = new OracleCommand(string.Format(
                "insert into {0}.SIGN_DOC(SIGN_DOC_ID, POS_NAME_SIGN, EMP_NAME, CODE_DOCUM, DEFAULT_NUMBER, SUBDIV_ID) " +
                "values(:SIGN_DOC_ID, :POS_NAME_SIGN, :EMP_NAME, :CODE_DOCUM, :DEFAULT_NUMBER, :SUBDIV_ID)",
                Connect.SchemaApstaff), Connect.CurConnect);
            aSign.InsertCommand.BindByName = true;
            aSign.InsertCommand.Parameters.Add("SIGN_DOC_ID", OracleDbType.Decimal, 0, "SIGN_DOC_ID");
            aSign.InsertCommand.Parameters.Add("POS_NAME_SIGN", OracleDbType.Varchar2, 0, "POS_NAME_SIGN");
            aSign.InsertCommand.Parameters.Add("EMP_NAME", OracleDbType.Varchar2, 0, "EMP_NAME");
            aSign.InsertCommand.Parameters.Add("CODE_DOCUM", OracleDbType.Varchar2, 0, "CODE_DOCUM");
            aSign.InsertCommand.Parameters.Add("DEFAULT_NUMBER", OracleDbType.Decimal, 0, "DEFAULT_NUMBER");
            aSign.InsertCommand.Parameters.Add("SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID");
            aSign.UpdateCommand = new OracleCommand(string.Format(
                @"update {0}.SIGN_DOC 
                set POS_NAME_SIGN=:POS_NAME_SIGN, EMP_NAME=:EMP_NAME, DEFAULT_NUMBER=:DEFAULT_NUMBER 
                where SIGN_DOC_ID=:SIGN_DOC_ID",
                Connect.SchemaApstaff), Connect.CurConnect);
            aSign.UpdateCommand.BindByName = true;
            aSign.UpdateCommand.Parameters.Add("SIGN_DOC_ID", OracleDbType.Decimal, 0, "SIGN_DOC_ID");
            aSign.UpdateCommand.Parameters.Add("POS_NAME_SIGN", OracleDbType.Varchar2, 0, "POS_NAME_SIGN");
            aSign.UpdateCommand.Parameters.Add("EMP_NAME", OracleDbType.Varchar2, 0, "EMP_NAME");
            aSign.UpdateCommand.Parameters.Add("DEFAULT_NUMBER", OracleDbType.Decimal, 0, "DEFAULT_NUMBER");
            aSign.DeleteCommand = new OracleCommand(string.Format(
                "delete from {0}.SIGN_DOC where SIGN_DOC_ID=:SIGN_DOC_ID",
                Connect.SchemaApstaff), Connect.CurConnect);
            aSign.DeleteCommand.BindByName = true;
            aSign.DeleteCommand.Parameters.Add("SIGN_DOC_ID", OracleDbType.Decimal, 0, "SIGN_DOC_ID");
            ds.Clear();
            aSign.Fill(ds.Tables["SIGNES"]);
            if (ds.Tables["SIGNES"].Columns.Contains("DEFAULT_NUMBER"))
                ds.Tables["SIGNES"].Columns["DEFAULT_NUMBER"].DataType = typeof(decimal);
            dgSignes.ItemsSource = ds.Tables["SIGNES"].DefaultView;
            //dgSignes.DataContext = ds.Tables["EMPS"].DefaultView;
            //dcEmps.ItemsSource = ds.Tables["EMPS"].DefaultView;
        }
        static Signes()
        {
            _addSign = new RoutedUICommand("Добавить подпись", "AddSign", typeof(Signes));
            _deleteSign = new RoutedUICommand("Удалить подпись", "DeleteSign", typeof(Signes));
            _saveSign = new RoutedUICommand("Сохранить подписи", "SaveSign", typeof(Signes));
            _nextStep = new RoutedUICommand("Далее >>>", "NextStep", typeof(Signes));
            ds.Tables.Add("SIGNES");
            ds.Tables.Add("EMPS");
        }

        /// <summary>
        /// Форма подписи
        /// </summary>
        /// <param name="con">соединение</param>
        /// <param name="owner">форма-владелец</param>
        /// <param name="Subdiv_id">подразделение документа</param>
        /// <param name="DocumentName">код документа</param>
        /// <param name="GroupBoxCaption">надпись в форме</param>
        /// <param name="NeedRowCount">сколько требуется подписей</param>
        /// <param name="str_signes">массив для возврата подписей в формате {ДОЛЖНОСТЬ,ФИО}</param>
        /// <returns>Возвращает результат диалога формы Ok  или Cancel</returns>
        public static bool? Show(decimal? Subdiv_id, string DocumentName, string GroupBoxCaption, int NeedRowCount, ref string[][] str_signes)
        {
            Signes r = new Signes(Subdiv_id, DocumentName);
            r.Title = GroupBoxCaption;
            r.RowCountNeed = NeedRowCount;
            r.ShowDialog();
            r.dgSignes.CommitEdit(DataGridEditingUnit.Row, true);
            List<string[]> l = new List<string[]>();
            if (r.DialogResult == true)
            {
                ds.Tables["SIGNES"].DefaultView.Sort = "DEFAULT_NUMBER";                
                for (int i = 0; i < ds.Tables["SIGNES"].DefaultView.Count; ++i)
                    if (Convert.ToInt32(ds.Tables["SIGNES"].DefaultView[i]["FL"]) == 1)
                        l.Add(new string[] { 
                            ds.Tables["SIGNES"].DefaultView[i]["pos_name_sign"].ToString(), 
                            ds.Tables["SIGNES"].DefaultView[i]["EMP_NAME"].ToString() 
                        });
            }
            str_signes = l.ToArray();
            return r.DialogResult;
        }

        /// <summary>
        /// Форма подписи
        /// </summary>
        /// <param name="con">соединение</param>
        /// <param name="owner">форма-владелец</param>
        /// <param name="Subdiv_id">подразделение документа</param>
        /// <param name="DocumentName">код документа</param>
        /// <param name="GroupBoxCaption">надпись в форме</param>
        /// <param name="NeedRowCount">сколько требуется подписей</param>
        /// <param name="str_signes">массив для возврата подписей в формате {ДОЛЖНОСТЬ,ФИО}</param>
        /// <returns>Возвращает результат диалога формы Ok  или Cancel</returns>
        public static bool? Show(decimal? Subdiv_id, string DocumentName, string GroupBoxCaption, int NeedRowCount, ref SignesRecord[] str_signes, Window sender=null)
        {
            Signes r = new Signes(Subdiv_id, DocumentName);
            if (sender!=null) r.Owner = sender;
            r.Title = GroupBoxCaption;
            r.RowCountNeed = NeedRowCount;
            r.ShowDialog();
            r.dgSignes.CommitEdit(DataGridEditingUnit.Row, true);
            List<string[]> l = new List<string[]>();
            if (r.DialogResult == true)
            {
                ds.Tables["SIGNES"].DefaultView.Sort = "DEFAULT_NUMBER";
                var p =ds.Tables["SIGNES"].DefaultView.OfType<DataRowView>().Where(t => t.Row.Field2<Decimal?>("FL") == 1);
                var p1 = p.OrderBy(t => t.Row.Field2<decimal?>("DEFAULT_NUMBER"));
                str_signes = p1.Select(t => new SignesRecord(t["pos_name_sign"].ToString(), t["EMP_NAME"].ToString(), t.Row.Field2<Decimal?>("DEFAULT_NUMBER"))).ToArray();
            }
            else str_signes = null;
            return r.DialogResult;
        }

        /// <summary>
        /// Форма подписи
        /// </summary>
        /// <param name="con">соединение</param>
        /// <param name="owner">форма-владелец</param>
        /// <param name="Subdiv_id">подразделение документа</param>
        /// <param name="DocumentName">код документа</param>
        /// <param name="GroupBoxCaption">надпись в форме</param>
        /// <param name="NeedRowCount">сколько требуется подписей</param>
        /// <param name="str_signes">Таблица для подписей в формате {ДОЛЖНОСТЬ,ФИО}</param>
        /// <returns>Возвращает результат диалога формы </returns>
        public static bool? Show(decimal? Subdiv_id, string DocumentName, string GroupBoxCaption, int NeedRowCount, ref DataTable dt)
        {
            SignesRecord[] str_signes = null;
            if (Signes.Show(Subdiv_id, DocumentName, GroupBoxCaption, NeedRowCount, ref str_signes)==true)
            {
                dt = new DataTable();
                dt.Columns.Add("FIO1");
                dt.Columns.Add("POS_NAME");
                dt.Columns.Add("ORDER_NUMBER");
                foreach (SignesRecord r in str_signes)
                {
                    dt.Rows.Add(r.EmpName, r.PosName, r.OrderNumber);
                }
                return true;
            }
            else
                return false;
        }

        public static RoutedUICommand AddSign
        { 
            get { return _addSign; } 
        }

        public static RoutedUICommand DeleteSign
        {
            get { return _deleteSign; }
        }

        public static RoutedUICommand SaveSign
        {
            get { return _saveSign; }
        }

        public static RoutedUICommand NextStep
        {
            get { return _nextStep; }
        }

        public int RowCountNeed
        {
            get
            {
                return RowCnt;
            }
            set
            {
                RowCnt = value;
                OnPropertyChanged("RowCountNeed");
            }
        }

        private void AddSign_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AddSign_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ds.Tables["SIGNES"].Rows.InsertAt(ds.Tables["SIGNES"].NewRow(), 0);
        }

        private void DeleteSign_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (dgSignes != null && dgSignes.SelectedItem != null)
            {
                e.CanExecute = true;
            }
        }

        private void DeleteSign_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить запись?", "АРМ \"Начисление премии\"",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ((DataRowView)dgSignes.SelectedItem).Delete();
            }
            dgSignes.Focus();
        }

        private void SaveSign_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ds.HasChanges())
            {
                e.CanExecute = true;
            }
        }

        private void SaveSign_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            dgSignes.CommitEdit(DataGridEditingUnit.Row, true);
            OracleTransaction transact = Connect.CurConnect.BeginTransaction();
            try
            {
                for (int i = 0; i < ds.Tables["SIGNES"].Rows.Count; ++i)
                    if (ds.Tables["SIGNES"].Rows[i].RowState == DataRowState.Added)
                    {
                        ds.Tables["SIGNES"].Rows[i]["SIGN_DOC_ID"] = new OracleCommand(string.Format("select {0}.SIGN_DOC_ID_SEQ.NEXTVAL from dual", Connect.SchemaApstaff), Connect.CurConnect).ExecuteScalar();
                        ds.Tables["SIGNES"].Rows[i]["CODE_DOCUM"] = Code_docum;
                        ds.Tables["SIGNES"].Rows[i]["SUBDIV_ID"] = subdiv_id;
                    }
                aSign.UpdateCommand.Transaction = transact;
                aSign.InsertCommand.Transaction = transact;
                aSign.DeleteCommand.Transaction = transact;
                aSign.Update(ds, "SIGNES");
                transact.Commit();
            }
            catch (Exception ex)
            {
                transact.Rollback();
                MessageBox.Show(ex.Message, "Зарплата предприятия - Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            CommandManager.InvalidateRequerySuggested();
        }

        private void NextStep_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NextStep_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            dgSignes.CommitEdit(DataGridEditingUnit.Row, true);
            int k = Convert.ToInt32(ds.Tables["SIGNES"].Compute("COUNT(FL)", "FL=1"));
            if (k < RowCnt)
            {
                MessageBox.Show("Требуется выбрать " + RowCnt + " ответственных", "Ошибка");
                return;
            }
            else
            {
                this.DialogResult = true;
                Close();
            }
        }

        private void btExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
    public class SignesRecord
    {
        public string PosName
        {
            get;
            set;
        }
        public string EmpName
        {
            get;
            set;
        }
        public decimal? OrderNumber
        {
            get;
            set;
        }
        /*public SignesRecord(string pos_name, string emp_name, int? order_num)
        {
            PosName = pos_name;
            EmpName = emp_name;
            OrderNumber = order_num;
        }*/
        public SignesRecord(string pos_name, string emp_name, decimal? order_num)
        {
            PosName = pos_name;
            EmpName = emp_name;
            OrderNumber = order_num;
        }
    }
}
