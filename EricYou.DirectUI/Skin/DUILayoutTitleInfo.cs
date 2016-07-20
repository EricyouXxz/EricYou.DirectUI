using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using EricYou.DirectUI.Utils;
using EricYou.DirectUI.Skin.Styles;

namespace EricYou.DirectUI.Skin
{
    /// <summary>
    /// 皮肤布局标题区信息类
    /// </summary>
    public class DUILayoutTitleInfo : ICloneable
    {
        #region 私有字段

        private string _iconSourceName=string.Empty; //图标资源名
        private bool _showIcon=false;           //是否显示图标
        private string _text=string.Empty;      //标题文本
        private int _titleAreaHeight=30;        //窗口标题区域高度（用于决定窗口图标和标题纵向位置）
        private int _titleAreaOffset=12;        //窗口标题区域距离左边框距离（用于决定窗口图标和标题横向位置）
        private string _fontName = string.Empty;//标题字体全局字体名

        #endregion

        #region 公共属性

        /// <summary>
        /// 获取图标资源名
        /// </summary>
        public string IconSourceName
        {
            get { return _iconSourceName; }
        }

        /// <summary>
        /// 获取是否显示图标的标识
        /// </summary>
        public bool ShowIcon
        {
            get { return _showIcon; }
        }

        /// <summary>
        /// 获取标题文本
        /// </summary>
        public string Text
        {
            get { return _text; }
        }

        /// <summary>
        /// 获取窗口标题区域高度（用于决定窗口图标和标题纵向位置）
        /// </summary>
        public int TitleAreaHeight
        {
            get { return _titleAreaHeight; }
        }

        /// <summary>
        /// 获取窗口标题区域距离左边框距离（用于决定窗口图标和标题横向位置）
        /// </summary>
        public int TitleAreaOffset
        {
            get { return _titleAreaOffset; }
        }

        /// <summary>
        /// 获取标题DUI字体(DUI字体包含字体前后景颜色等更多属性)
        /// </summary>
        public DUIFont DUIFont
        {
            get
            {
                return DUISkinManager.GetCurrentSkinManager().GetFont(_fontName);
            }
        }

        #endregion

        private DUILayoutTitleInfo(DUILayoutTitleInfo layoutTitleInfo)
        {
            this._iconSourceName = layoutTitleInfo._iconSourceName;
            this._showIcon = layoutTitleInfo._showIcon;
            this._text = layoutTitleInfo._text;
            this._titleAreaHeight = layoutTitleInfo._titleAreaHeight;
            this._titleAreaOffset = layoutTitleInfo._titleAreaOffset;
            this._fontName = layoutTitleInfo._fontName;
        }

        #region public DUILayoutTitleInfo(XmlNode titleNode)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="titleNode">布局标题区定义节</param>
        public DUILayoutTitleInfo(XmlNode titleNode)
        {
            if (titleNode != null)
            {
                //读取基础属性
                _iconSourceName = XMLConfigReader.ReadString("iconSourceName", titleNode.Attributes["iconSourceName"]);
                _showIcon = XMLConfigReader.ReadBoolean(titleNode.Name, "showIcon", titleNode.Attributes["showIcon"]);
                _text = XMLConfigReader.ReadString("text", titleNode.Attributes["text"]);
                _titleAreaHeight = XMLConfigReader.ReadInt(titleNode.Name, "titleAreaHeight", titleNode.Attributes["titleAreaHeight"]);
                _titleAreaOffset = XMLConfigReader.ReadInt(titleNode.Name, "titleAreaOffset", titleNode.Attributes["titleAreaOffset"]);
                _fontName = XMLConfigReader.ReadString("fontName", titleNode.Attributes["fontName"]);

            }
        }
        #endregion

        /// <summary>
        /// 克隆函数
        /// </summary>
        /// <returns>克隆后的新DUILayoutTitleInfo对象</returns>
        public object Clone()
        {
            DUILayoutTitleInfo newLayoutTitleInfo = new DUILayoutTitleInfo(this);
            return newLayoutTitleInfo;
        }
    }
}
