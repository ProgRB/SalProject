using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace Salary.Helpers
{
    public class AppXmlHelper
    {
        /// <summary>
        /// Считывает элементы из файла XML данных лежащем в директории приложения
        /// </summary>
        /// <param name="elementNodeName">Искомый тип - наименование элемента-узла XML файла</param>
        /// <returns></returns>
        public static IEnumerable<XElement> GetElements(string elementNodeName)
        {
            XDocument doc = XDocument.Load(File.OpenRead(Connect.CurrentAppPath + "/XmlData/SalaryXmlData.xml"));
            return doc.Descendants(elementNodeName);
        }
    }
}
