namespace EricYou.DirectUI.TestUI
{
    partial class RoundPanelTestForm
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
            this.components = new System.ComponentModel.Container();
            this.duiRoundPanel1 = new EricYou.DirectUI.Forms.Extentions.DUIRoundPanel(this.components);
            this.SuspendLayout();
            // 
            // duiRoundPanel1
            // 
            this.duiRoundPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.duiRoundPanel1.AreaColor = System.Drawing.Color.White;
            this.duiRoundPanel1.BackColor = System.Drawing.Color.Transparent;
            this.duiRoundPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.duiRoundPanel1.BorderColor = System.Drawing.Color.Silver;
            this.duiRoundPanel1.BorderWidth = 3;
            this.duiRoundPanel1.Location = new System.Drawing.Point(145, 96);
            this.duiRoundPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.duiRoundPanel1.Name = "duiRoundPanel1";
            this.duiRoundPanel1.RoundRadius = 8;
            this.duiRoundPanel1.Size = new System.Drawing.Size(223, 55);
            this.duiRoundPanel1.TabIndex = 0;
            // 
            // RoundPanelTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(490, 248);
            this.Controls.Add(this.duiRoundPanel1);
            this.Name = "RoundPanelTestForm";
            this.Text = "RoundPanelTestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Forms.Extentions.DUIRoundPanel duiRoundPanel1;
    }
}