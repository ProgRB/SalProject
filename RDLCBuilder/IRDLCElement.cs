using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace RDLCBuilder
{
    public abstract class RdlcElement
    {
        public string GetXmlElement()
        {
            return null;
        }

        public string Name
        {
            get;
            set;
        }
        public string Value
        {
            get;
            set;
        }
        public string Border
        {
            get;
            set;
        }
        public double Height
        {
            get;
            set;
        }
        public double Width
        {
            get;
            set;
        }
        public string TextAlign
        {
            get;
            set;
        }

        public double Top
        {
            get;
            set;
        }
        public double Left
        {
            get;
            set;
        }

        abstract public void GetXElement(XElement xmlfile, XNamespace xmlns);

        public RdlcStyle Style
        {
            get;
            set;
        }
    }

    public class RdlcStyle
    {
        decimal _paddingLeft = 2, _paddingRight = 2, _paddingTop = 2, _paddingBottom = 2;

        public decimal PaddingBottom
        {
            get { return _paddingBottom; }
            set { _paddingBottom = value; }
        }

        public decimal PaddingTop
        {
            get { return _paddingTop; }
            set { _paddingTop = value; }
        }
        public decimal Paddingleft
        {
            get
            {
                return _paddingLeft;
            }
            set
            { 
                _paddingLeft = value;
            }
        }
        public decimal PaddingRight
        {
            get
            {
                return _paddingRight;
            }
            set
            {
                _paddingRight = value;
            }
        }

        RdlcBorderStyle _borderStyle = RdlcBorderStyle.Default;
        public RdlcBorderStyle Border
        {
            get
            {
                return _borderStyle;
            }
            set
            {
                _borderStyle = value;
            }            
        }
        public XElement GetXElement(XNamespace xmlns)
        {
            XElement x = new XElement(xmlns+"Style",
                new XElement(xmlns+"Border",
                    new XElement(xmlns+"Style", Border.ToString())),
                new XElement(xmlns+"PaddingLeft", Paddingleft.ToString() + "pt"),
                new XElement(xmlns+"PaddingRight", PaddingRight.ToString() + "pt"),
                new XElement(xmlns+"PaddingTop", PaddingTop.ToString() + "pt"),
                new XElement(xmlns+"PaddingBottom", PaddingBottom.ToString() + "pt")
            );
            return x;
        }
    }

    public enum RdlcBorderStyle
    { 
        Default=0,
        None=1,
        Solid=2,
        Dotted=3
    }
}
