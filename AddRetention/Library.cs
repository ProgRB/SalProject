using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using Oracle.DataAccess.Client;
using System.Data;
using System.Windows.Controls;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Media;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Oracle.DataAccess.Types;
using Salary.View;
using Salary;
using System.Data.EntityClient;
using System.Configuration;
using Microsoft.Win32;
using System.Collections.Specialized;
using System.Reflection;
using LibrarySalary.Helpers;

namespace Salary
{
    class Library
    {
        static Library()
        {
            
        }
    }

    
    
    /*public static class ColumnWidthSaver
    {
        //static string pathToProgramFiles = Application.Current.;
        //Функция, которая заполняет длину колонок таблицы (DataGridView)
        public static void FillWidthOfColumn(this DataGrid dataGridView)
        {
            //Если файл существует            
            if (File.Exists(pathToProgramFiles + @"\WidthOfColumn.bin"))
            {
                FileStream file = File.Open(pathToProgramFiles + @"\WidthOfColumn.bin", FileMode.Open);
                if (file.Length == 0)
                {
                    file.Close();
                    return;
                }
                BinaryFormatter formater = new BinaryFormatter();
                ArrayList listOfGrids = (ArrayList)formater.Deserialize(file);
                //Перебираем все значения списка
                for (int i = 0; i < listOfGrids.Count; i++)
                {
                    DataGridWidth dataGridWidth = (DataGridWidth)listOfGrids[i];
                    //Если имена совпадают
                    if (dataGridWidth.Name == dataGridView.Name)
                    {
                        //Тогда меняем значение всех столбцов
                        Dictionary<string, int> columns = dataGridWidth.Columns;
                        for (int j = 0; j < dataGridView.Columns.Count; j++)
                        {
                            try
                            {
                                dataGridView.Columns[j].Width = columns[dataGridView.Columns[j].Name];
                            }
                            catch { }
                        }
                    }
                }
                file.Close();
            }
        }
        //Функция которая сохраняет длину колонок таблицы (DataGridView)
        public static void SaveWidthOfColumn(object sender, DataGridColumnEventArgs e)
        {
            //Если файл существует
            DataGrid dataGridView = (DataGrid)sender;
            if (File.Exists(pathToProgramFiles + @"\WidthOfColumn.bin"))
            {
                FileStream fileStream = File.Open(pathToProgramFiles + @"\WidthOfColumn.bin", FileMode.Open);
                BinaryFormatter formater = new BinaryFormatter();
                if (fileStream.Length == 0)
                {
                    fileStream.Close();
                    return;
                }
                ArrayList listOfGrids = (ArrayList)formater.Deserialize(fileStream);
                fileStream.Close();
                //Встречается ли данная таблица в списке
                bool haveDataGridView = false;
                //Перебираем все значения списка
               /* for (int i = 0; i < listOfGrids.Count; i++)
                {
                    DataGridWidth dataGridWidth = (DataGridWidth)listOfGrids[i];
                    //Если имена совпадают
                    if (dataGridWidth.Name == dataGridView.Name)
                    {
                        haveDataGridView = true;
                        //Тогда меняем значение всех столбцов
                        Dictionary<string, double> columns = dataGridWidth.Columns;
                        for (int j = 0; j < dataGridView.Columns.Count; j++)
                        {
                            columns[dataGridView.Columns[j].Header.ToString()] = dataGridView.Columns[j].Width.Value;
                        }
                    }
                }
                //Если такой таблицы не было, то добавляем ее в массив
                if (!haveDataGridView)
                {
                    DataGridWidth dataGridWidth = FillDataGridWidth(dataGridView);
                    listOfGrids.Add(dataGridWidth);
                }
                //Осуществляем сериализацию
                fileStream = File.Open(pathToProgramFiles + @"\WidthOfColumn.bin", FileMode.Create);
                formater.Serialize(fileStream, listOfGrids);
                fileStream.Close();
            }
            else //Если файла нет
            {
                /*BinaryFormatter formater = new BinaryFormatter();
                ArrayList listOfGrids = new ArrayList();
                //Заполняем список
                listOfGrids.Add(FillDataGridWidth(dataGridView));
                FileStream fileStream = File.Open(pathToProgramFiles + @"\WidthOfColumn.bin", FileMode.Create);
                formater.Serialize(fileStream, listOfGrids);
                fileStream.Close();
            }
        }
        /*private static DataGridWidth FillDataGridWidth(this DataGrid dataGrid)
        {
            DataGridWidth dataGridWidth = new DataGridWidth(dataGrid.Name);
            for (int i = 0; i < dataGrid.Columns.Count; i++)
                dataGridWidth.Columns.Add(dataGrid.Columns[i].Name, dataGrid.Columns[i].Width);
            return dataGridWidth;
        }
    }
    [Serializable]
    public class DataGridWidth
    {
        private Dictionary<string, int> m_Columns = new Dictionary<string, int>();
        private string m_Names;
        public DataGridWidth(string name)
        {
            m_Names = name;
        }
        public string Name { get { return m_Names; } }
        public Dictionary<string, int> Columns
        {
            get
            {
                return m_Columns;
            }
            set
            {
                m_Columns = value;
            }
        }
    }
    public static class ColumnCaptionLoader
    {
        private static Dictionary<int, string> MapOfCaptions;
        public static void ReadColumnCaptions()
        {
            StreamReader r = new StreamReader(new FileStream(Connect.CurrentAppPath + "\\ColCaption.ini", FileMode.Open, FileAccess.Read), Encoding.GetEncoding(1251));
            MapOfCaptions = new Dictionary<int, string>();
            while (!r.EndOfStream)
            {
                string s = r.ReadLine();
                if (s.Length > 2 && s.Substring(0, 2) != "//")
                {
                    int k = s.IndexOf(' ');
                    string st = s.Substring(0, s.IndexOf(' '));
                    MapOfCaptions.Add(st.ToUpper().GetHashCode(), s.Substring(st.Length));
                }
            }
            r.Close();
        }
        public static void LoadColumnCaption(this DataGrid dg)
        {
            /*if (MapOfCaptions == null) ReadColumnCaptions();
            for (int i = 0; i < dg.Columns.Count; ++i)
            {
                int k = (dg.Name + '.' + dg.Columns[i].Name).ToUpper().GetHashCode();
                int k1 = dg.Columns[i].Name.ToUpper().GetHashCode();
                if (MapOfCaptions.ContainsKey(k))
                    dg.Columns[i].HeaderText = MapOfCaptions[k];
                else
                    if (MapOfCaptions.ContainsKey(k1))
                        dg.Columns[i].HeaderText = MapOfCaptions[k1];
            }
        }
    }*/

    

