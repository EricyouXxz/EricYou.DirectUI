using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using EricYou.DirectUI.Native;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using EricYou.DirectUI.Utils;
using System.ComponentModel;
using System.IO;
using System.Configuration;
using EricYou.DirectUI.Skin;
using System.Drawing.Design;
using EricYou.DirectUI.Skin.Designer;
using EricYou.DirectUI.Skin.Buttons;
using System.ComponentModel.Design;
using System.Reflection;
using EricYou.DirectUI.Skin.Styles;
using System.Runtime.InteropServices;

namespace EricYou.DirectUI.Forms
{
    /// <summary>
    /// DirectUI换肤窗口基类
    /// </summary>
    public class DUIForm : System.Windows.Forms.Form
    {
        #region private variable

        /// <summary>
        /// 窗口唯一标识
        /// </summary>
        Guid _uniqueID = Guid.NewGuid();

        /// <summary>
        /// 是否是激活状态
        /// </summary>
        private bool _bActived = true;

        /// <summary>
        /// 窗口边框宽度
        /// </summary>
        private int bWidth, bHeight;

        /// <summary>
        /// 背景图片缓冲区
        /// </summary>
        private Bitmap _backbuffer_bitmap;

        /// <summary>
        /// 背景图片字典缓存（字典键为大小信息“width*height”）
        /// </summary>
        private static IDictionary<string, Bitmap> _backgroundBitmapDict = new Dictionary<string, Bitmap>();

        /// <summary>
        /// 是否开启背景图片字典缓存的标识
        /// </summary>
        private static bool _enableBackgroundBitmapCache = true;

        /// <summary>
        /// 当前采用的主题布局名
        /// </summary>
        private string _layoutName = "main.layout"; 
        /// <summary>
        /// 确定窗口标题栏右上角是否有关闭按钮框
        /// </summary>
        private bool _closeBox = true;
        /// <summary>
        /// 确定窗口标题栏右上角是否有最大化按钮
        /// </summary>
        private bool _maximizeBox = true;
        /// <summary>
        /// 确定窗口标题栏右上角是否有最小化按钮
        /// </summary>
        private bool _minimizeBox = true;
        /// <summary>
        /// 调试信息字符串
        /// </summary>
        private string _debugMessage = "xxxx";
        /// <summary>
        /// 标识是否在窗口下方输出调试信息（鼠标位置等）
        /// </summary>
        private bool _showDebugMessage = false; 

        /// <summary>
        /// 系统按钮：恢复普通状态按钮
        /// </summary>
        private EricYou.DirectUI.Skin.Buttons.DUIButton restoreButton = null;
        /// <summary>
        /// 系统按钮：恢最大化按钮
        /// </summary>
        private EricYou.DirectUI.Skin.Buttons.DUIButton maxButton = null;
        /// <summary>
        /// 系统按钮：最小化按钮
        /// </summary>
        private EricYou.DirectUI.Skin.Buttons.DUIButton minButton = null;
        /// <summary>
        /// 系统按钮：关闭按钮
        /// </summary>
        private EricYou.DirectUI.Skin.Buttons.DUIButton closeButton = null;

        #endregion

        #region properties

        /// <summary>
        /// 获取窗口唯一标识
        /// </summary>
        public Guid UniqueID
        {
            get { return _uniqueID; }
        }

        /// <summary>
        /// 获取和设置窗口激活状态
        /// </summary>
        public virtual bool Actived
        {
            get { return _bActived; }
            set { _bActived = value; }
        }

        ///// <summary>
        ///// 获取和设置窗口标题区域高度（用于决定窗口图标和标题位置）
        ///// </summary>
        //[Description("窗口标题区域高度（用于决定窗口图标和标题纵向位置）")]
        //[DefaultValue(30)]
        //public int TitleAreaHeight
        //{
        //    get { return _titleAreaHeight; }
        //    set 
        //    {
        //        _titleAreaHeight = value;
        //        UpdateFormBackground();
        //    }
        //}

        ///// <summary>
        ///// 获取和设置窗口标题区域距离左边框距离（用于决定窗口图标和标题横向位置）
        ///// </summary>
        //[Description("窗口标题区域距离左边框距离（用于决定窗口图标和标题横向位置）")]
        //[DefaultValue(12)]
        //public int TitleAreaOffset
        //{
        //    get { return _titleAreaOffset; }
        //    set
        //    {
        //        _titleAreaOffset = value;
        //        UpdateFormBackground();
        //    }
        //}

        /// <summary>
        /// 获取和设置是否显示窗口图标的标识,定义此属性以屏蔽基类同名属性
        /// </summary>
        //[Browsable(false)]
        //public bool ShowIcon
        //{
        //    get { return true ; }
        //    set
        //    {
        //        ;
        //    }
        //}

        /// <summary>
        /// 窗口Icon对象,定义此属性以屏蔽基类同名属性
        /// </summary>
        //[Browsable(false)]
        //public Icon Icon
        //{
        //    get { return null; }
        //    set
        //    {
        //        ;
        //    }
        //}

        /// <summary>
        /// 获取和设置背景图片缓冲区
        /// </summary>
        //protected Bitmap _backbuffer_bitmap
        //{
        //    get
        //    {
        //        return this._backbuffer_bitmap;
        //    }
        //    set
        //    {
        //        if (this._backbuffer_bitmap != null)
        //        {
        //            this._backbuffer_bitmap.Dispose();
        //        }
        //        this._backbuffer_bitmap = value;
        //    }
        //}

        /// <summary>
        /// 获取和设置当前采用的主题布局名
        /// </summary>
        [DefaultValue("main.layout")]
        [Description("布局名，用于指定窗口使用的皮肤")]
        [Category("换肤框架")]
        [Editor(typeof(LayoutNameEditor), typeof(UITypeEditor))]
        public string LayoutName
        {
            get { return _layoutName; }
            set
            {
                _layoutName = value;
                UpdateFormBackground();
            }
        }

        /// <summary>
        /// 确定窗口标题栏右上角是否有关闭按钮框
        /// </summary>
        [Description("确定窗口标题栏右上角是否有关闭按钮框")]
        public bool CloseBox
        {
            get { return _closeBox; }
            set 
            { 
                _closeBox = value;
                if (this.closeButton != null)
                {
                    this.closeButton.Visible = _closeBox;
                    this.UpdateFormBackground();
                    this.Invalidate();
                    this.Update();
                }
            }
        }
        /// <summary>
        /// 确定窗口标题栏右上角是否有最大化按钮框
        /// </summary>
        [Description("确定窗口标题栏右上角是否有最大化按钮框")]
        public bool MaximizeBox
        {
            get { return _maximizeBox; }
            set
            {
                _maximizeBox = value;
                if (this.maxButton != null)
                {
                    this.maxButton.Visible = _maximizeBox;
                    this.UpdateFormBackground();
                    this.Invalidate();
                    this.Update();
                }
            }
        }
        /// <summary>
        /// 确定窗口标题栏右上角是否有最小化按钮框
        /// </summary>
        [Description("确定窗口标题栏右上角是否有最小化按钮框")]
        public bool MinimizeBox
        {
            get { return _minimizeBox; }
            set
            {
                _minimizeBox = value;
                if (this.minButton != null)
                {
                    this.minButton.Visible = _minimizeBox;
                    this.UpdateFormBackground();
                    this.Invalidate();
                    this.Update();
                }
            }
        }

        /// <summary>
        /// 获取和设置窗口区域
        /// </summary>
        protected Region WndRegion
        {
            get
            {
                return base.Region;
            }
            set
            {
                if (this.WndRegion != null)
                {
                    this.WndRegion.Dispose();
                }
                base.Region = value;
            }
        }

