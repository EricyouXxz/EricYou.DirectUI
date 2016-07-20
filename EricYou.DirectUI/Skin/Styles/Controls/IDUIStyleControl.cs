using System;
using System.Collections.Generic;
using System.Text;

namespace EricYou.DirectUI.Skin.Styles.Controls
{
    /// <summary>
    /// DUI样式控件接口
    /// </summary>
    public interface  IDUIStyleControl
    {
        /// <summary>
        /// 获取和设置样式配置名
        /// </summary>
        string DUIStyleName
        {
            get;
            set;
        }

        /// <summary>
        /// 根据DUIStyleName从配置读取并设置自身样式
        /// </summary>
        void ProcessDUIStyle();
    }
}
