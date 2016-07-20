using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace EricYou.DirectUI.Utils
{
    public class WindowAnimator
    {
        #region 常量申明
        /// <summary>
        /// 自左向右显示控件.当使用AW_CENTER标志时,该标志将被忽略.
        /// </summary>
        public const Int32 AW_HOR_POSITIVE = 0x00000001;

        /// <summary>
        /// 自右向左显示控件.当使用AW_CENTER标志时,该标志将被忽略.
        /// </summary>
        public const Int32 AW_HOR_NEGATIVE = 0x00000002;

        /// <summary>
        /// 自顶向下显示控件.该标志可以在滚动动画和滑动动画中使用.当使用AW_CENTER标志时,该标志将被忽略.
        /// </summary>
        public const Int32 AW_VER_POSITIVE = 0x00000004;

        /// <summary>
        /// 自下向上显示控件.该标志可以在滚动动画和滑动动画中使用.当使用AW_CENTER标志时,该标志将被忽略.
        /// </summary>
        public const Int32 AW_VER_NEGATIVE = 0x00000008;

        /// <summary>
        /// 若使用AW_HIDE标志,则使控件向内重叠;若未使用AW_HIDE标志,则使控件向外扩展.
        /// </summary>
        public const Int32 AW_CENTER = 0x00000010;

        /// <summary>
        /// 隐藏控件.默认则显示控件.
        /// </summary>
        public const Int32 AW_HIDE = 0x00010000;

        /// <summary>
        /// 激活控件.在使用AW_HIDE标志后不要使用这个标志.
        /// </summary>
        public const Int32 AW_ACTIVATE = 0x00020000;

        /// <summary>
        /// 使用滑动类型.默认则为滚动动画类型.当使用AW_CENTER标志时,这个标志就被忽略.
        /// </summary>
        public const Int32 AW_SLIDE = 0x00040000;

        /// <summary>
        /// 使用淡入效果.只有当hWnd为顶层控件时才可以使用此标志.
        /// </summary>
        public const Int32 AW_BLEND = 0x00080000;
        #endregion

        [DllImportAttribute("user32.dll")]
        public static extern bool AnimateWindow(IntPtr hWnd, int dwTime, int dwFlags);
    }
}
