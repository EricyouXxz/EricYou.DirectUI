using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Xml;
using EricYou.DirectUI.Utils;
using System.Configuration;
using EricYou.DirectUI.Skin.Buttons;
using EricYou.DirectUI.Skin.Styles;
using EricYou.DirectUI.Skin.Styles.Controls;
using System.Windows.Forms;
using EricYou.DirectUI.Forms;

namespace EricYou.DirectUI.Skin
{
    /// <summary>
    /// 皮肤管理类，该类管理皮肤所有资源、参数，管理换肤操作
    /// </summary>
    public class DUISkinManager
    {
        #region private variables
        /// <summary>
        /// 私有静态实例，作为单例
        /// </summary>
        private static DUISkinManager _instance = null;

        /// <summary>
        /// 锁对象
        /// </summary>
        private static readonly object _lockObj = new object();

        /// <summary>
        /// 当前皮肤信息
        /// </summary>
        private DUISkinInfo _currentSkinInfo = null;

        /// <summary>
        /// 当前皮肤图片资源字典
        /// </summary>
        private IDictionary<string, Image> _imageSources = null;
        /// <summary>
        /// 当前皮肤参数字典
        /// </summary>
        private IDictionary<string, string> _parameters = null;
        /// <summary>
        /// 当前主题下的布局列表
        /// </summary>
        private IDictionary<string, DUILayout> _layouts = null;
        /// <summary>
        /// 被DUIFrom引用的布局对象拷贝字典
        /// </summary>
        private IDictionary<Guid, IDictionary<string,DUILayout>> _formLayouts = null;
        /// <summary>
        /// 主题皮肤全局字体字典
        /// </summary>
        private IDictionary<string, DUIFont> _fonts = null;
        /// <summary>
        /// 主题控件样式字典
        /// </summary>
        private IDictionary<Type, IDictionary<string,IDUIStyle>> _styles = null;
        /// <summary>
        /// 主题皮肤列表
        /// </summary>
        private static IList<DUISkinInfo> _skinInfoList = null;
        /// <summary>
        /// 存储使用DUIForm作为基类的所有正在使用的窗口对象
        /// 皮肤管理器维护DUIFrom对象列表，以作消息通知
        /// </summary>
        private static IList<DUIForm> _referencedForms = new List<DUIForm>();
    
        #endregion

        #region properties

        /// <summary>
        /// 获取和设置当前皮肤信息
        /// </summary>
        public DUISkinInfo CurrentSkinInfo
        {
            get { return _currentSkinInfo; }
            set { _currentSkinInfo = value; }
        }
        /// <summary>
        /// 获取当前主题下的布局列表
        /// </summary>
        public IDictionary<string, DUILayout> Layouts
        {
            get { return _layouts; }
        }
        #endregion

        #region construction
        private DUISkinManager()
        { 
        }

        private DUISkinManager(DUISkinInfo skinInfo)
        {
            if (!Directory.Exists(skinInfo.SkinBaseDirectory))
            {
                throw new Exception("未能找到皮肤根目录Skins[" + skinInfo.SkinBaseDirectory + "],不能加载皮肤资源，请重新安装程序！");
            }
            if (!Directory.Exists(skinInfo.SkinDirectory))
            {
                throw new Exception("未能找到皮肤目录" + skinInfo.SkinName + ",不能加载皮肤资源，请重新安装程序！");
            }
            if (!File.Exists(skinInfo.SkinSettingFileName))
            {
                throw new Exception("未能找到皮肤目录" + skinInfo.SkinName + "下的皮肤配置文件SkinSettings.config,不能加载皮肤资源，请重新安装程序！");
            }


            //载入皮肤配置文件
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(skinInfo.SkinSettingFileName);

            //读取皮肤图片资源
            IDictionary<string, Image> tempSkinImageDict = new Dictionary<string, Image>();
            LoadSkinImageSources(skinInfo, xmlDoc, tempSkinImageDict);

            //读取皮肤参数配置
            IDictionary<string, string> tempSkinParametersDict = new Dictionary<string, string>();
            LoadSkinParamters(skinInfo, xmlDoc, tempSkinParametersDict);

            Dictionary<string, DUIFont> tempFontsDict = new Dictionary<string, DUIFont>(); 
            IDictionary<Type, IDictionary<string, IDUIStyle>> tempSkinStylesDict
                = new Dictionary<Type, IDictionary<string, IDUIStyle>>();
            LoadSkinStyles(skinInfo, xmlDoc, tempFontsDict, tempSkinStylesDict);

            IDictionary<string, DUILayout> tempSkinLayoutsDict = new Dictionary<string, DUILayout>();
            LoadSkinLayouts(skinInfo, tempSkinLayoutsDict);

            //---------------------------------------
            //刷新当前皮肤基础参数值
            this._currentSkinInfo = skinInfo;

            this._imageSources = tempSkinImageDict;
            this._parameters = tempSkinParametersDict;
            this._fonts = tempFontsDict;
            this._styles = tempSkinStylesDict;
            this._layouts = tempSkinLayoutsDict;
            this._formLayouts = new Dictionary<Guid, IDictionary<string, DUILayout>>();


        }
        #endregion

