using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Interop;

namespace Salary.Helpers
{
    /// <summary>
    /// Вспомгательный класс, конвертирует ВПФ окно в виндоус форма окно, для показа диалогового окна
    /// </summary>
    public class Wpf32Window : System.Windows.Forms.IWin32Window
    {
        public IntPtr Handle { get; private set; }

        public Wpf32Window(Window wpfWindow)
        {
            Handle = new WindowInteropHelper(wpfWindow).Handle;
        }
    }
}
