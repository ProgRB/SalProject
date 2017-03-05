using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace LibrarySalary.Helpers
{
    public class ExcelHelper
    {
        public static void PivotToTable(DataTable table, string outFilePath, IEnumerable<string> columnFields, IEnumerable<string> rowFields, string dataField, string pageField=null)
        {
            FileInfo fileInfo = new FileInfo(outFilePath);
            var excel = new ExcelPackage(fileInfo);
            if (excel.Workbook.Worksheets.Any(r => r.Name == "Data"))
                excel.Workbook.Worksheets.Delete(excel.Workbook.Worksheets["Data"]);
            if (excel.Workbook.Worksheets.Any(r => r.Name == "Данные сводной таблицы"))
                excel.Workbook.Worksheets.Delete(excel.Workbook.Worksheets["Данные сводной таблицы"]);
            var wsData = excel.Workbook.Worksheets.Add("Data");
            wsData.Hidden =  eWorkSheetHidden.VeryHidden;
            var wsPivot = excel.Workbook.Worksheets.Add("Данные сводной таблицы");
            wsData.Cells["A1"].LoadFromDataTable(table, true, OfficeOpenXml.Table.TableStyles.Medium6);
            if (table.Rows.Count != 0)
            {
                foreach (DataColumn col in table.Columns)
                {
                    // format all dates in german format (adjust accordingly)
                    if (col.DataType == typeof(System.DateTime))
                    {
                        var colNumber = col.Ordinal + 1;
                        var range = wsData.Cells[2, colNumber, table.Rows.Count + 1, colNumber];
                        range.Style.Numberformat.Format = "dd.MM.yyyy";
                    }
                }
            }

            var dataRange = wsData.Cells[wsData.Dimension.Address.ToString()];
            dataRange.AutoFitColumns();
            var pivotTable = wsPivot.PivotTables.Add(wsPivot.Cells["A3"], dataRange, "Сводная таблица");
            pivotTable.MultipleFieldFilters = true;
            pivotTable.RowGrandTotals = true;
            pivotTable.ColumGrandTotals = true;
            pivotTable.Compact = true;
            pivotTable.CompactData = true;
            pivotTable.GridDropZones = false;
            pivotTable.Outline = false;
            pivotTable.OutlineData = false;
            pivotTable.ShowError = true;
            pivotTable.ErrorCaption = "[ошибка]";
            pivotTable.ShowHeaders = true;
            pivotTable.UseAutoFormatting = true;
            pivotTable.ApplyWidthHeightFormats = true;
            pivotTable.ShowDrill = true;
            pivotTable.FirstDataCol = 3;
            pivotTable.RowHeaderCaption = "Фильтр строк";

            if (pageField != null)
            {
                var modelField = pivotTable.Fields[pageField];
                pivotTable.PageFields.Add(modelField);
                modelField.Sort = OfficeOpenXml.Table.PivotTable.eSortType.Ascending;
            }

            var colField = pivotTable.Fields[dataField];
            var func = pivotTable.DataFields.Add(colField);
            func.Function = OfficeOpenXml.Table.PivotTable.DataFieldFunctions.Sum;
            func.Name = "Сумма значений";

            foreach (string s in rowFields)
            {
                var rowf = pivotTable.Fields[s];
                pivotTable.RowFields.Add(rowf).Sort = OfficeOpenXml.Table.PivotTable.eSortType.Ascending;
            }

            foreach (string s in columnFields)
            {
                var rowf = pivotTable.Fields[s];
                pivotTable.ColumnFields.Add(rowf).Sort = OfficeOpenXml.Table.PivotTable.eSortType.Ascending;
            }
            excel.Save();
        }
    }
}