    public enum DocumentType
    {
        PDF=1,
        TIFF=2,
        Jpeg=3,
        Bmp=4,
        Png=5,
        XPS=6
    }

    


    public class DatePickerCalendar
    {
        public static readonly DependencyProperty IsMonthYearProperty =
            DependencyProperty.RegisterAttached("IsMonthYear", typeof(bool), typeof(DatePickerCalendar),
                                                new PropertyMetadata(OnIsMonthYearChanged));

        public static bool GetIsMonthYear(DependencyObject dobj)
        {
            return (bool)dobj.GetValue(IsMonthYearProperty);
        }

        public static void SetIsMonthYear(DependencyObject dobj, bool value)
        {
            dobj.SetValue(IsMonthYearProperty, value);
        }

        private static void OnIsMonthYearChanged(DependencyObject dobj, DependencyPropertyChangedEventArgs e)
        {
            var datePicker = (DatePicker)dobj;

            Application.Current.Dispatcher
                .BeginInvoke(DispatcherPriority.Loaded,
                             new Action<DatePicker, DependencyPropertyChangedEventArgs>(SetCalendarEventHandlers),
                             datePicker, e);
        }

        private static void SetCalendarEventHandlers(DatePicker datePicker, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue)
                return;

            if ((bool)e.NewValue)
            {
                datePicker.CalendarOpened += DatePickerOnCalendarOpened;
                datePicker.CalendarClosed += DatePickerOnCalendarClosed;
            }
            else
            {
                datePicker.CalendarOpened -= DatePickerOnCalendarOpened;
                datePicker.CalendarClosed -= DatePickerOnCalendarClosed;
            }
        }

        private static void DatePickerOnCalendarOpened(object sender, RoutedEventArgs routedEventArgs)
        {
            var calendar = GetDatePickerCalendar(sender);
            calendar.DisplayMode = CalendarMode.Year;

            calendar.DisplayModeChanged += CalendarOnDisplayModeChanged;
        }

        private static void DatePickerOnCalendarClosed(object sender, RoutedEventArgs routedEventArgs)
        {
            var datePicker = (DatePicker)sender;
            var calendar = GetDatePickerCalendar(sender);
            datePicker.SelectedDate = calendar.SelectedDate;

            calendar.DisplayModeChanged -= CalendarOnDisplayModeChanged;
        }

