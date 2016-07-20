using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using EricYou.DirectUI.Utils;

namespace EricYou.DirectUI.Skin.Images
{
    /// <summary>
    /// 背景图片类
    /// </summary>
    public class DUIImage
    {
        /// <summary>
        /// 图片资源名称
        /// </summary>
        private string _sourceName;
        /// <summary>
        /// 获取和设置图片资源名称
        /// </summary>
        public string SourceName
        {
            get { return _sourceName; }
            set { _sourceName = value; }
        }
        /// <summary>
        /// 图片贴图时所占矩形左上角的点的纵坐标，取负数时需要和窗口高度取差得到纵坐标
        /// </summary>
        private int _top;
        /// <summary>
        /// 获取和设置图片贴图时所占矩形左上角的点的纵坐标
        /// </summary>
        public int Top
        {
            get { return _top; }
            set { _top = value; }
        }
        /// <summary>
        /// 图片贴图时所占矩形左上角的点的横坐标，取负数时需要和窗口宽度取差得到横坐标
        /// </summary>
        private int _left;
        /// <summary>
        /// 获取和设置图片贴图时所占矩形左上角的点的横坐标
        /// </summary>
        public int Left
        {
            get { return _left; }
            set { _left = value; }
        }
        /// <summary>
        /// 图片贴图时所占矩形右下角的点的横坐标，取负数时需要和窗口宽度取差得到横坐标
        /// </summary>
        private int _right;
        /// <summary>
        /// 获取和设置图片贴图时所占矩形右下角的点的横坐标
        /// </summary>
        public int Right
        {
            get { return _right; }
            set { _right = value; }
        }
        /// <summary>
        /// 图片贴图时所占矩形右下角的点的纵坐标，取负数时需要和窗口高度取差得到纵坐标
        /// </summary>
        private int _bottom;
        /// <summary>
        /// 获取和设置图片贴图时所占矩形右下角的点的纵坐标
        /// </summary>
        public int Bottom
        {
            get { return _bottom; }
            set { _bottom = value; }
        }

        /// <summary>
        /// 在双缓冲背景图上绘制当前图片
        /// </summary>
        /// <param name="backBufferBitmap">当前双缓冲绘图缓存的背景图片</param>
        /// <param name="g">与背景缓冲图绑定的绘图对象</param>
        /// <param name="offsetPoint">偏移点，用于消除窗口最大化时窗口边框厚度对点计算造成的误差</param>
        public void Render(Image backBufferBitmap, Graphics g, Point offsetPoint)
        {
            Point actualTopLeftPoint = CommonFunctions.GetImagePoint(
                backBufferBitmap, offsetPoint.X, offsetPoint.Y, new Point(_left, _top));
            Point actualBottomRightPoint = CommonFunctions.GetImagePoint(
                backBufferBitmap, offsetPoint.X, offsetPoint.Y, new Point(_right, _bottom));


            Image image = DUISkinManager.GetCurrentSkinManager().GetImageSource(this._sourceName); ;
            Rectangle imageRectange = new Rectangle(
                                            actualTopLeftPoint.X,
                                            actualTopLeftPoint.Y,
                                            actualBottomRightPoint.X - actualTopLeftPoint.X,
                                            actualBottomRightPoint.Y - actualTopLeftPoint.Y);

            //在背景换冲图上绘制当前按钮
            g.DrawImage(image, imageRectange, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
        }

    }
}
