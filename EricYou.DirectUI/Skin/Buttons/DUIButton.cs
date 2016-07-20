using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using EricYou.DirectUI.Utils;
using EricYou.DirectUI.Skin.Styles;

namespace EricYou.DirectUI.Skin.Buttons
{
    /// <summary>
    /// 皮肤背景按钮类
    /// </summary>
    public class DUIButton : ICloneable
    {
        /// <summary>
        /// 按钮唯一名称
        /// </summary>
        private string _name;
        /// <summary>
        /// 获取和设置按钮唯一名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 按钮左上角坐标点
        /// </summary>
        private Point _position;
        /// <summary>
        /// 获取和设置按钮左上角坐标点
        /// </summary>
        public Point Position
        {
            get { return _position; }
            set { _position = value; }
        }

        /// <summary>
        /// 按钮文字
        /// </summary>
        private string _text;

        /// <summary>
        /// 获取和设置按钮文字
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        /// <summary>
        /// 按钮文字对齐方式
        /// </summary>
        private ContentAlignment _textAlignment;

        /// <summary>
        /// 获取和设置按钮文字对齐方式
        /// </summary>
        public ContentAlignment TextAlignment
        {
            get { return _textAlignment; }
            set { _textAlignment = value; }
        }



        /// <summary>
        /// 按钮文字绘制时居中的横向偏移（文字默认按横向居中方式绘制）
        /// </summary>
        private int _textOffsetX;

        /// <summary>
        /// 获取和设置按钮文字绘制时居中的横向偏移（文字默认按横向居中方式绘制）
        /// </summary>
        public int TextOffsetX
        {
            get { return _textOffsetX; }
            set { _textOffsetX = value; }
        }

        /// <summary>
        /// 按钮文字绘制时居中的纵向偏移（文字默认按纵向居中方式绘制）
        /// </summary>
        private int _textOffsetY;

        /// <summary>
        /// 获取和设置按钮文字绘制时居中的纵向偏移（文字默认按纵向居中方式绘制）
        /// </summary>
        public int TextOffsetY
        {
            get { return _textOffsetY; }
            set { _textOffsetY = value; }
        }


        /// <summary>
        /// 按钮普通状态下的贴图资源名
        /// </summary>
        private string _normalSourceName;
        /// <summary>
        /// 获取和设置按钮普通状态下的贴图资源名
        /// </summary>
        public string NormalSourceName
        {
            get { return _normalSourceName; }
            set { _normalSourceName = value; }
        }

        /// <summary>
        /// 普通状态下按钮文字字体样式
        /// </summary>
        private string _normalFontName;

        /// <summary>
        /// 获取和设置普通状态下按钮文字字体样式
        /// </summary>
        public string NormalFontName
        {
            get { return _normalFontName; }
            set { _normalFontName = value; }
        }

        /// <summary>
        /// 按钮鼠标悬停状态下的贴图资源名
        /// </summary>
        private string _hoverSourceName;
        /// <summary>
        /// 获取和设置按钮鼠标悬停状态下的贴图资源名
        /// </summary>
        public string HoverSourceName
        {
            get { return _hoverSourceName; }
            set { _hoverSourceName = value; }
        }

        /// <summary>
        /// 鼠标滑过状态下按钮文字字体样式
        /// </summary>
        private string _hoverFontName;

        /// <summary>
        /// 获取和设置鼠标滑过状态下按钮文字字体样式
        /// </summary>
        public string HoverFontName
        {
            get { return _hoverFontName; }
            set { _hoverFontName = value; }
        }

        /// <summary>
        /// 按钮按下状态下的贴图资源名
        /// </summary>
        private string _downSourceName;
        /// <summary>
        /// 获取和设置按钮按下状态下的贴图资源名
        /// </summary>
        public string DownSourceName
        {
            get { return _downSourceName; }
            set { _downSourceName = value; }
        }

        /// <summary>
        /// 鼠标按下状态下按钮文字字体样式
        /// </summary>
        private string _downFontName;

        /// <summary>
        /// 获取和设置鼠标按下状态下按钮文字字体样式
        /// </summary>
        public string DownFontName
        {
            get { return _downFontName; }
            set { _downFontName = value; }
        }

        /// <summary>
        /// 按钮状态
        /// </summary>
        private ButtonStatus _status;
        /// <summary>
        /// 获取和设置按钮状态
        /// </summary>
        public ButtonStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary>
        /// 按钮是否保持按下状态
        /// </summary>
        private bool _holdDown =false;
        /// <summary>
        /// /// <summary>
        /// 获取和设置按钮是否保持按下状态的标识
        /// </summary>
        /// </summary>
        public bool HoldDown
        {
            get { return _holdDown; }
            set { _holdDown = value; }
        }
        /// <summary>
        /// 按钮是否可见
        /// </summary>
        private bool _visible;
        /// <summary>
        /// 获取和设置按钮是否可见的标识
        /// </summary>
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        
        /// <summary>
        /// 按钮所属按钮组
        /// </summary>
        private DUIButtonGroup _ownerGroup;
        /// <summary>
        /// 获取和设置按钮所属按钮组
        /// </summary>
        public DUIButtonGroup OwnerGroup
        {
            get { return _ownerGroup; }
            set { _ownerGroup = value; }
        }
        /// <summary>
        /// 按钮点击事件委托对象
        /// </summary>
        private ButtonClickEventHandler _buttonClick;


