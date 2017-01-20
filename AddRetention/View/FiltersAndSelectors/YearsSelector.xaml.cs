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
using Salary.Helpers;
using System.ComponentModel;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for YearsSelector.xaml
    /// </summary>
    public partial class YearsSelector : Window
    {
        public YearsSelector()
        {
            InitializeComponent();
            Model = new PeriodYearsModel();
            this.DataContext = Model;
        }
        public PeriodYearsModel Model
        {
            get;
            set;
        }
        public DateTime Year1
        {
            get
            {
                return new DateTime(Model.Year1, 1, 1);
            }
        }
        public DateTime Year2
        {
            get
            {
                return new DateTime(Model.Year2, 1, 1);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
    public class PeriodYearsModel : NotificationObject, IDataErrorInfo
    {
        public PeriodYearsModel()
        { 
        }
        private int _year1 = DateTime.Today.Year - 2, _year2 = DateTime.Today.Year - 1;
        public int Year1
        {
            get
            {
                return _year1;
            }
            set
            {
                _year1 = value;
                RaisePropertyChanged(() => Year1);
            }
        }
        public int Year2
        {
            get
            {
                return _year2;
            }
            set
            {
                _year2 = value;
                RaisePropertyChanged(() => Year2);
            }
        }


        public string Error
        {
            get 
            {
                return String.Empty;
            }
        }

        public string this[string columnName]
        {
            get 
            {
                if (Year1 == Year2) return "Выбранные года для периода не должны совпадать!";
                return string.Empty;
            }
        }
    }
}
