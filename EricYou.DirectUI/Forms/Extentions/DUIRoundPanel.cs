using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EricYou.DirectUI.Forms.Extentions
{
    public class DUIRoundPanel : System.Windows.Forms.Panel
    {
        public DUIRoundPanel():base()
        {
            base.DoubleBuffered = true;
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.UserPaint, true);

            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AreaColor = Color.White;
            this.BorderColor = Color.Black;
            this.BorderWidth = 1;

        }

        public DUIRoundPanel(IContainer container):this()
        {
            container.Add(this);

            //InitializeComponent();
        }


        // 圆角
        // ===============================================================================================
        private int _radius;  // 圆角弧度

        /// <summary>圆角弧度(0为不要圆角)</summary>
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("圆角弧度(0为不要圆角)")]
        public int RoundRadius
        {
            get
            {
                return _radius;
            }
            set
            {
                if (value < 0) { _radius = 0; }
                else { _radius = value; }
                base.Refresh();
            }
        }

        private Color _areaColor;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("内部颜色")]
        public Color AreaColor
        {
            get
            {
                return _areaColor;
            }
            set
            {
                _areaColor = value;
                base.Invalidate();
                base.Update();
            }
        }

        private Color _borderColor;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("边框颜色")]
        public Color BorderColor
        {
            get
            {
                return _borderColor;
            }
            set
            {
                _borderColor = value;
                base.Invalidate();
                base.Update();
            }
        }

        private int _boderWidth;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("边框宽度")]
        public int BorderWidth
        {
            get
            {
                return _boderWidth;
            }
            set
            {
                _boderWidth = value;
                base.Invalidate();
                base.Update();
            }
        }


        // 圆角代码
        public GraphicsPath BuildPath()
        {
            // -----------------------------------------------------------------------------------------------
            // 已经是.net提供给我们的最容易的改窗体的属性了(以前要自己调API)
            System.Drawing.Drawing2D.GraphicsPath oPath = new System.Drawing.Drawing2D.GraphicsPath();
            int x = this.BorderWidth;//(int)Math.Ceiling((this.BorderWidth - 1) / 2.0);
            int y = this.BorderWidth;//(int)Math.Ceiling((this.BorderWidth - 1) / 2.0);
            int thisWidth = this.Width - 2 * this.BorderWidth -1;
            int thisHeight = this.Height - 2 * this.BorderWidth -1;
            int angle = _radius;
            if (angle > 0)
            {
                System.Drawing.Graphics g = CreateGraphics();
                oPath.AddArc(x, y, angle, angle, 180, 90);                                 // 左上角
                oPath.AddArc(thisWidth - angle, y, angle, angle, 270, 90);                 // 右上角
                oPath.AddArc(thisWidth - angle, thisHeight - angle, angle, angle, 0, 90);  // 右下角
                oPath.AddArc(x, thisHeight - angle, angle, angle, 90, 90);                 // 左下角
                oPath.CloseAllFigures();
                //this.Region = new System.Drawing.Region(oPath);
            }
            // -----------------------------------------------------------------------------------------------
            else
            {
                oPath.AddLine(x + angle, y, thisWidth - angle, y);                         // 顶端
                oPath.AddLine(thisWidth, y + angle, thisWidth, thisHeight - angle);        // 右边
                oPath.AddLine(thisWidth - angle, thisHeight, x + angle, thisHeight);       // 底边
                oPath.AddLine(x, y + angle, x, thisHeight - angle);                        // 左边
                oPath.CloseAllFigures();
                //this.Region = new System.Drawing.Region(oPath);
            }

            return oPath;
        }
        // ===============================================================================================

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs pe)
        {
            pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pe.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            //base.OnPaint(pe);

            GraphicsPath path = BuildPath();
            pe.Graphics.FillPath(new SolidBrush(this.AreaColor), path);
            pe.Graphics.DrawPath(new System.Drawing.Pen(this.BorderColor, this.BorderWidth), path);


            //Round();  // 圆角

        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            base.Refresh();
        }
    }
}
