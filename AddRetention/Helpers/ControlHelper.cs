using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Salary.Helpers
{
    public static class SomeClass
    {
        public static readonly DependencyProperty TagProperty = DependencyProperty.RegisterAttached(
            "Tag",
            typeof(object),
            typeof(SomeClass),
            new FrameworkPropertyMetadata(null));

        public static object GetTag(DependencyObject dependencyObject)
        {
            return dependencyObject.GetValue(TagProperty);
        }

        public static void SetTag(DependencyObject dependencyObject, object value)
        {
            dependencyObject.SetValue(TagProperty, value);
        }
    }
}
