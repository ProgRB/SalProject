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
    /// Interaction logic for SubdivListSelector.xaml
    /// </summary>
    public partial class SubdivListSelector : Window
    {
        static RoutedUICommand _selectSubdivs = new RoutedUICommand("Выбрать", "selectSubdiv", typeof(SubdivListSelector), new InputGestureCollection(new InputGesture[]{new KeyGesture(Key.Enter, ModifierKeys.Control)}));
        DataTable t;
        public SubdivListSelector()
        {
            t = AppDataSet.Tables["SUBDIV"].Copy();
            t.Columns.Add("CHECK", typeof(decimal));
            InitializeComponent();

            
            listSub.ItemsSource = new DataView(t, "", "CODE_SUBDIV", DataViewRowState.CurrentRows);
        }
        public static RoutedUICommand SelectSubdivCommand
        {
            get
            {
                return _selectSubdivs;
            }
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void SelectSubdiv_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = t!=null && SelectedSubdivIDs.Length > 0;
        }

        private void SelectSubdiv_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult=true;
            this.Close();
        }

        /// <summary>
        /// выбранные строки подраделений
        /// </summary>
        public List<DataRow> SelectedSubdiv
        {
            get
            {
                return t.Rows.Cast<DataRow>().Where(r => r["CHECK"] != DBNull.Value && r["CHECK"].ToString()=="1").ToList();
            }
        }

        /// <summary>
        /// Выбранные подразделения 
        /// </summary>
        public decimal[] SelectedSubdivIDs
        {
            get
            {
                return t.Rows.OfType<DataRow>().Where(r => r.Field2<Decimal?>("CHECK") == 1).Select(r => r.Field2<Decimal>("SUBDIV_ID")).ToArray();
            }
        }
    }
}
