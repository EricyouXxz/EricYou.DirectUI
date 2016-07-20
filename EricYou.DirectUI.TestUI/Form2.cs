using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EricYou.DirectUI.Forms;

namespace EricYou.DirectUI.TestUI
{
    public partial class Form2 : DUIForm
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void duiButtonEx1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("hello world!");  
        }
    }
}
