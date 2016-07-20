using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Reflection;
using System.Xml;
using System.Windows.Forms;

namespace EricYou.DirectUI.Utils
{
    public static class XMLConfigReader
    {

        #region public static Color ReadColor(string nodeName, string attibuteName, XmlAttribute attribute)
        /// <summary>
        /// 读取颜色型配置节
        /// </summary>
        /// <param name="nodeName">读取的XmlAttribute所属XmlNode的name</param>
        /// <param name="attributeName">要读取的配置节属性名</param>
        /// <param name="attribute">要读取的配置节属性对象</param>
        /// <returns>读取并转换好的Color对象，若数据不存在则返回Color.Empty</returns>
        public static Color ReadColor(string nodeName, string attributeName, XmlAttribute attribute)
        {
            //返回值
            Color retColor = Color.Empty;

            if (attribute == null)
            {
                return Color.Empty; //配置节不存在时返回空颜色
            }
            string colorString = attribute.Value.Trim();
            if (string.IsNullOrEmpty(colorString))
            {
                return Color.Empty; //配置节存在，但值为空的时候，返回空颜色
            }

            if (colorString.ToLower().StartsWith("color:"))
            {
                string[] splitString = colorString.Split(new char[] { ':' });
                if (splitString.Length != 2)
                {
                    throw new Exception(string.Format(
                                       "读取节点{0}中的颜色属性{1}时，值[{2}]格式不合法，请检查皮肤配置文件相关配置节！",
                                       nodeName, attributeName,colorString));
                }

                string colorName = splitString[1];
                PropertyInfo colorProperty = typeof(Color).GetProperty(colorName);
                if (colorProperty == null)
                {
                    retColor = Color.Empty;
                }
                else
                {
                    retColor = (Color)colorProperty.GetValue(null, null);
                }

            }
            else if (colorString.ToLower().StartsWith("systemcolors:"))
            {
                string[] splitString = colorString.Split(new char[] { ':' });
                if (splitString.Length != 2)
                {
                    throw new Exception(string.Format(
                                      "读取节点{0}中的颜色属性{1}时，值[{2}]格式不合法，请检查皮肤配置文件相关配置节！",
                                       nodeName, attributeName,colorString));
                }

                string colorName = splitString[1];
                PropertyInfo colorProperty = typeof(SystemColors).GetProperty(colorName);
                if (colorProperty == null)
                {
                    retColor = Color.Empty;
                }
                else
                {
                    retColor = (Color)colorProperty.GetValue(null, null);
                }
            }
            else if (colorString.ToLower().StartsWith("rgb:"))
            {
                string[] splitString = colorString.Split(new char[] { ':' });
                if (splitString.Length != 2)
                {
                    throw new Exception(string.Format(
                                       "读取节点{0}中的颜色属性{1}时，值[{2}]格式不合法，请检查皮肤配置文件相关配置节！",
                                       nodeName, attributeName,colorString));
                }

                string rgbColorValue = splitString[1];
                string[] splitRgbColorValue = rgbColorValue.Split(new char[] { ',' });
                if (splitRgbColorValue.Length != 3)
                {
                    throw new Exception(string.Format(
                                     "读取节点{0}中的颜色属性{1}时，值[{2}]格式不合法，请检查皮肤配置文件相关配置节！",
                                       nodeName, attributeName,colorString));
                }
                else
                {
                    try
                    {
                        retColor = Color.FromArgb(Convert.ToInt32(splitRgbColorValue[0]),
                                                    Convert.ToInt32(splitRgbColorValue[1]),
                                                    Convert.ToInt32(splitRgbColorValue[2]));
                    }
                    catch
                    {
                        throw new Exception(string.Format(
                                       "读取节点{0}中的颜色属性{1}时，值[{2}]格式不合法，请检查皮肤配置文件相关配置节！",
                                       nodeName, attributeName,colorString));
                    }
                }
            }
            else
            {
                throw new Exception(string.Format(
                    "读取节点{0}中的颜色属性{1}时，值[{2}]格式不合法，请检查皮肤配置文件相关配置节！",
                                       nodeName, attributeName,colorString));
            }

            return retColor;
        }
        #endregion

