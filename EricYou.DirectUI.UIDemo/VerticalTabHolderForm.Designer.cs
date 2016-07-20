namespace EricYou.DirectUI.UIDemo
{
    partial class VerticalTabHolderForm
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
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainContentHolder1
            // 
            this.mainContentHolder1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainContentHolder1.BackColor = System.Drawing.Color.Transparent;
            this.mainContentHolder1.Location = new System.Drawing.Point(135, 34);
            this.mainContentHolder1.Margin = new System.Windows.Forms.Padding(0);
            this.mainContentHolder1.Name = "mainContentHolder1";
            this.mainContentHolder1.Size = new System.Drawing.Size(549, 340);
            this.mainContentHolder1.SwitchDirection = EricYou.DirectUI.Forms.WindowSwitchDirection.Positive;
            this.mainContentHolder1.SwitchMilliseconds = 150;
            this.mainContentHolder1.SwitchMode = EricYou.DirectUI.Forms.WindowSwitchMode.Vertical;
            this.mainContentHolder1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(77, 339);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(46, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "关闭";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(25, 339);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(46, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "截图";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // VerticalTabHolderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 374);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.mainContentHolder1);
            this.FormSize = new System.Drawing.Size(683, 374);
            this.LayoutName = "verticalTabHolder.layout";
            this.Name = "VerticalTabHolderForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);

        }

        #endregion

        private Forms.MainContentHolder mainContentHolder1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}