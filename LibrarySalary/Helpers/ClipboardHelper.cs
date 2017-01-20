using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace LibrarySalary.Helpers
{
    public static class ClipboardHelper
    {
        public delegate string[] ParseFormat(string value);

        /// <summary>
        /// Парсит данные из буфера обмена как формат CSV
        /// </summary>
        /// <returns></returns>
        public static List<string[]> ParseClipboardData()
        {
            List<string[]> clipboardData = new List<string[]>();

            // get the data and set the parsing method based on the format
            // currently works with CSV and Text DataFormats            
            IDataObject dataObj = System.Windows.Clipboard.GetDataObject();

            if (dataObj != null)
            {
                string[] formats = dataObj.GetFormats();
                if (formats.Contains(DataFormats.CommaSeparatedValue))
                {


                    string clipboardString = (string)dataObj.GetData(DataFormats.CommaSeparatedValue);
                    {
                        // EO: Subject to error when a CRLF is included as part of the data but it work for the moment and I will let it like it is
                        // WARNING ! Subject to errors
                        string[] lines = clipboardString.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                        string[] lineValues;
                        foreach (string line in lines)
                        {
                            lineValues = CsvHelper.ParseLineCommaSeparated(line);
                            if (lineValues != null)
                            {
                                clipboardData.Add(lineValues);
                            }
                        }
                    }
                }
                else if (formats.Contains(DataFormats.Text))
                {
                    string clipboardString = (string)dataObj.GetData(DataFormats.Text);
                    clipboardData = CsvHelper.ParseText(clipboardString);
                }
            }

            return clipboardData;
        }

        /// <summary>
        /// Парсим скопированные данные из экселя как XML формат
        /// </summary>
        /// <returns></returns>
        public static List<object[]> ParseClipboardXMLSpreadsheet()
        {
            List<object[]> clipboardData = new List<object[]>();

            // get the data and set the parsing method based on the format
            // currently works with CSV and Text DataFormats      
            string format = "XML Spreadsheet";
            IDataObject dataObj = System.Windows.Clipboard.GetDataObject();
            if (dataObj != null)
            {
                string[] formats = dataObj.GetFormats();
                if (formats.Contains(format))
                {
                    MemoryStream ms = (MemoryStream)dataObj.GetData(format);
                    XmlDocument doc = new XmlDocument();
                    doc.Load(ms);
                    XmlNodeList tables = doc.GetElementsByTagName("Table");
                    if (tables.Count != 0)
                    {
                        XmlNode table = tables[0];

                        var rows = table.ChildNodes.OfType<XmlNode>()
                            .Where(r => r.Name == "Row");

                        string str = string.Empty;
                        try
                        {
                            foreach (XmlNode row in rows)
                            {
                                //бежим по каждой ячейке в строке и епашим данные
                                var cells = (row.ChildNodes.OfType<XmlNode>().Where(r => r.Name == "Cell")).ToList();
                                object[] valuesRow = new object[cells.Count];
                                for (int i = 0; i < cells.Count; i++)
                                {
                                    XmlNode cellData = cells[i].OfType<XmlNode>().Where(r => r.Name == "Data").FirstOrDefault();
                                    if (cellData != null) // если есть данные в ячейки то записываем их в ячейку массива
                                    {
                                        string dataType = cellData.Attributes.OfType<XmlAttribute>().Where(r => r.Name.Contains(":Type")).Select(r => r.Value).FirstOrDefault();
                                        if (dataType == "Number")
                                        {
                                            valuesRow[i] = decimal.Parse(cellData.InnerText, CultureInfo.InvariantCulture);
                                        }
                                        else
                                            valuesRow[i] = cellData.InnerText;
                                    }
                                }
                                str = string.Join("%", valuesRow);
                                clipboardData.Add(valuesRow);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message + " " + str, ex);
                        }
                    }

                }
            }
            return clipboardData;
        }

    }
}
