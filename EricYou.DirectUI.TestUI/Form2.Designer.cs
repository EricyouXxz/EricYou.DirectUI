namespace EricYou.DirectUI.TestUI
{
    partial class Form2
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
            this.duiButtonEx1 = new EricYou.DirectUI.Forms.Extentions.DUIButtonEx();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.duiRoundPanel1 = new EricYou.DirectUI.Forms.Extentions.DUIRoundPanel(this.components);
            this.SuspendLayout();
            // 
            // duiButtonEx1
            // 
            this.duiButtonEx1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.duiButtonEx1.BackImageSourceNameDown = "VertivalButton_Down";
            this.duiButtonEx1.BackImageSourceNameHover = "VertivalButton_Hover";
            this.duiButtonEx1.BackImageSourceNameNormal = "VertivalButton_Normal";
            this.duiButtonEx1.Location = new System.Drawing.Point(28, 120);
            this.duiButtonEx1.Name = "duiButtonEx1";
            this.duiButtonEx1.Size = new System.Drawing.Size(134, 46);
            this.duiButtonEx1.TabIndex = 0;
            this.duiButtonEx1.Text = "sdfasd";
            this.duiButtonEx1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.duiButtonEx1.TextFontNameDown = "verticalButtonFont_Down";
            this.duiButtonEx1.TextFontNameHover = "verticalButtonFont_Hover";
            this.duiButtonEx1.TextFontNameNormal = "verticalButtonFont_Normal";
            this.duiButtonEx1.TextOffsetX = 33;
            this.duiButtonEx1.Click += new System.EventHandler(this.duiButtonEx1_Click);
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(178, 50);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(406, 225);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 125;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 117;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Width = 130;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Width = 117;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Width = 108;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(28, 186);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(78, 16);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // duiRoundPanel1
            // 
            this.duiRoundPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.duiRoundPanel1.AreaColor = System.Drawing.Color.White;
            this.duiRoundPanel1.BackColor = System.Drawing.Color.Transparent;
            this.duiRoundPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.duiRoundPanel1.BorderColor = System.Drawing.Color.Gray;
            this.duiRoundPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.duiRoundPanel1.BorderWidth = 3;
            this.duiRoundPanel1.Location = new System.Drawing.Point(106, 308);
            this.duiRoundPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.duiRoundPanel1.Name = "duiRoundPanel1";
            this.duiRoundPanel1.RoundRadius = 6;
            this.duiRoundPanel1.Size = new System.Drawing.Size(222, 43);
            this.duiRoundPanel1.TabIndex = 3;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 455);
            this.Controls.Add(this.duiRoundPanel1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.duiButtonEx1);
            this.FormSize = new System.Drawing.Size(719, 455);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Forms.Extentions.DUIButtonEx duiButtonEx1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.CheckBox checkBox1;
        private Forms.Extentions.DUIRoundPanel duiRoundPanel1;
    }
}