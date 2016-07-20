using System;
using System.Collections.Generic;
using System.Text;

namespace EricYou.DirectUI.Native
{
    /// <summary>
    /// win32消息及其他常量定义
    /// </summary>
    public static class NativeConst
    {
        public const int WM_CREATE                  = 0x0001;
        public const int WM_DESTROY                 = 0x0002;
        public const int WM_MOVE                    = 0x0003; 
        public const int WM_SIZE                    = 0x0005;
        public const int WM_ACTIVATE                = 0x0006;
        public const int WM_SETFOCUS                = 0x0007;
        public const int WM_KILLFOCUS               = 0x0008;
        public const int WM_ENABLE                  = 0x000A;
        public const int WM_SETREDRAW               = 0x000B;
        public const int WM_SETTEXT                 = 0x000C;
        public const int WM_GETTEXT                 = 0x000D;
        public const int WM_GETTEXTLENGTH           = 0x000E;
        public const int WM_PAINT                   = 0x000F;
        public const int WM_CLOSE                   = 0x0010;
        public const int WM_QUERYENDSESSION         = 0x0011;
        public const int WM_QUIT                    = 0x0012;
        public const int WM_QUERYOPEN               = 0x0013;
        public const int WM_ERASEBKGND              = 0x0014;
        public const int WM_SYSCOLORCHANGE          = 0x0015;
        public const int WM_ENDSESSION              = 0x0016;
        public const int WM_SHOWWINDOW              = 0x0018;
        public const int WM_ACTIVATEAPP             = 0x001C;
        public const int WM_FONTCHANGE              = 0x001D;
        public const int WM_TIMECHANGE              = 0x001E;
        public const int WM_CANCELMODE              = 0x001F;
        public const int WM_SETCURSOR               = 0x0020;
        public const int WM_MOUSEACTIVATE           = 0x0021;
        public const int WM_CHILDACTIVATE           = 0x0022;
        public const int WM_QUEUESYNC               = 0x0023;
        public const int WM_GETMINMAXINFO           = 0x0024;
        public const int WM_PAINTICON               = 0x0026;
        public const int WM_ICONERASEBKGND          = 0x0027;
        public const int WM_NEXTDLGCTL              = 0x0028;
        public const int WM_SPOOLERSTATUS           = 0x002A;
        public const int WM_DRAWITEM                = 0x002B;
        public const int WM_MEASUREITEM             = 0x002C;
        public const int WM_VKEYTOITEM              = 0x002E;
        public const int WM_CHARTOITEM              = 0x002F;
        public const int WM_SETFONT                 = 0x0030;
        public const int WM_GETFONT                 = 0x0031;
        public const int WM_SETHOTKEY               = 0x0032;
        public const int WM_GETHOTKEY               = 0x0033;
        public const int WM_QUERYDRAGICON           = 0x0037;
        public const int WM_COMPAREITEM             = 0x0039;
        public const int WM_COMPACTING              = 0x0041;
        public const int WM_WINDOWPOSCHANGING       = 0x0046;
        public const int WM_WINDOWPOSCHANGED        = 0x0047;
        public const int WM_POWER                   = 0x0048;
        public const int WM_COPYDATA                = 0x004A;
        public const int WM_CANCELJOURNA            = 0x004B;
        public const int WM_NOTIFY                  = 0x004E;
        public const int WM_INPUTLANGCHANGEREQUEST  = 0x0050;
        public const int WM_INPUTLANGCHANGE         = 0x0051;
        public const int WM_TCARD                   = 0x0052;
        public const int WM_HELP                    = 0x0053;
        public const int WM_USERCHANGED             = 0x0054;
        public const int WM_NOTIFYFORMAT            = 0x0055;
        public const int WM_CONTEXTMENU             = 0x007B;
        public const int WM_STYLECHANGING           = 0x007C;
        public const int WM_STYLECHANGED            = 0x007D;
        public const int WM_DISPLAYCHANGE           = 0x007E;
        public const int WM_GETICON                 = 0x007F;
        public const int WM_SETICON                 = 0x0080;
        public const int WM_NCCREATE                = 0x0081;
        public const int WM_NCDESTROY               = 0x0082;
        public const int WM_NCCALCSIZE              = 0x0083;
        public const int WM_NCHITTEST               = 0x0084;
        public const int WM_NCPAINT                 = 0x0085;
        public const int WM_NCACTIVATE              = 0x0086;
        public const int WM_GETDLGCODE              = 0x0087;
        public const int WM_NCMOUSEMOVE             = 0x00A0;
        public const int WM_NCLBUTTONDOWN           = 0x00A1;
        public const int WM_NCLBUTTONUP             = 0x00A2;
        public const int WM_NCLBUTTONDBLCLK         = 0x00A3;
        public const int WM_NCRBUTTONDOWN           = 0x00A4;
        public const int WM_NCRBUTTONUP             = 0x00A5;
        public const int WM_NCRBUTTONDBLCLK         = 0x00A6;
        public const int WM_NCMBUTTONDOWN           = 0x00A7;
        public const int WM_NCMBUTTONUP             = 0x00A8;
        public const int WM_NCMBUTTONDBLCLK         = 0x00A9;
        public const int WM_KEYFIRST                = 0x0100;
        public const int WM_KEYUP                   = 0x0101;
        public const int WM_CHAR                    = 0x0102;
        public const int WM_DEADCHAR                = 0x0103;
        public const int WM_SYSKEYDOWN              = 0x0104;
        public const int WM_SYSKEYUP                = 0x0105;
        public const int WM_SYSCHAR                 = 0x0106;
        public const int WM_SYSDEADCHAR             = 0x0107;
        public const int WM_INITDIALOG              = 0x0110;
        public const int WM_COMMAND                 = 0x0111;
        public const int WM_SYSCOMMAND              = 0x0112;
        public const int WM_TIMER                   = 0x0113;
        public const int WM_HSCROLL                 = 0x0114;
        public const int WM_VSCROLL                 = 0x0115;
        public const int WM_INITMENU                = 0x0116;
        public const int WM_INITMENUPOPUP           = 0x0117;
        public const int WM_MENUSELECT              = 0x011F;
        public const int WM_MENUCHAR                = 0x0120;
        public const int WM_ENTERIDLE               = 0x0121;
        public const int WM_CTLCOLORMSGBOX          = 0x0132;
        public const int WM_CTLCOLOREDIT            = 0x0133;
        public const int WM_CTLCOLORLISTBOX         = 0x0134;
        public const int WM_CTLCOLORBTN             = 0x0135;
        public const int WM_CTLCOLORDLG             = 0x0136;
        public const int WM_CTLCOLORSCROLLBAR       = 0x0137;
        public const int WM_CTLCOLORSTATIC          = 0x0138;
        public const int WM_SHARED_MENU             = 0x01E2;
        public const int WM_MOUSEFIRST              = 0x0200;
        public const int WM_MOUSEMOVE               = 0x0200;
        public const int WM_LBUTTONDOWN             = 0x0201;
        public const int WM_LBUTTONUP               = 0x0202;
        public const int WM_LBUTTONDBLCLK           = 0x0203;
        public const int WM_RBUTTONDOWN             = 0x0204;
        public const int WM_RBUTTONUP               = 0x0205;
        public const int WM_RBUTTONDBLCLK           = 0x0206;
        public const int WM_MBUTTONDOWN             = 0x0207;
        public const int WM_MBUTTONUP               = 0x0208;
        public const int WM_MBUTTONDBLCLK           = 0x0209;
        public const int WM_MOUSEWHEEL              = 0x020A;
        public const int WM_PRINT                   = 0x0317;

