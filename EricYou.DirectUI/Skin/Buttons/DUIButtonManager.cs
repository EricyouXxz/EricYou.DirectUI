using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Drawing;
using EricYou.DirectUI.Utils;

namespace EricYou.DirectUI.Skin.Buttons
{
    /// <summary>
    /// 背景按钮管理器
    /// </summary>
    public class DUIButtonManager :ICloneable
    {
        /// <summary>
        /// 所属皮肤布局对象
        /// </summary>
        private DUILayout _ownerLayout;

        /// <summary>
        /// 获取和设置所属皮肤布局对象
        /// </summary>
        public DUILayout OwnerLayout
        {
            get { return _ownerLayout; }
            set { _ownerLayout = value; }
        }

        /// <summary>
        /// 按钮分组字典，用户按分组名访问按钮分组
        /// </summary>
        private IDictionary<string, DUIButtonGroup> _buttonGroupDict = new Dictionary<string, DUIButtonGroup>();

        /// <summary>
        /// 按钮分组列表，用户按配置文件出现顺序访问按钮分组
        /// </summary>
        private IList<DUIButtonGroup> _buttonGroupList = new List<DUIButtonGroup>();

        /// <summary>
        /// 供克隆函数调用的私有构造函数
        /// </summary>
        /// <param name="ownerLayout"></param>
        /// <param name="buttonGroupDict"></param>
        private DUIButtonManager(DUILayout ownerLayout, IDictionary<string, DUIButtonGroup> buttonGroupDict)
        {
            _ownerLayout = ownerLayout;
            if (buttonGroupDict != null && buttonGroupDict.Count > 0)
            {
                foreach (string groupName in buttonGroupDict.Keys)
                {
                    DUIButtonGroup newButtonGroup = (DUIButtonGroup)buttonGroupDict[groupName].Clone();
                    newButtonGroup.OwnerButtonManager = this;
                    this._buttonGroupDict.Add(groupName, newButtonGroup);
                    this._buttonGroupList.Add(newButtonGroup);
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ownerLayout"></param>
        public DUIButtonManager(DUILayout ownerLayout)
        {
            _ownerLayout = ownerLayout;
        }

        /// <summary>
        /// 从XML配置文件加载按钮信息，形成分组和按钮对象
        /// </summary>
        /// <param name="xmlDoc"></param>
        public void LoadButtonFromXml(XmlDocument xmlDoc)
        {
            if (xmlDoc == null)
            {
                throw new Exception("加载布局" + this._ownerLayout.Name + "中的背景按钮时，文档对象为空！");
            }
            XmlElement layoutElement = xmlDoc.DocumentElement;
            foreach (XmlNode childNode in layoutElement.ChildNodes)
            {
                if (childNode.Name == "buttons")
                {
                    foreach (XmlNode buttonNode in childNode.ChildNodes)
                    {
                        if (buttonNode.Name == "button")
                        {
                            if (buttonNode.Attributes["name"] != null
                                && buttonNode.Attributes["position"] != null
                                && buttonNode.Attributes["enable"] != null
                                && buttonNode.Attributes["normalSourceName"] != null
                                && buttonNode.Attributes["hoverSourceName"] != null
                                && buttonNode.Attributes["downSourceName"] != null)
                            {
                                //enble属性为false,或者该属性不存在或非法值时，不加载该按钮
                                string enableString = buttonNode.Attributes["enable"].Value;
                                if (string.IsNullOrEmpty(enableString) 
                                    || (enableString.ToLower() != "true" && enableString.ToLower() != "false"))
                                {
                                    continue;
                                }
                                bool enble = Convert.ToBoolean(enableString);
                                if (enble == false)
                                {
                                    continue;
                                }

                                //创建按钮对象
                                DUIButton newButton = new DUIButton();
                                newButton.Name = buttonNode.Attributes["name"].Value;
                                newButton.Position = ConvertStringToPoint(newButton.Name, buttonNode.Attributes["position"].Value);
                                newButton.NormalSourceName = buttonNode.Attributes["normalSourceName"].Value;
                                newButton.HoverSourceName = buttonNode.Attributes["hoverSourceName"].Value;
                                newButton.DownSourceName = buttonNode.Attributes["downSourceName"].Value;
                                newButton.Status = ButtonStatus.Normal;
                                newButton.HoldDown = false;
                                newButton.Visible = true;

                                //新加属性
                                newButton.Text = XMLConfigReader.ReadString("text", buttonNode.Attributes["text"]);
                                newButton.TextAlignment = XMLConfigReader.ReadEnumTypeConfig<ContentAlignment>(buttonNode.Name, "textAlignment", buttonNode.Attributes["textAlignment"]);
                                newButton.TextOffsetX = XMLConfigReader.ReadInt(buttonNode.Name, "textOffsetX", buttonNode.Attributes["textOffsetX"]);
                                newButton.TextOffsetY = XMLConfigReader.ReadInt(buttonNode.Name, "textOffsetY", buttonNode.Attributes["textOffsetY"]);
                                newButton.NormalFontName = XMLConfigReader.ReadString("normalFontName", buttonNode.Attributes["normalFontName"]);
                                newButton.HoverFontName = XMLConfigReader.ReadString("hoverFontName", buttonNode.Attributes["hoverFontName"]);
                                newButton.DownFontName = XMLConfigReader.ReadString("downFontName", buttonNode.Attributes["downFontName"]);

                                string buttonGroupName = buttonNode.Attributes["group"] == null ? string.Empty : buttonNode.Attributes["group"].Value;
                                if (buttonGroupName == string.Empty)
                                {
                                    buttonGroupName = DUIButtonGroup.NormalGroupName;
                                }
                                if (!this._buttonGroupDict.ContainsKey(buttonGroupName))
                                {
                                    DUIButtonGroup newButtonGroup = new DUIButtonGroup(buttonGroupName, this);
                                    this._buttonGroupDict.Add(buttonGroupName, newButtonGroup);
                                    this._buttonGroupList.Add(newButtonGroup);
                                }

                                DUIButtonGroup buttonGroup = this._buttonGroupDict[buttonGroupName];
                                newButton.OwnerGroup = buttonGroup;
                                buttonGroup.AddButton(newButton.Name, newButton);
                            }
                        }
                    }
                }
            }

        }

        /// <summary>
        /// 使用按钮分组名称获取按钮分组对象的索引器
        /// </summary>
        /// <param name="buttonName">按钮分组名</param>
        /// <returns>按钮分组对象（当找不到对应名称的按钮分组对象时，返回null)</returns>
        public DUIButtonGroup this[string buttonGroupName]
        {
            get
            {
                if(_buttonGroupDict.ContainsKey(buttonGroupName))
                {
                    return _buttonGroupDict[buttonGroupName];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 处理布局所有按钮的鼠标移过焦点事件
        /// </summary>
        /// <param name="backBufferBitmap">当前双缓冲绘图缓存的背景图片</param>
        /// <param name="offsetPoint">偏移点，用于消除窗口最大化时窗口边框厚度对点计算造成的误差</param>
        /// <param name="p">鼠标位置</param>
        public bool OnMouseMove(Image backBufferBitmap, Point offsetPoint, Point p)
        {
            bool HitButton = false;
            //绘制按钮组时，按照配置文件出现的顺序依次绘制，越晚出现的按钮组越在上层
            //当Button有重叠的时候，从最上层的Button开始遍历
            for (int i = this._buttonGroupList.Count - 1; i >= 0; i--)
            {
                DUIButtonGroup buttonGroup = this._buttonGroupList[i];
                
                ////只要任意按钮组的任意按钮命中移过焦点处理，则返回
                //if (buttonGroup.OnMouseMove(backBufferBitmap, offsetPoint, p) == true)
                //{
                //    return;
                //}
                HitButton = HitButton || buttonGroup.OnMouseMove(backBufferBitmap, offsetPoint, p);
            }
            return HitButton;
        }

        /// <summary>
        /// 处理布局所有按钮的鼠标按下事件
        /// </summary>
        /// <param name="backBufferBitmap">当前双缓冲绘图缓存的背景图片</param>
        /// <param name="offsetPoint">偏移点，用于消除窗口最大化时窗口边框厚度对点计算造成的误差</param>
        /// <param name="p">鼠标位置</param>
        public void OnMouseDown(Image backBufferBitmap, Point offsetPoint, Point p)
        {
            //绘制按钮组时，按照配置文件出现的顺序依次绘制，越晚出现的按钮组越在上层
            //当Button有重叠的时候，从最上层的Button开始遍历
            for (int i = this._buttonGroupList.Count - 1; i >= 0; i--)
            {
                DUIButtonGroup buttonGroup = this._buttonGroupList[i];
                
                ////只要任意按钮组的任意按钮命中按下事件，则返回
                //if (buttonGroup.OnMouseDown(backBufferBitmap, offsetPoint, p) == true)
                //{
                //    return;
                //}
                buttonGroup.OnMouseDown(backBufferBitmap, offsetPoint, p);
            }
        }

        /// <summary>
        /// 处理布局所有按钮的鼠标弹起事件
        /// </summary>
        /// <param name="backBufferBitmap">当前双缓冲绘图缓存的背景图片</param>
        /// <param name="offsetPoint">偏移点，用于消除窗口最大化时窗口边框厚度对点计算造成的误差</param>
        /// <param name="p">鼠标位置</param>
        public void OnMouseUp(Image backBufferBitmap, Point offsetPoint, Point p)
        {
            //绘制按钮组时，按照配置文件出现的顺序依次绘制，越晚出现的按钮组越在上层
            //当Button有重叠的时候，从最上层的Button开始遍历
            for (int i = this._buttonGroupList.Count - 1; i >= 0; i--)
            {
                DUIButtonGroup buttonGroup = this._buttonGroupList[i];
                buttonGroup.OnMouseUp(backBufferBitmap, offsetPoint, p);
            }
        }

        /// <summary>
        /// 绘制按钮
        /// </summary>
        /// <param name="backBufferBitmap">当前双缓冲绘图缓存的背景图片</param>
        /// <param name="g">与背景缓冲图绑定的绘图对象</param>
        /// <param name="offsetPoint">偏移点，用于消除窗口最大化时窗口边框厚度对点计算造成的误差</param>
        public void RenderButtons(Image backBufferBitmap, Graphics g, Point offsetPoint)
        {
            //绘制按钮时，按照配置文件出现的顺序依次绘制，越晚出现的按钮越在上层
            for (int i = 0; i < this._buttonGroupList.Count; i++)
            {
                DUIButtonGroup buttonGroup = this._buttonGroupList[i];
                buttonGroup.RenderButtonGroup(backBufferBitmap, g, offsetPoint);
            }
        }

        /// <summary>
        /// 将代表点坐标的字符串转换为点对象
        /// </summary>
        /// <param name="pointName">点名称</param>
        /// <param name="strPoint">点坐标字符串</param>
        /// <returns>点对象</returns>
        private Point ConvertStringToPoint(string buttonName, string strPoint)
        {
            string[] splitStrings = strPoint.Split(new char[] { ',' });
            if (splitStrings.Length != 2)
            {
                throw new Exception("加载布局" + this._ownerLayout.Name + "中的按钮" + buttonName + "时，position属性格式存在问题！应该为【int,int】!");
            }

            int x = Convert.ToInt32(splitStrings[0]);
            int y = Convert.ToInt32(splitStrings[1]);

            return new Point(x, y);
        }

        /// <summary>
        /// 克隆函数
        /// </summary>
        /// <returns>克隆后的</returns>
        public object Clone()
        {
            DUIButtonManager newButtonManager = new DUIButtonManager(this._ownerLayout, this._buttonGroupDict);
            return newButtonManager;
        }
    }
}