        /// <summary>
        /// 构造函数
        /// </summary>
        public DUIButton()
        {
 
        }

        /// <summary>
        /// 设置按钮点击处理函数
        /// </summary>
        /// <param name="handler">点击函数名</param>
        public void SetClickEventHandler(ButtonClickEventHandler handler)
        {
            _buttonClick = new ButtonClickEventHandler(handler);
        }

        /// <summary>
        /// 执行按钮的点击事件
        /// </summary>
        public void InvokeClickHandler()
        {
            if (_buttonClick != null)
            {
                _buttonClick(this);
            }
        }

        /// <summary>
        /// 测试点p是否在当前按钮的矩形范围内
        /// </summary>
        /// <param name="backBufferBitmap">当前双缓冲绘图缓存的背景图片</param>
        /// <param name="offsetPoint">偏移点，用于消除窗口最大化时窗口边框厚度对点计算造成的误差</param>
        /// <param name="p">待测试点</param>
        /// <returns></returns>
        public bool RectangleContains(Image backBufferBitmap, Point offsetPoint, Point p)
        {
            //此处计算按钮矩形范围以按钮普通状态的贴图图片大小为准，
            //理论上要求同一个按钮的各种状态的图片尺寸需一样，否则可能造成后续状态判断鼠标位置偏差
            Image buttonImage = DUISkinManager.GetCurrentSkinManager().GetImageSource(this._normalSourceName);
            Rectangle buttonRectangle = new Rectangle(
                                                CommonFunctions.GetImagePoint(backBufferBitmap, offsetPoint.X, offsetPoint.Y, this._position).X,
                                                CommonFunctions.GetImagePoint(backBufferBitmap, offsetPoint.X, offsetPoint.Y, this._position).Y,
                                                buttonImage.Width,
                                                buttonImage.Height);
            //拷贝一份，防止被修改
            Point tempPoint = new Point(p.X, p.Y);
            tempPoint.Offset(-offsetPoint.X, -offsetPoint.Y);
            return buttonRectangle.Contains(tempPoint);
        }