        public const int HC_ACTION                  = 0;
        public const int WH_CALLWNDPROC             = 4;

        //LONG GetWindowLong(HWND hWnd，int nlndex)函数nIndex取值,以及
        //LONG SetWindowLong（HWND hWnd，int nlndex，LONG dwNewLong）函数nIndex取值
        public const int GWL_EXSTYLE                = -20;  //获得扩展窗口风格。
        public const int GWL_STYLE                  = -16;  //获得窗口风格。
        public const int GWL_WNDPROC                = -4;   //获得窗口过程的地址，或代表窗口过程的地址的句柄。必须使用CallWindowProc函数调用窗口过程。
        public const int GWL_HINSTANCE              = -6;   //获得应用事例的句柄。
        public const int GWL_HWNDPARENT             = -8;   //如果父窗口存在，获得父窗口句柄。
        public const int GWL_ID                     = -12;  //获得窗口标识。
        public const int GWL_USERDATA               = -21;  //获得与窗口有关的32位值。每一个窗口均有一个由创建该窗口的应用程序使用的32位值。
        //在hWnd参数标识了一个对话框时也可用下列值：
        public const int DWL_DLGPROC                = 4;    //获得对话框过程的地址，或一个代表对话框过程的地址的句柄。必须使用函数CallWindowProc来调用对话框过程。
        public const int DWL_MSGRESULT              = 0;    //获得在对话框过程中一个消息处理的返回值。
        public const int DWL_USER                   = 8;    //获得应用程序私有的额外信息，例如一个句柄或指针。[1]

