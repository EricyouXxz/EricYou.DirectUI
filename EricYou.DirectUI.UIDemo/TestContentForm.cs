using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using EricYou.DirectUI.Forms;

namespace EricYou.DirectUI.UIDemo
{
    public partial class TestContentForm : Form
    {

        public TestContentForm(string lableTest)
        {
            InitializeComponent();
            this.label1.Text = lableTest;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

        }

    }
}
