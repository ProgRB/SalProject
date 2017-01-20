using EntityGenerator;
using LibrarySalary.Helpers;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Salary.View.Taxes
{
    /// <summary>
    /// Interaction logic for TaxCompanyEditor.xaml
    /// </summary>
    public partial class TaxCompanyEditor : Window
    {
        TaxCompanyModel _model;
        public TaxCompanyEditor(decimal? tax_company_id)
        {
            _model = new TaxCompanyModel(tax_company_id);
            InitializeComponent();
            DataContext = Model;
        }

        public TaxCompanyModel Model
        {
            get
            {
                return _model;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model != null && Model.HasChanges && string.IsNullOrEmpty(Model.Error);
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Exception ex = Model.Save();
            if (ex != null)
                MessageBox.Show(Window.GetWindow(this), ex.GetFormattedException(), "Ошибка сохранения данных");
            else
            {
                this.DialogResult = true;
                Close();
            }
        }
    }

    /// <summary>
    /// Модель данных для формы редактирование организации налогов
    /// </summary>
    public class TaxCompanyModel : TaxCompany, IDataErrorInfo
    {
        DataSet ds;
        public TaxCompanyModel(decimal? tax_company_id)
            : base(Connect.CurConnect)
        {
            ds = new System.Data.DataSet();
            DataAdapter.SelectCommand = new OracleCommand("select * from salary.tax_company where tax_company_id=:p_tax_company_id", AdapterConnection);
            DataAdapter.SelectCommand.BindByName = true;
            DataAdapter.SelectCommand.Parameters.Add("p_tax_company_id", OracleDbType.Decimal, tax_company_id, ParameterDirection.Input);
            DataAdapter.TableMappings.Add("Table", "TAX_COMPANY");
            DataAdapter.Fill(ds);
            if (ds.Tables["TAX_COMPANY"].Rows.Count == 0)
            {
                DataRow r = ds.Tables["TAX_COMPANY"].NewRow();
                ds.Tables["TAX_COMPANY"].Rows.Add(r);
            }
            this.DataRow = ds.Tables["TAX_COMPANY"].Rows[0];
        }

        /// <summary>
        /// Имеются ли изменения в модели
        /// </summary>
        public bool HasChanges
        {
            get
            {
                return ds.HasChanges();
            }
        }
    }
}
