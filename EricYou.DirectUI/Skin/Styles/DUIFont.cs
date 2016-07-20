using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using EricYou.DirectUI.Utils;

namespace EricYou.DirectUI.Skin.Styles
{
    /// <summary>
    /// DUI字体类
    /// </summary>
    public class DUIFont
    {
        #region 私有字段
        private string _name;                   //字体名
        private Color _backColor=Color.Empty;   //背景颜色
        private Color _foreColor=Color.Empty;   //前景（字体）颜色
        private Font _font=null;                //标题字体

        #endregion

        #region 公共属性
        /// <summary>
        /// 获取字体名
        /// </summary>
        public string Name
        {
          get { return _name; }
        }

        /// <summary>
        /// 获取背景颜色
        /// </summary>
        public Color BackColor
        {
          get { return _backColor; }
        }

        /// <summary>
        /// 前景（字体）颜色
        /// </summary>
        public Color ForeColor
        {
            get { return _foreColor; }
        }

        /// <summary>
        /// 获取字体对象
        /// </summary>
        public Font Font
        {
            get { return _font; }
        }

        #endregion

        #region public DUIFont(XmlNode fontNode)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="titleNode">字体定义节点</param>
        public DUIFont(XmlNode fontNode)
        {
            if (fontNode != null)
            {
                //读取基础属性
                _name = XMLConfigReader.ReadString("name", fontNode.Attributes["name"]);
                _backColor = XMLConfigReader.ReadColor(fontNode.Name, "backColor", fontNode.Attributes["backColor"]);
                _foreColor = XMLConfigReader.ReadColor(fontNode.Name, "foreColor", fontNode.Attributes["foreColor"]);

                string _fontFamily = XMLConfigReader.ReadString("fontFamily", fontNode.Attributes["fontFamily"]);
                int _fontSize = XMLConfigReader.ReadInt(fontNode.Name, "fontSize", fontNode.Attributes["fontSize"]);
                bool _fontBold = XMLConfigReader.ReadBoolean(fontNode.Name, "fontBold", fontNode.Attributes["fontBold"]);
                bool _fontItalic = XMLConfigReader.ReadBoolean(fontNode.Name, "fontItalic", fontNode.Attributes["fontItalic"]);
                bool _fontUnderline = XMLConfigReader.ReadBoolean(fontNode.Name, "fontUnderline", fontNode.Attributes["fontUnderline"]);
                bool _fontStrikeout = XMLConfigReader.ReadBoolean(fontNode.Name, "fontStrikeout", fontNode.Attributes["fontStrikeout"]);

                //构造组合属性
                try
                {
                    FontFamily fontFamily = new FontFamily(_fontFamily);
                    FontStyle fontStyle = FontStyle.Regular;
                    fontStyle = _fontBold ? (fontStyle | FontStyle.Bold) : fontStyle;
                    fontStyle = _fontItalic ? (fontStyle | FontStyle.Italic) : fontStyle;
                    fontStyle = _fontUnderline ? (fontStyle | FontStyle.Underline) : fontStyle;
                    fontStyle = _fontStrikeout ? (fontStyle | FontStyle.Strikeout) : fontStyle;

                    _font = new Font(fontFamily, _fontSize, fontStyle);

                }
                catch
                {
                    _font = null;
                }
            }
        }
        #endregion
    }
}
