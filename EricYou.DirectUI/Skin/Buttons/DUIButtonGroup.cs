using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace EricYou.DirectUI.Skin.Buttons
{
    /// <summary>
    /// 皮肤背景按钮组
    /// </summary>
    public class DUIButtonGroup : ICloneable
    {
        /// <summary>
        /// 按钮组名
        /// </summary>
        private string _name;
        /// <summary>
        /// 使用背景按钮唯一名作为键保存按钮对象的字典
        /// </summary>
        private IDictionary<string, DUIButton> _buttonDict = new Dictionary<string, DUIButton>();
        /// <summary>
        /// 按配置文件顺序存储按钮对象的列表，此列表用于按顺序处理按钮的情况
        /// </summary>
        private IList<DUIButton> _buttonList = new List<DUIButton>();
        /// <summary>
        /// 所属按钮管理器
        /// </summary>
        private DUIButtonManager _ownerButtonManager;
        /// <summary>
        /// 当按钮分组名为空时采用的预定义组名
        /// </summary>
        public static string NormalGroupName = "###NormalGroupName###";

        /// <summary>
        /// 获取和设置所属按钮管理器
        /// </summary>
        public DUIButtonManager OwnerButtonManager
        {
            get { return _ownerButtonManager; }
            set { _ownerButtonManager = value; }
        }

        /// <summary>
        /// 供克隆函数调用的私有构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ownerButtonManager"></param>
        /// <param name="buttonDict"></param>
        private DUIButtonGroup(string name, DUIButtonManager ownerButtonManager, 
            IDictionary<string, DUIButton> buttonDict)
        {
            this._name = name;
            this._ownerButtonManager = ownerButtonManager;
            if (buttonDict != null && buttonDict.Count > 0)
            {
                foreach (string buttonName in buttonDict.Keys)
                {
                    DUIButton newButton = (DUIButton)buttonDict[buttonName].Clone();
                    newButton.OwnerGroup = this;
                    _buttonDict.Add(buttonName, newButton);
                    _buttonList.Add(newButton);
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">按钮组名</param>
        /// <param name="ownerButtonManager">按钮管理器名称</param>
        public DUIButtonGroup(string name, DUIButtonManager ownerButtonManager)
        {
            _name = name;
            _ownerButtonManager = ownerButtonManager;
        }

        /// <summary>
        /// 向按钮组添加一个指定名称的按钮对象
        /// </summary>
        /// <param name="buttonName"></param>
        /// <param name="button"></param>
        public void AddButton(string buttonName, DUIButton button)
        {
            if(_buttonDict.ContainsKey(buttonName))
            {
                throw new Exception("布局" + this._ownerButtonManager.OwnerLayout.Name + "的按钮组" + _name + "中已经存在名为" + buttonName + "的按钮，不能重复加载，请检查局部配置文件！");
            }
            this._buttonDict.Add(buttonName, button);
            this._buttonList.Add(button);
        }

        /// <summary>
        /// 使用按钮名称获取按钮对象的索引器
        /// </summary>
        /// <param name="buttonName">按钮唯一名</param>
        /// <returns>按钮对象（当找不到对应名称的按钮对象时，返回null)</returns>
        public DUIButton this[string buttonName]
        {
            get
            {
                if (!string.IsNullOrEmpty(buttonName)
                    && _buttonDict != null
                    && _buttonDict.ContainsKey(buttonName))
                {
                    return _buttonDict[buttonName];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 将分组内所有按钮的状态置为指定状态
        /// </summary>
        /// <param name="status"></param>
        public void ResetAllButtonStatus(ButtonStatus status)
        {
            foreach (string buttonName in _buttonDict.Keys)
            {
                _buttonDict[buttonName].Status = status;
            }
        }

        /// <summary>
        /// 将分组内所有按钮的保持按下状态置为false
        /// </summary>
        public void ResetAllButtonHoldDownStatus()
        {
            foreach (string buttonName in _buttonDict.Keys)
            {
                _buttonDict[buttonName].HoldDown = false;
            }
        }

        /// <summary>
        /// 处理分组内所有按钮的鼠标移过焦点事件
        /// </summary>
        /// <param name="backBufferBitmap">当前双缓冲绘图缓存的背景图片</param>
        /// <param name="offsetPoint">偏移点，用于消除窗口最大化时窗口边框厚度对点计算造成的误差</param>
        /// <param name="p">鼠标位置</param>
        /// <returns>若本组内有按钮命中移过测试，且状态被改变，则返回true,否则返回false.</returns>
        public bool OnMouseMove(Image backBufferBitmap, Point offsetPoint, Point p)
        {
            ////当Button有重叠的时候，从最上层的Button开始遍历
            //for (int i = this._buttonList.Count-1; i >=0 ; i--)
            //{
            //    Button button = this._buttonList[i];
            //    if (button.Visible == true 
            //        && button.Status != ButtonStatus.Hover 
            //        && button.RectangleContains(backBufferBitmap, offsetPoint, p))
            //    {
            //        ResetAllButtonStatus(ButtonStatus.Normal);
            //        button.Status = ButtonStatus.Hover;

            //        //当有一个按钮获得鼠标焦点时，立即返回true,那么其他分组将不再做焦点获得处理
            //        //任何时候只有一个按钮能获得焦点
            //        return true;
            //    }
            //}
            //return false;
            bool HitButton = false; //标识是否有按钮命中

            for (int i = this._buttonList.Count - 1; i >= 0; i--)
            {
                DUIButton button = this._buttonList[i];
                if (button.Visible == true)
                {
                    if (button.RectangleContains(backBufferBitmap, offsetPoint, p))
                    {
                        if (button.Status != ButtonStatus.Down)
                        {
                            button.Status = ButtonStatus.Hover;
                            HitButton = true;
                        }
                    }
                    else
                    {
                        button.Status = ButtonStatus.Normal;
                    }
                }
            }
            return HitButton;
        }

        /// <summary>
        /// 处理分组内所有按钮的鼠标按下事件
        /// </summary>
        /// <param name="backBufferBitmap">当前双缓冲绘图缓存的背景图片</param>
        /// <param name="offsetPoint">偏移点，用于消除窗口最大化时窗口边框厚度对点计算造成的误差</param>
        /// <param name="p">鼠标位置</param>
        /// <returns>若本组内有按钮命中移过测试，且状态被改变，则返回true,否则返回false.</returns>
        public void OnMouseDown(Image backBufferBitmap, Point offsetPoint, Point p)
        {
            ////当Button有重叠的时候，从最上层的Button开始遍历
            //for (int i = this._buttonList.Count - 1; i >= 0; i--)
            //{
            //    Button button = this._buttonList[i];
            //    if (button.Visible == true 
            //        && button.Status != ButtonStatus.Down 
            //        && button.RectangleContains(backBufferBitmap, offsetPoint, p))
            //    {
            //        //ResetAllButtonStatus(ButtonStatus.Normal);
            //        button.Status = ButtonStatus.Down;

            //        //当有一个按钮获得鼠标按下焦点时，立即返回true,那么其他分组将不再做按下焦点处理
            //        //任何时候只有一个按钮能获得按下焦点
            //        return true;
            //    }
            //}
            //return false;

            for (int i = this._buttonList.Count - 1; i >= 0; i--)
            {
                DUIButton button = this._buttonList[i];
                if (button.Visible == true
                    && button.Status != ButtonStatus.Down
                    && button.RectangleContains(backBufferBitmap, offsetPoint, p))
                {
                    //ResetAllButtonStatus(ButtonStatus.Normal);
                    button.Status = ButtonStatus.Down;
                }
            }
        }

        /// <summary>
        /// 处理分组内所有按钮的鼠标弹起事件
        /// </summary>
        /// <param name="backBufferBitmap">当前双缓冲绘图缓存的背景图片</param>
        /// <param name="offsetPoint">偏移点，用于消除窗口最大化时窗口边框厚度对点计算造成的误差</param>
        /// <param name="p">鼠标位置</param>
        public void OnMouseUp(Image backBufferBitmap, Point offsetPoint, Point p)
        {
            //当Button有重叠的时候，从最上层的Button开始遍历
            for (int i = this._buttonList.Count - 1; i >= 0; i--)
            {
                DUIButton button = this._buttonList[i];
                if (button.Visible == true 
                    && button.Status == ButtonStatus.Down 
                    && button.RectangleContains(backBufferBitmap, offsetPoint, p))
                {
                    button.Status = ButtonStatus.Normal;
                    button.InvokeClickHandler();
                }
            }
        }

        /// <summary>
        /// 绘制按钮
        /// </summary>
        /// <param name="backBufferBitmap">当前双缓冲绘图缓存的背景图片</param>
        /// <param name="g">与背景缓冲图绑定的绘图对象</param>
        /// <param name="offsetPoint">偏移点，用于消除窗口最大化时窗口边框厚度对点计算造成的误差</param>
        public void RenderButtonGroup(Image backBufferBitmap, Graphics g,  Point offsetPoint)
        {
            //绘制按钮时，按照配置文件出现的顺序依次绘制，越晚出现的按钮越在上层
            for (int i = 0; i < this._buttonList.Count; i++)
            {
                DUIButton button = this._buttonList[i];
                if (button.Visible == true)
                {
                    button.Render(backBufferBitmap, g, offsetPoint);
                }
            }
        }

        /// <summary>
        /// 克隆函数
        /// </summary>
        /// <returns>克隆后的DUIButtonGroup对象</returns>
        public object Clone()
        {
            DUIButtonGroup newDUIButtonGroup
                = new DUIButtonGroup(this._name, this._ownerButtonManager, this._buttonDict);
            return newDUIButtonGroup;
        }
    }
}
