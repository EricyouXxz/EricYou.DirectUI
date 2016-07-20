using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace EricYou.DirectUI.Skin
{
    /// <summary>
    /// 皮肤布局-背景信息类
    /// </summary>
    public class DUIBackgroundInfo : ICloneable
    {
        ///// <summary>
        ///// 所属皮肤管理器对象引用
        ///// </summary>
        //private SkinManager _ownerManager = null;

        ///// <summary>
        ///// 获取和设置所属皮肤管理器对象引用
        ///// </summary>
        //public SkinManager OwnerManager
        //{
        //    get { return _ownerManager; }
        //    set { _ownerManager = value; }
        //}

        /// <summary>
        /// 背景图片资源名称
        /// </summary>
        private string _sourceName;

        /// <summary>
        /// 获取和设置背景图片资源名称
        /// </summary>
        public string SourceName
        {
            get { return _sourceName; }
            set { _sourceName = value; }
        }

        /// <summary>
        /// 用于处理布局放大缩小时控制资源图片缩放的左上角控制点字符串
        /// </summary>
        private Point _topLeftPoint;

        /// <summary>
        /// 获取用于处理布局放大缩小时控制资源图片缩放的左上角控制点
        /// （坐标为负数表示距离窗口右或下边界的距离，不是实际坐标）
        /// </summary>
        public Point TopLeftPoint
        {
            get 
            {
                return _topLeftPoint;
            }
        }

        /// <summary>
        /// 用于处理布局放大缩小时控制资源图片缩放的右上角控制点字符串
        /// </summary>
        private Point _topRightPoint;

        /// <summary>
        /// 获取用于处理布局放大缩小时控制资源图片缩放的右上角控制点
        /// （坐标为负数表示距离窗口右或下边界的距离，不是实际坐标）
        /// </summary>
        public Point TopRightPoint
        {
            get 
            {
                return _topRightPoint;
            }
        }

        /// <summary>
        /// 用于处理布局放大缩小时控制资源图片缩放的左下角控制点字符串
        /// </summary>
        private Point _bottomLeftPoint;

        /// <summary>
        /// 获取用于处理布局放大缩小时控制资源图片缩放的左下角控制点
        /// （坐标为负数表示距离窗口右或下边界的距离，不是实际坐标）
        /// </summary>
        public Point BottomLeftPoint
        {
            get 
            {
                return _bottomLeftPoint;
            }
        }

        /// <summary>
        /// 用于处理布局放大缩小时控制资源图片缩放的右下角控制点字符串
        /// </summary>
        private Point _bottomRightPoint;

        /// <summary>
        /// 获取用于处理布局放大缩小时控制资源图片缩放的右下角控制点
        /// （坐标为负数表示距离窗口右或下边界的距离，不是实际坐标）
        /// </summary>
        public Point BottomRightPoint
        {
            get 
            {
                return _bottomRightPoint;
            }
        }

        /// <summary>
        /// 供克隆函数调用的私有构造函数
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="topLeftPoint"></param>
        /// <param name="topRightPoint"></param>
        /// <param name="bottomLeftPoint"></param>
        /// <param name="bottomRightPoint"></param>
        private DUIBackgroundInfo(string sourceName, Point topLeftPoint, Point topRightPoint,
            Point bottomLeftPoint, Point bottomRightPoint)
        {
            this._sourceName = sourceName;
            this._topLeftPoint = topLeftPoint;
            this._topRightPoint = topRightPoint;
            this._bottomLeftPoint = bottomLeftPoint;
            this._bottomRightPoint = bottomRightPoint;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="topLeftPoint"></param>
        /// <param name="topRightPoint"></param>
        /// <param name="bottomLeftPoint"></param>
        /// <param name="bottomRightPoint"></param>
        public DUIBackgroundInfo(string sourceName, string topLeftPoint, string topRightPoint,
            string bottomLeftPoint, string bottomRightPoint)
        {
            if (string.IsNullOrEmpty(sourceName))
            {
                throw new Exception("初始化BackGroundInfo对象时sourceName为空");
            }
            if (string.IsNullOrEmpty(topLeftPoint))
            {
                throw new Exception("初始化BackGroundInfo对象时topLeftPoint为空");
            }
            if (string.IsNullOrEmpty(topRightPoint))
            {
                throw new Exception("初始化BackGroundInfo对象时topRightPoint为空");
            }
            if (string.IsNullOrEmpty(bottomLeftPoint))
            {
                throw new Exception("初始化BackGroundInfo对象时bottomLeftPoint为空");
            }
            if (string.IsNullOrEmpty(bottomRightPoint))
            {
                throw new Exception("初始化BackGroundInfo对象时bottomRightPoint为空");
            }

            _sourceName = sourceName;
            _topLeftPoint = this.ConvertStringToPoint("topLeftPoint", topLeftPoint);
            _topRightPoint = this.ConvertStringToPoint("topRightPoint", topRightPoint);
            _bottomLeftPoint = this.ConvertStringToPoint("bottomLeftPoint", bottomLeftPoint);
            _bottomRightPoint = this.ConvertStringToPoint("bottomRightPoint", bottomRightPoint);
        }

        /// <summary>
        /// 将代表点坐标的字符串转换为点对象
        /// </summary>
        /// <param name="pointName">点名称</param>
        /// <param name="strPoint">点坐标字符串</param>
        /// <returns>点对象</returns>
        private Point ConvertStringToPoint(string pointName, string strPoint)
        {
            string[] splitStrings = strPoint.Split(new char[] { ',' });
            if (splitStrings.Length != 2)
            {
                throw new Exception("背景" + this._sourceName + "的配置" + pointName + "格式存在问题！应该为【int,int】!");
            }

            int x = Convert.ToInt32(splitStrings[0]);
            int y = Convert.ToInt32(splitStrings[1]); 

            return new Point(x, y);
        }

        /// <summary>
        /// 克隆函数
        /// </summary>
        /// <returns>克隆的DUIBackgroundInfo对象</returns>
        public object Clone()
        {
            DUIBackgroundInfo newBackgroundInfo
                = new DUIBackgroundInfo(this._sourceName, this._topLeftPoint, this._topRightPoint,
                    this._bottomLeftPoint, this._bottomRightPoint);

            return newBackgroundInfo;
        }
    }
}