        /// <summary>
        /// 在双缓冲背景图上绘制当前按钮
        /// </summary>
        /// <param name="backBufferBitmap">当前双缓冲绘图缓存的背景图片</param>
        /// <param name="g">与背景缓冲图绑定的绘图对象</param>
        /// <param name="offsetPoint">偏移点，用于消除窗口最大化时窗口边框厚度对点计算造成的误差</param>
        public void Render(Image backBufferBitmap, Graphics g, Point offsetPoint)
        {
            Image buttonImage = null;
            DUIFont textDUIFont = null;
            Color textColor = Color.Empty;

            Rectangle buttonRectange;

            //HoldDown标志的优先级高于按钮状态，若HoldDown为true,则按钮绘制为按下状态
            if (this.HoldDown)
            {
                buttonImage = DUISkinManager.GetCurrentSkinManager().GetImageSource(this._downSourceName);
                textDUIFont = DUISkinManager.GetCurrentSkinManager().GetFont(this._downFontName);

            }
            //根据按钮状态采用不同的按钮贴图
            else
            {
                if (this.Status == ButtonStatus.Down)
                {
                    buttonImage = DUISkinManager.GetCurrentSkinManager().GetImageSource(this._downSourceName);
                    textDUIFont = DUISkinManager.GetCurrentSkinManager().GetFont(this._downFontName);
                }
                else if (this.Status == ButtonStatus.Hover)
                {
                    buttonImage = DUISkinManager.GetCurrentSkinManager().GetImageSource(this._hoverSourceName);
                    textDUIFont = DUISkinManager.GetCurrentSkinManager().GetFont(this._hoverFontName);
                }
                else if (this.Status == ButtonStatus.Normal)
                {
                    buttonImage = DUISkinManager.GetCurrentSkinManager().GetImageSource(this._normalSourceName);
                    textDUIFont = DUISkinManager.GetCurrentSkinManager().GetFont(this._normalFontName);
                }
                else
                {
                    ; 
                }
            }
            //计算按钮在背景缓冲图上的矩形范围
            buttonRectange = new Rectangle(
                                    CommonFunctions.GetImagePoint(backBufferBitmap, offsetPoint.X, offsetPoint.Y, this._position).X,
                                    CommonFunctions.GetImagePoint(backBufferBitmap, offsetPoint.X, offsetPoint.Y, this._position).Y,
                                    buttonImage.Width,
                                    buttonImage.Height);
            //在背景换冲图上绘制当前按钮
            g.DrawImage(buttonImage, buttonRectange, 0, 0, buttonImage.Width, buttonImage.Height, GraphicsUnit.Pixel);
            
            //-------------------------------------------------------------------------------------
            //绘制按钮文字
            if (!string.IsNullOrEmpty(this._text))
            {
                Font textFont = null;
                if (textDUIFont == null
                    || textDUIFont.Font == null)
                {
                    //配置中没有指定标题字体的，使用系统默认字体
                    textFont = SystemFonts.DefaultFont;
                }
                else
                {
                    textFont = textDUIFont.Font;
                }

                if (textDUIFont == null
                    || textDUIFont.ForeColor.Equals(Color.Empty))
                {
                    textColor = Color.Red; //红色突出字体颜色加载失败
                }
                else
                {
                    textColor = textDUIFont.ForeColor;
                }

                //------------------------------
                //计算按钮文字绘制位置
                Size textSize = g.MeasureString(this._text, textFont).ToSize(); //计算文本绘制后的长度
                int textInnerPositionX = 0; //按钮文字在按钮矩形内部的起始位置横坐标
                int textInnerPositionY = 0; //按钮文字在按钮矩形内部的起始位置横坐标

                //根据文字对齐方式计算文字在按钮矩形内部的起始位置
                switch (this._textAlignment)
                {
                    case ContentAlignment.TopLeft:
                        textInnerPositionX = 0;
                        textInnerPositionY = 0;
                        break;
                    case ContentAlignment.TopCenter:
                        textInnerPositionX = (buttonRectange.Width - textSize.Width) / 2;
                        textInnerPositionY = 0;
                        break;
                    case ContentAlignment.TopRight:
                        textInnerPositionX = buttonRectange.Width - textSize.Width;
                        textInnerPositionY = 0;
                        break;
                    case ContentAlignment.MiddleLeft:
                        textInnerPositionX = 0;
                        textInnerPositionY = (buttonRectange.Height - textSize.Height) / 2;
                        break;
                    case ContentAlignment.MiddleCenter:
                        textInnerPositionX = (buttonRectange.Width - textSize.Width) / 2;
                        textInnerPositionY = (buttonRectange.Height - textSize.Height) / 2;
                        break;
                    case ContentAlignment.MiddleRight:
                        textInnerPositionX = buttonRectange.Width - textSize.Width;
                        textInnerPositionY = (buttonRectange.Height - textSize.Height) / 2;
                        break;
                    case ContentAlignment.BottomLeft:
                        textInnerPositionX = 0;
                        textInnerPositionY = buttonRectange.Height - textSize.Height;
                        break;
                    case ContentAlignment.BottomCenter:
                        textInnerPositionX = (buttonRectange.Width - textSize.Width) / 2;
                        textInnerPositionY = buttonRectange.Height - textSize.Height;
                        break;
                    case ContentAlignment.BottomRight:
                        textInnerPositionX = buttonRectange.Width - textSize.Width;
                        textInnerPositionY = buttonRectange.Height - textSize.Height;
                        break;
                }

                //计算按钮文字在整个背景上的绘制起始位置，并绘制文字
                int textStartX = buttonRectange.X + textInnerPositionX + this._textOffsetX;
                int textStartY = buttonRectange.Y + textInnerPositionY + this._textOffsetY;
                using (Brush textBrush = new SolidBrush(textColor))
                {
                    g.DrawString(this._text, textFont, textBrush, textStartX, textStartY);
                }
            }
        }

        /// <summary>
        /// 克隆函数
        /// </summary>
        /// <returns>克隆后的对象</returns>
        public object Clone()
        {
            DUIButton newButton = new DUIButton();
            newButton.Name = this._name;
            newButton.Position = this._position;
            newButton.Text = this._text;
            newButton.TextAlignment = this._textAlignment;
            newButton.TextOffsetX = this._textOffsetX;
            newButton.TextOffsetY = this._textOffsetY;
            newButton.NormalSourceName = this._normalSourceName;
            newButton.NormalFontName = this._normalFontName;
            newButton.HoverSourceName = this._hoverSourceName;
            newButton.HoverFontName = this._hoverFontName;
            newButton.DownSourceName = this._downSourceName;
            newButton.DownFontName = this._downFontName;
            newButton.Status = this._status;
            newButton.HoldDown = this._holdDown;
            newButton.Visible = this._visible;
            newButton.OwnerGroup = this._ownerGroup;

            return newButton;
        }
    }

    /// <summary>
    /// 背景按钮点击委托类型
    /// </summary>
    public delegate void ButtonClickEventHandler(DUIButton sender);

    /// <summary>
    /// 背景按钮状态枚举
    /// </summary>
    public enum ButtonStatus
    {
        /// <summary>
        /// 普通状态
        /// </summary>
        Normal, 
        /// <summary>
        /// 鼠标悬停状态
        /// </summary>
        Hover, 
        /// <summary>
        /// 鼠标按下状态
        /// </summary>
        Down,
    }
}