        #region private void LoadSkinImageSources(SkinInfo skinInfo, XmlDocument xmlDoc, IDictionary<string, Image> imageDict)
        /// <summary>
        /// 读取皮肤图片资源
        /// </summary>
        /// <param name="skinName">皮肤唯一名（皮肤根目录名）</param>
        /// <param name="xmlDoc">皮肤配置文件XML文档对象</param>
        /// <param name="imageDict">图片资源字典</param>
        private void LoadSkinImageSources(DUISkinInfo skinInfo, XmlDocument xmlDoc, IDictionary<string, Image> imageDict)
        {
            if (xmlDoc == null || imageDict == null)
            {
                return;
            }
            XmlElement skinSettingElement = xmlDoc.DocumentElement;
            if (skinSettingElement.Name != "skinSetting")
            {
                throw new Exception("加载皮肤目录" + skinInfo.SkinName + "中的皮肤配置文件出错，根标签不合法!");
            }


            if (!Directory.Exists(skinInfo.SkinImagesDirectory))
            {
                throw new Exception("皮肤目录" + skinInfo.SkinName + "\\images不存在，无法加载皮肤资源!");    
            }
           
            foreach (XmlNode childNode in skinSettingElement.ChildNodes)
            {
                if (childNode.Name == "imageSources")
                {
                    foreach (XmlNode imageNode in childNode.ChildNodes)
                    {
                        if (imageNode.Name == "image")
                        {
                            string imageName = imageNode.Attributes["name"]!=null?imageNode.Attributes["name"].Value:null;
                            string imageFileName = imageNode.Attributes["fileName"] != null ? imageNode.Attributes["fileName"].Value : null;
                            if (string.IsNullOrEmpty(imageName) || string.IsNullOrEmpty(imageFileName))
                            {
                                continue;
                            }

                            string imageFileFullName = skinInfo.SkinImagesDirectory + "\\" + imageFileName;
                            if (!File.Exists(imageFileFullName))
                            {
                                throw new Exception("加载皮肤资源时找不到文件" + imageFileFullName + "!");
                            }

                            Image imageSource = CommonFunctions.LoadImageFromFile(imageFileFullName);
                            if (!imageDict.ContainsKey(imageName))
                            {
                                imageDict.Add(imageName, imageSource);
                            }
                            else
                            {
                                imageDict[imageName] = imageSource;
                            }

                        }
                    }
                }
            }
        }
        #endregion