        #region public static int ReadInt(string nodeName, string attributeName, XmlAttribute attribute)
        /// <summary>
        /// 读取整形型配置节
        /// </summary>
        /// <param name="nodeName">读取的XmlAttribute所属XmlNode的name</param>
        /// <param name="attributeName">要读取的配置节属性名</param>
        /// <param name="attribute">要读取的配置节属性对象</param>
        /// <returns>读取并转换好的整形数据,若数据不存在则返回-1</returns>
        public static int ReadInt(string nodeName, string attributeName, XmlAttribute attribute)
        {
            int retInt = -1;

            if (attribute == null)
            {
                //不存在指定属性时返回-1
                return -1;
            }
            string value = attribute.Value.Trim();
            if (string.IsNullOrEmpty(value))
            {
                return -1; //配置节存在，但值为空的时候，返回-1
            }

            try
            {
                retInt = Convert.ToInt32(value.Trim());
            }
            catch
            {
                throw new Exception(string.Format(
                    "读取节点{0}中的整形属性{1}时，值[{2}]格式不合法，请检查皮肤配置文件相关配置节！",
                    nodeName, attributeName,value));
            }
            return retInt;
        }
        #endregion

        #region public static string ReadString(string attributeName, XmlAttribute attribute)
        /// <summary>
        /// 读取字符串型样式配置节
        /// </summary>
        /// <param name="attributeName">要读取的配置节属性名</param>
        /// <param name="attribute">要读取的配置节属性对象</param>
        /// <returns>读取并转换好的字符串数据，若数据不存在则返回string.Empty</returns>
        public static string ReadString(string attributeName, XmlAttribute attribute)
        {
            string retString = string.Empty;

            if (attribute == null)
            {
                return string.Empty;
            }
            string value = attribute.Value.Trim();
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty; //配置节存在，但值为空的时候，返回string.Empty
            }
            return value;
        }
        #endregion

        #region public static bool ReadBoolean(string nodeName, string attributeName, XmlAttribute attribute)
        /// <summary>
        /// 读取布尔型样式配置节
        /// </summary>
        /// <param name="nodeName">读取的XmlAttribute所属XmlNode的name</param>
        /// <param name="attributeName">要读取的配置节属性名</param>
        /// <param name="attribute">要读取的配置节属性对象</param>
        /// <returns>读取并转换好的布尔型数据,若数据不存在则返回false</returns>
        public static bool ReadBoolean(string nodeName, string attributeName, XmlAttribute attribute)
        {
            bool retBoolean = false;

            if (attribute == null)
            {
                //不存在指定属性时返回false
                return false;
            }
            string value = attribute.Value.Trim();
            if (string.IsNullOrEmpty(value))
            {
                return false; //配置节存在，但值为空的时候，返回false
            }

            if (value.ToLower() == "false")
            {
                retBoolean = false;
            }
            else if (value.ToLower() == "true")
            {
                retBoolean = true;
            }
            else
            {
                throw new Exception(string.Format(
                    "读取节点{0}中的布尔型属性{1}时，值[{2}]格式不合法，请检查皮肤配置文件相关配置节！",
                    nodeName, attributeName,value));
            }

            return retBoolean;
        }
        #endregion

