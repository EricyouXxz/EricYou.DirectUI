using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EricYou.DirectUI.Forms;
using EricYou.DirectUI.Utils;
using System.Configuration;
using EricYou.DirectUI.Skin;
using EricYou.DirectUI.Skin.Buttons;

namespace EricYou.DirectUI.TestUI
{
    public partial class Form1 : EricYou.DirectUI.Forms.DUIForm
    {

        private EricYou.DirectUI.Skin.Buttons.DUIButton maxButton2 = null;
        private EricYou.DirectUI.Skin.Buttons.DUIButton minButton2 = null;
        private EricYou.DirectUI.Skin.Buttons.DUIButton closeButton2 = null;

        public Form1()
        {
            InitializeComponent();

            this.comboBox1.DataSource = DUISkinManager.GetSkinInfoList(true);
            this.comboBox1.DisplayMember = "SkinDisplayName";
            this.comboBox1.ValueMember = "SkinName";
            this.comboBox1.SelectedIndex = 0;
            
            foreach (DUISkinInfo skinInfo in this.comboBox1.Items)
            {
                if (skinInfo.SkinName == Program.skinName)
                {
                    this.comboBox1.SelectedItem = skinInfo;
                }
            }
        }

        protected override void ProcessUserButtons()
        {
            base.ProcessUserButtons();

            //获取背景按钮对象
            if (this.Layout.ButtonManager["TestGroup"] != null)
            {
                maxButton2 = this.Layout.ButtonManager["TestGroup"]["btMax2"];
                minButton2 = this.Layout.ButtonManager["TestGroup"]["btMin2"];
                closeButton2 = this.Layout.ButtonManager["TestGroup"]["btClose2"];
            }

            if (maxButton2 != null)
            {
                maxButton2.SetClickEventHandler(OnMaxButton2Click);
            }
            if (minButton2 != null)
            {
                minButton2.SetClickEventHandler(OnMiniButton2Click);
            }
            if (closeButton2 != null)
            {
                closeButton2.SetClickEventHandler(OnCloseButton2Click);
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //this.Size = new System.Drawing.Size(600, 432);
        }

        public override void OnSkinChanged(DUISkinManager skinManager)
        {
            base.OnSkinChanged(skinManager);

            int listView1LocationX 
                = Convert.ToInt32(DUISkinManager.GetCurrentSkinManager().GetParameter("listview1-location-x"));
            int listView1LocationY
                = Convert.ToInt32(DUISkinManager.GetCurrentSkinManager().GetParameter("listview1-location-y"));
            this.listView1.Location = new System.Drawing.Point(listView1LocationX, listView1LocationY);

            int listView1LocationWidth
                 = Convert.ToInt32(DUISkinManager.GetCurrentSkinManager().GetParameter("listview1-location-width"));
            int listView1LocationHeight
                 = Convert.ToInt32(DUISkinManager.GetCurrentSkinManager().GetParameter("listview1-location-height"));
            this.listView1.Size = new System.Drawing.Size(listView1LocationWidth, listView1LocationHeight);

            int button1LocationX
                = Convert.ToInt32(DUISkinManager.GetCurrentSkinManager().GetParameter("button1-location-x"));
            int button1LocationY
                = Convert.ToInt32(DUISkinManager.GetCurrentSkinManager().GetParameter("button1-location-y"));
            this.button1.Location = new Point(button1LocationX, button1LocationY);

            int button2LocationX
                = Convert.ToInt32(DUISkinManager.GetCurrentSkinManager().GetParameter("button2-location-x"));
            int button2LocationY
                = Convert.ToInt32(DUISkinManager.GetCurrentSkinManager().GetParameter("button2-location-y"));
            this.button2.Location = new Point(button2LocationX, button2LocationY);


        }

        //protected override void DrawBackGround(Graphics g)
        //{
        //    base.DrawBackGround(g);
        //    g.DrawImage(new Icon(this.Icon,96,96).ToBitmap(),60,60);
        //}

        private void button1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(new Icon(this.Icon, 96, 96).ToBitmap(), 60, 60);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ChangeSkinTo(this.comboBox1.SelectedItem as DUISkinInfo);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Click OK!");
            Form1 form = new Form1();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.UniqueID.ToString()+" Click Cancel!");
        }


        private void OnCloseButton2Click(EricYou.DirectUI.Skin.Buttons.DUIButton sender)
        {
            //MessageBox.Show("You click close button!这是一个Hold住的按钮！");
            sender.OwnerGroup.ResetAllButtonHoldDownStatus();
            sender.HoldDown = true;
        }

        private void OnMiniButton2Click(EricYou.DirectUI.Skin.Buttons.DUIButton sender)
        {
            //MessageBox.Show("You click mini button!这是一个Hold住的按钮！");
            sender.OwnerGroup.ResetAllButtonHoldDownStatus();
            sender.HoldDown = true;
        }

        private void OnMaxButton2Click(EricYou.DirectUI.Skin.Buttons.DUIButton sender)
        {
            //MessageBox.Show("You click max button!这是一个Hold住的按钮！");
            sender.OwnerGroup.ResetAllButtonHoldDownStatus();
            sender.HoldDown = true;
        }

        private void duiStyleButton1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
        }
    }
}
