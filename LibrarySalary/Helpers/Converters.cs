using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using Salary.Helpers;
using System.ComponentModel;
using System.Collections;

namespace Salary
{
    public class MultiSumConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!Array.TrueForAll(values, r => r != null && r != DependencyProperty.UnsetValue))
                return GridLength.Auto;
            double d = values.Sum(r => double.IsNaN((double)r) ? 0 : (double)r) + double.Parse((parameter == null ? "0" : parameter.ToString()), System.Globalization.CultureInfo.InvariantCulture);
            return new GridLength(d);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class DivideConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double c = double.Parse(parameter.ToString(), System.Globalization.CultureInfo.InvariantCulture);
            return ((double)value) * c;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MinusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double c = double.Parse(parameter.ToString(), System.Globalization.CultureInfo.InvariantCulture);
            return ((double)value) - c;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    

    public class SourceToNameConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!Array.TrueForAll(values, t => t != null && t != DependencyProperty.UnsetValue))
                return null;
            else
            {
                string propertyName = (string)parameter;
                CollectionViewSource c = values[0] as CollectionViewSource;
                DataView list = c.View.SourceCollection as DataView;
                DataRow rr = list.Table.Rows.Find(values[1]);
                if (rr != null)
                    return rr[propertyName];
                return null;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FindFieldConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!Array.TrueForAll(values, t => t != null && t != DependencyProperty.UnsetValue))
                return null;
            else
            {
                string propertyName = (string)parameter;
                IEnumerable l1 = values[0] as IEnumerable;
                IEnumerable<object> l = l1.OfType<object>();
                if (l.Count() > 0)
                {
                    PropertyDescriptor td = TypeDescriptor.GetProperties(l.ElementAt(0))[propertyName];
                    var t = from p in l
                            where td.GetValue(p).Equals(values[1])
                            select p;
                    return t.FirstOrDefault();
                }
                return null;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }



    public class GetErrorElementConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return ValidationHelper.GetError(value as FrameworkElement);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DecimalToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == DBNull.Value || value == null || System.Convert.ToDecimal(value) == 0)
                return false;
            else return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool? d = (bool?)value;
            if (d.HasValue && d.Value)
                return 1m;
            else return 0m;
        }
    }
    public class InvertBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((bool?)value == false);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((bool?)value == false);
        }
    }
    public class InvertVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((Visibility?)value == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((Visibility?)value == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible);
        }
    }
    public class BoolToGridDetailsVisiblConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((bool?)value == false ? DataGridRowDetailsVisibilityMode.Collapsed : DataGridRowDetailsVisibilityMode.VisibleWhenSelected);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (DataGridRowDetailsVisibilityMode)value == DataGridRowDetailsVisibilityMode.Visible || (DataGridRowDetailsVisibilityMode)value == DataGridRowDetailsVisibilityMode.VisibleWhenSelected;
        }
    }
    public class DecimalToVisiblityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value == null || (decimal)value == 0 ? DataGridRowDetailsVisibilityMode.Collapsed : DataGridRowDetailsVisibilityMode.Visible);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((DataGridRowDetailsVisibilityMode)value == DataGridRowDetailsVisibilityMode.Visible || (DataGridRowDetailsVisibilityMode)value == DataGridRowDetailsVisibilityMode.VisibleWhenSelected ? 1 : 0);
        }
    }

    public class ChildConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
                return (value as DataRowView).CreateChildView(parameter.ToString());
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsNullConverter : IValueConverter 
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool fl = value == null || value == DBNull.Value;
            return fl;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

namespace Salary.View
{
    public class CollectionToSumConverter : IValueConverter
    {
        public string SumField
        {
            get;
            set;
        }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return GetSum(value);
        }

        private decimal? GetSum(object value)
        {
            if (value != null)
            {
                IList<Object> dv = null;
                IList<Object> vg = value as IList<Object>;
                if (vg != null)
                    dv = vg;
                else
                {
                    Xceed.Wpf.DataGrid.DataGridCollectionView v1 = value as Xceed.Wpf.DataGrid.DataGridCollectionView;
                    if (v1 != null)
                        dv = (v1.SourceCollection as DataView).OfType<Object>().ToList();
                    else
                    {
                        CollectionViewSource v = value as CollectionViewSource;
                        if (v != null)
                        {
                            dv = (v.Source as DataView).OfType<Object>().ToList();
                        }
                        else
                        {
                            DataView vv = value as DataView;
                            if (vv != null)
                                dv = vv.OfType<Object>().ToList();
                        }
                    }
                }
                if (dv != null)
                {
                    if (dv.Count>0)
                    {
                        Type tp = dv[0].GetType();
                        if (tp == typeof(DataRowView))
                            return dv.OfType<Object>().Sum(t => (t as DataRowView).Row.Field2<Decimal?>(SumField));
                        else
                            return dv.OfType<Object>().Sum(t => GetSum((t as CollectionViewGroup).Items));
                    }
                    else return 0;
                }
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class EqualConverter : IMultiValueConverter
    {
        public object DefaulValue
        {
            get;
            set;
        }
        public object FalseValue
        {
            get;
            set;
        }
        public object TrueValue
        {
            get;
            set;
        }
        public Type CompareType
        {
            get;
            set;
        }
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Length > 1)
            {
                if (Array.TrueForAll(values, t=>t==DependencyProperty.UnsetValue || t==null))
                    return DefaulValue;
                if (values[0].Equals(values[1]))
                    return TrueValue;
                else
                    return FalseValue;
            }
            else return DefaulValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

namespace Reporting.Converters
{
    public class ExpandStateSaver : IMultiValueConverter
    {
        public static Dictionary<string, bool> states_exp = new Dictionary<string, bool>();
        public ExpandStateSaver()
        { }

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] != null && values[1] != null)
            {
                string st = values[1].ToString();
                if (states_exp.ContainsKey(st))
                    return states_exp[st];
                else return false;
            }
            else return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return new object[] { true, null };
        }
    }
}