        #region private void LoadSkinParamters(SkinInfo skinInfo, XmlDocument xmlDoc, IDictionary<string, string> parameterDict)
        /// <summary>
        /// 读取皮肤参数配置
        /// </summary>
        /// <param name="skinName">皮肤唯一名（皮肤根目录名）</param>
        /// <param name="xmlDoc">皮肤配置文件XML文档对象</param>
        /// <param name="parameterDict">皮肤参数字典</param>
        private void LoadSkinParamters(DUISkinInfo skinInfo, XmlDocument xmlDoc, IDictionary<string, string> parameterDict)
        {
            if (xmlDoc == null || parameterDict == null)
            {
                return;
            }
            XmlElement skinSettingElement = xmlDoc.DocumentElement;
            if (skinSettingElement.Name != "skinSetting")
            {
                throw new Exception("加载皮肤目录" + skinInfo.SkinName + "中的皮肤配置文件出错，根标签不合法!");
            }

            foreach (XmlNode childNode in skinSettingElement.ChildNodes)
            {
                if (childNode.Name == "skinParameters")
                {
                    foreach (XmlNode imageNode in childNode.ChildNodes)
                    {
                        if (imageNode.Name == "parameter")
                        {
                            string parameterName = imageNode.Attributes["name"] != null ? imageNode.Attributes["name"].Value : null;
                            string parameterValue = imageNode.Attributes["value"] != null ? imageNode.Attributes["value"].Value : null;
                            if (string.IsNullOrEmpty(parameterName) || string.IsNullOrEmpty(parameterValue))
                            {
                                continue;
                            }

                            if (!parameterDict.ContainsKey(parameterName))
                            {
                                parameterDict.Add(parameterName, parameterValue);
                            }
                            else
                            {
                                parameterDict[parameterName] = parameterValue;
                            }
                        }
                    }
                }
            }
        }
        #endregion 

