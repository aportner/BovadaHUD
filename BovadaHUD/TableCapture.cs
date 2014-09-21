using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace BovadaHUD
{
    class TableCapture
    {
        [DllImport("dwmapi.dll")]
        private static extern int DwmGetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE dwAttribute, out RECT pvAttribute, int cbAttribute);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC", SetLastError = true)]
        private static extern IntPtr CreateCompatibleDC([In] IntPtr hdc);

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        private static extern IntPtr CreateCompatibleBitmap([In] IntPtr hdc, int nWidth, int nHeight);

        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        public static extern IntPtr SelectObject([In] IntPtr hdc, [In] IntPtr hgdiobj);

        [DllImport("gdi32.dll", EntryPoint = "BitBlt", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, CopyPixelOperation dwRop);

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        public static extern bool DeleteDC([In] IntPtr hdc);

        [DllImport("user32.dll")]
        private static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);


        private IntPtr hWnd;


        public TableCapture( IntPtr hWnd )
        {
            this.hWnd = hWnd;
        }


        public Bitmap capture()
        {
            // get te hDC of the target window
            IntPtr hdcSrc = GetWindowDC(hWnd);

            // get the size
            RECT rect = new RECT(0, 0, 100, 100);
            int stat = DwmGetWindowAttribute( hWnd, DWMWINDOWATTRIBUTE.ExtendedFrameBounds, out rect, Marshal.SizeOf( rect ) );

            // create a device context we can copy to
            IntPtr hdcDest = CreateCompatibleDC(hdcSrc);

            // create a bitmap we can copy it to,
            // using GetDeviceCaps to get the width/height
            IntPtr hBitmap = CreateCompatibleBitmap(hdcSrc, rect.Size.Width, rect.Size.Height);

            // select the bitmap object
            IntPtr hOld = SelectObject(hdcDest, hBitmap);

            // bitblt over
            BitBlt(hdcDest, 0, 0, rect.Size.Width, rect.Size.Height, hdcSrc, 0, 0, CopyPixelOperation.SourceCopy | CopyPixelOperation.CaptureBlt);

            // restore selection
            SelectObject(hdcDest, hOld);

            // clean up
            DeleteDC(hdcDest);
            ReleaseDC(hWnd, hdcSrc);

            // get a .NET image object for it
            Bitmap bitmap = Bitmap.FromHbitmap(hBitmap);

            DeleteObject(hBitmap);

            return bitmap;
        }
    }
}
