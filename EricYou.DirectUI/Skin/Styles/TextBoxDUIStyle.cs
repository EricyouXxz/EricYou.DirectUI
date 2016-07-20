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
    public class TextBoxDUIStyle : GenericDUIStyle
    {
        #region 私有字段

        private string _fontName;         //字体名
        private BorderStyle _borderStyle; //边框样式

        #endregion

        public TextBoxDUIStyle(XmlNode styleNode)
            :base(styleNode)
        {
            if (styleNode == null)
            {
                throw new Exception("传入了空的XmlNode对象，无法初始化LabelDUIStyle对象！");
            }

            //读取基础属性
            _fontName = XMLConfigReader.ReadString("fontName", styleNode.Attributes["fontName"]);
            _borderStyle = XMLConfigReader.ReadBorderStyle(styleNode.Name, "borderStyle", styleNode.Attributes["borderStyle"]);
        }
        /// <summary>
        /// 为DUI样式控件赋值样式
        /// </summary>
        /// <param name="control">DUI样式控件基类对象</param>
        public override void SetControlStyle(IDUIStyleControl control)
        {
            DUIStyleTextBox textBox = control as DUIStyleTextBox;
            if (textBox == null)
            {
                return;
            }

            //获得皮肤全局字体对象
            DUIFont textboxFont = DUISkinManager.GetCurrentSkinManager().GetFont(_fontName);
            if (textboxFont != null)
            {
                if (textboxFont.Font != null)
                {
                    textBox.Font = textboxFont.Font;
                }
                if (textboxFont.BackColor != Color.Empty)
                {
                    textBox.BackColor = textboxFont.BackColor;
                }
                if (textboxFont.ForeColor != Color.Empty)
                {
                    textBox.ForeColor = textboxFont.ForeColor;
                }
            }
            textBox.BorderStyle = _borderStyle;
        }
    }
}
