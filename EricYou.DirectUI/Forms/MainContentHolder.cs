using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using EricYou.DirectUI.Utils;
using EricYou.DirectUI.Native;
using EricYou.DirectUI.Skin;

namespace EricYou.DirectUI.Forms
{
    /// <summary>
    /// 主内容窗口容器（支持内容切换）
    /// </summary>
    public partial class MainContentHolder : UserControl
    {
        /// <summary>
        /// 窗口字典
        /// </summary>
        private IDictionary<string, Form> _contentFormDict = new Dictionary<string, Form>();
        /// <summary>
        /// 窗口列表
        /// </summary>
        private IList<Form> _contentFormList = new List<Form>();

        /// <summary>
        /// 当前显示的窗口引用
        /// </summary>
        private Form _currentShowForm = null;
        /// <summary>
        /// 当前显示的窗口的标识名
        /// </summary>
        private string _currentShowFormName = string.Empty;

        /// <summary>
        /// 切换动画持续时间
        /// </summary>
        private int _switchMilliseconds = 150;
        /// <summary>
        /// 切换动画模式
        /// </summary>
        private WindowSwitchMode _switchMode = WindowSwitchMode.Horhorizontal;
        /// <summary>
        /// 切换动画方向
        /// </summary>
        private WindowSwitchDirection _switchDirection = WindowSwitchDirection.Positive;

        /// <summary>
        /// 全局切换标志，任何一个实例处于切换状态时，该标志被设置为true
        /// </summary>
        private static bool SwitchSignal = false;

        [Description("窗口切换动画的移出和移入持续时间")]
        public int SwitchMilliseconds
        {
            get { return _switchMilliseconds; }
            set { _switchMilliseconds = value; }
        }

        [Description("窗口切换动画的切换方式")]
        public WindowSwitchMode SwitchMode
        {
            get { return _switchMode; }
            set { _switchMode = value; }
        }

        [Description("窗口切换动画的切换方向")]
        public WindowSwitchDirection SwitchDirection
        {
            get { return _switchDirection; }
            set { _switchDirection = value; }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                //设置控件的WS_EX_COMPOSITED样式，使窗口背景刷新时控件不闪烁，但该标志会影响窗口滑动切换的动画效果
                //因此在MainContentHolder.SwitchToForm函数中，窗口动画开始前会禁用该标志，动画结束后重新打开该标志
                cp.ExStyle |= (int)NativeConst.WS_EX_COMPOSITED;  // Turn on WS_EX_COMPOSITED

                return cp;
            }
        } 

