using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using EricYou.DirectUI.Native;
using EricYou.DirectUI.Utils;

namespace EricYou.DirectUI.Forms
{
    /// <summary>
    /// DirectUI换肤弹出窗口基类
    /// </summary>
    public class DUIPopupForm : DUIForm
    {
        //使用该种方式设置窗口WS_EX_COMPOSITED样式，将导致VS设计器假死，估注释
        protected override CreateParams CreateParams
        {
            get
            {
                //使用DesignMode进行设计时判断只能用于顶层窗口，不能用于窗口内的控件
                //只有在运行期才设置窗口的WS_EX_COMPOSITED样式
                if (!this.DesignMode)
                {
                    CreateParams cp = base.CreateParams;
                    //设置窗口的WS_EX_COMPOSITED样式，使窗口背景刷新时控件不闪烁
                    cp.ExStyle |= (int)NativeConst.WS_EX_COMPOSITED;  // Turn on WS_EX_COMPOSITED
                    return cp;
                }
                else
                {
                    return base.CreateParams;
                }
            }
        }

    }
}
