using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EricYou.DirectUI.Skin.Buttons;
using EricYou.DirectUI.Utils;
using EricYou.DirectUI.Forms;
using EricYou.DirectUI.Native;
using System.Drawing.Imaging;

namespace EricYou.DirectUI.UIDemo
{
    public partial class MainForm : DUIForm
    {
        private DUIButton btMenuButton1 = null;
        private DUIButton btMenuButton2 = null;
        private DUIButton btMenuButton3 = null;
        private DUIButton btMenuButton4 = null;
        private DUIButton btMenuButton5 = null;
        private DUIButton btMenuButton6 = null;
        private DUIButton btMenuButton7= null;
        private DUIButton btMenuButton8 = null;

        public MainForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// 此函数用于获取背景按钮对象，并绑定按钮事件，又皮肤框架调用
        /// </summary>
        protected override void ProcessUserButtons()
        {
            base.ProcessUserButtons();

            //获取背景按钮对象
            if (this.Layout.ButtonManager["Menu"] != null)
            {
                btMenuButton1 = this.Layout.ButtonManager["Menu"]["btMenu1"];
                btMenuButton2 = this.Layout.ButtonManager["Menu"]["btMenu2"];
                btMenuButton3 = this.Layout.ButtonManager["Menu"]["btMenu3"];
                btMenuButton4 = this.Layout.ButtonManager["Menu"]["btMenu4"];
                btMenuButton5 = this.Layout.ButtonManager["Menu"]["btMenu5"];
                btMenuButton6 = this.Layout.ButtonManager["Menu"]["btMenu6"];
                btMenuButton7 = this.Layout.ButtonManager["Menu"]["btMenu7"];
                btMenuButton8 = this.Layout.ButtonManager["Menu"]["btMenu8"];
            }

            //绑定按钮事件
            if (btMenuButton1 != null)
            {
                btMenuButton1.SetClickEventHandler(OnMenuButtonClick);
            }
            if (btMenuButton2 != null)
            {
                btMenuButton2.SetClickEventHandler(OnMenuButtonClick);
            }
            if (btMenuButton3 != null)
            {
                btMenuButton3.SetClickEventHandler(OnMenuButtonClick);
            }
            if (btMenuButton4 != null)
            {
                btMenuButton4.SetClickEventHandler(OnMenuButtonClick);
            }
            if (btMenuButton5 != null)
            {
                btMenuButton5.SetClickEventHandler(OnMenuButtonClick);
            }
            if (btMenuButton6 != null)
            {
                btMenuButton6.SetClickEventHandler(OnMenuButtonClick);
            }
            if (btMenuButton7 != null)
            {
                btMenuButton7.SetClickEventHandler(OnMenuButtonClick);
            }
            if (btMenuButton8 != null)
            {
                btMenuButton8.SetClickEventHandler(OnMenuButtonClick);
            }
           
        }

        private void OnMenuButtonClick(DUIButton sender)
        {
            //MessageBox.Show("You click close button!这是一个Hold住的按钮！");

            //将同组的所有按钮置为非按下状态
            sender.OwnerGroup.ResetAllButtonHoldDownStatus();
            //将当前按钮置为按下状态
            sender.HoldDown = true;
            this.Update();  //此处调用一次窗口刷新函数，导致一次重绘，使按钮状态及时更新，提高用户体验


            this.mainContentHolder.SwitchToForm(sender.Name.Substring(6, 1)) ;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e); //调用基类的OnShow函数，此函数中初始化了背景按钮，若不调用则会出错
            this.Update();   //调用窗口更新函数，可以使皮肤先于内容区窗口显示，提升用户体验

            //初始化内容区窗口
            this.mainContentHolder.AddContentForm("1", new TestContentForm("【1号窗口】"));
            this.mainContentHolder.AddContentForm("2", new TestContentForm("【2号窗口】"));
            this.mainContentHolder.AddContentForm("3", new TestContentForm("【3号窗口】"));
            this.mainContentHolder.AddContentForm("4", new TestContentForm("【4号窗口】"));
            this.mainContentHolder.AddContentForm("5", new TestContentForm("【5号窗口】"));
            this.mainContentHolder.AddContentForm("6", new TestContentForm("【6号窗口】"));
            this.mainContentHolder.AddContentForm("7", new TestContentForm("【7号窗口】"));
            this.mainContentHolder.AddContentForm("8", new TestContentForm("【8号窗口】"));

            //该函数必须在内容区窗口添加完毕后调用，且必须在OnShown函数中调用，只能调用一次
            this.mainContentHolder.OnContainerShown();

            //默认第一个一级按钮被按下，且显示第一个内容区窗口
            this.btMenuButton1.HoldDown = true;
            this.mainContentHolder.SwitchToForm("1", WindowAnimator.AW_SLIDE | WindowAnimator.AW_VER_POSITIVE);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Font font = new Font(new FontFamily("微软雅黑"), 20, FontStyle.Bold);
            
            //label1.Font = font;
            //label1.ForeColor = Color.Transparent;
            //this.textBox2.Font = font;
            //this.textBox2.ForeColor = Color.Red;

            //IntPtr vHandle = NativeMethods.GetWindowDC(this.Handle);
            //Graphics g = Graphics.FromHdc(vHandle);
          
            //PaintDeactive(g); //失去焦点时，窗口标题栏泛白显示
            //MessageBox.Show("abc");

            //g.Dispose();
            //NativeMethods.ReleaseDC(this.Handle, vHandle);
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
           
        //   PaintDeactive(e.Graphics); //失去焦点时，窗口标题栏泛白显示

          
        //}

        //private void MainForm_Deactivate(object sender, EventArgs e)
        //{
        //    DUIForm form = this.mainContentHolder.GetContentForm("1");
        //    if (form != null)
        //    {
        //        form.BActived = false;
                
        //        form.Refresh();
                
        //    }
        //}

        //private void MainForm_Activated(object sender, EventArgs e)
        //{
        //    DUIForm form = this.mainContentHolder.GetContentForm("1");
        //    if (form != null)
        //    {
        //        form.BActived = true;
        //        form.Refresh();
        //    }
        //}
    }
}
