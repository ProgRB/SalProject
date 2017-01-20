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

namespace Salary
{
    /// <summary>
    /// Interaction logic for NumericUpDown.xaml
    /// </summary>
    public partial class NumericUpDown : UserControl
    {
        private static DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(decimal?), typeof(NumericUpDown), new PropertyMetadata(null, ValueProperty_Changed));
        public NumericUpDown()
        {
            InitializeComponent();
        }
        public event EventHandler ValueChanged;
        public decimal? Value
        {
            get
            {
                return (decimal?)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }
        private static void ValueProperty_Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if ((sender as NumericUpDown).ValueChanged != null)
                (sender as NumericUpDown).ValueChanged(sender, EventArgs.Empty);
        }

        private void IncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Value == null)
                this.Value = 1;
            else
                this.Value += 1;
        }

        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Value == null)
                this.Value = -1;
            else this.Value -= 1;
        }
    }
}
