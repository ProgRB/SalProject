using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextReportRender
{
    public abstract class Element:ITxtRender
    {
        
        public void Render()
        {
            
        }
        public void RenderBorder()
        { 

        }

        public int Width
        {
            get;
            set;
        }
        public int Height
        {
            get;
            set;
        }
        public int Left
        {
            get;
            set;
        }
        public int Top
        {
            get;
            set;
        }
    }
}