        #region public static BorderStyle ReadBorderStyle(string nodeName,string attributeName, XmlAttribute attribute)
        /// <summary>
        /// 读BorderStyle型样式配置节
        /// </summary>
        /// <param name="nodeName">读取的XmlAttribute所属XmlNode的name</param>
        /// <param name="attributeName">要读取的配置节属性名</param>
        /// <param name="attribute">要读取的配置节属性对象</param>
        /// <returns>读取并转换好的BorderStyle枚举值,若数据不存在则返回BorderStyle.None</returns>
        public static BorderStyle ReadBorderStyle(string nodeName, string attributeName, XmlAttribute attribute)
        {
            BorderStyle retBorderStyle = BorderStyle.None;

            if (attribute == null)
            {
                //不存在指定属性时返回BorderStyle.None
                return BorderStyle.None;
            }

            string value = attribute.Value.Trim();
            if (string.IsNullOrEmpty(value))
            {
                return BorderStyle.None; //配置节存在，但值为空的时候，返回BorderStyle.None
            }

            try
            {
                retBorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), value);
            }
            catch
            {

                throw new Exception(string.Format(
                     "读取节点{0}中的BorderStyle型属性{1}时，值[{2}]格式不合法，请检查皮肤配置文件相关配置节！",
                     nodeName, attributeName,value));
            }

            return retBorderStyle;
        }
        #endregion

        #region public static FlatStyle ReadFlatStyle(string nodeName, string attributeName, XmlAttribute attribute)
        /// <summary>
        /// 读FlatStyle型样式配置节
        /// </summary>
        /// <param name="nodeName">读取的XmlAttribute所属XmlNode的name</param>
        /// <param name="attributeName">要读取的配置节属性名</param>
        /// <param name="attribute">要读取的配置节属性对象</param>
        /// <returns>读取并转换好的FlatStyle枚举值,若数据不存在则返回FlatStyle.Flat</returns>
        public static FlatStyle ReadFlatStyle(string nodeName, string attributeName, XmlAttribute attribute)
        {
            FlatStyle retFlatStyle = FlatStyle.Flat;

            if (attribute == null)
            {
                //不存在指定属性时返回BorderStyle.None
                return FlatStyle.Flat;
            }

            string value = attribute.Value.Trim();
            if (string.IsNullOrEmpty(value))
            {
                return FlatStyle.Flat; //配置节存在，但值为空的时候，返回BorderStyle.None
            }

            try
            {
                retFlatStyle = (FlatStyle)Enum.Parse(typeof(FlatStyle), value);
            }
            catch
            {

                throw new Exception(string.Format(
                     "读取节点{0}中的FlatStyle型属性{1}时，值[{2}]格式不合法，请检查皮肤配置文件相关配置节！",
                     nodeName, attributeName,value));
            }

            return retFlatStyle;
        }
        #endregion

        #region public static T ReadEnumTypeConfig<T>(string nodeName, string attributeName, XmlAttribute attribute)
        /// <summary>
        /// 读FlatStyle型样式配置节
        /// </summary>
        /// <param name="nodeName">读取的XmlAttribute所属XmlNode的name</param>
        /// <param name="attributeName">要读取的配置节属性名</param>
        /// <param name="attribute">要读取的配置节属性对象</param>
        /// <returns>读取并转换好的FlatStyle枚举值,若数据不存在则返回FlatStyle.Flat</returns>
        public static T ReadEnumTypeConfig<T>(string nodeName, string attributeName, XmlAttribute attribute)
        {
            T retEnumValue = (T)typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public)[0].GetValue(null);

            if (attribute == null)
            {
                //不存在指定属性时返回BorderStyle.None
                return retEnumValue;
            }

            string value = attribute.Value.Trim();
            if (string.IsNullOrEmpty(value))
            {
                return retEnumValue; //配置节存在，但值为空的时候，返回BorderStyle.None
            }

            try
            {
                retEnumValue = (T)Enum.Parse(typeof(T), value);
            }
            catch
            {

                throw new Exception(string.Format(
                     "读取节点{0}中的{1}型属性{2}时，值[{3}]格式不合法，请检查皮肤配置文件相关配置节！",
                     nodeName,typeof(T).Name, attributeName, value));
            }

            return retEnumValue;
        }
        #endregion
    }
}
