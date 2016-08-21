using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using EricYou.DirectUI.Skin.Styles;
using EricYou.DirectUI.Skin;

namespace EricYou.DirectUI.Forms.Extentions
{
    [DefaultEvent("Click")]
    public class DUIButtonEx : Control
    {
        #region private variables and public properties
        
        #region Status
        /// <summary>
        /// 按钮状态
        /// </summary>
        private ButtonStatus _status = ButtonStatus.Normal;
        /// <summary>
        /// 获取和设置按钮状态
        /// </summary>
        [DefaultValue(ButtonStatus.Normal)]
        [Description("按钮状态")]
        [Category("换肤框架")]
        public ButtonStatus Status
        {
            get { return _status; }
            set 
            {
                _status = value;
                this.Invalidate();
            }
        }
        #endregion

        #region TextAlignment
        /// <summary>
        /// 文字对齐方式
        /// </summary>
        private ContentAlignment _textAlignment = ContentAlignment.MiddleCenter;
        /// <summary>
        /// 获取和设置文字对齐方式
        /// </summary>
        [DefaultValue(ContentAlignment.MiddleCenter)]
        [Description("文字对齐方式")]
        [Category("换肤框架")]
        public ContentAlignment TextAlignment
        {
            get { return _textAlignment; }
            set
            { 
                _textAlignment = value;
                this.Invalidate();
            }
        }
        #endregion

        #region TextOffsetX
        /// <summary>
        /// 按钮文字绘制时起始坐标的横向偏移
        /// </summary>
        private int _textOffsetX = 0;
        /// <summary>
        /// 获取和设置按钮文字绘制时起始坐标的横向偏移
        /// </summary>
        [DefaultValue(0)]
        [Description("xxx")]
        [Category("换肤框架")]
        public int TextOffsetX
        {
            get { return _textOffsetX; }
            set 
            {
                _textOffsetX = value;
                this.Invalidate();
            }
        }
        #endregion

        #region TextOffsetY
        /// <summary>
        /// 按钮文字绘制时起始坐标的纵向偏移
        /// </summary>
        private int _textOffsetY = 0;
        /// <summary>
        /// 获取和设置按钮文字绘制时起始坐标的纵向偏移
        /// </summary>
        [DefaultValue(0)]
        [Description("按钮文字绘制时起始坐标的纵向偏移")]
        [Category("换肤框架")]
        public int TextOffsetY
        {
            get { return _textOffsetY; }
            set 
            { 
                _textOffsetY = value;
                this.Invalidate();
            }
        }
        #endregion

        #region BackImageSourceNameNormal
        /// <summary>
        /// 普通状态下背景图资源名
        /// </summary>
        private string _backImageSourceNameNormal = string.Empty;
        /// <summary>
        /// 获取和设置普通状态下背景图资源名
        /// </summary>
        [DefaultValue("")]
        [Description("普通状态下背景图资源名")]
        [Category("换肤框架")]
        public string BackImageSourceNameNormal
        {
            get { return _backImageSourceNameNormal; }
            set 
            {
                _backImageSourceNameNormal = value;
                this.Invalidate();
            }
        }
        #endregion

        #region BackImageSourceNameHover
        /// <summary>
        /// 鼠标滑过状态下背景图资源名
        /// </summary>
        private string _backImageSourceNameHover = string.Empty;
        /// <summary>
        /// 获取和设置鼠标滑过状态下背景图资源名
        /// </summary>
        [DefaultValue("")]
        [Description("鼠标滑过状态下背景图资源名")]
        [Category("换肤框架")]
        public string BackImageSourceNameHover
        {
            get { return _backImageSourceNameHover; }
            set 
            { 
                _backImageSourceNameHover = value;
                this.Invalidate();
            }
        }
        #endregion

