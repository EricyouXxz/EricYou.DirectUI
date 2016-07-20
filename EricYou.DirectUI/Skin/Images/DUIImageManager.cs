using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;

namespace EricYou.DirectUI.Skin.Images
{
    /// <summary>
    /// 背景图片管理器类
    /// </summary>
    public class DUIImageManager : ICloneable
    {
        /// <summary>
        /// 所属皮肤布局对象
        /// </summary>
        private DUILayout _ownerLayout;
        
        /// <summary>
        /// 背景图片列表
        /// </summary>
        private IList<DUIImage> _dUIImageList = new List<DUIImage>();

        /// <summary>
        /// 获取和设置所属皮肤布局对象
        /// </summary>
        public DUILayout OwnerLayout
        {
            get { return _ownerLayout; }
            set { _ownerLayout = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ownerLayout"></param>
        public DUIImageManager(DUILayout ownerLayout)
        {
            _ownerLayout = ownerLayout;
        }

        /// <summary>
        /// 供克隆函数使用的私有构造函数
        /// </summary>
        /// <param name="ownerLayout"></param>
        /// <param name="duiImageList"></param>
        private DUIImageManager(DUILayout ownerLayout, IList<DUIImage> duiImageList)
        {
            _ownerLayout = ownerLayout;
            _dUIImageList = duiImageList;
        }

        /// <summary>
        /// 从XML文档加载DUIImage对象列表
        /// </summary>
        /// <param name="xmlDoc"></param>
        public void LoadDUIImageFromXml(XmlDocument xmlDoc)
        {
            if (xmlDoc == null)
            {
                throw new Exception("加载布局" + this._ownerLayout.Name + "中的贴图图片时，文档对象为空！");
            }
            XmlElement layoutElement = xmlDoc.DocumentElement;
            foreach (XmlNode childNode in layoutElement.ChildNodes)
            {
                if (childNode.Name == "images")
                {
                    foreach (XmlNode imageNode in childNode.ChildNodes)
                    {
                        if (imageNode.Name == "image")
                        {
                            if (imageNode.Attributes["sourceName"] != null
                                && imageNode.Attributes["enable"] != null
                                && imageNode.Attributes["top"] != null
                                && imageNode.Attributes["left"] != null
                                && imageNode.Attributes["right"] != null
                                && imageNode.Attributes["bottom"] != null)
                            {
                                //enble属性为false,或者该属性不存在或非法值时，不加载该按钮
                                string enableString = imageNode.Attributes["enable"].Value;
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
                                DUIImage newImage = new DUIImage();
                                newImage.SourceName = imageNode.Attributes["sourceName"].Value;
                                newImage.Top = Convert.ToInt32(imageNode.Attributes["top"].Value);
                                newImage.Left = Convert.ToInt32(imageNode.Attributes["left"].Value);
                                newImage.Right = Convert.ToInt32(imageNode.Attributes["right"].Value);
                                newImage.Bottom = Convert.ToInt32(imageNode.Attributes["bottom"].Value);

                                this._dUIImageList.Add(newImage);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 绘制背景图片
        /// </summary>
        /// <param name="backBufferBitmap">当前双缓冲绘图缓存的背景图片</param>
        /// <param name="g">与背景缓冲图绑定的绘图对象</param>
        /// <param name="offsetPoint">偏移点，用于消除窗口最大化时窗口边框厚度对点计算造成的误差</param>
        public void RenderImages(Image backBufferBitmap, Graphics g, Point offsetPoint)
        {
            //绘制按钮时，按照配置文件出现的顺序依次绘制，越晚出现的按钮越在上层
            for (int i = 0; i < this._dUIImageList.Count; i++)
            {
                DUIImage image = this._dUIImageList[i];
                image.Render(backBufferBitmap, g, offsetPoint);
            }
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns>新的DUIImageManager对象</returns>
        public object Clone()
        {
            DUIImageManager duiImageManager = new DUIImageManager(this._ownerLayout, this._dUIImageList);
            return duiImageManager;
        }
    }
}
