using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace EricYou.DirectUI.Native
{
    public static class InnerWin32
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public Point Reserved;
            public Point MaxSize;
            public Point MaxPosition;
            public Point MinTrackSize;
            public Point MaxTrackSize;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            public int X;
            public int Y;
            public Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        public const uint TPM_LEFTBUTTON = 0;
        public const uint TPM_RIGHTBUTTON = 2;
        public const uint TPM_LEFTALIGN = 0;
        public const uint TPM_CENTERALIGN = 4;
        public const uint TPM_RIGHTALIGN = 8;
        public const uint TPM_TOPALIGN = 0;
        public const uint TPM_VCENTERALIGN = 0x10;
        public const uint TPM_BOTTOMALIGN = 0x20;
        public const uint TPM_RETURNCMD = 0x100;  


    }
}