        #region private void LoadSkinStyles(DUISkinInfo skinInfo, XmlDocument xmlDoc, IDictionary<Type, IDUIStyle> styleDict)
        /// <summary>
        /// 读取皮肤控件样式配置
        /// </summary>
        /// <param name="skinName">当前皮肤信息对象</param>
        /// <param name="xmlDoc">皮肤配置文件XML文档对象</param>
        /// <param name="parameterDict">皮肤控件样式字典</param>
        private void LoadSkinStyles(DUISkinInfo skinInfo, XmlDocument xmlDoc, 
            IDictionary<string,DUIFont> fontDict, IDictionary<Type, IDictionary<string,IDUIStyle>> styleDict)
        {
            if (xmlDoc == null || fontDict == null || styleDict == null)
            {
                return;
            }
            XmlElement skinSettingElement = xmlDoc.DocumentElement;
            if (skinSettingElement.Name != "skinSetting")
            {
                throw new Exception("加载皮肤目录" + skinInfo.SkinName + "中的皮肤配置文件出错，根标签不合法!");
            }

            //遍历所有二级节点
            foreach (XmlNode childNode in skinSettingElement.ChildNodes)
            {
                //处理样式节点
                if (childNode.Name == "styles")
                {
                    foreach (XmlNode typeStyleNode in childNode.ChildNodes)
                    {
                        //全局Font
                        if (typeStyleNode.Name == "fonts")
                        {
                            #region 全局Font处理
                            foreach (XmlNode fontNode in typeStyleNode.ChildNodes)
                            {
                                if (fontNode.Name == "font")
                                {
                                    DUIFont newFont = new DUIFont(fontNode);


                                    if (fontDict.ContainsKey(newFont.Name))
                                    {
                                        fontDict[newFont.Name] = newFont;
                                    }
                                    else
                                    {
                                        fontDict.Add(newFont.Name, newFont);
                                    }
                                }
                            }
                            #endregion
                        }
                        //Label样式
                        else if (typeStyleNode.Name == "labelStyles")
                        {
                            #region labelStyles处理
                            foreach (XmlNode lableStyleNode in typeStyleNode.ChildNodes)
                            {
                                if (lableStyleNode.Name == "labelStyle")
                                {
                                    LabelDUIStyle labelDUIStyle = new LabelDUIStyle(lableStyleNode);

                                    IDictionary<string, IDUIStyle> tmpDUIStyleDict = null;
                                    if (styleDict.ContainsKey(typeof(DUIStyleLabel)))
                                    {
                                        tmpDUIStyleDict = styleDict[typeof(DUIStyleLabel)];
                                    }
                                    else
                                    {
                                        tmpDUIStyleDict = new Dictionary<string, IDUIStyle>();
                                        styleDict.Add(typeof(DUIStyleLabel), tmpDUIStyleDict);
                                    }

                                    if (tmpDUIStyleDict.ContainsKey(labelDUIStyle.Name))
                                    {
                                        tmpDUIStyleDict[labelDUIStyle.Name] = labelDUIStyle;
                                    }
                                    else
                                    {
                                        tmpDUIStyleDict.Add(labelDUIStyle.Name, labelDUIStyle);
                                    }
                                }
                            }
                            #endregion
                        }
                        //TextBox样式
                        else if (typeStyleNode.Name == "textboxStyles")
                        {
                            #region textboxStyles处理
                            foreach (XmlNode textboxStyleNode in typeStyleNode.ChildNodes)
                            {
                                if (textboxStyleNode.Name == "textboxStyle")
                                {
                                    TextBoxDUIStyle txtboxDUIStyle = new TextBoxDUIStyle(textboxStyleNode);

                                    IDictionary<string, IDUIStyle> tmpDUIStyleDict = null;
                                    if (styleDict.ContainsKey(typeof(DUIStyleTextBox)))
                                    {
                                        tmpDUIStyleDict = styleDict[typeof(DUIStyleTextBox)];
                                    }
                                    else
                                    {
                                        tmpDUIStyleDict = new Dictionary<string, IDUIStyle>();
                                        styleDict.Add(typeof(DUIStyleTextBox), tmpDUIStyleDict);
                                    }

                                    if (tmpDUIStyleDict.ContainsKey(txtboxDUIStyle.Name))
                                    {
                                        tmpDUIStyleDict[txtboxDUIStyle.Name] = txtboxDUIStyle;
                                    }
                                    else
                                    {
                                        tmpDUIStyleDict.Add(txtboxDUIStyle.Name, txtboxDUIStyle);
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (typeStyleNode.Name == "buttonStyles")
                        {
                            #region buttonStyles处理
                            foreach (XmlNode buttonStyleNode in typeStyleNode.ChildNodes)
                            {
                                if (buttonStyleNode.Name == "buttonStyle")
                                {
                                    ButtonDUIStyle buttonDUIStyle = new ButtonDUIStyle(buttonStyleNode);

                                    IDictionary<string, IDUIStyle> tmpDUIStyleDict = null;
                                    if (styleDict.ContainsKey(typeof(DUIStyleButton)))
                                    {
                                        tmpDUIStyleDict = styleDict[typeof(DUIStyleButton)];
                                    }
                                    else
                                    {
                                        tmpDUIStyleDict = new Dictionary<string, IDUIStyle>();
                                        styleDict.Add(typeof(DUIStyleButton), tmpDUIStyleDict);
                                    }

                                    if (tmpDUIStyleDict.ContainsKey(buttonDUIStyle.Name))
                                    {
                                        tmpDUIStyleDict[buttonDUIStyle.Name] = buttonDUIStyle;
                                    }
                                    else
                                    {
                                        tmpDUIStyleDict.Add(buttonDUIStyle.Name, buttonDUIStyle);
                                    }
                                }
                            }
                            #endregion
                        }
                        else
                        { }
                    }
                }
            }
        }
        #endregion 

        #region private void LoadSkinLayouts(SkinInfo skinInfo, IDictionary<string, string> layoutDict)
        /// <summary>
        /// 读取皮肤参数配置
        /// </summary>
        /// <param name="skinName">皮肤唯一名（皮肤根目录名）</param>
        /// <param name="parameterDict">皮肤参数字典</param>
        private void LoadSkinLayouts(DUISkinInfo skinInfo, IDictionary<string, DUILayout> layoutDict) 
        {
            if (layoutDict == null)
            {
                return;
            }
            foreach (string layoutFileName in Directory.GetFiles(skinInfo.SkinLayoutDirectory))
            {
                FileInfo fileInfo = new FileInfo(layoutFileName);
                if (fileInfo.Extension.ToLower().Equals(".layout"))
                {
                    DUILayout newLayout = new DUILayout(layoutFileName);

                    layoutDict.Add(newLayout.Name, newLayout);
                    
                }
            }
        }
        #endregion 

        #region 公共方法

        #region public static SkinManager CreateSkinManager(SkinInfo skinInfo)
        /// <summary>
        /// 创建指定皮肤唯一名的皮肤管理器，此方法调用后，新皮肤资源将载入，就皮肤资源将被释放
        /// </summary>
        /// <param name="skinName">皮肤唯一名（皮肤根目录名）</param>
        /// <param name="recreate">是否强制重新创建</param>
        /// <returns>皮肤管理器</returns>
        public static DUISkinManager CreateSkinManager(DUISkinInfo skinInfo, bool recreate)
        {
            if (recreate || _instance==null || _instance.CurrentSkinInfo == null ||
                !_instance.CurrentSkinInfo.SkinName.Equals(skinInfo.SkinName))
            {
                lock(_lockObj)
                {
                    //二次判断，以支持多线程
                    if (recreate || _instance==null || _instance.CurrentSkinInfo == null ||
                        !_instance.CurrentSkinInfo.SkinName.Equals(skinInfo.SkinName))
                    {
                        try
                        {
                            DUIForm.FlushBackgroundBitmapDict();
                            _instance = new DUISkinManager(skinInfo);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("换肤时产生异常：" + ex.Message);
                        }
                        
                        BroadCastSkinChangeEvent();

                    }
                }
            }
            return _instance;
        }
        #endregion

        #region public static SkinManager GetCurrentSkinManager()
        /// <summary>
        /// 获取当前皮肤管理器，从该管理器可以获取到最近一次载入的皮肤资源
        /// </summary>
        /// <returns>皮肤管理器对象</returns>
        public static DUISkinManager GetCurrentSkinManager()
        {
            return _instance;
        }
        #endregion

        #region public static IList<SkinInfo> GetSkinInfoList(bool refreshCache)
        /// <summary>
        /// 获取所有皮肤信息列表
        /// </summary>
        /// <param name="refreshCache">是否根据磁盘目录重新刷新皮肤信息列表</param>
        /// <returns>皮肤信息列表</returns>
        public static IList<DUISkinInfo> GetSkinInfoList(bool refreshCache)
        {
            if (_skinInfoList == null || refreshCache)
            {
                lock (_lockObj)
                {
                    if (_skinInfoList == null || refreshCache)
                    {
                        _skinInfoList = new List<DUISkinInfo>();

                        string tempAppBaseDirectory = CommonFunctions.GetAppBaseDirectory();

                        string tempSkinBaseDirectory = tempAppBaseDirectory.TrimEnd(new char[]{'\\'}) + "\\Skins";
                        foreach (string subDirectoryFullName in Directory.GetDirectories(tempSkinBaseDirectory))
                        {
                            string subDirectoryName = new DirectoryInfo(subDirectoryFullName).Name;
                            DUISkinInfo newSkinInfo = new DUISkinInfo(tempAppBaseDirectory, subDirectoryName);
                            if (newSkinInfo.CreateOK)
                            {
                                _skinInfoList.Add(newSkinInfo);
                            }
                        }
                    }
                }
            }
            return _skinInfoList;
        }
        #endregion

        #region public static void ProcessControlDUIStyle(Control control)
        /// <summary>
        /// 递归处理IDUIStyleControl的样式
        /// </summary>
        /// <param name="control">Control类型的对象</param>
        public static void ProcessControlDUIStyle(Control control)
        {
            Stack<Control> controlStack = new Stack<Control>();
            controlStack.Push(control);

            while (controlStack.Count > 0)
            {
                Control currentControl = controlStack.Pop();
                IDUIStyleControl duiStyleControl = currentControl as IDUIStyleControl;
                if (duiStyleControl != null)
                {
                    duiStyleControl.ProcessDUIStyle();
                }

                foreach (Control childControl in currentControl.Controls)
                {
                    DUIForm controlTest = childControl as DUIForm;
                    if (controlTest == null)    //子控件是DUIForm类型的，不做递归样式处理，因为这种类型的内嵌窗口会自己处理样式
                    {
                        controlStack.Push(childControl);
                    }
                }
            }
        }
        #endregion

        #region public static void AttachToDUIFrom(DUIForm form)
        /// <summary>
        /// 将DUIForm窗口实例附加到皮肤管理器对象
        /// 皮肤管理器对象维护一个DUIFrom对象列表，以作消息通知
        /// </summary>
        /// <param name="form">DUIFrom对象</param>
        public static void AttachToDUIFrom(DUIForm form)
        {
            if ((_referencedForms != null && form != null)
                && !_referencedForms.Contains(form))
            {
                _referencedForms.Add(form);
            }
        }
        #endregion

        #region public static void DetachFromDUIFrom(DUIForm form)
        /// <summary>
        /// 将DUIForm窗口实例从皮肤管理器维护的DUIFrom对象列表移除
        /// </summary>
        /// <param name="form">DUIFrom对象</param>
        public static void DetachFromDUIFrom(DUIForm form)
        {
            if ((_referencedForms != null && form != null)
                && _referencedForms.Contains(form))
            {
                _referencedForms.Remove(form);
            }
        }
        #endregion

        #region public static void BroadCastSkinChangeEvent()
        /// <summary>
        /// 向维护的DUIForm对象列表广播皮肤切换事件，使这些窗口的皮肤同步改变
        /// </summary>
        public static void BroadCastSkinChangeEvent()
        {
            if (_instance != null 
                && (_referencedForms != null && _referencedForms.Count > 0))
            {
                foreach (DUIForm form in _referencedForms)
                {
                    if (form != null && !form.IsDisposed)
                    {
                        form.OnSkinChanged(_instance);
                    }
                }

            }
        }
        #endregion

        #region public Image GetImageSource(string imageName)
        /// <summary>
        /// 获取当前皮肤指定名称的图片资源
        /// </summary>
        /// <param name="imageName">图片逻辑名</param>
        /// <returns>指定逻辑名的图片对象，若该名称的图片未加载，则抛出异常</returns>
        public Image GetImageSource(string imageName)
        {
            if (this._imageSources != null && this._imageSources.ContainsKey(imageName))
            {
                return this._imageSources[imageName];
            }
            else
            {
                throw new Exception(string.Format("未能获取到皮肤{0}({1})名为{2}的图片资源！",
                    this._currentSkinInfo.SkinDisplayName,
                    this._currentSkinInfo.SkinName,
                    imageName));
            }
        }
        #endregion

        #region public string GetParameter(string parameterName)
        /// <summary>
        /// 获取当前皮肤指定名称的参数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <returns>指定名称的参数值，总是一个字符串，使用者自行转换为自己需要的类型，
        /// 若该名称的参数不存在，则抛出异常</returns>
        public string GetParameter(string parameterName)
        {
            if (this._parameters != null && this._parameters.ContainsKey(parameterName))
            {
                return this._parameters[parameterName];
            }
            else
            {
                throw new Exception(string.Format("未能获取到皮肤{0}({1})名为{2}的配置参数！",
                   this._currentSkinInfo.SkinDisplayName,
                   this._currentSkinInfo.SkinName,
                   parameterName));
            }
        }
        #endregion

        #region public DUIFont GetFont(string fontName)
        /// <summary>
        /// 获取指定名称的皮肤全局字体
        /// </summary>
        /// <param name="fontName">字体名称</param>
        /// <returns>DUIFont字体对象，若找不到则抛出异常</returns>
        public DUIFont GetFont(string fontName)
        {
            if (string.IsNullOrEmpty(fontName) || this._fonts == null)
            {
                //throw new Exception(string.Format("未能获取到名为{0}的全局字体，皮肤字体字典为空！",fontName));
                return null;
            }
            if (!this._fonts.ContainsKey(fontName))
            {
                //throw new Exception(string.Format(
                //    "未能获取到名为{0}的全局字体，皮肤字体字典未找到该名称的字体！", fontName));
                return null;
            }
            return _fonts[fontName];
        }
        #endregion

        #region public IDUIStyle GetStyle(Type DUIControlType, string styleName)
        /// <summary>
        /// 获取当前皮肤配置中指定类型控件的指定名称的样式
        /// </summary>
        /// <param name="DUIControlType">控件类型</param>
        /// <param name="styleName">样式名</param>
        /// <returns>IDUIStyle接口对象,若不存在则返回null</returns>
        public IDUIStyle GetStyle(Type DUIControlType, string styleName)
        {
            if (this._styles == null)
            {
                return null;
            }
            if (!this._styles.ContainsKey(DUIControlType))
            {
                return null;
            }
            if(!this._styles[DUIControlType].ContainsKey(styleName))
            {
                return null;
            }

            return this._styles[DUIControlType][styleName];
        }
        #endregion

        #region public IDictionary<string,IDUIStyle> GetStyleDict(Type DUIControlType)
        /// <summary>
        /// 获取当前皮肤配置中指定类型控件的样式字典
        /// </summary>
        /// <param name="DUIControlType">控件类型</param>
        /// <returns>样式字典对象。若不存在则返回null</returns>
        public IDictionary<string,IDUIStyle> GetStyleDict(Type DUIControlType)
        {
            if (this._styles == null)
            {
                return null;
            }
            if (!this._styles.ContainsKey(DUIControlType))
            {
                return null;
            }
            
            return this._styles[DUIControlType];
        }
        #endregion

        #region public DUILayout GetLayoutCopy(string layoutName)
        /// <summary>
        /// 获取指定名称的布局对象的深度拷贝
        /// </summary>
        /// <param name="layoutName">布局名称</param>
        /// <returns>指定名称的布局对象，若该名称的布局不存在，则抛出异常</returns>
        private DUILayout GetLayoutCopy(string layoutName)
        {
            if (this._layouts != null && this._layouts.ContainsKey(layoutName))
            {
                return this._layouts[layoutName].Clone() as DUILayout;
            }
            else
            {
                throw new Exception(string.Format("未能获取到皮肤{0}({1})名为{2}的的布局信息！",
                   this._currentSkinInfo.SkinDisplayName,
                   this._currentSkinInfo.SkinName,
                   layoutName));
            }
        }
        #endregion

        #region public DUILayout GetLayout(Guid formUniqueID, string layoutName)
        /// <summary>
        /// 返回指定DUIForm窗口的DUILayout对象
        /// </summary>
        /// <param name="formUniqueID">窗口唯一标识</param>
        /// <param name="layoutName">布局名称</param>
        /// <returns></returns>
        public DUILayout GetLayout(Guid formUniqueID, string layoutName)
        {
            DUILayout retLayout = null;

            if (this._formLayouts != null)
            {
                if (this._formLayouts.ContainsKey(formUniqueID))
                {
                    if (this._formLayouts[formUniqueID].ContainsKey(layoutName))
                    {
                        retLayout = this._formLayouts[formUniqueID][layoutName];
                    }
                    else
                    {
                        //获得布局对象的拷贝
                        retLayout = GetLayoutCopy(layoutName);
                        //缓存到拷贝字典中
                        this._formLayouts[formUniqueID].Add(layoutName, retLayout);
                    }
                }
                else
                {
                    //创建二级字典
                    this._formLayouts.Add(formUniqueID, new Dictionary<string, DUILayout>());
                    //获得布局对象的拷贝
                    retLayout = GetLayoutCopy(layoutName);
                    //缓存到拷贝字典中
                    this._formLayouts[formUniqueID].Add(layoutName, retLayout);
                }
            }
            if (retLayout == null)
            {
                throw new Exception(string.Format("未能获取到窗口{0}名为{1}的的布局信息！",
                   formUniqueID, layoutName));
            }

            return retLayout;
        }
        #endregion

        #region public void RemoveLayout(Guid formUniqueID)
        /// <summary>
        /// 从皮肤管理器布局拷贝缓存中移除指定DUIForm窗口的DUILayout对象
        /// </summary>
        /// <param name="formUniqueID">窗口唯一标识</param>
        public void RemoveLayout(Guid formUniqueID)
        {
            if (this._formLayouts != null)
            {
                if (this._formLayouts.ContainsKey(formUniqueID))
                {
                    this._formLayouts.Remove(formUniqueID);
                }
            }
        }
        #endregion

        #endregion
    }
}