        #region BackImageSourceNameDown
        /// <summary>
        /// 鼠标按下状态下背景图资源名
        /// </summary>
        private string _backImageSourceNameDown = string.Empty;
        /// <summary>
        /// 获取和设置鼠标按下状态下背景图资源名
        /// </summary>
        [DefaultValue("")]
        [Description("鼠标按下状态下背景图资源名")]
        [Category("换肤框架")]
        public string BackImageSourceNameDown
        {
            get { return _backImageSourceNameDown; }
            set 
            { 
                _backImageSourceNameDown = value;
                this.Invalidate();
            }
        }
        #endregion

        #region TextFontNameNormal
        /// <summary>
        /// 普通状态下按钮文本字体样式名
        /// </summary>
        private string _textFontNameNormal = string.Empty;
        /// <summary>
        /// 获取和设置普通状态下按钮文本字体样式名
        /// </summary>
        [DefaultValue("")]
        [Description("普通状态下按钮文本字体样式名")]
        [Category("换肤框架")]
        public string TextFontNameNormal
        {
            get { return _textFontNameNormal; }
            set 
            {
                _textFontNameNormal = value;
                this.Invalidate();
            }
        }
        #endregion

        #region TextFontNameHover
        /// <summary>
        /// 鼠标滑过状态下按钮文本字体样式名
        /// </summary>
        private string _textFontNameHover = string.Empty;
        /// <summary>
        /// 获取和设置鼠标滑过状态下按钮文本字体样式名
        /// </summary>
        [DefaultValue("")]
        [Description("鼠标滑过状态下按钮文本字体样式名")]
        [Category("换肤框架")]
        public string TextFontNameHover
        {
            get { return _textFontNameHover; }
            set
            { 
                _textFontNameHover = value;
                this.Invalidate();
            }
        }
        #endregion

        #region TextFontNameDown
        /// <summary>
        /// 鼠标按下状态下按钮文本字体样式名
        /// </summary>
        private string _textFontNameDown = string.Empty;
        /// <summary>
        /// 获取和设置鼠标按下状态下按钮文本字体样式名
        /// </summary>
        [DefaultValue("")]
        [Description("鼠标按下状态下按钮文本字体样式名")]
        [Category("换肤框架")]
        public string TextFontNameDown
        {
            get { return _textFontNameDown; }
            set 
            { 
                _textFontNameDown = value;
                this.Invalidate();
            }
        }
        #endregion

        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DUIButtonEx
            // 
            this.ResumeLayout(false);

        }

        public DUIButtonEx():base()
        {
            base.DoubleBuffered = true;
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void  OnMouseEnter(EventArgs e)
        {
 	         base.OnMouseEnter(e);

            this._status = ButtonStatus.Hover;
            this.Invalidate();
            //this.Update();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            this.Status = ButtonStatus.Normal;
            this.Invalidate();
            //this.Update();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            this._status = ButtonStatus.Down;
            this.Invalidate();
            //this.Update();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            //MessageBox.Show(string.Format("x={0} y={1}", e.X, e.Y));

            base.OnMouseUp(e);

            //鼠标抬起事件触发时，若鼠标已不在按钮的工作区范围内，则按钮状态修改为norm状态，否则为hover状态
            Rectangle buttonRect = new Rectangle(this.Location, this.Size);
            Rectangle buttonScreenRect = this.Parent.RectangleToScreen(buttonRect);
            if(buttonScreenRect.Contains(MousePosition))
            {
                this._status = ButtonStatus.Hover;
            }
            else
            {
                this._status = ButtonStatus.Normal;
            }
            this.Invalidate();
            //this.Update();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Image buttonImage = null;
            DUIFont textDUIFont = null;
            Color textColor = Color.Empty;

            Rectangle buttonRectange;


            if (this.Status == ButtonStatus.Down)
            {
                if (!string.IsNullOrEmpty(this._backImageSourceNameDown))
                {
                    buttonImage = DUISkinManager.GetCurrentSkinManager().GetImageSource(this._backImageSourceNameDown);
                }
                textDUIFont = DUISkinManager.GetCurrentSkinManager().GetFont(this._textFontNameDown);
            }
            else if (this.Status == ButtonStatus.Hover)
            {
                if (!string.IsNullOrEmpty(this._backImageSourceNameHover))
                {
                    buttonImage = DUISkinManager.GetCurrentSkinManager().GetImageSource(this._backImageSourceNameHover);
                }
                textDUIFont = DUISkinManager.GetCurrentSkinManager().GetFont(this._textFontNameHover);
            }
            else if (this.Status == ButtonStatus.Normal)
            {
                if (!string.IsNullOrEmpty(this._backImageSourceNameNormal))
                {
                    buttonImage = DUISkinManager.GetCurrentSkinManager().GetImageSource(this._backImageSourceNameNormal);
                }
                textDUIFont = DUISkinManager.GetCurrentSkinManager().GetFont(this._textFontNameNormal);
            }
            else
            {
                ;
            }

            //绘制背景颜色
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), this.ClientRectangle);
            
