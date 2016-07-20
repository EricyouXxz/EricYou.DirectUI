namespace EricYou.DirectUI.Skin.Designer
{
    partial class StyleNameSelectDropDownList
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbStyleList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lbStyleList
            // 
            this.lbStyleList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbStyleList.FormattingEnabled = true;
            this.lbStyleList.ItemHeight = 12;
            this.lbStyleList.Location = new System.Drawing.Point(0, 0);
            this.lbStyleList.Name = "lbStyleList";
            this.lbStyleList.Size = new System.Drawing.Size(146, 136);
            this.lbStyleList.TabIndex = 0;
            this.lbStyleList.Click += new System.EventHandler(this.lbLayoutList_Click);
            // 
            // StyleNameSelectDropDownList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbStyleList);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "StyleNameSelectDropDownList";
            this.Size = new System.Drawing.Size(146, 136);
            this.Load += new System.EventHandler(this.LayoutSelectDropDownList_Load);
            this.Leave += new System.EventHandler(this.LayoutSelectDropDownList_Leave);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbStyleList;

    }
}
