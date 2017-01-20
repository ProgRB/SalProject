using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityGenerator
{
    /// <summary>
    /// Атрибут для объявления схемы таблицы.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class| AttributeTargets.Method)]
    public class SchemaNameAttribute: Attribute
    {
        public SchemaNameAttribute(string name)
        { 
            this.name=name;
        }
        public string Name
        {
            get
            {
                return name;
            }
        }
        private string name;
    }
}