            //绘制按钮图片
            if (buttonImage != null)
            {
                e.Graphics.DrawImage(buttonImage,
                    new Rectangle((ClientRectangle.Width - buttonImage.Width) / 2,
                                   (ClientRectangle.Height - buttonImage.Height) / 2,
                                   buttonImage.Width, buttonImage.Height),
                    0, 0, buttonImage.Width, buttonImage.Height, GraphicsUnit.Pixel);
            }

            //-------------------------------------------------------------------------------------
            //绘制按钮文字
            if (!string.IsNullOrEmpty(this.Text))
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
                Size textSize = e.Graphics.MeasureString(this.Text, textFont).ToSize(); //计算文本绘制后的长度
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
                        textInnerPositionX = (this.ClientRectangle.Width - textSize.Width) / 2;
                        textInnerPositionY = 0;
                        break;
                    case ContentAlignment.TopRight:
                        textInnerPositionX = this.ClientRectangle.Width - textSize.Width;
                        textInnerPositionY = 0;
                        break;
                    case ContentAlignment.MiddleLeft:
                        textInnerPositionX = 0;
                        textInnerPositionY = (this.ClientRectangle.Height - textSize.Height) / 2;
                        break;
                    case ContentAlignment.MiddleCenter:
                        textInnerPositionX = (this.ClientRectangle.Width - textSize.Width) / 2;
                        textInnerPositionY = (this.ClientRectangle.Height - textSize.Height) / 2;
                        break;
                    case ContentAlignment.MiddleRight:
                        textInnerPositionX = this.ClientRectangle.Width - textSize.Width;
                        textInnerPositionY = (this.ClientRectangle.Height - textSize.Height) / 2;
                        break;
                    case ContentAlignment.BottomLeft:
                        textInnerPositionX = 0;
                        textInnerPositionY = this.ClientRectangle.Height - textSize.Height;
                        break;
                    case ContentAlignment.BottomCenter:
                        textInnerPositionX = (this.ClientRectangle.Width - textSize.Width) / 2;
                        textInnerPositionY = this.ClientRectangle.Height - textSize.Height;
                        break;
                    case ContentAlignment.BottomRight:
                        textInnerPositionX = this.ClientRectangle.Width - textSize.Width;
                        textInnerPositionY = this.ClientRectangle.Height - textSize.Height;
                        break;
                }

                //计算按钮文字在整个背景上的绘制起始位置，并绘制文字
                int textStartX = this.ClientRectangle.X + textInnerPositionX + this._textOffsetX;
                int textStartY = this.ClientRectangle.Y + textInnerPositionY + this._textOffsetY;
                using (Brush textBrush = new SolidBrush(textColor))
                {
                    e.Graphics.DrawString(this.Text, textFont, textBrush, textStartX, textStartY);
                }
            }

        }

    }

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
