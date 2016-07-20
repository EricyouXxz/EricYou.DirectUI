using System;
using System.Collections.Generic;
using System.Text;
using EricYou.DirectUI.Skin.Styles.Controls;
using System.Xml;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace EricYou.DirectUI.Skin.Styles
{
    /// <summary>
    /// DUI样式基类
    /// </summary>
    public abstract class GenericDUIStyle : IDUIStyle
    {
        /// <summary>
        /// 样式名
        /// </summary>
        private string _name;

        /// <summary>
        /// 获取和设置样式名
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="styleNode">包含样式信息的XML节点</param>
        public GenericDUIStyle(XmlNode styleNode)
        {
            if (styleNode == null)
            {
                throw new Exception("传入了空的XmlNode对象，无法初始化DUIStyle对象！");
            }
            if (styleNode.Attributes["name"] == null)
            {
                throw new Exception("当前XmlNode不存在name属性，无法初始化DUIStyle对象！");
            }

            _name = styleNode.Attributes["name"].Value;
        }

        #region 接口IDUIStyle成员
        /// <summary>
        /// 为DUI样式控件赋值样式
        /// </summary>
        /// <param name="control">DUI样式控件基类对象</param>
        public abstract void SetControlStyle(IDUIStyleControl control);

        #endregion
    }
}
