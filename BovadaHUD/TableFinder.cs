using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BovadaHUD
{
    class TableFinder
    {
        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);


        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string sClass, string sWindow);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);


        public TableFinder()
        {
        }


        private static string GetWindowText(IntPtr hWnd)
        {
            int size = GetWindowTextLength(hWnd);
            if (size++ > 0)
            {
                var builder = new StringBuilder(size);
                GetWindowText(hWnd, builder, builder.Capacity);
                return builder.ToString();
            }

            return String.Empty;
        }


        public Table FindTable()
        {
            Table table = null;
            IntPtr found = IntPtr.Zero;

            EnumWindows(delegate(IntPtr wnd, IntPtr param)
            {
                String windowText = GetWindowText(wnd);
                // Console.WriteLine(windowText);

                if (windowText.Contains( "Hold'em" ) )
                {
                    found = wnd;
                }

                return true;
            }, IntPtr.Zero);

            if ( found != IntPtr.Zero )
            {
                table = new Table(found);
            }

            return table;
        }
    }
}
