using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace EricYou.DirectUI.Native
{
    public static class NativeMethods
    {


        #region 系统函数

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool AdjustWindowRectEx(EricYou.DirectUI.Native.RECT lpRect, int dwStyle, bool bMenu, int dwExStyle);
 
        [DllImport("user32.dll")]
        public static extern long SetWindowLong(IntPtr hWnd, int nIndex, long nNewLong);

        [DllImport("user32.dll")]
        public static extern long GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern int PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        //[DllImport("user32.dll")]
        //public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        [DllImport("User32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("User32.dll")]
        public static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("User32.dll")]
        public static extern int TrackPopupMenu(IntPtr hMenu, uint uFlags, int x, int y, int nReserved, IntPtr hWnd, out Rectangle prcRect);

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("user32")]
        public static extern int GetSystemMetrics(int nIndex);

        [DllImport("user32")]
        public static extern bool IsZoomed(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern int SetWindowPos(IntPtr hwnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint wFlags);

        #endregion

        #region 图形相关

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(
            IntPtr hdcDest, // 目标 DC的句柄         
            int nXDest, int nYDest, int nWidth, int nHeight,
            IntPtr hdcSrc,  // 源DC的句柄         
            int nXSrc, int nYSrc,
            System.Int32 dwRop // 光栅的处理数值         
            );

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hObject);

        #endregion
    }


    //[StructLayout(LayoutKind.Sequential)]
    //public struct RECT
    //{
    //    public int Left;
    //    public int Top;
    //    public int Right;
    //    public int Bottom;

    //}

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        public RECT(System.Drawing.Rectangle r)
        {
            this.left = r.Left;
            this.top = r.Top;
            this.right = r.Right;
            this.bottom = r.Bottom;
        }

        public static RECT FromXYWH(int x, int y, int width, int height)
        {
            return new RECT(x, y, x + width, y + height);
        }

        public System.Drawing.Size Size
        {
            get
            {
                return new System.Drawing.Size(this.right - this.left, this.bottom - this.top);
            }
        }
    } 

    [StructLayout(LayoutKind.Sequential)]
    public struct PWINDOWPOS
    {
        public IntPtr hwnd;
        public IntPtr hwndInsertAfter;
        public int x;
        public int y;
        public int cx;
        public int cy;
        public uint flags;
    }



    [StructLayout(LayoutKind.Sequential)]
    public struct NCCALCSIZE_PARAMS
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public RECT[] rgrc;
        public PWINDOWPOS lppos;
    }

}
