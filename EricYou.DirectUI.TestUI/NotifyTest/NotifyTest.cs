using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EricYou.DirectUI.Forms.Notification;

namespace EricYou.DirectUI.TestUI.NotifyTest
{
    public partial class NotifyTest : Form
    {
        public NotifyTest()
        {
            InitializeComponent();

            NotificationService.RegisterNotifyEventHandler("Clear", OnEventClear);
        }

        private void btnOpenNewForm_Click(object sender, EventArgs e)
        {
            NotifyTest testForm = new NotifyTest();
            testForm.Show();
        }

        //--------------------------------------------------------------
        private void btnRigisterA_Click(object sender, EventArgs e)
        {
            NotificationService.RegisterNotifyEventHandler("A", OnEventA);
        }

        private void btnRigisterB_Click(object sender, EventArgs e)
        {
            NotificationService.RegisterNotifyEventHandler("B", OnEventB);
        }

        private void btnRigisterC_Click(object sender, EventArgs e)
        {
            NotificationService.RegisterNotifyEventHandler("C", OnEventC);
        }

        //--------------------------------------------------------------
        private void OnEventA(object message)
        {
            txbNotifyMessage.Text += "Event A : " + message.ToString() + "\r\n";
        }

        private void OnEventB(object message)
        {
            txbNotifyMessage.Text += "Event B : " + message.ToString() + "\r\n";
        }

        private void OnEventC(object message)
        {
            txbNotifyMessage.Text += "Event C : " + message.ToString() + "\r\n";
        }

        //--------------------------------------------------------------

        private void btnDerigisterA_Click(object sender, EventArgs e)
        {
            NotificationService.DeregisterNotifyEventHandler("A", OnEventA);
        }

        private void btnDerigisterB_Click(object sender, EventArgs e)
        {
            NotificationService.DeregisterNotifyEventHandler("B", OnEventB);
        }

        private void btnDerigisterC_Click(object sender, EventArgs e)
        {
            NotificationService.DeregisterNotifyEventHandler("C", OnEventC);
        }

        //--------------------------------------------------------------

        private void btnNotifyA_Click(object sender, EventArgs e)
        {
            NotificationService.Notify("A", txbMessage.Text);
        }

        private void btnNotifyB_Click(object sender, EventArgs e)
        {
            NotificationService.Notify("B", txbMessage.Text);
        }

        private void btnNotifyC_Click(object sender, EventArgs e)
        {
            NotificationService.Notify("C", txbMessage.Text);
        }

        //---------------------------------------------------------------
        private void btnClear_Click(object sender, EventArgs e)
        {
            NotificationService.Notify("Clear", null);
        }

        private void OnEventClear(object message)
        {
            txbNotifyMessage.Text = string.Empty;
        }

        private void NotifyTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            NotificationService.DeregisterNotifyEventHandler("Clear", OnEventClear);
        }



        

    }
}
