using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WExcel = Microsoft.Office.Interop.Excel;
using System.Threading;
using AddRetention;
namespace AddRetention
{
    public class Excel
    {
        public static string AppExecPath;
        static Excel()
        {
            AppExecPath = Connect.CurrentAppPath;
        }
        private static WExcel.Application m_ExcelApp;

        #region Функции преобразования  декартовых координат в экселевские  и обратно

            /// <summary>
            /// Перевод номера столбца в символьный эквивалент.
            /// </summary>
            public static string ParseColNum(int ColNum)
            {
                StringBuilder sb = new StringBuilder();
                if (ColNum <= 90) return Convert.ToChar(ColNum).ToString();
                sb.Append(Convert.ToChar(64 + (ColNum - 65) / 26));
                sb.Append(Convert.ToChar(65 + (ColNum - 65) % 26));
                return sb.ToString();
            }

            /// <summary>
            /// Выделяет из строки имя столбца и преобразует его в числовой вид
            /// </summary>
            /// <param name="st">Координат ячейки эксель в виде 'A10'</param>
            /// <returns>Номер столбца</returns>
            public static int ExNameToColNum(string st)
            {
                int i = 0, p = 1;
                st = st.Substring(0, st.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }));
                for (int j = st.Length - 1; j > -1; --j)
                {
                    i += p * (st[j] - 64);
                    p *= 26;
                }
                return i;
            }
            public static int ExNameToRowNum(string st)
            {
                return Convert.ToInt32(st.Substring(st.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' })));
            }
            /// <summary>
            /// Преобразует номер столбца в формат эксель(27->'AA')
            /// </summary>
            /// <param name="ColNum">Номер столбца(начиная с 1)</param>
            /// <returns>возвращает строку в формате столбца Excel</returns>
            public static string ColNumToEx(int ColNum)
            {
                string st = "";
                while (ColNum > 0)
                {
                    --ColNum;
                    st = Convert.ToChar(ColNum % 26 + 65) + st;
                    ColNum = ColNum / 26;
                }
                return st;
            }
            /// <summary>
            /// Смещает адрес ячейки в формате Excel на К строк
            /// </summary>
            /// <param name="Cell">Адрес в формате Excel</param>
            /// <param name="k">требуемое смещение</param>
            /// <returns></returns>
            public static string AddRows(string Cell, int k)
            {
                string s = "";
                for (int i = 0; i < Cell.Length && Cell[i] > '9'; i++)
                    s += Cell[i];
                s += (Convert.ToInt32(Cell.Substring(s.Length, Cell.Length - s.Length)) + k).ToString();
                return s;
            }
            public static string AddCols(string Cell, int k)
            {
                return ColNumToEx(ExNameToColNum(Cell) + k) + Cell.Substring(Cell.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }));
            }

        #endregion
        public class PrintParameter
        {
            public string StartCell { get; set; }
            public DataTable[] DataTables { get; set; }
            public string TemplateFileName { get; set; }
            public ExcelParameter[] ExcelParams;
            public TotalRowsStyle[] TotalParams;
            public object Wait;
            public object CancelWait;
        }
        private static void StartPrint(object data)
        {   
            PrintParameter d = (PrintParameter)data;
            /*lock (d.Wait)
            {
                d.Wait = true;
            }*/
            m_ExcelApp = new WExcel.Application();
            try
            {
                //Создание книги Excel
                WExcel._Worksheet m_Sheet;
                object oMissing = System.Reflection.Missing.Value;
                m_ExcelApp.Visible = false;
                m_ExcelApp.Workbooks.Open(AppExecPath + @"\Reports\" + d.TemplateFileName, oMissing, oMissing,
                oMissing, oMissing, oMissing, oMissing, oMissing, oMissing,
                oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
                m_Sheet = (WExcel._Worksheet)m_ExcelApp.ActiveSheet;


                //Проверяем - есть ли столбцы означающие итоги.
                Dictionary<string, Dictionary<int, string>> hs = new Dictionary<string, Dictionary<int, string>>();
                if (d.TotalParams != null)
                {
                    foreach (TotalRowsStyle c in d.TotalParams)
                    {
                        WExcel.Style my_style = m_ExcelApp.ActiveWorkbook.Styles.Add("style_" + c.TotalFlagColumnName + c.TotalFlagValue.GetHashCode().ToString(), Type.Missing);
                        my_style.Interior.Color = ColorTranslator.ToOle(c.BackColor);
                        if (c.ForeColor != Color.Empty) my_style.Font.Color = ColorTranslator.ToOle(c.ForeColor);
                        if (!hs.ContainsKey(c.TotalFlagColumnName.ToUpper()))
                            hs.Add(c.TotalFlagColumnName.ToUpper(), new Dictionary<int, string>());
                        hs[c.TotalFlagColumnName.ToUpper()].Add(c.TotalFlagValue.GetHashCode(), my_style.Name);
                    }

                }

                //Заполняем массив данных
                //Перебираем все таблицы
                int sumCountRow = 0, sum = 0, max = 1, RowInStr;
                for (int i = 0; i < d.DataTables.Count(); i++)
                {
                    sum += d.DataTables[i].Rows.Count;
                    max = Math.Max(d.DataTables[i].Columns.Count, max);
                }
                sum = Math.Max(sum, 1);
                max = Math.Max(1, max - hs.Count);
                //ставим границы в экселе
                WExcel.Range r = m_Sheet.get_Range(d.StartCell,m_Sheet.get_Range(d.StartCell).get_Offset(sum-1,max-1).Address);
                //r = r.get_Offset(sum-1,max-1);// Excel.AddCols(Excel.AddRows(d.StartCell, sum - 1), max - 1));
                r.BorderAround(WExcel.XlLineStyle.xlContinuous, WExcel.XlBorderWeight.xlThin, WExcel.XlColorIndex.xlColorIndexAutomatic, Type.Missing);
                r.Borders.LineStyle = WExcel.XlLineStyle.xlContinuous;
                r.Borders.Weight = WExcel.XlBorderWeight.xlThin;
                object[,] str = new object[sum, max];

                for (int i = 0; i < d.DataTables.Count(); i++)
                {
                    //Перебираем все колонки
                    int j = -1;
                    for (int column = 0; column < d.DataTables[i].Columns.Count; column++)
                    {
                        RowInStr = sumCountRow;
                        if (hs.ContainsKey(d.DataTables[i].Columns[column].ColumnName.ToUpper()))
                        {
                            Dictionary<int, string> t = hs[d.DataTables[i].Columns[column].ColumnName.ToUpper()];
                            for (int k = 0; k < d.DataTables[i].Rows.Count; ++k, RowInStr++)
                                if (t.ContainsKey(d.DataTables[i].Rows[k][column].GetHashCode()))
                                {
                                    WExcel.Range r_row = m_Sheet.get_Range(Excel.AddRows(d.StartCell, RowInStr), Excel.AddCols(Excel.AddRows(d.StartCell, RowInStr), max - 1));
                                    r_row.Style = t[d.DataTables[i].Rows[k][column].GetHashCode()];
                                    r_row.HorizontalAlignment = WExcel.XlHAlign.xlHAlignCenter;
                                    r_row.Borders.LineStyle = WExcel.XlLineStyle.xlContinuous;
                                    r_row.Borders.Weight = WExcel.XlBorderWeight.xlThin;
                                }
                        }
                        else
                        {
                            ++j;
                            if (d.DataTables[i].Columns[column].DataType == typeof(DateTime))
                                for (int row = 0; row < d.DataTables[i].Rows.Count; row++, RowInStr++)
                                    str[RowInStr, j] = (d.DataTables[i].Rows[row][column] == DBNull.Value ? "" : ((DateTime)d.DataTables[i].Rows[row][column]).ToShortDateString());
                            else
                                for (int row = 0; row < d.DataTables[i].Rows.Count; row++, RowInStr++)
                                    str[RowInStr, j] = d.DataTables[i].Rows[row][column].ToString();
                        }
                    }
                    sumCountRow += d.DataTables[i].Rows.Count;
                }
                
                r.set_Value(Type.Missing, str);
                //Заполняем отдельные параметры
                if (d.ExcelParams != null)
                    foreach (ExcelParameter parameter in d.ExcelParams)
                    {
                        WExcel.Range r2 = m_Sheet.get_Range(parameter.NameOfExcel, parameter.NameOfEndMergeExcel);
                        if (parameter.NameOfEndMergeExcel != parameter.NameOfExcel)
                        {
                            r2.Merge(false);
                            r2.HorizontalAlignment = parameter.TextAlign;
                        }
                        if (parameter.Borders != null)
                            foreach (WExcel.XlBordersIndex border in parameter.Borders)
                            {
                                r2.Borders[border].LineStyle = WExcel.XlLineStyle.xlContinuous;
                            }
                        r2.Value2 = parameter.Value;
                    }
                m_ExcelApp.DisplayAlerts = false;
                m_ExcelApp.Visible = true;
                lock (d.Wait) { d.Wait = false; }
                m_Sheet.PrintPreview(true);
                //if (!ShowEditor) m_ExcelApp.Quit();
            }
            catch
            {
                if (!(bool)d.CancelWait)
                {
                    m_ExcelApp.DisplayAlerts = false;
                    m_ExcelApp.Visible = true;
                    m_ExcelApp.Quit();
                    m_ExcelApp = null;
                }
                lock (d.Wait) { d.Wait = false; }
                //throw;
            }
            finally
            {
                //Что бы там ни было вызываем сборщик мусора
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }
        private static void StartWait(object data)
        {
            WaitForm wf = new WaitForm();
            wf.Owner = App.Current.MainWindow;
            wf.Show();
            while (wf.IsActive && (bool)((PrintParameter)data).Wait)
            {
                wf.Dispatcher.BeginInvoke(new Action(delegate { wf.pb.Value = (wf.pb.Value + 1) % wf.pb.Maximum; }), System.Windows.Threading.DispatcherPriority.Background);
                Thread.Sleep(300);
            }
            if ((bool)((PrintParameter)data).Wait )
            {
                lock (((PrintParameter)data).CancelWait)
                {
                    ((PrintParameter)data).CancelWait = true;
                }
            }
            else
                wf.Close();
        }
        public static void PrintWithBorder(string nameOfTemplate, string startExcel, System.Data.DataTable[] tables, ExcelParameter[] excelParameters)
        {
            PrintWithBorder(false, nameOfTemplate, startExcel, tables, excelParameters);
        }

        public static void PrintWithBorder(bool ShowEditor, string nameOfTemplate, string startExcel, System.Data.DataTable[] tables, ExcelParameter[] excelParameters)
        {
            PrintWithBorder(ShowEditor, nameOfTemplate, startExcel, tables, excelParameters, null);
        }
        /// <summary>
        /// Печать с рамкой вокруг данных
        /// </summary>
        /// <param name="ShowEditor">Показывать ли отчет в режиме редактирования</param>
        /// <param name="nameOfTemplate">Шаблон</param>
        /// <param name="startExcel">Стартовый угол данных</param>
        /// <param name="tables">таблицы данных</param>
        /// <param name="excelParameters">Параметры</param>         
        public static void PrintWithBorder(bool ShowEditor, string nameOfTemplate, string startExcel, System.Data.DataTable[] tables, ExcelParameter[] excelParameters, TotalRowsStyle[] totalRowStyles)
        {
            PrintParameter p = new PrintParameter();
            p.Wait = true;
            p.CancelWait = false;
            p.ExcelParams = excelParameters;
            p.TotalParams = totalRowStyles;
            p.StartCell = startExcel;
            p.DataTables = tables;
            p.TemplateFileName = nameOfTemplate;
            Thread t = new Thread(StartPrint);
            t.IsBackground = true;
            Thread t1 = new Thread(StartWait);
            t1.IsBackground = true;
            t.Start(p);
            //t1.Start(p);
            App.Current.MainWindow.IsEnabled= false;
            while (t.IsAlive && !(bool)p.CancelWait)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }
            App.Current.MainWindow.IsEnabled = true;
            if ((bool)p.CancelWait)
            {
                t.Abort();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }


        public static void Print(string nameOfTemplate, string startExcel, System.Data.DataTable[] tables)
        {
            Print(nameOfTemplate, startExcel, tables, null,  true);
        }
        public static void Print(string nameOfTemplate, string startExcel, System.Data.DataTable[] tables, ExcelParameter[] excelParameters)
        {
            Print(nameOfTemplate, startExcel, tables, excelParameters,  true);
        }
        
        /// <summary>
        /// Печать в Excel
        /// </summary>
        /// <param name="nameOfTemplate">Наименование шаблона с расширением</param>
        /// <param name="startExcel">Имя ячейки (пример:В8)</param>
        /// <param name="tables">Массивы данных в виде DataTable</param>
        /// <param name="excelParameters">Дополнительные параметры,
        /// с помощью этих параметров задаются значения отдельных ячеек Excel</param>
        public static void Print(string nameOfTemplate, string startExcel, System.Data.DataTable[] tables, ExcelParameter[] excelParameters,bool flagQuit)
        {
            WExcel.Application m_ExcelApp = new WExcel.Application();
            try
            {
                //Создание страницы книги Excel
                WExcel._Worksheet m_Sheet;
                object oMissing = System.Reflection.Missing.Value;
                m_ExcelApp.Visible = false;
                string PathOfTemplate = Connect.CurrentAppPath + @"\Reports\" + nameOfTemplate;
                m_ExcelApp.Workbooks.Open(PathOfTemplate, oMissing, oMissing,
                oMissing, oMissing, oMissing, oMissing, oMissing, oMissing,
                oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
                m_Sheet = (WExcel._Worksheet)m_ExcelApp.ActiveSheet;
                //Заполняем отдельные параметры
                if (excelParameters != null)
                    foreach (ExcelParameter parameter in excelParameters)
                    {
                        WExcel.Range r = m_Sheet.get_Range(parameter.NameOfExcel, parameter.NameOfEndMergeExcel);
                        r.Value2 = parameter.Value;
                        r.Merge(false);
                        if (parameter.Borders != null)
                        {
                            foreach (WExcel.XlBordersIndex border in parameter.Borders)
                            {
                                m_Sheet.get_Range(parameter.NameOfExcel, Type.Missing).Borders[border].LineStyle = WExcel.XlLineStyle.xlContinuous;
                            }
                        }
                    }
                
                //Заполняем массив данных
                //Перебираем все таблицы
                int sumCountRow = 0, sum = 0, max = 1, RowInStr;
                for (int i = 0; i < tables.Count(); i++)
                {
                    sum += tables[i].Rows.Count;
                    max = Math.Max(tables[i].Columns.Count, max);
                }
                sum = Math.Max(sum, 1);
                if (tables.Count() > 0)//если есть че выполнять - выделять.
                {
                    WExcel.Range r = m_Sheet.get_Range(startExcel, Excel.AddCols(Excel.AddRows(startExcel, sum-1), max-1));
                    string[,] str = new string[sum, max];
                    for (int i = 0; i < tables.Count(); i++)
                    {
                        //Перебираем все колонки
                        for (int column = 0; column < tables[i].Columns.Count; column++)
                        {
                            RowInStr = sumCountRow;
                            if (tables[i].Columns[column].DataType == typeof(DateTime))
                                for (int row = 0; row < tables[i].Rows.Count; row++, RowInStr++)
                                    str[RowInStr, column] = (tables[i].Rows[row][column] == DBNull.Value ? "" : ((DateTime)tables[i].Rows[row][column]).ToShortDateString());
                            else
                                for (int row = 0; row < tables[i].Rows.Count; row++, RowInStr++)
                                    str[RowInStr, column] = tables[i].Rows[row][column].ToString();
                        }
                        sumCountRow += tables[i].Rows.Count;
                    }
                    r.set_Value(Type.Missing, str);
                    //заверщили заполнение.
                }
                m_ExcelApp.DisplayAlerts = false;
                m_ExcelApp.Visible = true;
                if (flagQuit)
                {
                    m_Sheet.PrintPreview(true);
                    m_ExcelApp.Quit();
                }
            }
            catch
            {
                m_ExcelApp.DisplayAlerts = false;
                m_ExcelApp.Visible = true;
                m_ExcelApp.Quit();
                m_ExcelApp = null;
                throw;
            }
            finally
            {
                //Что бы там ни было вызываем сборщик мусора
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }


        /// <summary>
        /// Печать в Excel
        /// </summary>
        /// <param name="nameOfTemplate">Наименование шаблона с расширением</param>
        /// <param name="startExcel">Имя ячейки (пример:В8)</param>
        /// <param name="tables">Массивы данных в виде DataTable</param>
        /// <param name="excelParameters">Дополнительные параметры,
        /// с помощью этих параметров задаются значения отдельных ячеек Excel</param>
        public static void PrintRepOtherType(string nameOfTemplate, string startExcel, System.Data.DataTable[] tables, int[] columnWidth, string[] ListFormat)
        {
            WExcel.Application m_ExcelApp = new WExcel.Application();
            try
            {
                //Создание страницы книги Excel
                WExcel._Worksheet m_Sheet;
                object oMissing = System.Reflection.Missing.Value;
                m_ExcelApp.Visible = false;
                string PathOfTemplate = Application.StartupPath + @"\Reports\" + nameOfTemplate;
                m_ExcelApp.Workbooks.Open(PathOfTemplate, oMissing, oMissing,
                oMissing, oMissing, oMissing, oMissing, oMissing, oMissing,
                oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
                m_Sheet = (WExcel._Worksheet)m_ExcelApp.ActiveSheet;
                //Заполняем массив данных
                //Перебираем все таблицы
                int sumCountRow = 0, sum = 0, max = 1, RowInStr;
                for (int i = 0; i < tables.Count(); i++)
                {
                    sum += tables[i].Rows.Count;
                    max = Math.Max(tables[i].Columns.Count, max);
                }
                sum = Math.Max(sum, 1);

                WExcel.Range r = m_Sheet.get_Range(startExcel, Excel.AddRows(Excel.AddCols(startExcel, max - 1), sum - 1));
                string cur_column = startExcel;
                for (int i = 0; i < columnWidth.Count(); i++)
                {
                    WExcel.Range r1 = m_Sheet.get_Range(cur_column, Excel.AddRows(cur_column, sum - 1));
                    r1.ColumnWidth = columnWidth[i];
                    cur_column = Excel.AddCols(cur_column, 1);
                }
                cur_column = startExcel;
                for (int i = 0; i < ListFormat.Count(); i++)
                {
                    cur_column = Excel.AddCols(cur_column, 1);
                    if (ListFormat[i] != "")
                    {
                        WExcel.Range r1 = m_Sheet.get_Range(cur_column, Excel.AddRows(cur_column, sum - 1));
                        r1.NumberFormat = "0,00";
                    }
                }
                string[,] str = new string[sum, max];
                for (int i = 0; i < tables.Count(); i++)
                {
                    //Перебираем все колонки
                    for (int column = 0; column < tables[i].Columns.Count; column++)
                    {

                        RowInStr = sumCountRow;
                        if (tables[i].Columns[column].DataType == typeof(DateTime))
                            for (int row = 0; row < tables[i].Rows.Count; row++, RowInStr++)
                                str[RowInStr, column] = (tables[i].Rows[row][column] == DBNull.Value ? "" : ((DateTime)tables[i].Rows[row][column]).ToShortDateString());
                        else
                            for (int row = 0; row < tables[i].Rows.Count; row++, RowInStr++)
                                str[RowInStr, column] = tables[i].Rows[row][column].ToString();
                    }

                    sumCountRow += tables[i].Rows.Count;
                }
                r.set_Value(Type.Missing, str);
                //заверщили заполнение.
                m_ExcelApp.DisplayAlerts = false;
                m_ExcelApp.Visible = true;
            }
            catch
            {
                m_ExcelApp.DisplayAlerts = false;
                m_ExcelApp.Visible = true;
                m_ExcelApp.Quit();
                m_ExcelApp = null;
                throw;
            }
            finally
            {
                //Что бы там ни было вызываем сборщик мусора
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

        public static void PrintRepOtherType(string nameOfTemplate, string startExcel, System.Data.DataTable[] tables, int[] columnWidth)
        {
            PrintRepOtherType(nameOfTemplate, startExcel, tables, columnWidth, new string[] { });
        }


        /// <summary>
        /// Печатает шаблон для каждой строки результата запроса
        /// </summary>
        /// <param name="nameOfTemplate">Путь к шаблону эксель</param>
        /// <param name="LeftUpCornerTemplate">Координаты левого верхнего угла шаблона</param>
        /// <param name="RightDownCornerTemplate">Нижнего правого угла</param>
        /// <param name="table">Таблицы с строками</param>
        /// <param name="excelParameters">Эксель параметры с пом. которых указываются координаты ячеек для каждого столбца таблицы</param>
        public static void PrintTemplateForEachRow(string nameOfTemplate, string LeftUpCornerTemplate, string RightDownCornerTemplate, System.Data.DataTable table, ExcelParameter[] excelParameters)
        {
            WExcel.Application m_ExcelApp = new WExcel.Application();
            try
            {

                WExcel._Worksheet m_Sheet;
                object oMissing = System.Reflection.Missing.Value;
                m_ExcelApp.Visible = false;
                string PathOfTemplate = AppExecPath + @"\Reports\" + nameOfTemplate;
                m_ExcelApp.Workbooks.Open(PathOfTemplate, oMissing, oMissing,
                oMissing, oMissing, oMissing, oMissing, oMissing, oMissing,
                oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
                m_Sheet = (WExcel._Worksheet)m_ExcelApp.ActiveSheet;

                WExcel.Range r = m_Sheet.get_Range(LeftUpCornerTemplate, RightDownCornerTemplate);
                WExcel.Range newRange;
                int n = r.Rows.Count, m = r.Columns.Count;
                for (int i = 1; i < table.Rows.Count; i++)
                {
                    newRange = m_Sheet.get_Range(AddRows(LeftUpCornerTemplate, n * i), AddRows(RightDownCornerTemplate, n * i));
                    r.Copy(newRange);
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < excelParameters.Length)
                        {
                            string s1 = excelParameters[j].NameOfExcel,
                                s2 = excelParameters[j].NameOfEndMergeExcel;
                            r = m_Sheet.get_Range(Excel.AddRows(s1, n * i), Excel.AddRows(s2, n * i));
                            r.Merge(false);
                            r.Value2 = excelParameters[j].Value;
                            r.Value2 = table.Rows[i][j].ToString();
                            if (excelParameters[j].Borders != null)
                            {
                                foreach (WExcel.XlBordersIndex border in excelParameters[j].Borders)
                                {
                                    r.Borders[border].LineStyle = WExcel.XlLineStyle.xlContinuous;
                                }
                            }
                        }
                    }
                }
                m_ExcelApp.DisplayAlerts = false;
                m_ExcelApp.Visible = true;
                m_Sheet.PrintPreview(true);
                m_ExcelApp.Quit();
            }
            catch
            {
                m_ExcelApp.DisplayAlerts = false;
                m_ExcelApp.Visible = true;
                m_ExcelApp.Quit();
                m_ExcelApp = null;
                throw;
            }
            finally
            {
                //Что бы там ни было вызываем сборщик мусора
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

        ~Excel()
        {
            if (m_ExcelApp!=null)
            try
            {
                m_ExcelApp.Quit();
                m_ExcelApp=null;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
        
}
