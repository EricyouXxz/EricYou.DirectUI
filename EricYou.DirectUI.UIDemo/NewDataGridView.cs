using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace EricYou.DirectUI.UIDemo
{
    public class NewListView : ListView
    {

        public bool bActived = true;

        public NewListView()
            :base()
        {
            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            PaintDeactive(e.Graphics); //失去焦点时，窗口标题栏泛白显示

        }

        /// <summary>
        /// 失去焦点时，窗口标题栏泛白显示
        /// </summary>
        /// <param name="g"></param>
        protected void PaintDeactive(Graphics g)
        {
            if (!this.bActived)
            {
                using (Bitmap bitmap = new Bitmap(base.Width, base.Height))
                {
                    Graphics.FromImage(bitmap).FillRectangle(new SolidBrush(Color.Black), 0, 0, bitmap.Width, bitmap.Height);
                    ImageAttributes imageAttr = new ImageAttributes();
                    ColorMatrix newColorMatrix = new ColorMatrix
                    {
                        Matrix00 = 1f,
                        Matrix11 = 1f,
                        Matrix22 = 1f,
                        Matrix33 = 0.3f,
                        Matrix44 = 1f
                    };
                    imageAttr.SetColorMatrix(newColorMatrix);
                    Rectangle destRect = new Rectangle(1, 1, bitmap.Width - 1, bitmap.Height);
                    g.DrawImage(bitmap, destRect, 1, 1, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttr);
                }
            }
        }
    }

    public class NewTabControl : TabControl
    {

        public bool bActived = true;

        public NewTabControl()
            : base()
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            PaintDeactive(e.Graphics); //失去焦点时，窗口标题栏泛白显示

        }

        /// <summary>
        /// 失去焦点时，窗口标题栏泛白显示
        /// </summary>
        /// <param name="g"></param>
        protected void PaintDeactive(Graphics g)
        {
            if (!this.bActived)
            {
                using (Bitmap bitmap = new Bitmap(base.Width, base.Height))
                {
                    Graphics.FromImage(bitmap).FillRectangle(new SolidBrush(Color.Black), 0, 0, bitmap.Width, bitmap.Height);
                    ImageAttributes imageAttr = new ImageAttributes();
                    ColorMatrix newColorMatrix = new ColorMatrix
                    {
                        Matrix00 = 1f,
                        Matrix11 = 1f,
                        Matrix22 = 1f,
                        Matrix33 = 0.3f,
                        Matrix44 = 1f
                    };
                    imageAttr.SetColorMatrix(newColorMatrix);
                    Rectangle destRect = new Rectangle(1, 1, bitmap.Width - 1, bitmap.Height);
                    g.DrawImage(bitmap, destRect, 1, 1, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttr);
                }
            }
        }
    }
}
