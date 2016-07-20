using System.Windows.Forms;
using EricYou.DirectUI.Skin.Styles.Controls;

namespace EricYou.DirectUI.TestUI
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new EricYou.DirectUI.Skin.Styles.Controls.DUIStyleLabel();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new EricYou.DirectUI.Skin.Styles.Controls.DUIStyleLabel();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.textBox1 = new EricYou.DirectUI.Skin.Styles.Controls.DUIStyleTextBox();
            this.textBox2 = new EricYou.DirectUI.Skin.Styles.Controls.DUIStyleTextBox();
            this.label3 = new EricYou.DirectUI.Skin.Styles.Controls.DUIStyleLabel();
            this.label4 = new EricYou.DirectUI.Skin.Styles.Controls.DUIStyleLabel();
            this.duiStyleButton1 = new EricYou.DirectUI.Skin.Styles.Controls.DUIStyleButton();
            this.button1 = new System.Windows.Forms.Button();
            this.duiButtonEx1 = new EricYou.DirectUI.Forms.Extentions.DUIButtonEx();
            this.duiButtonEx2 = new EricYou.DirectUI.Forms.Extentions.DUIButtonEx();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(119, 47);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
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
            this.listView1.Location = new System.Drawing.Point(12, 197);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(452, 130);
            this.listView1.TabIndex = 2;
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
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aToolStripMenuItem,
            this.bToolStripMenuItem,
            this.cToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(85, 70);
            // 
            // aToolStripMenuItem
            // 
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Size = new System.Drawing.Size(84, 22);
            this.aToolStripMenuItem.Text = "a";
            // 
            // bToolStripMenuItem
            // 
            this.bToolStripMenuItem.Name = "bToolStripMenuItem";
            this.bToolStripMenuItem.Size = new System.Drawing.Size(84, 22);
            this.bToolStripMenuItem.Text = "b";
            // 
            // cToolStripMenuItem
            // 
            this.cToolStripMenuItem.Name = "cToolStripMenuItem";
            this.cToolStripMenuItem.Size = new System.Drawing.Size(84, 22);
            this.cToolStripMenuItem.Text = "c";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.DUIStyleName = "Blue";
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(48, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "切换主题：";
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button2.Location = new System.Drawing.Point(479, 236);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(210, 532);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "这几个是Hold住的按钮：";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Gray;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.DUIStyleName = "default";
            this.textBox1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.textBox1.ForeColor = System.Drawing.Color.Gray;
            this.textBox1.Location = new System.Drawing.Point(119, 83);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 23);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "Test1";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.Gray;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.DUIStyleName = "default";
            this.textBox2.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.textBox2.ForeColor = System.Drawing.Color.Gray;
            this.textBox2.Location = new System.Drawing.Point(119, 112);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 23);
            this.textBox2.TabIndex = 9;
            this.textBox2.Text = "Test2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.DUIStyleName = "Red";
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(64, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Test1：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.DUIStyleName = "Red";
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(64, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Test2：";
            // 
            // duiStyleButton1
            // 
            this.duiStyleButton1.BackColor = System.Drawing.Color.Gray;
            this.duiStyleButton1.DUIStyleName = "default";
            this.duiStyleButton1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.duiStyleButton1.ForeColor = System.Drawing.Color.Black;
            this.duiStyleButton1.Location = new System.Drawing.Point(244, 77);
            this.duiStyleButton1.Name = "duiStyleButton1";
            this.duiStyleButton1.Size = new System.Drawing.Size(109, 26);
            this.duiStyleButton1.TabIndex = 10;
            this.duiStyleButton1.Text = "确定";
            this.duiStyleButton1.UseVisualStyleBackColor = false;
            this.duiStyleButton1.Click += new System.EventHandler(this.duiStyleButton1_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(479, 197);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // duiButtonEx1
            // 
            this.duiButtonEx1.Location = new System.Drawing.Point(110, 347);
            this.duiButtonEx1.Name = "duiButtonEx1";
            this.duiButtonEx1.Size = new System.Drawing.Size(130, 30);
            this.duiButtonEx1.TabIndex = 12;
            this.duiButtonEx1.Text = "duiButtonEx1";
            // 
            // duiButtonEx2
            // 
            this.duiButtonEx2.BackImageSourceNameDown = "VertivalButton_Down";
            this.duiButtonEx2.BackImageSourceNameHover = "VertivalButton_Hover";
            this.duiButtonEx2.BackImageSourceNameNormal = "VertivalButton_Normal";
            this.duiButtonEx2.Location = new System.Drawing.Point(431, 47);
            this.duiButtonEx2.Name = "duiButtonEx2";
            this.duiButtonEx2.Size = new System.Drawing.Size(134, 46);
            this.duiButtonEx2.TabIndex = 13;
            this.duiButtonEx2.Text = "abcc";
            this.duiButtonEx2.TextFontNameDown = "verticalButtonFont_Down";
            this.duiButtonEx2.TextFontNameHover = "verticalButtonFont_Hover";
            this.duiButtonEx2.TextFontNameNormal = "verticalButtonFont_Normal";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 452);
            this.Controls.Add(this.duiButtonEx2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.duiStyleButton1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.duiButtonEx1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.FormSize = new System.Drawing.Size(630, 462);
            this.Location = new System.Drawing.Point(700, 300);
            this.Name = "Form1";
            this.ShowDebugMessage = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "中华人民共和国";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cToolStripMenuItem;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button button2;
        private EricYou.DirectUI.Skin.Styles.Controls.DUIStyleTextBox textBox1;
        private EricYou.DirectUI.Skin.Styles.Controls.DUIStyleTextBox textBox2;
        private DUIStyleLabel label2;
        private DUIStyleLabel label3;
        private DUIStyleLabel label4;
        private DUIStyleLabel label1;
        private DUIStyleButton duiStyleButton1;
        private Button button1;
        private Forms.Extentions.DUIButtonEx duiButtonEx1;
        private Forms.Extentions.DUIButtonEx duiButtonEx2;








    }
}

