using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Data;

namespace RDLCBuilder
{
    public class RdlcParagraph : RdlcElement
    {
        public RdlcParagraph(string value)
        {
            this.Value = value;
        }
        public override void GetXElement(XElement node, XNamespace xmlns)
        {
            node.Add(
                new XElement(xmlns+"Paragraph",
                    new XElement(xmlns+"TextRuns",
                        new XElement(xmlns+"TextRun",
                            new XElement(xmlns+"Value", this.Value)
                            )
                            )
                            )
                            );
        }
    }

    public class RdlcText: RdlcElement
    {
        public RdlcText(string value)
        {
            Paragraphs = new List<RdlcParagraph>();
            Paragraphs.Add(new RdlcParagraph(value));
            Style = new RdlcStyle();
        }
        public override void GetXElement(XElement node, XNamespace xmlns)
        {
            
            XElement x = new XElement(xmlns+"TextBox");
            XElement parag = new XElement(xmlns+"Paragraphs");
            if (this.Paragraphs != null)
                foreach (RdlcParagraph r in Paragraphs)
                    parag.Add(new XElement(xmlns+"Paragraph", new XElement(xmlns+"TextRuns", new XElement(xmlns+"TextRun", new XElement(xmlns+"Value", r.Value)))));
            x.Add(parag);
            if (Style != null)
            {
                x.Add(this.Style.GetXElement(xmlns));
            }
            node.Add(x);
        }
        public List<RdlcParagraph> Paragraphs
        {
            get;
            set;
        }
    }

    public class RdlcDataSources : RdlcElement
    {
        public override void GetXElement(XElement xmlfile, XNamespace xmlns)
        {
            XNamespace rd = "rd";
            xmlfile.Add( 
                new XElement(xmlns+"DataSources",
                    new XElement(xmlns+"DataSource", new XAttribute("Name", "ReportDataSet"),
                            new XElement(xmlns+"ConnectionProperties",
                                new XElement(xmlns+"DataProvider", "System.Data.DataSet"),
                                new XElement(xmlns+"ConnectString", "")
                                ),
                                new XElement(rd+"DataSourceID", "8082b6f3-8e2b-40a7-b013-19e2bbd830e4")
                                ))
                        );
        }
    }

    public class RdlcDataSets : RdlcElement
    {
        public RdlcDataSets(DataSet ds)
        {
            DataSet = ds;
        }
        public DataSet DataSet
        {
            get;
            set;
        }
        public override void GetXElement(XElement xmlfile, XNamespace xmlns)
        {
            XNamespace rd = "rd";
            int i = 1;
            XElement dsets = new XElement(xmlns+"DataSets");
            xmlfile.Add(dsets);
            foreach (DataTable t in DataSet.Tables)
            {
                XElement dataset = new XElement(xmlns + "DataSet", new XAttribute("Name", "DataSet" + i.ToString()));
                dsets.Add(dataset);
                XElement fields = new XElement(xmlns+"Fields");
                dataset.Add(fields);
                foreach (DataColumn c in t.Columns)
                {
                    fields.Add(new XElement(xmlns+"Field", new XAttribute("Name", c.ColumnName),
                        new XElement(xmlns+"DataField", c.ColumnName),
                        new XElement(rd+"TypeName", c.DataType.FullName)
                        ));
                }
                
                dataset.Add(new XElement(xmlns+"Query",
                    new XElement(xmlns+"DataSourceName", "ReportDataSet"),
                    new XElement(xmlns+"CommandText")));
                dataset.Add(new XElement(rd+"DataSetInfo",
                        new XElement(rd+"DataSetName", "ReportDataSet"),
                        new XElement(rd+"SchemaPath", @"C:\JOB\ASalary\AddRetention\Reports\ReportDataSet.xsd"),
                        new XElement(rd+"TableName", "Table"+i.ToString()),
                        new XElement(rd+"TableAdapterFillMethod"),
                        new XElement(rd+"TableAdapterGetDataMethod"),
                        new XElement(rd+"TableAdapterName")));
                ++i;
            }
        }
    }
}
