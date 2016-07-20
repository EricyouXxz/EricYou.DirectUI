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
            this.SuspendLayout();
            // 
            // mainContentHolder
            // 
            this.mainContentHolder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainContentHolder.BackColor = System.Drawing.Color.Transparent;
            this.mainContentHolder.Location = new System.Drawing.Point(0, 111);
            this.mainContentHolder.Name = "mainContentHolder";
            this.mainContentHolder.Size = new System.Drawing.Size(1146, 686);
            this.mainContentHolder.SwitchDirection = EricYou.DirectUI.Forms.WindowSwitchDirection.Positive;
            this.mainContentHolder.SwitchMilliseconds = 200;
            this.mainContentHolder.SwitchMode = EricYou.DirectUI.Forms.WindowSwitchMode.Vertical;
            this.mainContentHolder.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 852);
            this.Controls.Add(this.mainContentHolder);
            this.FormSize = new System.Drawing.Size(1146, 852);
            this.MinimumSize = new System.Drawing.Size(1026, 739);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private EricYou.DirectUI.Forms.MainContentHolder mainContentHolder;
    }
}