        /// <summary>
        /// 构造函数
        /// </summary>
        public MainContentHolder()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 向窗口容器中添加指定名称的窗口实例
        /// </summary>
        /// <param name="name">窗口名称</param>
        /// <param name="contentForm">窗口实例</param>
        public void AddContentForm(string name, Form contentForm)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new Exception("向MainContentHolder中添加了未命名的Form实例！");
            }
            if (contentForm == null)
            {
                throw new Exception("向MainContentHolder中添加了名为" + name + "空Form实例！");
            }

            contentForm.Dock = DockStyle.None;
            contentForm.FormBorderStyle = FormBorderStyle.None; //设置窗体无边框。
            contentForm.Location = new Point(0, 0);
            contentForm.Size = new Size(1, 1);
            contentForm.TopLevel = false;                       //设置不以顶级窗口显示。
            this.pnLoader.Controls.Add(contentForm);            //先加到pnLoader中，使窗口初次绘图
            contentForm.Show();                                 //窗口初次绘图

            //WindowAnimator.AnimateWindow(contentForm.Handle, 1, WindowAnimator.AW_HIDE | WindowAnimator.AW_SLIDE | WindowAnimator.AW_HOR_POSITIVE);
            this.pnMain.Controls.Add(contentForm);              //将窗口移动到主显示Panel中
            //contentForm.Dock = DockStyle.None ;               //设置窗口停靠模式为Fill
            contentForm.Size = new Size(1, 1);

            if (_contentFormDict.ContainsKey(name))             //如果新加入的窗口与已有窗口同名，则卸载旧窗口，安装新窗口
            {
                RemoveContentForm(name);
            }

            _contentFormDict.Add(name, contentForm);
            _contentFormList.Add(contentForm);

        }

        /// <summary>
        /// 从窗口容器容器中移除指定名称的窗口实例
        /// </summary>
        /// <param name="name">窗口名称</param>
        public void RemoveContentForm(string name)
        {
            if(_contentFormDict.ContainsKey(name)
                && _contentFormDict[name] != null)
            {
                RemoveContentForm(_contentFormDict[name]);
            }

        }

        /// <summary>
        /// 从窗口容器容器中移除指定实例的窗口
        /// </summary>
        /// <param name="contentForm">窗口实例</param>
        public void RemoveContentForm(Form contentForm)
        {
            if(contentForm!=null)
            {
                string formName = null;
                foreach(string key in _contentFormDict.Keys)
                {
                    if(_contentFormDict[key].Equals(contentForm))
                    {
                        formName = key;                             //获得指定窗口实例对应的窗口名
                    }
                }
                if(formName!=null)                                  //从窗口字典移除窗口
                {
                    _contentFormDict.Remove(formName);
                }
                _contentFormList.Remove(contentForm);               //从窗口列表移除窗口

                if(_currentShowForm == contentForm)                 //若移除的窗口实例是当前正在显示的窗口实例，则取消当前显示窗口变量的引用
                {
                    _currentShowForm = null;
                    _currentShowFormName = string.Empty;
                }

                this.pnMain.Controls.Remove(contentForm);           //从主显示panel中移除重复的窗口
                contentForm.Close();                                //关闭窗口
            }
        }

        /// <summary>
        /// 清除容器中所有窗口实例
        /// </summary>
        public void Clear()
        {
            List<Form> formList = new List<Form>();
            foreach(Form form in _contentFormDict.Values)
            {
                formList.Add(form);
            }

            formList.ForEach(f => {
                RemoveContentForm(f);
            });
        }

        public void OnContainerShown()
        {
            for (int i = 0; i < _contentFormList.Count; i++)
            {
                Form form = _contentFormList[i];
                if (form != null)
                {
                    //将窗口隐藏
                    WindowAnimator.AnimateWindow(form.Handle, 1, WindowAnimator.AW_HIDE | WindowAnimator.AW_SLIDE | WindowAnimator.AW_HOR_POSITIVE);
                }
            }
            //for (int i = 0; i < _contentFormList.Count; i++)
            //{
            //    Form form = _contentFormList[i];
            //    if (form != null)
            //    {
            //        this.pnMain.Controls.Add(form);  //将窗口移动到主显示Panel中
            //    }
            //}
            for (int i = 0; i < _contentFormList.Count; i++)
            {
                Form form = _contentFormList[i];
                if (form != null)
                {
                    form.Dock = DockStyle.Fill;      //设置窗口停靠模式为Fill
                }
            }
        }

        public void SwitchToForm(string name)
        {
            if (_currentShowFormName == name)
            {
                return;
            }

            Form toForm = null;
            if (_contentFormDict.ContainsKey(name))
            {
                toForm = _contentFormDict[name];
            }
            if (toForm == null)
            {
                throw new Exception("主内容区无法切换到名为" + name + "的Content窗口！");
            }

            //取消窗口WS_EX_COMPOSITED样式，以解决本控件切换内容窗口时图像卡顿的问题
            UnsetStyle(NativeConst.GWL_EXSTYLE, NativeConst.WS_EX_COMPOSITED);

            if (_currentShowForm != null)
            {
                int currentShowFormIndex = _contentFormList.IndexOf(_currentShowForm);
                int toFormIndex = _contentFormList.IndexOf(toForm);

                //toForm.Dock = DockStyle.Fill;

                //if (toFormIndex > currentShowFormIndex)
                //{
                //    WindowAnimator.AnimateWindow(_currentShowForm.Handle, _switchMilliseconds,
                //        WindowAnimator.AW_HIDE | WindowAnimator.AW_SLIDE | WindowAnimator.AW_HOR_NEGATIVE);
                //    WindowAnimator.AnimateWindow(toForm.Handle, _switchMilliseconds,
                //        WindowAnimator.AW_ACTIVATE | WindowAnimator.AW_SLIDE | WindowAnimator.AW_HOR_NEGATIVE);
                //}
                //else
                //{
                //    WindowAnimator.AnimateWindow(_currentShowForm.Handle, _switchMilliseconds,
                //        WindowAnimator.AW_HIDE | WindowAnimator.AW_SLIDE | WindowAnimator.AW_HOR_POSITIVE);
                //    WindowAnimator.AnimateWindow(toForm.Handle, _switchMilliseconds,
                //        WindowAnimator.AW_ACTIVATE | WindowAnimator.AW_SLIDE | WindowAnimator.AW_HOR_POSITIVE);
                //}
                 WindowAnimator.AnimateWindow(_currentShowForm.Handle, _switchMilliseconds,
                        WindowAnimator.AW_HIDE | GetWindowSwitchFlags(toFormIndex > currentShowFormIndex));
                 WindowAnimator.AnimateWindow(toForm.Handle, _switchMilliseconds,
                     WindowAnimator.AW_ACTIVATE | GetWindowSwitchFlags(toFormIndex > currentShowFormIndex));

                 //_currentShowForm.Dock = DockStyle.None;
                 //_currentShowForm.Location = new Point(0, 0);
                 //_currentShowForm.Size = new Size(1, 1);
            }
            else
            {
                //toForm.Dock = DockStyle.Fill;

                WindowAnimator.AnimateWindow(toForm.Handle, _switchMilliseconds,
                    WindowAnimator.AW_ACTIVATE | GetWindowSwitchFlags(true));
            }
            _currentShowForm = toForm;
            _currentShowFormName = name;

            //子窗口含有Tab和MainContentHolder时，切换时调用该接口函数，
            //完成其中下一级子窗体的延迟加载
            ISubContentHolder subContentHolder = _currentShowForm as ISubContentHolder;
            if (subContentHolder != null)
            {
                //执行下一级子窗体延迟加载
                subContentHolder.OnFirstShow();
            }

            //设置窗口WS_EX_COMPOSITED样式，以解决本控件单独刷新时闪烁的问题
            SetStyle(NativeConst.GWL_EXSTYLE, NativeConst.WS_EX_COMPOSITED);
        }

        public void SwitchToForm(string name, int switchFlags)
        {
            if (_currentShowFormName == name)
            {
                return;
            }

            Form toForm = null;
            if (_contentFormDict.ContainsKey(name))
            {
                toForm = _contentFormDict[name];
            }
            if (toForm == null)
            {
                throw new Exception("主内容区无法切换到名为" + name + "的Content窗口！");
            }

            //取消窗口WS_EX_COMPOSITED样式，以解决本控件切换内容窗口时图像卡顿的问题
            UnsetStyle(NativeConst.GWL_EXSTYLE, NativeConst.WS_EX_COMPOSITED);

            if (_currentShowForm != null)
            {

                //toForm.Dock = DockStyle.Fill;

                WindowAnimator.AnimateWindow(_currentShowForm.Handle, _switchMilliseconds,
                    WindowAnimator.AW_HIDE | switchFlags);
                WindowAnimator.AnimateWindow(toForm.Handle, _switchMilliseconds,
                    WindowAnimator.AW_ACTIVATE | switchFlags);

                //_currentShowForm.Dock = DockStyle.None;
                //_currentShowForm.Location = new Point(0, 0);
                //_currentShowForm.Size = new Size(1, 1);
            }
            else
            {
                //toForm.Dock = DockStyle.Fill;
                WindowAnimator.AnimateWindow(toForm.Handle, _switchMilliseconds,
                    WindowAnimator.AW_ACTIVATE | switchFlags);
            }
            _currentShowForm = toForm;
            _currentShowFormName = name;

            //子窗口含有Tab和MainContentHolder时，切换时调用该接口函数，
            //完成其中下一级子窗体的延迟加载
            ISubContentHolder subContentHolder = _currentShowForm as ISubContentHolder;
            if (subContentHolder != null)
            {
                //执行下一级子窗体延迟加载
                subContentHolder.OnFirstShow();
            }

            //设置窗口WS_EX_COMPOSITED样式，以解决本控件单独刷新时闪烁的问题
            SetStyle(NativeConst.GWL_EXSTYLE, NativeConst.WS_EX_COMPOSITED);
        }

        /// <summary>
        /// 直接显示指定名称的窗口
        /// </summary>
        /// <param name="name"></param>
        public void ShowForm(string name)
        {
            if (_currentShowFormName == name)
            {
                return;
            }

            Form toForm = null;
            if (_contentFormDict.ContainsKey(name))
            {
                toForm = _contentFormDict[name];
            }
            if (toForm == null)
            {
                throw new Exception("主内容区无法切换到名为" + name + "的Content窗口！");
            }

            //取消窗口WS_EX_COMPOSITED样式，以解决本控件切换内容窗口时图像卡顿的问题
            UnsetStyle(NativeConst.GWL_EXSTYLE, NativeConst.WS_EX_COMPOSITED);

            if (_currentShowForm != null)
            {
                NativeMethods.ShowWindow(_currentShowForm.Handle, NativeConst.SW_HIDE);
                NativeMethods.ShowWindow(toForm.Handle, NativeConst.SW_SHOW);
            }
            else
            {
                NativeMethods.ShowWindow(toForm.Handle, NativeConst.SW_SHOW);
            }
            _currentShowForm = toForm;
            _currentShowFormName = name;

            //子窗口含有Tab和MainContentHolder时，切换时调用该接口函数，
            //完成其中下一级子窗体的延迟加载
            ISubContentHolder subContentHolder = _currentShowForm as ISubContentHolder;
            if (subContentHolder != null)
            {
                //执行下一级子窗体延迟加载
                subContentHolder.OnFirstShow();
            }

            //设置窗口WS_EX_COMPOSITED样式，以解决本控件单独刷新时闪烁的问题
            SetStyle(NativeConst.GWL_EXSTYLE, NativeConst.WS_EX_COMPOSITED);
        }

        /// <summary>
        /// 获取窗口切换动画的参数
        /// </summary>
        /// <param name="dynamicSwitchDirection">动态切换方向，根据当前窗口与即将切换到的窗口的顺序决定</param>
        /// <returns></returns>
        private int GetWindowSwitchFlags(bool dynamicSwitchDirection)
        {
            int flag = 0;
            if (this._switchMode == WindowSwitchMode.Horhorizontal
                || this._switchMode == WindowSwitchMode.Vertical)
            {
                flag |= WindowAnimator.AW_SLIDE;
                if (this._switchMode == WindowSwitchMode.Vertical)
                {
                    if (this._switchDirection == WindowSwitchDirection.Positive)
                    {
                        if (dynamicSwitchDirection)
                        {
                            flag |= WindowAnimator.AW_VER_POSITIVE;
                        }
                        else
                        {
                            flag |= WindowAnimator.AW_VER_NEGATIVE;
                        }
                    }
                    else
                    {
                        if (dynamicSwitchDirection)
                        {
                            flag |= WindowAnimator.AW_VER_NEGATIVE;
                        }
                        else
                        {
                            flag |= WindowAnimator.AW_VER_POSITIVE;
                        }
                    }
                }
                else
                {
                    if (this._switchDirection == WindowSwitchDirection.Positive)
                    {
                        if (dynamicSwitchDirection)
                        {
                            flag |= WindowAnimator.AW_HOR_NEGATIVE;
                        }
                        else
                        {
                            flag |= WindowAnimator.AW_HOR_POSITIVE;
                        }
                    }
                    else
                    {
                        if (dynamicSwitchDirection)
                        {
                            flag |= WindowAnimator.AW_HOR_POSITIVE;
                        }
                        else
                        {
                            flag |= WindowAnimator.AW_HOR_NEGATIVE;
                        }
                    }
                }
            }
            else
            {
                flag |= WindowAnimator.AW_CENTER;
            }

            return flag;
        }

        public Form GetContentForm(string name)
        {
            if(_contentFormDict.ContainsKey(name))
            {
                return _contentFormDict[name] as Form;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 设置当前控件及其相同类型父控件的Window Style或者Extended Window Styles
        /// </summary>
        /// <param name="nIndex">偏移量，取GWL_和DWL_开头的常量</param>
        /// <param name="windowStyle">窗口样式常量，取WS_和WS_EX_开头的常量</param>
        public void SetStyle(int nIndex, long windowStyle)
        {
            Control currentControl = this;
            while (currentControl != null)
            {
                //如果当前控件是MainContentHolder类型控件，则设置该控件的样式
                MainContentHolder holderControl = currentControl as MainContentHolder;
                if (holderControl != null)
                {
                    CommonFunctions.SetWindowStyle(holderControl.Handle, nIndex, windowStyle);
                }
                
                ////如果当前控件是DUIForm类型控件，则设置该控件的样式
                //DUIForm duiForm = currentControl as DUIForm;
                //if (duiForm != null)
                //{
                //    CommonFunctions.SetWindowStyle(duiForm.Handle, nIndex, windowStyle);
                //}
                
                //继续处理父控件
                currentControl = currentControl.Parent;
            }
        }

        /// <summary>
        /// 取消设置当前控件及其相同类型父控件的Window Style或者Extended Window Styles
        /// </summary>
        /// <param name="nIndex">偏移量，取GWL_和DWL_开头的常量</param>
        /// <param name="windowStyle">窗口样式常量，取WS_和WS_EX_开头的常量</param>
        public void UnsetStyle(int nIndex, long windowStyle)
        {
            Control currentControl = this;
            while (currentControl != null)
            {
                //如果当前控件是MainContentHolder类型控件，则设置该控件的样式
                MainContentHolder holderControl = currentControl as MainContentHolder;
                if (holderControl != null)
                {
                    CommonFunctions.UnsetWindowStyle(holderControl.Handle, nIndex, windowStyle);
                }

                ////如果当前控件是DUIForm类型控件，则设置该控件的样式
                //DUIForm duiForm = currentControl as DUIForm;
                //if (duiForm != null)
                //{
                //    CommonFunctions.UnsetWindowStyle(duiForm.Handle, nIndex, windowStyle);
                //}

                //继续处理父控件
                currentControl = currentControl.Parent;
            }
        }

    }

    /// <summary>
    /// 窗口切换模式
    /// </summary>
    public enum WindowSwitchMode
    {
        /// <summary>
        /// 水平切换
        /// </summary>
        Horhorizontal,

        /// <summary>
        /// 垂直切换
        /// </summary>
        Vertical,

        /// <summary>
        /// 中心向外围切换
        /// </summary>
        Center
    }

    /// <summary>
    /// 窗口切换方向
    /// </summary>
    public enum WindowSwitchDirection
    {
        /// <summary>
        /// 正向
        /// </summary>
        Positive,

        /// <summary>
        /// 反向
        /// </summary>
        Nagetive
    }
}
