using System;
using System.Collections.Generic;
using System.Text;

namespace EricYou.DirectUI.Forms
{
    /// <summary>
    /// 子窗口内容容器接口，用户子窗口内容延迟加载
    /// </summary>
    public interface ISubContentHolder
    {
        /// <summary>
        /// 第一次切换到本窗口时由MainContentHolder调用的函数
        /// 用于延迟加载窗口子内容
        /// </summary>
        void OnFirstShow();
    }
}
