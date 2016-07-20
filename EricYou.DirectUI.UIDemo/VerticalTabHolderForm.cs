using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EricYou.DirectUI.Forms;
using EricYou.DirectUI.Skin.Buttons;
using EricYou.DirectUI.Utils;
using System.Drawing.Imaging;

namespace EricYou.DirectUI.UIDemo
{
    public partial class VerticalTabHolderForm : DUIForm
    {
        private DUIButton btVertical1 = null;
        private DUIButton btVertical2 = null;
        private DUIButton btVertical3 = null;
        private DUIButton btVertical4 = null;

        public VerticalTabHolderForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 此函数用于获取背景按钮对象，并绑定按钮事件，又皮肤框架调用
        /// </summary>
        protected override void ProcessUserButtons()
        {
            base.ProcessUserButtons();

            //获取背景按钮对象
            if (this.Layout.ButtonManager["btVertical"] != null)
            {
                btVertical1 = this.Layout.ButtonManager["btVertical"]["btVertical1"];
                btVertical2 = this.Layout.ButtonManager["btVertical"]["btVertical2"];
                btVertical3 = this.Layout.ButtonManager["btVertical"]["btVertical3"];
                btVertical4 = this.Layout.ButtonManager["btVertical"]["btVertical4"];
            }

            //绑定按钮事件
            if (btVertical1 != null)
            {
                btVertical1.SetClickEventHandler(OnbtVerticalClick);
            }
            if (btVertical2 != null)
            {
                btVertical2.SetClickEventHandler(OnbtVerticalClick);
            }
            if (btVertical3 != null)
            {
                btVertical3.SetClickEventHandler(OnbtVerticalClick);
            }
            if (btVertical4 != null)
            {
                btVertical4.SetClickEventHandler(OnbtVerticalClick);
            }
        }

        private void OnbtVerticalClick(DUIButton sender)
        {
            //MessageBox.Show("You click close button!这是一个Hold住的按钮！");

            //将同组的所有按钮置为非按下状态
            sender.OwnerGroup.ResetAllButtonHoldDownStatus();
            //将当前按钮置为按下状态
            sender.HoldDown = true;
            this.Update();  //此处调用一次窗口刷新函数，导致一次重绘，使按钮状态及时更新，提高用户体验


           this.mainContentHolder1.SwitchToForm(sender.Name.Substring(10, 1));
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e); //调用基类的OnShow函数，此函数中初始化了背景按钮，若不调用则会出错
            this.Update();   //调用窗口更新函数，可以使皮肤先于内容区窗口显示，提升用户体验

            //初始化内容区窗口
            this.mainContentHolder1.AddContentForm("1", new TestContentForm("【1号窗口】"));
            this.mainContentHolder1.AddContentForm("2", new TestContentForm("【2号窗口】"));
            this.mainContentHolder1.AddContentForm("3", new TestContentForm("【3号窗口】"));
            this.mainContentHolder1.AddContentForm("4", new TestContentForm("【4号窗口】"));;

            //该函数必须在内容区窗口添加完毕后调用，且必须在OnShown函数中调用，只能调用一次
            this.mainContentHolder1.OnContainerShown();

            //默认第一个一级按钮被按下，且显示第一个内容区窗口
            this.btVertical1.HoldDown = true;
            this.Invalidate();
            this.Update();
            this.mainContentHolder1.SwitchToForm("1", WindowAnimator.AW_CENTER);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = CommonFunctions.CaptureWindow(this);
            bitmap.Save(@"D:\1.png", ImageFormat.Png);
        }
    }
}
