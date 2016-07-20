using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Drawing;
using EricYou.DirectUI.Forms;
using EricYou.DirectUI.Skin.Buttons;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using EricYou.DirectUI.Utils;
using EricYou.DirectUI.Skin.Images;

namespace EricYou.DirectUI.Skin
{
    /// <summary>
    /// 皮肤布局类
    /// </summary>
    public class DUILayout : ICloneable
    {
        //private SkinManager _ownerManager = null;

        //public SkinManager OwnerManager
        //{
        //    get { return _ownerManager; }
        //    set { _ownerManager = value; }
        //}

        /// <summary>
        /// 布局名（布局文件名）
        /// </summary>
        private string _name;

        /// <summary>
        /// 获取布局名（布局文件名）
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// 该布局透明关键色
        /// </summary>
        private Color _transparentKey;

        /// <summary>
        /// 获取该布局透明关键色
        /// </summary>
        public Color TransparentKey
        {
            get { return _transparentKey; }
        }

        /// <summary>
        /// 窗口从上边框开始，可以点击移动窗口的区域高度
        /// </summary>
        private int _captionHeight;
        /// <summary>
        /// 获取和设置窗口从上边框开始，可以点击移动窗口的区域高度
        /// </summary>
        public int CaptionHeight
        {
            get { return _captionHeight; }
            set { _captionHeight = value; }
        }

        /// <summary>
        /// 布局标题区信息
        /// </summary>
        private DUILayoutTitleInfo _titleInfo;

        /// <summary>
        /// 获取布局标题区信息
        /// </summary>
        public DUILayoutTitleInfo TitleInfo
        {
            get { return _titleInfo; }
        }


        /// <summary>
        /// 该布局普通尺寸背景信息
        /// </summary>
        private DUIBackgroundInfo _normalBackgroundInfo;

        /// <summary>
        /// 获取该布局普通尺寸背景信息
        /// </summary>
        public DUIBackgroundInfo NormalBackgroundInfo
        {
            get { return _normalBackgroundInfo; }
        }

        /// <summary>
        /// 该布局最大化尺寸背景信息
        /// </summary>
        private DUIBackgroundInfo _maximizedbackgroundInfo;

        /// <summary>
        /// 获取该布局最大化尺寸背景信息
        /// </summary>
        public DUIBackgroundInfo MaximizedbackgroundInfo
        {
            get { return _maximizedbackgroundInfo; }
        }

        /// <summary>
        /// 布局背景图片管理器对象
        /// </summary>
        private DUIImageManager _imageManager;
        /// <summary>
        /// 获取和设置布局背景图片管理器对象
        /// </summary>
        public DUIImageManager ImageManager
        {
            get { return _imageManager; }
        }

        /// <summary>
        /// 布局按钮管理器对象
        /// </summary>
        private DUIButtonManager _buttonManager;

        /// <summary>
        /// 获取布局按钮管理器对象
        /// </summary>
        public DUIButtonManager ButtonManager
        {
            get { return _buttonManager; }
        }

