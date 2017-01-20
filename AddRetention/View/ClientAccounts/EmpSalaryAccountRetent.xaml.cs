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
    /// Interaction logic for EmpSalaryAccountRetent.xaml
    /// </summary>
    public partial class EmpSalaryAccountRetent : Window
    {
        DataSet ds;
        OracleDataAdapter odaSal;
        public EmpSalaryAccountRetent(object p_transfer_id)
        {
            odaSal = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectSalaryTransferringSum.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaSal.SelectCommand.BindByName = true;
            odaSal.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, p_transfer_id, ParameterDirection.Input);
            odaSal.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSal.TableMappings.Add("Table", "Salary");
            ds = new DataSet();
            InitializeComponent();
            this.DataContext = this;
        }
        public DataView SalarySource
        {
            get
            {
                LoadData();
                if (ds != null && ds.Tables.Contains("Salary"))
                    return new DataView(ds.Tables["SALARY"], "", "PAY_DATE DESC, CODE_PAYMENT", DataViewRowState.CurrentRows);
                else
                    return null;
            }
        }
        private void LoadData()
        {
            try
            {
                odaSal.Fill(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка получения данных");
            }
        }

    }
}