        //屏蔽Size显示，改用FormSize来设置窗口大小
        [Browsable(false)]
        public Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                base.Size = value;
            }
        }

        [Description("设置窗口大小")]
        [Category("换肤框架")]
        public Size FormSize
        {
            get
            {
                return base.Size;
            }
            set
            {
                int widthValue = value.Width;
                int heightValue = value.Height;

                //只有当要设置的窗口大小与缓存的正常状态窗口大小不同时，才执行尺寸调整
                if (widthValue != this.RestoreBounds.Width || heightValue != this.RestoreBounds.Height)
                {
                    //获取当前窗口大小需要调整的偏移量
                    Size offsetSize = CommonFunctions.GetWindowResizeOffset(this.FormBorderStyle);
                    widthValue += offsetSize.Width;   //调整宽度
                    heightValue += offsetSize.Height; //调整高度
                }
                this.Size = new Size(widthValue, heightValue);
            }
        }


        /// <summary>
        /// 获取当前窗口的布局对象
        /// </summary>
        protected DUILayout Layout
        {
            get
            {
                //获取当前窗口对应的布局对象拷贝，当前窗口第一次调用该Property时，执行拷贝操作
                //之后调用将取缓存中的对象。窗口关闭时在Closed函数中移除缓存的拷贝
                //换肤时将导致所有拷贝失效
                DUILayout formLayout
                    = DUISkinManager.GetCurrentSkinManager().GetLayout(this._uniqueID,this._layoutName);
                return formLayout;
            }
        }

        //获取和设置是否在窗口下方输出调试信息（鼠标位置等）的标识
        [DefaultValue(false)]
        [Description("标识是否在窗口下方输出调试信息（鼠标位置等）")]
        [Category("换肤框架")]
        public bool ShowDebugMessage
        {
            get { return _showDebugMessage; }
            set { _showDebugMessage = value; }
        }

        /// <summary>
        /// 获取和设置是否开启背景图片字典缓存的标识
        /// </summary>
        [Browsable(false)]
        public static bool EnableBackgroundBitmapCache
        {
            get { return DUIForm._enableBackgroundBitmapCache; }
            set { DUIForm._enableBackgroundBitmapCache = value; }
        }

        //该设置被注释，原因是导致某些DataGridView控件闪烁2013-01-14
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        //设置窗口的WS_EX_COMPOSITED样式，使窗口背景刷新时控件不闪烁，但该标志会影响窗口滑动切换的动画效果
        //        //因此在MainContentHolder.SwitchToForm函数中，窗口动画开始前会禁用该标志，动画结束后重新打开该标志
        //        cp.ExStyle |= (int)NativeConst.WS_EX_COMPOSITED;  // Turn on WS_EX_COMPOSITED
        //        return cp;
        //    }
        //} 


        #region 废弃代码
        /* protected Rectangle ContentRectangle
        {
            get { return new Rectangle(0, 0, Width - bWidth * 2, Height - bHeight * 2); }
        }
        protected virtual Rectangle IconRectangle
        {
            get { return new Rectangle(5, 5, 16, 16); }
        }
        protected virtual Rectangle CloseButtonRectangle
        {
            get { return new Rectangle(ContentRectangle.Width - 36, 1, 31, 20); }
        }

        protected virtual Rectangle MinButtonRectangle
        {
            get { return new Rectangle(ContentRectangle.Width - 98, 1, 31, 20); }
        }

        protected virtual Rectangle MaxButtonRectangle
        {
            get { return new Rectangle(ContentRectangle.Width - 67, 1, 31, 20); }
        }
        */
        #endregion

        #endregion

        #region constructor
        public DUIForm()
        {
            base.DoubleBuffered = true;
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.UserPaint, true);

            //设计时模式下不会调用Program类中的初始函数，所以在此调用
            if (CommonFunctions.IsInDesignMode())
            {
                string appBaseDirectory = CommonFunctions.GetAppBaseDirectory();
                //string skinName = ConfigurationManager.AppSettings["SkinName"];
                //if (string.IsNullOrEmpty(skinName))
                //{
                //    skinName = "Red";
                //}
                string skinName = CommonFunctions.GetDesignModeSkinName();
                //MessageBox.Show(skinName);
               
                DUISkinInfo skinInfo = new DUISkinInfo(appBaseDirectory, skinName);
                DUISkinManager.CreateSkinManager(skinInfo,true);
            }

            //将DUIForm窗口实例附加到皮肤管理器对象
            DUISkinManager.AttachToDUIFrom(this);
        }
        #endregion

        #region private void InitializeComponent()
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DUIForm
            // 
            this.ClientSize = new System.Drawing.Size(627, 231);
            this.Name = "DUIForm";
            this.ResumeLayout(false);

        }
        #endregion

        #region protected override void OnShown(EventArgs e)
        /// <summary>
        /// 窗口初始化完成，在显示前调用
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            //Show窗口时
            //this.SuspendLayout();
            OnSkinChanged(DUISkinManager.GetCurrentSkinManager());
            //this.ResumeLayout(false);
            //this.PerformLayout();

            ////处理系统按钮
            //ProcessSystemButtons();
            ////处理用户自定义按钮
            //ProcessUserButtons();
            ////递归处理窗口DUI控件的样式
            //DUISkinManager.ProcessControlDUIStyle(this);
        }
        #endregion

        #region private void ProcessSystemButtons()
        /// <summary>
        /// 处理系统按钮的对象获取、事件绑定、显示和隐藏
        /// </summary>
        private void ProcessSystemButtons()
        {
            //获取背景按钮对象
            if (this.Layout.ButtonManager[DUIButtonGroup.NormalGroupName] != null)
            {
                restoreButton = this.Layout.ButtonManager[DUIButtonGroup.NormalGroupName]["btRestore"];
                maxButton = this.Layout.ButtonManager[DUIButtonGroup.NormalGroupName]["btMax"];
                minButton = this.Layout.ButtonManager[DUIButtonGroup.NormalGroupName]["btMin"];
                closeButton = this.Layout.ButtonManager[DUIButtonGroup.NormalGroupName]["btClose"];
            }

            //绑定默认处理方法
            if (restoreButton != null)
            {
                //用替换方式而不用增量方式，防止同一个方法被挂载多次
                restoreButton.SetClickEventHandler(OnRestoreButtonClick);
            }
            if (maxButton != null)
            {
                maxButton.SetClickEventHandler(OnMaxButtonClick);
            }
            if (minButton != null)
            {
                minButton.SetClickEventHandler(OnMiniButtonClick);
            }
            if (closeButton != null)
            {
                closeButton.SetClickEventHandler(OnCloseButtonClick);
            }
            //-----------------------------------------
            if (this.MinimizeBox)
            {
                if (minButton != null)
                {
                    minButton.Visible = true;
                }
            }
            else
            {
                if (minButton != null)
                {
                    minButton.Visible = false;
                }
            }
            //-----------------------------------------
            if (this.CloseBox)
            {
                if (closeButton != null)
                {
                    closeButton.Visible = true;
                }
            }
            else
            {
                if (closeButton != null)
                {
                    closeButton.Visible = false;
                }
            }
            //-----------------------------------------
            if (MaximizeBox)
            {

                if (this.WindowState == FormWindowState.Normal)
                {
                    if (restoreButton != null)
                    {
                        restoreButton.Visible = false;
                    }
                    if (maxButton != null)
                    {
                        maxButton.Visible = true;
                    }
                }
                else
                {
                    if (restoreButton != null)
                    {
                        restoreButton.Visible = true;
                    }
                    if (maxButton != null)
                    {
                        maxButton.Visible = false;
                    }
                }
            }
        }
        #endregion

        #region protected virtual void ProcessUserButtons()
        /// <summary>
        /// 处理用户按钮的对象获取、事件绑定等
        /// </summary>
        protected virtual void ProcessUserButtons()
        {
            ;
        }
        #endregion

        #region protected override void OnActivated(EventArgs e)
        protected override void OnActivated(EventArgs e)
        {
            this._bActived = true;
            base.Invalidate();
            base.OnActivated(e);
        }
        #endregion

        #region protected override void OnDeactivate(EventArgs e)
        protected override void OnDeactivate(EventArgs e)
        {
            this._bActived = false;
            base.Invalidate();
            base.OnDeactivate(e);
        }
        #endregion

        #region protected override void OnPaint(PaintEventArgs e)
        /// <summary>
        /// 界面绘制处理方法
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.TranslateTransform(bWidth, bHeight);

            if (_showWindowState == WindowShadeState.Showing
                || _showWindowState == WindowShadeState.Closing)
            {
                if (this._bitmapWindowShadeLight != null)
                {
                    g.DrawImage(this._bitmapWindowShadeLight, 0, 0);
                }
                else
                {
                    int i = 0;
                }
            }
            else if(_showWindowState == WindowShadeState.Shown)
            {
                if (this._bitmapWindowShade != null)
                {
                    g.DrawImage(this._bitmapWindowShade, 0, 0);
                }
                else
                {
                    int i = 0;
                }
            }
            else
            {
                //拷贝当前背景，在副本上绘制按钮
                using (Bitmap backgroundCopy = this._backbuffer_bitmap.Clone() as Bitmap)//new Bitmap(this._backbuffer_bitmap..Width, this._backbuffer_bitmap.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                {
                    this.Layout.RenderBackgroudButtons(backgroundCopy, new Point(bWidth, bHeight));
                    g.DrawImage(backgroundCopy, 0, 0);
                    //CommonFunctions.BitBltDrawImage(g, 0, 0, backgroundCopy.Width, backgroundCopy.Height, backgroundCopy, 0, 0);
                }
            }

            if (_showDebugMessage)
            {
                g.DrawString(_debugMessage, SystemFonts.DefaultFont, Brushes.Red, 0, this.Height - 20 - 2 * bHeight);
            }
        }
        #endregion

        #region protected void ChangeSkinTo(DUISkinInfo skinInfo)
        /// <summary>
        /// 皮肤切换方法，将引发当前打开的所有窗口的皮肤切换
        /// </summary>
        /// <param name="skinInfo">皮肤信息对象</param>
        protected void ChangeSkinTo(DUISkinInfo skinInfo)
        {
            DUISkinManager skinManager = DUISkinManager.CreateSkinManager(skinInfo,false);

            //this.UpdateFormBackground();

            //this.ProcessSystemButtons();
            //this.ProcessUserButtons();

            //this.SuspendLayout();
            ///this.OnSkinChanged(skinManager);
            //this.ResumeLayout(false);
            //this.PerformLayout();
            //this.Update();
        }
        #endregion

        #region public virtual void OnSkinChanged(DUISkinManager skinManager)
        /// <summary>
        /// 皮肤切换事件，皮肤切换后引发
        /// </summary>
        /// <param name="skinManager">皮肤管理器对象</param>
        public virtual void OnSkinChanged(DUISkinManager skinManager)
        {
            this.SuspendLayout();

            //将当前窗口所有直接子控件置为无效，引发重绘。
            foreach (Control c in this.Controls)
            {
                c.Invalidate();
            }
            
            this.UpdateFormBackground();
            this.ProcessSystemButtons();
            this.ProcessUserButtons();
            DUISkinManager.ProcessControlDUIStyle(this);
            
            this.ResumeLayout(true);
            //this.PerformLayout(); //以上一句的参数为true时，内部已经执行了 this.PerformLayout()
            //this.Update();
        }
        #endregion

        #region 废弃代码
        //private void DrawCloseButton(Graphics g)
        //{
        //    Image close;
        //    switch (closeStatus)
        //    {
        //        case ButtonStatus.Hover:
        //            close = Resources.IMCloseButton_Hover;
        //            break;
        //        case ButtonStatus.Down:
        //            close = Resources.IMCloseButton_Down;
        //            break;
        //        default:
        //            close = Resources.IMCloseButton_Normal;
        //            break;
        //    }
        //    g.DrawImage(close, CloseButtonRectangle, new Rectangle(0, 0, 31, 20), GraphicsUnit.Pixel);
        //}

        //private void DrawMinButton(Graphics g)
        //{
        //    Image min;
        //    switch (MinStatus)
        //    {
        //        case ButtonStatus.Hover:
        //            min = Resources.IMMinButton_Hover;
        //            break;
        //        case ButtonStatus.Down:
        //            min = Resources.IMMinButton_Down;
        //            break;
        //        default:
        //            min = Resources.IMMinButton_Normal;
        //            break;
        //    }

        //    g.DrawImage(min, MinButtonRectangle, new Rectangle(0, 0, 31, 20), GraphicsUnit.Pixel);
        //}

        //private void DrawMaxButton(Graphics g)
        //{
        //    Image max;
        //    switch (MaxStatus)
        //    {
        //        case ButtonStatus.Hover:
        //            max = WindowState == FormWindowState.Maximized ? Resources.IMRestoreButton_Hover : Resources.IMMaxButton_Hover;
        //            break;
        //        case ButtonStatus.Down:
        //            max = WindowState == FormWindowState.Maximized ? Resources.IMRestoreButton_Down : Resources.IMMaxButton_Down;
        //            break;
        //        default:
        //            max = WindowState == FormWindowState.Maximized ? Resources.IMRestoreButton_Normal : Resources.IMMaxButton_Normal;
        //            break;
        //    }
        //    g.DrawImage(max, MaxButtonRectangle, new Rectangle(0, 0, 31, 20), GraphicsUnit.Pixel);
        //}
        #endregion 

        #region protected virtual void DrawBackGround(Graphics g)
        /// <summary>
        /// 在指定绘图图片绘制皮肤背景
        /// </summary>
        /// <param name="g"></param>
        protected virtual void DrawBackGround(Graphics g)
        {
            //g.Clear(Color.FromArgb(170, 217, 251));
            g.Clear(Color.Gray);
            //DrawBorder(g);此处注释，改为调用layout中的方法
            this.Layout.RenderBackground(g, this._backbuffer_bitmap, this, new Point(bWidth, bHeight));

            //------------------------------------------------------------
            //绘制窗口图标和标题
            //图标或者title距离左边的距离
            DUILayout layout = this.Layout;
            if (this.Layout.TitleInfo != null)
            {
                int titleTextStartX = layout.TitleInfo.TitleAreaOffset;
                //只有指定了图标才绘制
                if (layout.TitleInfo.ShowIcon && !string.IsNullOrEmpty(layout.TitleInfo.IconSourceName))
                {
                    Image iconImage = DUISkinManager.GetCurrentSkinManager().GetImageSource(layout.TitleInfo.IconSourceName);
                    if (iconImage != null)
                    {
                        Rectangle iconRectangle = new Rectangle(titleTextStartX, 
                            (layout.TitleInfo.TitleAreaHeight - iconImage.Height) / 2, iconImage.Width, iconImage.Height);
                        g.DrawImage(iconImage, iconRectangle);

                        titleTextStartX = titleTextStartX + iconImage.Width + 2;
                    }
                }

                //配置中没有指定Title文本的，用窗口对象自己的Text文本
                string titleText = layout.TitleInfo.Text;
                if(string.IsNullOrEmpty(titleText))
                {
                    titleText = this.Text;
                }

                Font titleFont = null;
                if (layout.TitleInfo.DUIFont == null
                    || layout.TitleInfo.DUIFont.Font == null)
                {
                    //配置中没有指定标题字体的，使用系统默认字体
                    titleFont = SystemFonts.CaptionFont;
                }
                else
                {
                    titleFont = layout.TitleInfo.DUIFont.Font;
                }

                Color titleColor = Color.Empty;
                if (layout.TitleInfo.DUIFont == null
                    || layout.TitleInfo.DUIFont.ForeColor.Equals(Color.Empty))
                {
                    titleColor = Color.DarkSlateBlue;
                }
                else
                {
                    titleColor = layout.TitleInfo.DUIFont.ForeColor;
                }

                int titleHeight = g.MeasureString(titleText, titleFont).ToSize().Height;
                using (Brush titleBrush = new SolidBrush(titleColor))
                {
                    g.DrawString(titleText, titleFont, titleBrush, titleTextStartX,
                        (layout.TitleInfo.TitleAreaHeight - titleHeight) / 2);
                }
            }
            //------------------------------------------------------------
        }
        #endregion

        #region 废弃代码
        /// <summary>
        /// 该函数废弃不用了，重构移动到Layout中
        /// </summary>
        /// <param name="g"></param>
        protected virtual void DrawBorder(Graphics g)
        {
            //获得当前窗口采用的布局对象
            DUILayout formLayout = this.Layout;

            TextureBrush brush;
            DUIBackgroundInfo backgroundInfo;
            Image backgroundImage;

            if (WindowState == FormWindowState.Maximized)
            {
                //获得布局最大化状态下背景信息
                backgroundInfo = formLayout.MaximizedbackgroundInfo;
                //获得最大化状态下的背景图
                backgroundImage = DUISkinManager.GetCurrentSkinManager().GetImageSource(formLayout.MaximizedbackgroundInfo.SourceName);
            }
            else
            {
                //获得布局普通状态下背景信息
                backgroundInfo = formLayout.NormalBackgroundInfo;
                //获得非最大化状态下的背景图
                backgroundImage = DUISkinManager.GetCurrentSkinManager().GetImageSource(formLayout.NormalBackgroundInfo.SourceName);
            }
            //--------------------------------------------------------
            //绘制顶部中间段边框
            //brush = new TextureBrush(Resources.IMBorderTop2, WrapMode.Tile, new Rectangle(10, 0, 1, 24));
            brush = new TextureBrush(
                backgroundImage,
                WrapMode.Tile,
                new Rectangle(
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopLeftPoint).X,
                    0,
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopRightPoint).X - CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopLeftPoint).X,//+99,
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopLeftPoint).Y));

            //g.FillRectangle(brush, new Rectangle(0, 0, Width, 24));
            //利用TextureBrush时的图像偏移问题http://www.cnblogs.com/lx1988729/articles/2605477.html
            brush.TranslateTransform( CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).X, 0);
            g.FillRectangle(
                brush,
                new Rectangle(
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).X,
                    0,
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).X - CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).X,
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).Y));
            brush.Dispose();
            //---------------------------------------------------------
            //绘制左部中间段边框
            //brush = new TextureBrush(Resources.IMBorderLeft);
            //g.FillRectangle(brush, new Rectangle(0, 0, 3, Height));
            brush = new TextureBrush(
                backgroundImage,
                WrapMode.Tile,
                new Rectangle(
                    0,
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopLeftPoint).Y,
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopLeftPoint).X,
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.BottomLeftPoint).Y - CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopLeftPoint).Y));
            brush.TranslateTransform(0, CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).Y);
            g.FillRectangle(
                brush,
                new Rectangle(
                    0,
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).Y,
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).X,
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.BottomLeftPoint).Y - CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).Y));
            brush.Dispose();


            //---------------------------------------------------------
            //绘制右部中间段边框
            //brush = new TextureBrush(Resources.IMBorderRight);
            //brush.TranslateTransform(base.Width - bWidth * 2, 0);
            //g.FillRectangle(brush, new Rectangle(base.Width - bWidth * 2 - 3, 0, 3, Height));
            brush = new TextureBrush(
                backgroundImage,
                WrapMode.Tile,
                new Rectangle(
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopRightPoint).X,
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopRightPoint).Y,
                    backgroundImage.Width - CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopRightPoint).X,
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.BottomRightPoint).Y - CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopRightPoint).Y));
            brush.TranslateTransform( CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).X - 2 * bWidth, 
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).Y);
            g.FillRectangle(
                brush,
                new Rectangle(
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).X - 2 * bWidth, //????原始代码中减了2bWidth,因为_backbuffer_bitmap的宽度已经减了bWidth,故此处只减一个bWidth
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).Y,
                    this._backbuffer_bitmap.Width + bWidth - CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).X,
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.BottomRightPoint).Y - CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).Y));
            brush.Dispose();

            //---------------------------------------------------------
            //绘制下部中间段边框
            //brush = new TextureBrush(Resources.IMBorderBottom, new Rectangle(10, 0, 1, 8));
            //brush.TranslateTransform(0, base.Height - bHeight * 2);
            //g.FillRectangle(brush, new Rectangle(0, base.Height - bHeight * 2 - 3, Width, 3));
            brush = new TextureBrush(
                backgroundImage,
                WrapMode.Tile,
                new Rectangle(
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.BottomLeftPoint).X,
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.BottomLeftPoint).Y,
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.BottomRightPoint).X - CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.BottomLeftPoint).X,
                    backgroundImage.Height - CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.BottomLeftPoint).Y));
            brush.TranslateTransform(CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.BottomLeftPoint).X,
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.BottomLeftPoint).Y - 2 * bHeight);
            g.FillRectangle(
                brush,
                new Rectangle(
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.BottomLeftPoint).X,
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.BottomLeftPoint).Y - 2 * bHeight,//????原始代码中减了2bHeight,因为_backbuffer_bitmap的高度已经减了bHeight,故此处只减一个bHeight
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.BottomRightPoint).X - CommonFunctions.GetImagePoint(backgroundImage, bWidth, bHeight, backgroundInfo.BottomLeftPoint).X,
                    this._backbuffer_bitmap.Height + bHeight - CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.BottomLeftPoint).Y));
            brush.Dispose();

            //---------------------------------------------------------
            //绘制四个角
            //if (WindowState == FormWindowState.Maximized)
            //{
            //    g.DrawImage(Resources.IMBorderLeftTop2, 0, 0, 3, 24);
            //    g.DrawImage(Resources.IMBorderRightTop2, base.Width - bWidth * 2 - 3, 0, 3, 24);
            //    g.DrawImage(Resources.IMBorderLeftBottom2, 0, base.Height - bHeight * 2 - 3, 3, 3);
            //    g.DrawImage(Resources.IMBorderRightBottom2, base.Width - bWidth * 2 - 3, base.Height - bHeight * 2 - 3, 3, 3);
            //}
            //else
            //{
            //    g.DrawImage(Resources.IMBorderTop, new Rectangle(0, 0, 25, 24), new Rectangle(0, 0, 25, 24), GraphicsUnit.Pixel);
            //    g.DrawImage(Resources.IMBorderTop, new Rectangle(base.Width - bWidth * 2 - 25, 0, 25, 24), new Rectangle(25, 0, 25, 24), GraphicsUnit.Pixel);
            //    g.DrawImage(Resources.IMBorderBottom, new Rectangle(0, base.Height - bHeight * 2 - 8, 10, 8), new Rectangle(0, 0, 10, 8), GraphicsUnit.Pixel);
            //    g.DrawImage(Resources.IMBorderBottom, new Rectangle(base.Width - bWidth * 2 - 10, base.Height - bHeight * 2 - 8, 10, 8), new Rectangle(40, 0, 10, 8), GraphicsUnit.Pixel);
            //}
            //---------------------------------------------------------
            //绘制左上角
            g.DrawImage(
                backgroundImage,
                new Rectangle(
                    0,
                    0,
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).X,
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).Y),
                    0,
                    0,
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopLeftPoint).X,
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopLeftPoint).Y,
                    GraphicsUnit.Pixel);
            //---------------------------------------------------------
            //绘制右上角
            g.DrawImage(
                backgroundImage,
                new Rectangle(
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).X,
                    0,
                    this._backbuffer_bitmap.Width + bWidth - CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).X,
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).Y),
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopRightPoint).X,
                    0,
                    backgroundImage.Width - CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopRightPoint).X,
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopRightPoint).Y,
                    GraphicsUnit.Pixel);
            //---------------------------------------------------------
            //绘制左下角
            g.DrawImage(
                backgroundImage,
                new Rectangle(
                    0,
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.BottomLeftPoint).Y,
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.BottomLeftPoint).X,
                    this._backbuffer_bitmap.Height + bHeight - CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.BottomLeftPoint).Y),
                    0,
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.BottomLeftPoint).Y,
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.BottomLeftPoint).X,
                    backgroundImage.Height - CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.BottomLeftPoint).Y,
                    GraphicsUnit.Pixel);
            //---------------------------------------------------------
            //绘制右下角
            g.DrawImage(
                backgroundImage,
                new Rectangle(
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).X,
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.BottomLeftPoint).Y,
                    this._backbuffer_bitmap.Width + bWidth - CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.BottomRightPoint).X,
                    this._backbuffer_bitmap.Height + bHeight - CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.BottomRightPoint).Y),
                    
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.BottomRightPoint).X,
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.BottomRightPoint).Y,
                    backgroundImage.Width - CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.BottomRightPoint).X,
                    backgroundImage.Height - CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.BottomRightPoint).Y,
                    GraphicsUnit.Pixel);

            //---------------------------------------------------------
            //绘制中间内容区
            //g.DrawImage(
            //    backgroundImage,
            //    new Rectangle(
            //        CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).X ,
            //        CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).Y ,
            //        CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).X
            //            - CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).X,
            //        CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.BottomRightPoint).Y
            //            - CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).Y),

            //        CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopLeftPoint).X,
            //        CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopLeftPoint).Y,
            //        CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopRightPoint).X
            //            - CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopLeftPoint).X,
            //        CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.BottomRightPoint).Y
            //            - CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopRightPoint).Y,
            //        GraphicsUnit.Pixel);
            brush = new TextureBrush(
                backgroundImage,
                WrapMode.Tile,
                new Rectangle(
                     CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopLeftPoint).X,
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopLeftPoint).Y,
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopRightPoint).X
                        - CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopLeftPoint).X,
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.BottomRightPoint).Y
                        - CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopRightPoint).Y));
            brush.TranslateTransform(CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).X,
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).Y);
            g.FillRectangle(
                brush,
               new Rectangle(
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).X ,
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).Y ,
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).X
                        - CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).X,
                    CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.BottomRightPoint).Y
                        - CommonFunctions.GetImagePoint(this._backbuffer_bitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).Y));
            brush.Dispose();

            //int height = g.MeasureString(Text, SystemFonts.CaptionFont).ToSize().Height;
            //g.DrawString(Text, SystemFonts.CaptionFont, Brushes.DarkSlateBlue, 22, (24 - height) / 2);
        }
        #endregion 

        #region protected override void OnSizeChanged(EventArgs e)
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            UpdateFormBackground();
        }
        #endregion

        #region protected void UpdateFormBackground()
        protected void UpdateFormBackground()
        {
            if (WindowState == FormWindowState.Normal)
            {
                bWidth = 0;
                bHeight = 0;
            }
            else
            {
                if (this.FormBorderStyle == System.Windows.Forms.FormBorderStyle.FixedSingle)
                {
                    bWidth = 0;// NativeMethods.GetSystemMetrics(32); //SM_CXFRAME 围绕可改变大小的窗口的边框的厚度
                    bHeight = 0;// NativeMethods.GetSystemMetrics(33);//SM_CYFRAME 围绕可改变大小的窗口的边框的厚度
                }
                else
                {
                    bWidth = NativeMethods.GetSystemMetrics(32); //SM_CXFRAME 围绕可改变大小的窗口的边框的厚度
                    bHeight = NativeMethods.GetSystemMetrics(33);//SM_CYFRAME 围绕可改变大小的窗口的边框的厚度
                }
            }

            int bgWidth = base.Width - bWidth;
            int bgHeight = base.Height - bHeight;
            if (bgWidth <= 0)
            {
                bgWidth = 1;
            }
            if (bgHeight <= 0)
            {
                bgHeight = 1;
            }

            //static bool DUIForm._enableBackgroundBitmapCache为全局标识，该标识为false时将完全禁用背景缓存
            //this.UseFormBackgroundCache()为局部标识，只决定当前窗口实例是否使用背景缓存
            if (DUIForm._enableBackgroundBitmapCache && this.UseFormBackgroundCache())
            {
                string key = string.Format("{0}[{1}*{2}]", this._layoutName, bgWidth, bgHeight);
                if (DUIForm._backgroundBitmapDict.ContainsKey(key))
                {
                    this._backbuffer_bitmap = DUIForm._backgroundBitmapDict[key];
                }
                else
                {
                    //http://stackoverflow.com/questions/2612487/how-to-fix-the-flickering-in-user-controls
                    //Use the Format32bppPArgb pixel format for that copy, it renders about 10 times faster than any other pixel format.
                    this._backbuffer_bitmap = new Bitmap(bgWidth, bgHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);//Format32bppPArgb Format32bppArgb);
                    using (Graphics g = Graphics.FromImage(this._backbuffer_bitmap))
                    {
                        DrawBackGround(g);
                    }
                    DUIForm._backgroundBitmapDict.Add(key, this._backbuffer_bitmap);
                }
            }else
            {
                //释放旧对象
                if (this._backbuffer_bitmap != null)
                {
                    this._backbuffer_bitmap.Dispose();
                }
                this._backbuffer_bitmap = new Bitmap(bgWidth, bgHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                using (Graphics g = Graphics.FromImage(this._backbuffer_bitmap))
                {
                    DrawBackGround(g);
                }
            }
            

            //获得当前窗口采用的布局对象
            this.WndRegion = BitmapToRegion.Convert(this._backbuffer_bitmap, this.Layout.TransparentKey, TransparencyMode.ColorKeyTransparent, 0, 0);
            base.Invalidate();
        }
        #endregion

        #region protected virtual bool UseFormBackgroundCache()
        /// <summary>
        /// 获取当前窗口实例是否使用背景图缓存的标识，子窗口重载可改变缓存使用
        /// </summary>
        /// <returns></returns>
        protected virtual bool UseFormBackgroundCache()
        {
            return true;
        }
        #endregion

        #region public static void FlushBackgroundBitmapDict()
        /// <summary>
        /// 清空并释放背景图片字典缓存中所有背景
        /// </summary>
        public static void FlushBackgroundBitmapDict()
        {
            if (DUIForm._backgroundBitmapDict != null
                && DUIForm._backgroundBitmapDict.Count > 0)
            {
                foreach (string key in DUIForm._backgroundBitmapDict.Keys)
                {
                    Bitmap bitmap = DUIForm._backgroundBitmapDict[key];
                    bitmap.Dispose();
                }

                DUIForm._backgroundBitmapDict.Clear();
            }
        }
        #endregion

        #region protected override void WndProc(ref Message m)
        /// <summary>
        /// 窗口消息处理函数
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case NativeConst.WM_NCPAINT:
                    return;
                case NativeConst.WM_NCCALCSIZE:
                    #region 暂不使用
                    ////base.WndProc(ref m);
                    // if(m.WParam != IntPtr.Zero)
                    // {
                    //     NCCALCSIZE_PARAMS Params;
                    //     Params = (NCCALCSIZE_PARAMS)m.GetLParam(typeof(NCCALCSIZE_PARAMS));
                    //     //600*432
                    //     int width1 = Params.rgrc[1].Right - Params.rgrc[1].Left;
                    //     int height1 = Params.rgrc[1].Bottom - Params.rgrc[1].Top;
                    //     int width2 = Params.rgrc[2].Right - Params.rgrc[2].Left;
                    //     int height2 = Params.rgrc[2].Bottom - Params.rgrc[2].Top;

                    //     //Params.rgrc[0].Top += 40 - (NativeMethods.GetSystemMetrics(NativeConst.SM_CYCAPTION) + NativeMethods.GetSystemMetrics(NativeConst.SM_CYFRAME)); //40为标题栏高度
                    //     //Params.rgrc[0].Top -= (NativeMethods.GetSystemMetrics(NativeConst.SM_CYCAPTION) + NativeMethods.GetSystemMetrics(NativeConst.SM_CYFRAME));
                    //     //Params.rgrc[0].Left -= NativeMethods.GetSystemMetrics(NativeConst.SM_CXFRAME);
                    //     Params.rgrc[0].Right = Params.rgrc[0].Left + 600;
                    //     Params.rgrc[0].Bottom = Params.rgrc[0].Top + 432;
                    //     Marshal.StructureToPtr(Params, m.LParam, false);
                    // }
                    #endregion 
                    return;
                case NativeConst.WM_NCACTIVATE:
                    m.Result = (IntPtr)1;
                    return;
                case NativeConst.WM_NCHITTEST://鼠标命中测试
                    WM_NCHITTEST(ref m);
                    return;
                case NativeConst.WM_NCRBUTTONUP:
                    #region 暂不使用
                    //此段代码完成当鼠标在标题栏位置点右键时，弹出窗口系统菜单
                    //Point vPoint = new Point((int)m.LParam);
                    //Point pt = PointToClient(vPoint);
                    //pt.Offset(-bWidth, -bHeight);
                    //Rectangle vRect;
                    //if (pt.Y < 24 + bHeight && !new Rectangle(base.Width - bWidth * 2 - 98, 1, 93, 20).Contains(pt))
                    //    NativeMethods.SendMessage(Handle, InnerWin32.WM_SYSCOMMAND, NativeMethods.TrackPopupMenu(
                    //        NativeMethods.GetSystemMenu(Handle, false),
                    //        InnerWin32.TPM_RETURNCMD | InnerWin32.TPM_LEFTBUTTON, vPoint.X, vPoint.Y,
                    //        0, Handle, out vRect), 0);
                    #endregion
                    base.WndProc(ref m);
                    break;
                case NativeConst.WM_NCLBUTTONDOWN://鼠标非客户区按下消息
                    WM_NCLBUTTONDOWN(ref m); 
                    base.WndProc(ref m);
                    break;
                case NativeConst.WM_NCLBUTTONUP://鼠标非客户去弹起消息
                    WM_NCLBUTTONUP(ref m);
                    base.WndProc(ref m);
                    break;
                case NativeConst.WM_NCLBUTTONDBLCLK: //非客户区鼠标左键双击事件
                    this.SuspendLayout();//挂起布局计算，这样双击导致窗口放大和复位的时候速度得到加快  
                    base.WndProc(ref m); //执行系统默认双击处理
                    this.Update();       //更新界面
                    this.ResumeLayout(true); //执行子控件布局计算
                    break;
                case NativeConst.WM_LBUTTONDOWN://鼠标客户区弹起消息
                    WM_LBUTTONDOWN(ref m);
                    base.WndProc(ref m);
                    break;
                case NativeConst.WM_LBUTTONUP://鼠标客户区弹起消息
                    WM_LBUTTONUP(ref m);
                    base.WndProc(ref m);
                    break;
                case NativeConst.WM_SYSCOMMAND:
                    WM_SYSCOMMAND(ref m);       //系统命令消息，此处用于处理max,restore按钮的变化
                    break;
                //case NativeConst.WM_WINDOWPOSCHANGING:
                //    base.WndProc(ref m);
                //    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        #endregion

        #region Windows消息处理函数

        #region private void WM_NCLBUTTONDOWN(ref Message m)
        private void WM_NCLBUTTONDOWN(ref Message m)
        {
            //消息参数中获取的鼠标坐标是以屏幕左上角为参考点，
            //所以调用PointToClient转换为以窗口左上角为参考点
            Point vPoint = new Point((int)m.LParam);
            Point pt = PointToClient(vPoint);

            #region 废弃代码
            /*
            pt.Offset(-bWidth, -bHeight);
            if (CloseButtonRectangle.Contains(pt) && closeStatus != ButtonStatus.Down)
            {
                closeStatus = ButtonStatus.Down;
                base.Invalidate();
            }
            else if (MinButtonRectangle.Contains(pt) && MinStatus != ButtonStatus.Down)
            {
                MinStatus = ButtonStatus.Down;
                base.Invalidate();
            }
            else if (MaxButtonRectangle.Contains(pt) && MaxStatus != ButtonStatus.Down)
            {
                MaxStatus = ButtonStatus.Down;
                base.Invalidate();
            }*/
            #endregion

            //新皮肤代码加入---------------------------------------------------------------
            this.Layout.ButtonManager.OnMouseDown(this._backbuffer_bitmap, new Point(bWidth, bHeight), pt);
            base.Invalidate();//刷新界面
        }
        #endregion

        #region private void WM_NCLBUTTONUP(ref Message m)
        private void WM_NCLBUTTONUP(ref Message m)
        {
            //消息参数中获取的鼠标坐标是以屏幕左上角为参考点，
            //所以调用PointToClient转换为以窗口左上角为参考点
            Point vPoint = new Point((int)m.LParam);
            Point pt = PointToClient(vPoint);

            #region 废弃代码
            /*pt.Offset(-bWidth, -bHeight);
            if (CloseButtonRectangle.Contains(pt))
            {
                this.Close();
            }
            else if (MinButtonRectangle.Contains(pt))
            {
                this.WindowState = FormWindowState.Minimized;
                MinStatus = ButtonStatus.Normal;
            }
            else if (MaxButtonRectangle.Contains(pt))
            {
                this.WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
                MaxStatus = ButtonStatus.Normal;
            }*/
            #endregion

            //新皮肤代码加入---------------------------------------------------------------
            this.Layout.ButtonManager.OnMouseUp(this._backbuffer_bitmap, new Point(bWidth, bHeight), pt);
            base.Invalidate();//刷新界面
        }
        #endregion

        #region private void WM_NCHITTEST(ref Message m)
        private void WM_NCHITTEST(ref Message m)
        {
            m.Result = new IntPtr(NativeConst.HTCLIENT); //默认返回客户区域命中值
            Point pt = PointToClient(new Point((int)m.LParam));
            
            //新皮肤代码加入---------------------------------------------------------------
            //获得当前窗口采用的布局对象
            DUILayout formLayout = this.Layout;
            bool bHit = formLayout.ButtonManager.OnMouseMove(this._backbuffer_bitmap, new Point(bWidth, bHeight), pt);
            //-----------------------------------------------------------------------------

            pt.Offset(-bWidth, -bHeight);
            int x = pt.X;
            int y = pt.Y;

            if (_showDebugMessage)
            {
                _debugMessage = string.Format("X={0}, Y={1},bWidth={2},bHeight={3} base.Width={4}, base.Height={5}", x, y, bWidth, bHeight,base.Width,base.Height);
            }
            base.Invalidate();//刷新界面

            //若命中了图片，则返回
            if (bHit)
            {
                m.Result = new IntPtr(NativeConst.HTOBJECT); //HTOBJECT 
                return;
            }

            #region 废弃代码
            /*
            if (IconRectangle.Contains(pt))
            {
                m.Result = new IntPtr(3);
                return;
            }
            if (ContainsClose(ref m, ref pt))
            {
                return;
            }
            if (ContainsMin(ref m, ref pt))
            {
                return;
            }
            if (ContainsMax(ref m, ref pt))
            {
                return;
            }*/
            #endregion

            #region 不可删除的关键代码
            if (y < 5 && x < 8 && WindowState == FormWindowState.Normal)
            {
                m.Result = new IntPtr(NativeConst.HTTOPLEFT);
                return;
            }
            if (y < 5 && x > Width - 8 && WindowState == FormWindowState.Normal)
            {
                m.Result = new IntPtr(NativeConst.HTTOPRIGHT);
                return;
            }
            if (y > Height - 5 && x > Width - 8 && WindowState == FormWindowState.Normal)
            {
                m.Result = new IntPtr(NativeConst.HTBOTTOMRIGHT);
                return;
            }
            if (y > Height - 5 && x < 8 && WindowState == FormWindowState.Normal)
            {
                m.Result = (IntPtr)NativeConst.HTBOTTOMLEFT;
                return;
            }
            if (y < 5 && WindowState == FormWindowState.Normal)
            {
                m.Result = new IntPtr(NativeConst.HTTOP);
                return;
            }
            if (y > (base.Height - 5))
            {
                m.Result = new IntPtr(NativeConst.HTBOTTOM);
                return;
            }
            if (x < 5 + bWidth)
            {
                m.Result = new IntPtr(NativeConst.HTLEFT);
                return;
            }
            if (x > (base.Width - 5))
            {
                m.Result = new IntPtr(NativeConst.HTRIGHT);
                return;
            }
            //使用布局配置文件中的配置决定控制窗口移动的点击区域
            if (y < formLayout.CaptionHeight)
            {
                m.Result = new IntPtr(NativeConst.HTCAPTION);
                return;
            }

            #endregion
        }
        #endregion

        #region private void WM_LBUTTONDOWN(ref Message m)
        private void WM_LBUTTONDOWN(ref Message m)
        {
            //WM_LBUTTONDOWN消息参数取出的坐标点以窗口左上角为参考点
            //所以不用调用PointToClient进行转换，如果进行转换，则参考点变为客户区左上角
            //Point pt = PointToClient(new Point((int)m.LParam));
            Point pt = new Point((int)m.LParam);

            this.Layout.ButtonManager.OnMouseDown(this._backbuffer_bitmap, new Point(bWidth, bHeight), pt);
            base.Invalidate();//刷新界面
            
        }
        #endregion

        #region private void WM_LBUTTONUP(ref Message m)
        private void WM_LBUTTONUP(ref Message m)
        {
            //WM_LBUTTONDOWN消息参数取出的坐标点以窗口左上角为参考点
            //所以不用调用PointToClient进行转换，如果进行转换，则参考点变为客户区左上角
            //Point pt = PointToClient(new Point((int)m.LParam));
            Point pt = new Point((int)m.LParam);

            this.Layout.ButtonManager.OnMouseUp(this._backbuffer_bitmap, new Point(bWidth, bHeight), pt);
            base.Invalidate();//刷新界面
        }
        #endregion

        #region private void WM_SYSCOMMAND(ref Message m)
        private void WM_SYSCOMMAND(ref Message m)
        {
            bool bZoomed = NativeMethods.IsZoomed(this.Handle);
            base.WndProc(ref m);
            if (NativeMethods.IsZoomed(this.Handle) != bZoomed)
            {
                if (!bZoomed)
                {
                    if (this.maxButton != null)
                    {
                        this.maxButton.Visible = false;
                    }
                    if (this.restoreButton != null)
                    {
                        this.restoreButton.Visible = true;
                    }
                }
                else
                {
                    if (this.maxButton != null)
                    {
                        this.maxButton.Visible = true;
                    }
                    if (this.restoreButton != null)
                    {
                        this.restoreButton.Visible = false;
                    }
                }
            }
        }
        #endregion

        #endregion

        #region 废弃代码
        //private bool ContainsClose(ref Message m, ref Point pt)
        //{
        //    bool result = false;
        //    if (CloseButtonRectangle.Contains(pt))
        //    {
        //        if (closeStatus == ButtonStatus.Normal)
        //        {
        //            closeStatus = ButtonStatus.Hover;
        //            MaxStatus = ButtonStatus.Normal;
        //            MinStatus = ButtonStatus.Normal;
        //            base.Invalidate();
        //        }
        //        m.Result = new IntPtr(19);
        //        result = true;
        //    }
        //    else if (closeStatus != ButtonStatus.Normal)
        //    {
        //        closeStatus = ButtonStatus.Normal;
        //        base.Invalidate();
        //        result = false;
        //    }
        //    return result;
        //}

        //private bool ContainsMax(ref Message m, ref Point pt)
        //{
        //    bool result = false;
        //    if (this.MaxButtonRectangle.Contains(pt))
        //    {
        //        if (MaxStatus == ButtonStatus.Normal)
        //        {
        //            MaxStatus = ButtonStatus.Hover;
        //            MinStatus = ButtonStatus.Normal;
        //            closeStatus = ButtonStatus.Normal;
        //            base.Invalidate();
        //        }
        //        m.Result = new IntPtr(19);
        //        result = true;
        //    }
        //    else if (MaxStatus != ButtonStatus.Normal)
        //    {
        //        MaxStatus = ButtonStatus.Normal;
        //        base.Invalidate();
        //        result = false;
        //    }
        //    return result;
        //}

        //private bool ContainsMin(ref Message m, ref Point pt)
        //{
        //    bool result = false;
        //    if (MinButtonRectangle.Contains(pt))
        //    {
        //        if (MinStatus == ButtonStatus.Normal)
        //        {
        //            MinStatus = ButtonStatus.Hover;
        //            MaxStatus = ButtonStatus.Normal;
        //            closeStatus = ButtonStatus.Normal;
        //            base.Invalidate();
        //        }
        //        m.Result = new IntPtr(19);
        //        result = true;
        //    }
        //    else if (MinStatus != ButtonStatus.Normal)
        //    {
        //        MinStatus = ButtonStatus.Normal;
        //        base.Invalidate();
        //        result = false;
        //    }
        //    return result;
        //}

        //enum ButtonStatus
        //{
        //    Normal, Hover, Down
        //}
        #endregion

        #region 系统按钮事件
        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender">产生事件的按钮对象</param>
        private void OnCloseButtonClick(EricYou.DirectUI.Skin.Buttons.DUIButton sender)
        {
            //MessageBox.Show("You click close button!");
            if (this.OnDUIFormClosing())
            {
                this.Close();
            }
        }

        /// <summary>
        /// 窗口关闭前执行的函数，函数返回true，则窗口继续执行关闭，否则窗口暂停关闭
        /// </summary>
        /// <returns></returns>
        protected virtual bool OnDUIFormClosing()
        {
            return true;
        }
        /// <summary>
        /// 最小化按钮事件
        /// </summary>
        /// <param name="sender">产生事件的按钮对象</param>
        private void OnMiniButtonClick(EricYou.DirectUI.Skin.Buttons.DUIButton sender)
        {
            //MessageBox.Show("You click mini button!");
            //this.WindowState = FormWindowState.Minimized;

            //调用Win32方法执行窗口最小化操作
            NativeMethods.SendMessage(this.Handle, NativeConst.WM_SYSCOMMAND, NativeConst.SC_MINIMIZE, 0);
        }
        /// <summary>
        /// 最大化按钮事件
        /// </summary>
        /// <param name="sender">产生事件的按钮对象</param>
        private void OnMaxButtonClick(EricYou.DirectUI.Skin.Buttons.DUIButton sender)
        {
            //MessageBox.Show("You click max button!");
            //----this.WindowState = FormWindowState.Maximized;
            //sender.Visible = false;
            //if (restoreButton != null)
            //{
            //    restoreButton.Visible = true;
            //}

            this.SuspendLayout();

            //调用Win32方法执行窗口最大化操作
            NativeMethods.SendMessage(this.Handle, NativeConst.WM_SYSCOMMAND, NativeConst.SC_MAXIMIZE, 0);
            this.Update();
            this.ResumeLayout(true);
            //this.PerformLayout(); //以上一句的参数为true时，内部已经执行了 this.PerformLayout()
        }
        /// <summary>
        /// 恢复普通状态按钮事件
        /// </summary>
        /// <param name="sender">产生事件的按钮对象</param>
        private void OnRestoreButtonClick(EricYou.DirectUI.Skin.Buttons.DUIButton sender)
        {
            //MessageBox.Show("You click restore button!");
            //---this.WindowState = FormWindowState.Normal;
            //sender.Visible = false;
            //if (maxButton != null)
            //{
            //    maxButton.Visible = true;
            //}

            this.SuspendLayout();

            //调用Win32方法执行窗口Restore操作
            NativeMethods.SendMessage(this.Handle, NativeConst.WM_SYSCOMMAND, NativeConst.SC_RESTORE, 0);
            this.Update();

            this.ResumeLayout(true);
            //this.PerformLayout(); //以上一句的参数为true时，内部已经执行了 this.PerformLayout()
        }
        #endregion

        #region  protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        /// <summary>
        /// 重载From.SetBoundsCore函数，以处理窗口Restore时大小发生变化的问题
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="specified"></param>
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            #region 废弃代码
            /*
            Size calcTest = this.SizeFromClientSize(new Size(width, height));
            int incrementX = calcTest.Width - width;
            int incrementY = calcTest.Height - height;
            */

            //Rectangle r = this.RestoreBounds;
            //if (this.RestoreBounds.X != -1 && this.RestoreBounds.X != 0
            //    && this.RestoreBounds.Y != -1 && this.RestoreBounds.Y != 0)
            //{
            //    width -= incrementX;
            //    height -= incrementY;
            //}

            
            //if (this.RestoreBounds.X != -1 && this.RestoreBounds.X != 0
            //    && this.RestoreBounds.Y != -1 && this.RestoreBounds.Y != 0)
            //{
            //    base.SetBoundsCore(x, y, this.ClientSize.Width, this.ClientSize.Height, specified);
            //    MessageBox.Show("base.SetBoundsCore(x, y, this.RestoreBounds.Width, this.RestoreBounds.Height, specified);");
            //}
            //else
            //{
              //  base.SetBoundsCore(x, y, width, height, specified);
            //    MessageBox.Show("base.SetBoundsCore(x, y, width, height, specified);");
            //}
            #endregion 

            //只有当要设置的窗口大小与缓存的正常状态窗口大小不同时，才执行尺寸调整
            if (width != this.RestoreBounds.Width || height != this.RestoreBounds.Height)
            {
                //获取当前窗口大小需要调整的偏移量
                Size offsetSize = CommonFunctions.GetWindowResizeOffset(this.FormBorderStyle);
                width -= offsetSize.Width;   //调整宽度
                height -= offsetSize.Height; //调整高度
            }

            base.SetBoundsCore(x, y, width, height, specified);
        }
        #endregion

        #region protected override void OnClosed(EventArgs e)
        /// <summary>
        /// 窗口关闭函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            //将DUIForm窗口实例从皮肤管理器维护的DUIFrom对象列表移除
            DUISkinManager.DetachFromDUIFrom(this);

            //将窗口对应的布局对象拷贝移出缓存
            DUISkinManager.GetCurrentSkinManager().RemoveLayout(this._uniqueID);
        }
        #endregion

        #region 窗口阴影效果显示与隐藏相关变量和方法实现

        #region internal enum WindowShadeState
        /// <summary>
        /// 窗口阴影显示与关闭过程状态枚举
        /// </summary>
        internal enum WindowShadeState
        {
            /// <summary>
            /// 阴影已关闭
            /// </summary>
            Closed,
            /// <summary>
            /// 阴影正在计算待显示
            /// </summary>
            Showing,
            /// <summary>
            /// 阴影已显示
            /// </summary>
            Shown,
            /// <summary>
            /// 阴影正在关闭显示中
            /// </summary>
            Closing
        }
        #endregion 

        #region private variables
        /// <summary>
        /// 当前窗口所处的阴影显示与关闭状态
        /// </summary>
        private WindowShadeState _showWindowState = WindowShadeState.Closed;
        /// <summary>
        /// 存储窗口显示阴影前的截图
        /// </summary>
        private Bitmap _bitmapWindowShadeLight = null;
        /// <summary>
        /// 存储窗口显示阴影前的截图（该图用于做加阴影的处理）
        /// </summary>
        private Bitmap _bitmapWindowShade = null;
        /// <summary>
        /// 当前窗口是否执行了隐藏所有直接子控件的操作
        /// </summary>
        private bool _allChildrenContrlIsInvisible = false;
        /// <summary>
        /// 当前窗口隐藏所有直接子控件前，存储每个控件的visible状态，用于之后恢复状态
        /// </summary>
        private IDictionary<string, bool> _childrenControlVisibleStateDict = new Dictionary<string, bool>();
        
        #endregion

        #region public void ShowWindowShade()
        /// <summary>
        /// 显示窗口阴影
        /// </summary>
        public void ShowWindowShade()
        {
            //已经在显示或显示中的，不重复执行操作
            if (_showWindowState == WindowShadeState.Showing
                || _showWindowState == WindowShadeState.Shown )
            {
                return;
            }
            //释放上一次截图,如果没有释放过的话
            if (_bitmapWindowShadeLight != null)
            {
                _bitmapWindowShadeLight.Dispose();
            }
            if (_bitmapWindowShade != null)
            {
                _bitmapWindowShade.Dispose();
            }

            //this.Invalidate();
            //this.Update();
            //System.Threading.Thread.Sleep(3000);

            //构造窗口截图的两个版本（原始版本存_bitmapWindowShadeLight，加阴影的版本存_bitmapWindowShade
            //_bitmapWindowShade = CommonFunctions.CaptureWindow(this); 替换为一下两句，窗口不显示也可以截图
            _bitmapWindowShade = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppPArgb);   
            this.DrawToBitmap(this._bitmapWindowShade,new Rectangle(0,0,this.Width,this.Height)); //窗口截图
            _bitmapWindowShadeLight = _bitmapWindowShade.Clone() as Bitmap; //克隆保存原始截图
            this.PaintWindowShade(_bitmapWindowShade);                      //绘制半透明阴影

            //状态为Showing时，窗口背景绘制不使用配置的皮肤，而使用_bitmapWindowShadeLight
            //这样做的效果是下一步开始隐藏子控件时，用户察觉不到控件消失，效果过度自然
            _showWindowState = WindowShadeState.Showing;
            //System.Threading.Thread.Sleep(3000);
            //this.Invalidate();
            this.Update();
            //System.Threading.Thread.Sleep(3000);
            //MessageBox.Show("_showWindowState = WindowShadeState.Showing;");

            //隐藏所有直接子控件
            DisableChildrenControlVisibleState();
            //System.Threading.Thread.Sleep(3000);
            //状态为Shown时，窗口背景绘制使用做过半透明阴影处理的窗口截图，此时窗口外观效果即已实现半透明阴影遮盖效果
            _showWindowState = WindowShadeState.Shown;
            this.Invalidate();
            this.Update();
            //System.Threading.Thread.Sleep(3000);
            //MessageBox.Show("_showWindowState = WindowShadeState.Shown;");
            
        }
        #endregion

        #region public void ShowRootWindowShade()
        /// <summary>
        /// 显示当前窗口所属最上层DUIForm类型窗口的窗口阴影
        /// 实现此方法的目的是使任何层级的子窗口直接调用后，阴影效果覆盖整个主窗口
        /// </summary>
        public void ShowRootWindowShade()
        {
            DUIForm rootDUIForm = null;
            Control currentControl = this;
            while (currentControl != null)  //递推查找最上层DUIForm类型窗口
            {
                DUIForm duiForm = currentControl as DUIForm;
                if (duiForm != null)
                {
                    rootDUIForm = duiForm;
                }
                currentControl = currentControl.Parent;
            }
            if (rootDUIForm != null)        //显示最上层DUIForm类型窗口的阴影
            {
                rootDUIForm.ShowWindowShade();
            }
        }
        #endregion

        #region public void CloseWindowShade()
        /// <summary>
        /// 关闭窗口阴影
        /// </summary>
        public void CloseWindowShade()
        {
            //状态为Closing时，窗口背景绘制不使用配置的皮肤，而使用_bitmapWindowShadeLight
            //这样做的效果是下一步开始恢复子控件显示时，用户察觉不到控件逐个显示，窗口不闪烁，效果过度自然
            _showWindowState = WindowShadeState.Closing;
            this.Invalidate();
            this.Update();

            //恢复显示所有直接子控件
            ResumeChildrenControlVisibleState();

            //状态为Closed时，窗口背景绘制使用配置的皮肤，阴影关闭完成
            _showWindowState = WindowShadeState.Closed;
            this.Invalidate();
            this.Update();

            //释放窗口阴影显示过程用到的截图
            if (_bitmapWindowShadeLight != null)
            {
                _bitmapWindowShadeLight.Dispose();
                _bitmapWindowShadeLight = null;
            }
            if (_bitmapWindowShade != null)
            {
                _bitmapWindowShade.Dispose();
                _bitmapWindowShade = null;
            }
        }
        #endregion

        #region public void CloseRootWindowShade()
        /// <summary>
        /// 关闭当前窗口所属最上层DUIForm类型窗口的窗口阴影
        /// 此方法与ShowRootWindowShade()方法配对使用
        /// </summary>
        public void CloseRootWindowShade()
        {
            DUIForm rootDUIForm = null;
            Control currentControl = this;
            while (currentControl != null)  //递推查找最上层DUIForm类型窗口
            {
                DUIForm duiForm = currentControl as DUIForm;
                if (duiForm != null)
                {
                    rootDUIForm = duiForm;
                }
                currentControl = currentControl.Parent;
            }
            if (rootDUIForm != null)
            {
                rootDUIForm.CloseWindowShade();//关闭最上层DUIForm类型窗口的阴影
            }
        }
        #endregion

        #region private void DisableChildrenControlVisibleState()
        /// <summary>
        /// //隐藏所有直接子控件
        /// </summary>
        private void DisableChildrenControlVisibleState()
        {
            //已经执行过隐藏操作且还未恢复，则不重复执行
            if(_allChildrenContrlIsInvisible)
            {
                return ;
            }
            this._childrenControlVisibleStateDict.Clear();

            foreach (Control control in this.Controls)
            {
                //将控件原visible状态记录到字典中，待恢复时使用
                this._childrenControlVisibleStateDict.Add(control.Name, control.Visible);
                
                //设置显示属性
                control.Visible = false;
            }

            //置已执行操作的标志
            _allChildrenContrlIsInvisible = true;
        }
        #endregion

        #region private void ResumeChildrenControlVisibleState()
        /// <summary>
        /// 恢复显示所有直接子控件
        /// </summary>
        private void ResumeChildrenControlVisibleState()
        {
            //已执行过恢复显示操作，或控件没有执行过隐藏操作的，不执行恢复操作
            if (!_allChildrenContrlIsInvisible)
            {
                return;
            }
            foreach (Control control in this.Controls)
            {
                //使用字典中记录的当前控件visible原始状态恢复控件显示，未获取到记录状态的默认为true
                if (this._childrenControlVisibleStateDict.ContainsKey(control.Name))
                {
                    control.Visible = this._childrenControlVisibleStateDict[control.Name];
                }
                else
                {
                    control.Visible = true;
                }
            }

            //置操作标志
            _allChildrenContrlIsInvisible = false;
        }
        #endregion

        #region protected void PaintWindowShade(Graphics g)
        /// <summary>
        /// 绘制阴影遮盖图层
        /// </summary>
        /// <param name="g"></param>
        private void PaintWindowShade(Image desImage)
        {
            using (Graphics desGraphics = Graphics.FromImage(desImage))
            {
                using (Bitmap bitmap = new Bitmap(base.Width, base.Height))
                {
                    using (Graphics bitmapGraphics = Graphics.FromImage(bitmap))
                    {
                        bitmapGraphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, bitmap.Width, bitmap.Height);
                    }

                    ImageAttributes imageAttr = new ImageAttributes();
                    ColorMatrix newColorMatrix = new ColorMatrix
                    {
                        Matrix00 = 1f,
                        Matrix11 = 1f,
                        Matrix22 = 1f,
                        Matrix33 = 0.6f,
                        Matrix44 = 1f
                    };
                    imageAttr.SetColorMatrix(newColorMatrix);
                    Rectangle destRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                    desGraphics.DrawImage(bitmap, destRect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttr);
                }
            }
        }
        #endregion

        #endregion

    }
}