        private static void CalendarOnDisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            var calendar = (Calendar)sender;
            if (calendar.DisplayMode != CalendarMode.Month)
                return;

            calendar.SelectedDate = GetSelectedCalendarDate(calendar.DisplayDate);

            var datePicker = GetCalendarsDatePicker(calendar);
            datePicker.IsDropDownOpen = false;
        }

        private static Calendar GetDatePickerCalendar(object sender)
        {
            var datePicker = (DatePicker)sender;
            var popup = (Popup)datePicker.Template.FindName("PART_Popup", datePicker);
            return ((Calendar)popup.Child);
        }

        private static DatePicker GetCalendarsDatePicker(FrameworkElement child)
        {
            var parent = (FrameworkElement)child.Parent;
            if (parent.Name == "PART_Root")
                return (DatePicker)parent.TemplatedParent;
            return GetCalendarsDatePicker(parent);
        }

        private static DateTime? GetSelectedCalendarDate(DateTime? selectedDate)
        {
            if (!selectedDate.HasValue)
                return null;
            return new DateTime(selectedDate.Value.Year, selectedDate.Value.Month, 1);
        }
    }

    public class DatePickerDateFormat
    {
        public static readonly DependencyProperty DateFormatProperty =
            DependencyProperty.RegisterAttached("DateFormat", typeof(string), typeof(DatePickerDateFormat),
                                                new PropertyMetadata(OnDateFormatChanged));

        public static string GetDateFormat(DependencyObject dobj)
        {
            return (string)dobj.GetValue(DateFormatProperty);
        }

        public static void SetDateFormat(DependencyObject dobj, string value)
        {
            dobj.SetValue(DateFormatProperty, value);
        }

        private static void OnDateFormatChanged(DependencyObject dobj, DependencyPropertyChangedEventArgs e)
        {
            var datePicker = (DatePicker)dobj;

            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Loaded, new Action<DatePicker>(ApplyDateFormat), datePicker);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="datePicker"></param>
        private static void ApplyDateFormat(DatePicker datePicker)
        {
            var binding = new Binding("SelectedDate")
            {
                RelativeSource = new RelativeSource { AncestorType = typeof(DatePicker) },
                Converter = new DatePickerDateTimeConverter(),
                ConverterParameter = new Tuple<DatePicker, string>(datePicker, GetDateFormat(datePicker))
            };
            
            var textBox = GetTemplateTextBox(datePicker);
            textBox.SetBinding(TextBox.TextProperty, binding);

            textBox.PreviewKeyDown -= TextBoxOnPreviewKeyDown;
            textBox.PreviewKeyDown += TextBoxOnPreviewKeyDown;

            datePicker.CalendarOpened -= DatePickerOnCalendarOpened;
            datePicker.CalendarOpened += DatePickerOnCalendarOpened;
        }

        private static TextBox GetTemplateTextBox(Control control)
        {
            control.ApplyTemplate();
            return (TextBox)control.Template.FindName("PART_TextBox", control);
        }

        private static void TextBoxOnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return)
                return;

            /* DatePicker subscribes to its TextBox's KeyDown event to set its SelectedDate if Key.Return was
             * pressed. When this happens its text will be the result of its internal date parsing until it
             * loses focus or another date is selected. A workaround is to stop the KeyDown event bubbling up
             * and handling setting the DatePicker.SelectedDate. */

            e.Handled = true;

            var textBox = (TextBox)sender;
            var datePicker = (DatePicker)textBox.TemplatedParent;
            var dateStr = textBox.Text;
            var formatStr = GetDateFormat(datePicker);
            DateTime? d = DatePickerDateTimeConverter.StringToDateTime(datePicker, formatStr, dateStr);
            if (d != datePicker.SelectedDate)
                datePicker.SelectedDate = d;
        }

        private static void DatePickerOnCalendarOpened(object sender, RoutedEventArgs e)
        {
            /* When DatePicker's TextBox is not focused and its Calendar is opened by clicking its calendar button
             * its text will be the result of its internal date parsing until its TextBox is focused and another
             * date is selected. A workaround is to set this string when it is opened. */

            var datePicker = (DatePicker)sender;
            var textBox = GetTemplateTextBox(datePicker);
            var formatStr = GetDateFormat(datePicker);
            string txt = DatePickerDateTimeConverter.DateTimeToString(formatStr, datePicker.SelectedDate);
            if (textBox.Text != txt)
                textBox.Text = txt;
        }

        private class DatePickerDateTimeConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                var formatStr = ((Tuple<DatePicker, string>)parameter).Item2;
                var selectedDate = (DateTime?)value;
                return DateTimeToString(formatStr, selectedDate);
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                var tupleParam = ((Tuple<DatePicker, string>)parameter);
                var dateStr = (string)value;
                return StringToDateTime(tupleParam.Item1, tupleParam.Item2, dateStr);
            }

            public static string DateTimeToString(string formatStr, DateTime? selectedDate)
            {
                return selectedDate.HasValue ? selectedDate.Value.ToString(formatStr) : null;
            }

            public static DateTime? StringToDateTime(DatePicker datePicker, string formatStr, string dateStr)
            {
                DateTime date;
                var canParse = DateTime.TryParseExact(dateStr, formatStr, System.Globalization.CultureInfo.CurrentCulture,
                                                      System.Globalization.DateTimeStyles.None, out date);

                if (!canParse)
                    canParse = DateTime.TryParse(dateStr, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out date);

                return canParse ? date : datePicker.SelectedDate;
            }
        }
    }

    public class DataGridStateSaver
    {
        public static readonly DependencyProperty IsSaveColumnWidthProperty =
            DependencyProperty.RegisterAttached("IsSaveColumnWidth", typeof(bool), typeof(DataGridStateSaver),
                                                new PropertyMetadata(OnIsSaveColumnWidth_Changed));
        public static void OnIsSaveColumnWidth_Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is DataGrid)
            {
                DataGrid dg = sender as DataGrid;
                dg.Columns.CollectionChanged+=new NotifyCollectionChangedEventHandler(Columns_CollectionChanged);
            }
        }

        static void  Columns_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (object cl in e.NewItems)
                {
                    DataGridColumn column = cl as DataGridColumn;
                }
            }
        }
        //private static void SetColumnSaveState
    }
    
   

    public class AppNotify
    {
        public AppNotify(string typeMessage, string message)
        {
            TypeMessage = typeMessage;
            Message = message;
            EventDate = DateTime.Now;
        }
        public string TypeMessage
        { get; set; }
        public string Message
        {
            get;
            set;
        }
        public DateTime EventDate
        { get; set; }
    }
    
    public static class DataGridHelper
    {
        
        /// <summary>
        /// Finds a parent of a given item on the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="child">A direct or indirect child of the
        /// queried item.</param>
        /// <returns>The first parent item that matches the submitted
        /// type parameter. If not matching item can be found, a null
        /// reference is being returned.</returns>
        public static T TryFindParent<T>(this DependencyObject child)
            where T : DependencyObject
        {
            //get parent item
            DependencyObject parentObject = GetParentObject(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                //use recursion to proceed with next level
                return TryFindParent<T>(parentObject);
            }
        }

        /// <summary>
        /// This method is an alternative to WPF's
        /// <see cref="VisualTreeHelper.GetParent"/> method, which also
        /// supports content elements. Keep in mind that for content element,
        /// this method falls back to the logical tree of the element!
        /// </summary>
        /// <param name="child">The item to be processed.</param>
        /// <returns>The submitted item's parent, if available. Otherwise
        /// null.</returns>
        public static DependencyObject GetParentObject(this DependencyObject child)
        {
            if (child == null) return null;

            //handle content elements separately
            ContentElement contentElement = child as ContentElement;
            if (contentElement != null)
            {
                DependencyObject parent = ContentOperations.GetParent(contentElement);
                if (parent != null) return parent;

                FrameworkContentElement fce = contentElement as FrameworkContentElement;
                return fce != null ? fce.Parent : null;
            }

            //also try searching for parent in framework elements (such as DockPanel, etc)
            FrameworkElement frameworkElement = child as FrameworkElement;
            if (frameworkElement != null)
            {
                DependencyObject parent = frameworkElement.Parent;
                if (parent != null) return parent;
            }

            //if it's not a ContentElement/FrameworkElement, rely on VisualTreeHelper
            return VisualTreeHelper.GetParent(child);
        }


        /// <summary>
        /// Tries to locate a given item within the visual tree,
        /// starting with the dependency object at a given position. 
        /// </summary>
        /// <typeparam name="T">The type of the element to be found
        /// on the visual tree of the element at the given location.</typeparam>
        /// <param name="reference">The main element which is used to perform
        /// hit testing.</param>
        /// <param name="point">The position to be evaluated on the origin.</param>
        public static T TryFindFromPoint<T>(UIElement reference, System.Windows.Point point)
          where T : DependencyObject
        {
            DependencyObject element = reference.InputHitTest(point)
                                         as DependencyObject;
            if (element == null) return null;
            else if (element is T) return (T)element;
            else return TryFindParent<T>(element);
        }


        
    }

    public class DataGridAddition
    {
        public static readonly DependencyProperty DoubleClickCommandProperty =
                DependencyProperty.RegisterAttached("DoubleClickCommand", typeof(RoutedUICommand), typeof(DataGridAddition),
                    new PropertyMetadata(OnDoubleClick_PropertyChanged)),
                DoubleClickParameterProperty = 
                DependencyProperty.RegisterAttached("DoubleClickParameter", typeof(object), typeof(DataGridAddition));
        public static RoutedUICommand GetDoubleClickCommand(DependencyObject e)
        {
            return (RoutedUICommand)e.GetValue(DoubleClickCommandProperty);
        }
        public static void SetDoubleClickCommand(DependencyObject obj, RoutedUICommand e)
        {
            obj.SetValue(DoubleClickCommandProperty, e);
        }
        public static object GetDoubleClickParameter(DependencyObject e)
        {
            return e.GetValue(DoubleClickParameterProperty);
        }
        public static void SetDoubleClickParameter(DependencyObject obj, RoutedUICommand e)
        {
            obj.SetValue(DoubleClickParameterProperty, e);
        }
        public static void OnDoubleClick_PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    new Action<Control, DependencyPropertyChangedEventArgs>(SetDataGridDoubleClick), sender as Control, e);
        }
        private static void SetDataGridDoubleClick(Control sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
                if (e.NewValue!=DataGridAddition.DoubleClickCommandProperty.DefaultMetadata.DefaultValue)
                    sender.MouseDoubleClick += new MouseButtonEventHandler(sender_MouseDoubleClick);
                else
                    sender.MouseDoubleClick -= new MouseButtonEventHandler(sender_MouseDoubleClick);
        }

        static void sender_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IInputElement elem = e.MouseDevice.DirectlyOver;
            if (e.ChangedButton == MouseButton.Left && elem != null && elem is FrameworkElement)
            {
                FrameworkElement f = elem as FrameworkElement;
                if (f.TryFindParent<DataGridRow>() != null || f.TryFindParent<Xceed.Wpf.DataGrid.Cell>() != null)
                {
                    Control dg = sender as Control;
                    RoutedUICommand r = dg.GetValue(DataGridAddition.DoubleClickCommandProperty) as RoutedUICommand;
                    object parameter = dg.GetValue(DataGridAddition.DoubleClickParameterProperty);
                    if (r != null)
                        r.Execute(parameter, elem);
                }
            }
        }
    }

    public class TextLoadHelper
    {
        private static OracleCommand maincomman, addcommand;
        static string filename;
        static Encoding enc;
        public static void LoadData(string FileName, OracleCommand cmdForLoad, Encoding fileEncoding, OracleCommand additionCommand = null)
        {
            BackgroundWorker bw = new BackgroundWorker();
            maincomman = cmdForLoad;
            addcommand = additionCommand;
            enc = fileEncoding;
            filename = FileName;
            bw.DoWork += BeginLoad;
            bw.RunWorkerCompleted += (s, e)=>
                {
                    if (e.Error != null)
                        if (e.Error is IOException)
                            MessageBox.Show("Ошибка чтения файла. Проверьте права доступа.\n" + e.Error.Message);
                        else
                            if (e.Error is OracleException)
                                MessageBox.Show(string.Format("Ошибка обработки записи процедурой. Проверьте правильность формата данных.\n" + e.Error.Message));
                    OnLoadFinished(e);
                };
            bw.RunWorkerAsync();
        }
        private static void BeginLoad(object sender, DoWorkEventArgs e)
        {
            int i = 1;

            OracleTransaction tr = maincomman.Connection.BeginTransaction();
            try
            {
                StreamReader r = new StreamReader(new FileStream(filename, FileMode.Open, FileAccess.Read), enc);
                string st;
                if (addcommand != null)
                    addcommand.ExecuteNonQuery();
                while (!r.EndOfStream)
                {
                    st = r.ReadLine();
                    maincomman.Parameters[0].Value = st;
                    maincomman.ExecuteNonQuery();
                    e.Result = i++;
                }
                tr.Commit();
            }
            catch (Exception ex)
            {
                tr.Rollback();
                throw ex;
            }
        }
        public static RunWorkerCompletedEventHandler LoadFinished;
        public static void OnLoadFinished(RunWorkerCompletedEventArgs e)
        {
            if (LoadFinished!=null)
                LoadFinished(null, e);
        }
    }

}


