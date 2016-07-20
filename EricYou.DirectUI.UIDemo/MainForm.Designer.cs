namespace EricYou.DirectUI.UIDemo
{
    partial class MainForm
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
            this.mainContentHolder = new EricYou.DirectUI.Forms.MainContentHolder();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainContentHolder
            // 
            this.mainContentHolder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainContentHolder.BackColor = System.Drawing.Color.Transparent;
            this.mainContentHolder.Location = new System.Drawing.Point(104, 154);
            this.mainContentHolder.Name = "mainContentHolder";
            this.mainContentHolder.Size = new System.Drawing.Size(939, 558);
            this.mainContentHolder.SwitchDirection = EricYou.DirectUI.Forms.WindowSwitchDirection.Positive;
            this.mainContentHolder.SwitchMilliseconds = 200;
            this.mainContentHolder.SwitchMode = EricYou.DirectUI.Forms.WindowSwitchMode.Vertical;
            this.mainContentHolder.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button1.Enabled = false;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.Location = new System.Drawing.Point(283, 452);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(189, 53);
            this.button1.TabIndex = 1;
            this.button1.Text = "登录";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1138, 844);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.mainContentHolder);
            this.FormSize = new System.Drawing.Size(1138, 844);
            this.MinimumSize = new System.Drawing.Size(1026, 739);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private EricYou.DirectUI.Forms.MainContentHolder mainContentHolder;
        private System.Windows.Forms.Button button1;
    }
}