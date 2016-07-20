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

namespace EricYou.DirectUI.UIDemo
{
    public partial class HorizontalTabHolderForm : DUIForm
    {
        private DUIButton btHorizontal1 = null;
        private DUIButton btHorizontal2 = null;
        private DUIButton btHorizontal3= null;
        private DUIButton btHorizontal4 = null;

        public HorizontalTabHolderForm()
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
            if (this.Layout.ButtonManager["btHorizontal"] != null)
            {
                btHorizontal1 = this.Layout.ButtonManager["btHorizontal"]["btHorizontal1"];
                btHorizontal2 = this.Layout.ButtonManager["btHorizontal"]["btHorizontal2"];
                btHorizontal3 = this.Layout.ButtonManager["btHorizontal"]["btHorizontal3"];
                btHorizontal4 = this.Layout.ButtonManager["btHorizontal"]["btHorizontal4"];
            }

            //绑定按钮事件
            if (btHorizontal1 != null)
            {
                btHorizontal1.SetClickEventHandler(OnbtHorizontalClick);
            }
            if (btHorizontal2 != null)
            {
                btHorizontal2.SetClickEventHandler(OnbtHorizontalClick);
            }
            if (btHorizontal3 != null)
            {
                btHorizontal3.SetClickEventHandler(OnbtHorizontalClick);
            }
            if (btHorizontal4 != null)
            {
                btHorizontal4.SetClickEventHandler(OnbtHorizontalClick);
            }
        }

        private void OnbtHorizontalClick(DUIButton sender)
        {
            //MessageBox.Show("You click close button!这是一个Hold住的按钮！");

            //将同组的所有按钮置为非按下状态
            sender.OwnerGroup.ResetAllButtonHoldDownStatus();
            //将当前按钮置为按下状态
            sender.HoldDown = true;
            this.Update();  //此处调用一次窗口刷新函数，导致一次重绘，使按钮状态及时更新，提高用户体验


           this.mainContentHolder1.SwitchToForm(sender.Name.Substring(12, 1));
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
            this.btHorizontal1.HoldDown = true;
            this.Invalidate();
            this.Update();
            this.mainContentHolder1.SwitchToForm("1", WindowAnimator.AW_CENTER);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btVertical_Click(object sender, EventArgs e)
        {
            this.ShowWindowShade();
            this.Update();
            VerticalTabHolderForm form = new VerticalTabHolderForm();
            form.ShowDialog();
            this.CloseWindowShade();
        }

    }
}