        //Extended Window Styles
        public const long WS_EX_ACCEPTFILES         = 0x00000010;
        public const long WS_EX_APPWINDOW           = 0x00040000;
        public const long WS_EX_CLIENTEDGE          = 0x00000200;
        public const long WS_EX_COMPOSITED          = 0x02000000;
        public const long WS_EX_CONTEXTHELP         = 0x00000400;
        public const long WS_EX_CONTROLPARENT       = 0x00010000;
        public const long WS_EX_DLGMODALFRAME       = 0x00000001;
        public const long WS_EX_LAYERED             = 0x00080000;
        public const long WS_EX_LAYOUTRTL           = 0x00400000;
        public const long WS_EX_LEFT                = 0x00000000;
        public const long WS_EX_LEFTSCROLLBAR       = 0x00004000;
        public const long WS_EX_LTRREADING          = 0x00000000;
        public const long WS_EX_MDICHILD            = 0x00000040;
        public const long WS_EX_NOACTIVATE          = 0x08000000;
        public const long WS_EX_NOINHERITLAYOUT     = 0x00100000;
        public const long WS_EX_NOPARENTNOTIFY      = 0x00000004;
        public const long WS_EX_NOREDIRECTIONBITMAP = 0x00200000;
        public const long WS_EX_OVERLAPPEDWINDOW    = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE);
        public const long WS_EX_PALETTEWINDOW       = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);
        public const long WS_EX_RIGHT               = 0x00001000;
        public const long WS_EX_RIGHTSCROLLBAR      = 0x00000000;
        public const long WS_EX_RTLREADING          = 0x00002000;
        public const long WS_EX_STATICEDGE          = 0x00020000;
        public const long WS_EX_TOOLWINDOW          = 0x00000080;
        public const long WS_EX_TOPMOST             = 0x00000008;
        public const long WS_EX_TRANSPARENT         = 0x00000020;
        public const long WS_EX_WINDOWEDGE          = 0x00000100;
        //Window Styles
        public const long WS_BORDER                 = 0x00800000;
        public const long WS_CAPTION                = 0x00C00000;
        public const long WS_CHILD                  = 0x40000000;
        public const long WS_CHILDWINDOW            = 0x40000000;
        public const long WS_CLIPCHILDREN           = 0x02000000;
        public const long WS_CLIPSIBLINGS           = 0x04000000;
        public const long WS_DISABLED               = 0x08000000;
        public const long WS_DLGFRAME               = 0x00400000;
        public const long WS_GROUP                  = 0x00020000;
        public const long WS_HSCROLL                = 0x00100000;
        public const long WS_ICONIC                 = 0x20000000;
        public const long WS_MAXIMIZE               = 0x01000000;
        public const long WS_MAXIMIZEBOX            = 0x00010000;
        public const long WS_MINIMIZE               = 0x20000000;
        public const long WS_MINIMIZEBOX            = 0x00020000;
        public const long WS_OVERLAPPED             = 0x00000000;
        public const long WS_OVERLAPPEDWINDOW       = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX);
		public const long WS_POPUP 			        = 0x80000000;
		public const long WS_POPUPWINDOW 			= (WS_POPUP | WS_BORDER | WS_SYSMENU);
        public const long WS_SIZEBOX                = 0x00040000;
        public const long WS_SYSMENU                = 0x00080000;
        public const long WS_TABSTOP                = 0x00010000;
        public const long WS_THICKFRAME             = 0x00040000;
        public const long WS_TILED                  = 0x00000000;
        public const long WS_TILEDWINDOW            = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX);
        public const long WS_VISIBLE                = 0x10000000;
        public const long WS_VSCROLL                = 0x00200000;

        public const int GW_HWNDFIRST               = 0;
        public const int GW_HWNDLAST                = 1;
        public const int GW_HWNDNEXT                = 2;
        public const int GW_HWNDPREV                = 3;
        public const int GW_OWNER                   = 4;
        public const int GW_CHILD                   = 5;

        public const int SC_RESTORE                 = 0xF120; //还原  
        public const int SC_MOVE                    = 0xF010; //移动  
        public const int SC_SIZE                    = 0xF000; //大小  
        public const int SC_MINIMIZE                = 0xF020; //最小化  
        public const int SC_MAXIMIZE                = 0xF030; //最大化  
        public const int SC_CLOSE                   = 0xF060; //关闭 

        //WM_NCHITTEST消息处理返回值
        public const int HTERROR                    = -2;
        public const int HTTRANSPARENT              = -1;
        public const int HTNOWHERE                  = 0;
        public const int HTCLIENT                   = 1;
        public const int HTCAPTION                  = 2;
        public const int HTSYSMENU                  = 3;
        public const int HTGROWBOX                  = 4;
        public const int HTSIZE                     = HTGROWBOX;
        public const int HTMENU                     = 5;
        public const int HTHSCROLL                  = 6;
        public const int HTVSCROLL                  = 7;
        public const int HTMINBUTTON                = 8;
        public const int HTMAXBUTTON                = 9;
        public const int HTLEFT                     = 10;
        public const int HTRIGHT                    = 11;
        public const int HTTOP                      = 12;
        public const int HTTOPLEFT                  = 13;
        public const int HTTOPRIGHT                 = 14;
        public const int HTBOTTOM                   = 15;
        public const int HTBOTTOMLEFT               = 16;
        public const int HTBOTTOMRIGHT              = 17;
        public const int HTBORDER                   = 18;
        public const int HTREDUCE                   = HTMINBUTTON;
        public const int HTZOOM                     = HTMAXBUTTON;
        public const int HTSIZEFIRST                = HTLEFT;
        public const int HTSIZELAST                 = HTBOTTOMRIGHT;
        public const int HTOBJECT                   = 19;
        public const int HTCLOSE                    = 20;
        public const int HTHELP                     = 21;

        //GetSystemMetrics参数
        public const int SM_CXSCREEN                = 0;
        public const int SM_CYSCREEN                = 1;
        public const int SM_CXFULLSCREEN            = 16;
        public const int SM_CYFULLSCREEN            = 17;
        public const int SM_CYMENU                  = 15;
        public const int SM_CYCAPTION               = 4;
        public const int SM_CXFRAME                 = 32;
        public const int SM_CYFRAME                 = 33;
        public const int SM_CXHSCROLL               = 21;
        public const int SM_CYHSCROLL               = 3;
        public const int SM_CXVSCROLL               = 2;
        public const int SM_CYVSCROLL               = 20;
        public const int SM_CXSIZE                  = 30;
        public const int SM_CYSIZE                  = 31;
        public const int SM_CXCURSOR                = 13;
        public const int SM_CYCURSOR                = 14;
        public const int SM_CXBORDER                = 5;
        public const int SM_CYBORDER                = 6;
        public const int SM_CXDOUBLECLICK           = 36;
        public const int SM_CYDOUBLECLICK           = 37;
        public const int SM_CXDLGFRAME              = 7;
        public const int SM_CXFIXEDFRAME            = SM_CXDLGFRAME;
        public const int SM_CYDLGFRAME              = 8;
        public const int SM_CYFIXEDFRAME            = SM_CYDLGFRAME;
        public const int SM_CXICON                  = 11;
        public const int SM_CYICON                  = 12;
        public const int SM_CXICONSPACING           = 38;
        public const int SM_CYICONSPACING           = 39;
        public const int SM_CXMIN                   = 28;
        public const int SM_CYMIN                   = 29;
        public const int SM_CXMINTRACK              = 34;
        public const int SM_CYMINTRACK              = 35;
        public const int SM_CXHTHUMB                = 10;
        public const int SM_CYVTHUMB                = 9;
        public const int SM_DBCSENABLED             = 42;
        public const int SM_DEBUG                   = 22;
        public const int SM_MENUDROPALIGNMENT       = 40;
        public const int SM_MOUSEPRESENT            = 19;
        public const int SM_PENWINDOWS              = 41;
        public const int SM_SWAPBUTTON              = 23;

        //ShowWindow参数
        public const int SW_FORCEMINIMIZE           = 11;
        public const int SW_HIDE                    = 0;
        public const int SW_MAXIMIZE                = 3;
        public const int SW_MINIMIZE                = 6;
        public const int SW_RESTORE                 = 9;
        public const int SW_SHOW                    = 5;
        public const int SW_SHOWDEFAULT             = 10;
        public const int SW_SHOWMAXIMIZED           = 3;
        public const int SW_SHOWMINIMIZED           = 2;
        public const int SW_SHOWMINNOACTIVE         = 7;
        public const int SW_SHOWNA                  = 8;
        public const int SW_SHOWNOACTIVATE          = 4;
        public const int SW_SHOWNORMAL              = 1;

        public const int MF_REMOVE                  = 0x1000;

        /// <summary>
        /// 贴图选项：将源矩形区域直接拷贝到目标矩形区域
        /// </summary>
        public const int ROP_SRCCOPY                = 0x00CC0020;
    }
}
