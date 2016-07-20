using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using EricYou.DirectUI.Utils;
using EricYou.DirectUI.Skin.Designer;
using System.Drawing.Design;

namespace EricYou.DirectUI.Skin.Styles.Controls
{
    /// <summary>
    /// DUI样式控件之Label控件
    /// </summary>
    public class DUIStyleLabel : Label, IDUIStyleControl
    {
        /// <summary>
        /// 样式配置名
        /// </summary>
        private string _dUIStyleName = string.Empty;

        /// <summary>
        /// 获取和设置样式配置名
        /// </summary>
        [Description("样式配置名，用于指定当前控件使用的样式配置")]
        [Category("换肤框架")]
        [DefaultValue("")]
        [Editor(typeof(StyleNameEditor), typeof(UITypeEditor))]
        public string DUIStyleName
        {
            get { return _dUIStyleName; }
            set
            {
                _dUIStyleName = value;
                ProcessDUIStyle();
                
            }
        }

        /// <summary>
        /// 根据DUIStyleName从配置读取并设置自身样式
        /// </summary>
        public void ProcessDUIStyle()
        {
            if (!string.IsNullOrEmpty(this._dUIStyleName))
            {
                IDUIStyle DUIStyle = DUISkinManager.GetCurrentSkinManager().GetStyle(this.GetType(), this._dUIStyleName);
                if (DUIStyle != null)
                {
                    DUIStyle.SetControlStyle(this);
                }
            }
        }
    }
}