        /// <summary>
        /// 供克隆函数调用的私有构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="transparentKey"></param>
        /// <param name="captionHeight"></param>
        /// <param name="normalBackgroundInfo"></param>
        /// <param name="maximizedbackgroundInfo"></param>
        /// <param name="imageManager"></param>
        /// <param name="buttonManager"></param>
        private DUILayout(string name, Color transparentKey, int captionHeight, DUILayoutTitleInfo titleInfo,
            DUIBackgroundInfo normalBackgroundInfo, DUIBackgroundInfo maximizedbackgroundInfo,
            DUIImageManager imageManager, DUIButtonManager buttonManager)
        {
            this._name = name;
            this._transparentKey = transparentKey;
            this._captionHeight = captionHeight;
            this._titleInfo = (DUILayoutTitleInfo)titleInfo.Clone();
            this._normalBackgroundInfo = (DUIBackgroundInfo)normalBackgroundInfo.Clone();
            this._maximizedbackgroundInfo = (DUIBackgroundInfo)maximizedbackgroundInfo.Clone();

            this._imageManager = (DUIImageManager)imageManager.Clone();
            this._imageManager.OwnerLayout = this;

            this._buttonManager = (DUIButtonManager)buttonManager.Clone();
            this._buttonManager.OwnerLayout = this;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="layoutFileName">布局文件全名称</param>
        public DUILayout(string layoutFileName)
        {

            FileInfo fileInfo = new FileInfo(layoutFileName);
            this._name = fileInfo.Name;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(layoutFileName);

            XmlElement layoutElement = xmlDoc.DocumentElement;

            //--------------------------------------------------
            //读取窗口贴图透明色设置
            string transparentKeyString = layoutElement.Attributes["transparentKey"] != null ? layoutElement.Attributes["transparentKey"].Value : null;
            if (string.IsNullOrEmpty(transparentKeyString))
            {
                throw new Exception("当前皮肤布局" + this._name + "未指定透明色！");
            }
            else
            {
                string[] tempSplitString = transparentKeyString.Split(new char[] { ','});
                if (tempSplitString.Length != 3)
                {
                    throw new Exception("当前皮肤布局" + this._name + "透明色配置信息不合法！");
                }
                else
                {
                    this._transparentKey = Color.FromArgb(
                        Convert.ToInt32(tempSplitString[0]), Convert.ToInt32(tempSplitString[1]), Convert.ToInt32(tempSplitString[2]));
                }

            }
            //--------------------------------------------------
            //读取窗口从上边框开始，可以点击移动窗口的区域高度
            string captionHeightString = layoutElement.Attributes["captionHeight"] != null ? layoutElement.Attributes["captionHeight"].Value : null;
            if (string.IsNullOrEmpty(captionHeightString))
            {
                this._captionHeight = 0;
            }
            else
            {
                try
                {
                    this._captionHeight = Convert.ToInt32(captionHeightString);
                }
                catch
                {
                    this._captionHeight = 0;
                }

            }
            //--------------------------------------------------
            //读取布局标题区配置
            XmlNodeList titleInfoList = layoutElement.GetElementsByTagName("title");
            if (titleInfoList == null || titleInfoList.Count==0)
            {
                _titleInfo = new DUILayoutTitleInfo(null);
            }
            else
            {
                if (titleInfoList.Count != 1)
                {
                    //存在多个title节点，创建DUILayoutTitleInfo对象失败
                    throw new Exception("当前皮肤布局" + this._name + "中，title配置不存在或不唯一！");
                }
                else
                {
                    _titleInfo = new DUILayoutTitleInfo(titleInfoList[0]);
                }
            }

            //--------------------------------------------------
            //读取布局普通尺寸背景信息
            XmlNodeList normalBackgroundList = layoutElement.GetElementsByTagName("background-normal");
            if (normalBackgroundList == null || normalBackgroundList.Count != 1)
            {
                //存在多个background-normal，创建Layout对象失败
                throw new Exception("当前皮肤布局" + this._name + "中，background-normal配置不存在或不唯一！");
            }
            _normalBackgroundInfo = new DUIBackgroundInfo(
                normalBackgroundList[0].Attributes["sourceName"] != null ? normalBackgroundList[0].Attributes["sourceName"].Value : null,
                normalBackgroundList[0].Attributes["topLeftPoint"] != null ? normalBackgroundList[0].Attributes["topLeftPoint"].Value : null,
                normalBackgroundList[0].Attributes["topRightPoint"] != null ? normalBackgroundList[0].Attributes["topRightPoint"].Value : null,
                normalBackgroundList[0].Attributes["bottomLeftPoint"] != null ? normalBackgroundList[0].Attributes["bottomLeftPoint"].Value : null,
                normalBackgroundList[0].Attributes["bottomRightPoint"] != null ? normalBackgroundList[0].Attributes["bottomRightPoint"].Value : null);
            //--------------------------------------------------
            //读取布局最大化尺寸背景信息
            XmlNodeList maximizedBackgroundList = layoutElement.GetElementsByTagName("background-maximized");
            if (maximizedBackgroundList == null || maximizedBackgroundList.Count != 1)
            {
                //存在多个background-normal，创建Layout对象失败
                throw new Exception("当前皮肤布局" + this._name + "中，background-maximized配置不存在或不唯一！");
            }
            _maximizedbackgroundInfo = new DUIBackgroundInfo(
                maximizedBackgroundList[0].Attributes["sourceName"] != null ? maximizedBackgroundList[0].Attributes["sourceName"].Value : null,
                maximizedBackgroundList[0].Attributes["topLeftPoint"] != null ? maximizedBackgroundList[0].Attributes["topLeftPoint"].Value : null,
                maximizedBackgroundList[0].Attributes["topRightPoint"] != null ? maximizedBackgroundList[0].Attributes["topRightPoint"].Value : null,
                maximizedBackgroundList[0].Attributes["bottomLeftPoint"] != null ? maximizedBackgroundList[0].Attributes["bottomLeftPoint"].Value : null,
                maximizedBackgroundList[0].Attributes["bottomRightPoint"] != null ? maximizedBackgroundList[0].Attributes["bottomRightPoint"].Value : null);
            //--------------------------------------------------
            //初始化背景图片管理器对象
            _imageManager = new DUIImageManager(this);
            _imageManager.LoadDUIImageFromXml(xmlDoc);

            //--------------------------------------------------
            //初始化按钮管理器对象
            _buttonManager = new DUIButtonManager(this);
            _buttonManager.LoadButtonFromXml(xmlDoc);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="backgroundBitmap"></param>
        /// <param name="form"></param>
        /// <param name="offsetPoint"></param>
        public void RenderBackground(Graphics g, Image backgroundBitmap, DUIForm form, Point offsetPoint )
        {
            TextureBrush brush;
            DUIBackgroundInfo backgroundInfo;
            Image backgroundImage;
            int bWidth = offsetPoint.X;
            int bHeight = offsetPoint.Y;

            if (form.WindowState == FormWindowState.Maximized)
            {
                //获得布局最大化状态下背景信息
                backgroundInfo = this.MaximizedbackgroundInfo;
                //获得最大化状态下的背景图
                backgroundImage = DUISkinManager.GetCurrentSkinManager().GetImageSource(this.MaximizedbackgroundInfo.SourceName);
            }
            else
            {
                //获得布局普通状态下背景信息
                backgroundInfo = this.NormalBackgroundInfo;
                //获得非最大化状态下的背景图
                backgroundImage = DUISkinManager.GetCurrentSkinManager().GetImageSource(this.NormalBackgroundInfo.SourceName);
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
            brush.TranslateTransform(CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).X, 0);
            g.FillRectangle(
                brush,
                new Rectangle(
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).X,
                    0,
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).X - CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).X,
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).Y));
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
            brush.TranslateTransform(0, CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).Y);
            g.FillRectangle(
                brush,
                new Rectangle(
                    0,
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).Y,
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).X,
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.BottomLeftPoint).Y - CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).Y));
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
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopRightPoint).X - 2*bWidth,
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopRightPoint).Y,
                    backgroundImage.Width + bWidth - CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopRightPoint).X,
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.BottomRightPoint).Y - CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.TopRightPoint).Y));
            brush.TranslateTransform(CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).X - 2 * bWidth,
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).Y);
            g.FillRectangle(
                brush,
                new Rectangle(
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).X ,//- 2 * bWidth, //????原始代码中减了2bWidth,因为BackBufferBitmap的宽度已经减了bWidth,故此处只减一个bWidth
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).Y,
                    backgroundBitmap.Width + bWidth - CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).X,
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.BottomRightPoint).Y - CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).Y));
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
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.BottomLeftPoint).Y - 2 * bHeight,
                    CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.BottomRightPoint).X - CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.BottomLeftPoint).X,
                    backgroundImage.Height + bHeight - CommonFunctions.GetImagePoint(backgroundImage, 0, 0, backgroundInfo.BottomLeftPoint).Y));
            brush.TranslateTransform(CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.BottomLeftPoint).X,
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.BottomLeftPoint).Y - 2 * bHeight);
            g.FillRectangle(
                brush,
                new Rectangle(
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.BottomLeftPoint).X,
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.BottomLeftPoint).Y - 2 * bHeight,//????原始代码中减了2bHeight,因为BackBufferBitmap的高度已经减了bHeight,故此处只减一个bHeight
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.BottomRightPoint).X - CommonFunctions.GetImagePoint(backgroundImage, bWidth, bHeight, backgroundInfo.BottomLeftPoint).X,
                    backgroundBitmap.Height + bHeight - CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.BottomLeftPoint).Y));
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
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).X,
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).Y),
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
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).X,
                    0,
                    backgroundBitmap.Width + bWidth - CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).X,
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).Y),
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
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.BottomLeftPoint).Y,
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.BottomLeftPoint).X,
                    backgroundBitmap.Height + bHeight - CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.BottomLeftPoint).Y),
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
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).X,
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.BottomLeftPoint).Y,
                    backgroundBitmap.Width + bWidth - CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.BottomRightPoint).X,
                    backgroundBitmap.Height + bHeight - CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.BottomRightPoint).Y),

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
            //        CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).X ,
            //        CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).Y ,
            //        CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).X
            //            - CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).X,
            //        CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.BottomRightPoint).Y
            //            - CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).Y),

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
            brush.TranslateTransform(CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).X,
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).Y);
            g.FillRectangle(
                brush,
               new Rectangle(
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).X,
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).Y,
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).X
                        - CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopLeftPoint).X,
                    CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.BottomRightPoint).Y
                        - CommonFunctions.GetImagePoint(backgroundBitmap, bWidth, bHeight, backgroundInfo.TopRightPoint).Y));
            brush.Dispose();

            //绘制背景图片
            this._imageManager.RenderImages(backgroundBitmap, g, offsetPoint);
        }

        /// <summary>
        /// 绘制背景按钮
        /// </summary>
        /// <param name="backgroundBitmap"></param>
        /// <param name="offsetPoint"></param>
        public void RenderBackgroudButtons(Image backgroundBitmap, Point offsetPoint)
        {
            //绘制背景按钮
            using (Graphics bg = Graphics.FromImage(backgroundBitmap))
            {
                this.ButtonManager.RenderButtons(backgroundBitmap, bg, offsetPoint);
            }
        }

        /// <summary>
        /// 克隆函数
        /// </summary>
        /// <returns>克隆的DUILayout对象</returns>
        public object Clone()
        {
            DUILayout newLayout = new DUILayout(this._name, this._transparentKey, this._captionHeight,
                this._titleInfo, this._normalBackgroundInfo, this._maximizedbackgroundInfo,
                this._imageManager, this._buttonManager);
            return newLayout;
        }
    }
}
