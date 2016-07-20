using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using EricYou.DirectUI.Skin.Styles.Controls;
using System.Windows.Forms;
using EricYou.DirectUI.Utils;

namespace EricYou.DirectUI.Skin.Styles
{
    /// <summary>
    /// DUIStyleTextBox控件DUI样式
    /// </summary>
    public class ButtonDUIStyle : GenericDUIStyle
    {
        #region 私有字段

        private string _fontName;     //字体名
        private FlatStyle _flatStyle; //按钮点击外观样式

        #endregion

        public ButtonDUIStyle(XmlNode styleNode)
            :base(styleNode)
        {
            if (styleNode == null)
            {
                throw new Exception("传入了空的XmlNode对象，无法初始化LabelDUIStyle对象！");
            }

            //读取基础属性
            _fontName = XMLConfigReader.ReadString("fontName", styleNode.Attributes["fontName"]);
            _flatStyle = XMLConfigReader.ReadFlatStyle(styleNode.Name, "flatStyle", styleNode.Attributes["flatStyle"]);
        }
        /// <summary>
        /// 为DUI样式控件赋值样式
        /// </summary>
        /// <param name="control">DUI样式控件基类对象</param>
        public override void SetControlStyle(IDUIStyleControl control)
        {
            DUIStyleButton button = control as DUIStyleButton;
            if (button == null)
            {
                return;
            }

            //获得皮肤全局字体对象
            DUIFont buttonFont = DUISkinManager.GetCurrentSkinManager().GetFont(_fontName);
            if (buttonFont != null)
            {
                if (buttonFont.Font != null)
                {
                    button.Font = buttonFont.Font;
                }
                if (buttonFont.BackColor != Color.Empty)
                {
                    button.BackColor = buttonFont.BackColor;
                }
                if (buttonFont.ForeColor != Color.Empty)
                {
                    button.ForeColor = buttonFont.ForeColor;
                }
            }
            button.FlatStyle = _flatStyle;
        }
    }
}
