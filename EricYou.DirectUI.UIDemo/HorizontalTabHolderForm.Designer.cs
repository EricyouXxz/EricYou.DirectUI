namespace EricYou.DirectUI.UIDemo
{
    partial class HorizontalTabHolderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainContentHolder1 = new EricYou.DirectUI.Forms.MainContentHolder();
            this.button1 = new System.Windows.Forms.Button();
            this.btVertical = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainContentHolder1
            // 
            this.mainContentHolder1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainContentHolder1.BackColor = System.Drawing.Color.Transparent;
            this.mainContentHolder1.Location = new System.Drawing.Point(0, 33);
            this.mainContentHolder1.Margin = new System.Windows.Forms.Padding(0);
            this.mainContentHolder1.Name = "mainContentHolder1";
            this.mainContentHolder1.Size = new System.Drawing.Size(627, 265);
            this.mainContentHolder1.SwitchDirection = EricYou.DirectUI.Forms.WindowSwitchDirection.Nagetive;
            this.mainContentHolder1.SwitchMilliseconds = 150;
            this.mainContentHolder1.SwitchMode = EricYou.DirectUI.Forms.WindowSwitchMode.Horhorizontal;
            this.mainContentHolder1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(569, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(46, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "关闭";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btVertical
            // 
            this.btVertical.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btVertical.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btVertical.Location = new System.Drawing.Point(499, 7);
            this.btVertical.Name = "btVertical";
            this.btVertical.Size = new System.Drawing.Size(64, 23);
            this.btVertical.TabIndex = 2;
            this.btVertical.Text = "纵向导航";
            this.btVertical.UseVisualStyleBackColor = true;
            this.btVertical.Click += new System.EventHandler(this.btVertical_Click);
            // 
            // HorizontalTabHolderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 300);
            this.Controls.Add(this.btVertical);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.mainContentHolder1);
            this.FormSize = new System.Drawing.Size(627, 300);
            this.LayoutName = "horizontalTabHolder.layout";
            this.Name = "HorizontalTabHolderForm";
            this.ShowIcon = false;
            this.ResumeLayout(false);

        }

        #endregion

        private Forms.MainContentHolder mainContentHolder1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btVertical;
    }
}