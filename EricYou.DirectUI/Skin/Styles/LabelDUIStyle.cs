using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using EricYou.DirectUI.Skin.Styles.Controls;
using EricYou.DirectUI.Utils;

namespace EricYou.DirectUI.Skin.Styles
{
    /// <summary>
    /// DUIStyleLabel控件DUI样式
    /// </summary>
    public class LabelDUIStyle : GenericDUIStyle
    {
        #region 私有字段

        private string _fontName;   //字体名

        #endregion

        public LabelDUIStyle(XmlNode styleNode)
            :base(styleNode)
        {
            if (styleNode == null)
            {
                throw new Exception("传入了空的XmlNode对象，无法初始化LabelDUIStyle对象！");
            }

            _fontName = XMLConfigReader.ReadString("fontName", styleNode.Attributes["fontName"]);
        }
        /// <summary>
        /// 为DUI样式控件赋值样式
        /// </summary>
        /// <param name="control">DUI样式控件基类对象</param>
        public override void SetControlStyle(IDUIStyleControl control)
        {
            DUIStyleLabel label = control as DUIStyleLabel;
            if (label == null)
            {
                return;
            }

            DUIFont labelFont = DUISkinManager.GetCurrentSkinManager().GetFont(_fontName);
            if (labelFont != null)
            {
                if (labelFont.Font != null)
                {
                    label.Font = labelFont.Font;
                }
                if (labelFont.BackColor != Color.Empty)
                {
                    label.BackColor = labelFont.BackColor;
                }
                if (labelFont.ForeColor != Color.Empty)
                {
                    label.ForeColor = labelFont.ForeColor;
                }
            }
        }
    }
}
