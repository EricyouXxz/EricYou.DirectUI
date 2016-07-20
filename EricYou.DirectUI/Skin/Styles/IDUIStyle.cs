using System;
using System.Collections.Generic;
using System.Text;
using EricYou.DirectUI.Skin.Styles.Controls;

namespace EricYou.DirectUI.Skin.Styles
{
    /// <summary>
    /// DUI样式接口
    /// </summary>
    public interface IDUIStyle
    {
        /// <summary>
        /// 为DUI样式控件赋值样式
        /// </summary>
        /// <param name="control">DUI样式控件基类对象</param>
        void SetControlStyle(IDUIStyleControl control);
    }
}
