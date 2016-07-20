using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace EricYou.DirectUI.Skin
{
    public class DUISkinInfo
    {
        #region private variables
        /// <summary>
        /// 应用程序根目录
        /// </summary>
        private string _appBaseDirectory = string.Empty;
        /// <summary>
        /// 皮肤根目录
        /// </summary>
        private string _skinBaseDirectory = string.Empty;
        /// <summary>
        /// 皮肤根目录
        /// </summary>
        private string _skinDirectory = string.Empty;
        /// <summary>
        /// 皮肤图片资源目录
        /// </summary>
        private string _skinImagesDirectory = string.Empty;
        /// <summary>
        /// 皮肤窗口布局配置文件目录
        /// </summary>
        private string _skinLayoutDirectory = string.Empty;
        /// <summary>
        /// 皮肤配置文件全路径
        /// </summary>
        private string _skinSettingFileName = string.Empty;
        /// <summary>
        /// 皮肤唯一名称（皮肤目录名称）
        /// </summary>
        private string _skinName = string.Empty;
        /// <summary>
        /// 皮肤展现名
        /// </summary>
        private string _skinDisplayName = string.Empty;

        /// <summary>
        /// 标识皮肤信息对象是否创建成功，若加载皮肤展现名过程中出现问题，则该字段为FALSE, 但不会触发异常
        /// </summary>
        private bool _createOK;

        #endregion

        #region properties
        /// <summary>
        /// 获取和设置应用程序根目录
        /// </summary>
        public string AppBaseDirectory
        {
            get { return _appBaseDirectory; }
            set { _appBaseDirectory = value; }
        }
        /// <summary>
        /// 获取和设置皮肤根目录
        /// </summary>
        public string SkinBaseDirectory
        {
            get { return _skinBaseDirectory; }
            set { _skinBaseDirectory = value; }
        }
        /// <summary>
        /// 获取和设置当前皮肤根目录
        /// </summary>
        public string SkinDirectory
        {
            get { return _skinDirectory; }
            set { _skinDirectory = value; }
        }
        /// <summary>
        /// 获取和设置皮肤图片资源目录
        /// </summary>
        public string SkinImagesDirectory
        {
            get { return _skinImagesDirectory; }
            set { _skinImagesDirectory = value; }
        }
        /// <summary>
        /// 获取和设置皮肤窗口布局配置文件目录
        /// </summary>
        public string SkinLayoutDirectory
        {
            get { return _skinLayoutDirectory; }
            set { _skinLayoutDirectory = value; }
        }
        /// <summary>
        /// 获取和设置当前皮肤配置文件全路径
        /// </summary>
        public string SkinSettingFileName
        {
            get { return _skinSettingFileName; }
            set { _skinSettingFileName = value; }
        }
        /// <summary>
        /// 获取和设置当前皮肤唯一名称（皮肤目录名称）
        /// </summary>
        public string SkinName
        {
            get { return _skinName; }
            set { _skinName = value; }
        }
        /// <summary>
        /// 获取和设置当前皮肤展现名
        /// </summary>
        public string SkinDisplayName
        {
            get { return _skinDisplayName; }
            set { _skinDisplayName = value; }
        }

        /// <summary>
        /// 获取和设置皮肤信息对象是否创建成功的标识，若加载皮肤展现名过程中出现问题，则该字段为FALSE, 但不会触发异常
        /// </summary>
        public bool CreateOK
        {
            get { return _createOK; }
            set { _createOK = value; }
        }


        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="appBaseDirectory">应用程序根目录</param>
        /// <param name="skinName">皮肤唯一名（皮肤文件夹名）</param>
        public DUISkinInfo(string appBaseDirectory, string skinName)
        {
            _appBaseDirectory = appBaseDirectory; 
            _skinBaseDirectory = appBaseDirectory.TrimEnd(new char[] { '\\' }) + "\\Skins";
            _skinDirectory = _skinBaseDirectory + "\\" + skinName;
            _skinImagesDirectory = _skinDirectory + "\\images";
            _skinLayoutDirectory = _skinDirectory + "\\layouts";
            _skinSettingFileName = _skinDirectory + "\\SkinSettings.config";
            _skinName = skinName;
            _skinDisplayName = getSkinDisplayName();

            if (_skinDisplayName == null)
            {
                _createOK = false;
            }
            else
            {
                _createOK = true;
            }
        }

        /// <summary>
        /// 获取皮肤展现名
        /// </summary>
        /// <returns>皮肤展现名</returns>
        private string getSkinDisplayName()
        {
            if (!File.Exists(_skinSettingFileName))
            {
                return null;
            }

            //载入皮肤配置文件
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_skinSettingFileName);

            XmlElement skinSettingElement = xmlDoc.DocumentElement;
            if (skinSettingElement.Name != "skinSetting")
            {
                return null;
            }

            return skinSettingElement.Attributes["displayName"] != null ? skinSettingElement.Attributes["displayName"].Value : null;
        }
    }
}
