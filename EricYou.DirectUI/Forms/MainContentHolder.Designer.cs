namespace EricYou.DirectUI.Forms
{
    partial class MainContentHolder
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
            this.pnLoader = new System.Windows.Forms.Panel();
            this.pnMain = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnLoader
            // 
            this.pnLoader.Location = new System.Drawing.Point(0, 0);
            this.pnLoader.Name = "pnLoader";
            this.pnLoader.Size = new System.Drawing.Size(1, 1);
            this.pnLoader.TabIndex = 0;
            // 
            // pnMain
            // 
            this.pnMain.BackColor = System.Drawing.Color.Transparent;
            this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMain.Location = new System.Drawing.Point(0, 0);
            this.pnMain.Margin = new System.Windows.Forms.Padding(0);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(319, 226);
            this.pnMain.TabIndex = 1;
            // 
            // MainContentHolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnMain);
            this.Controls.Add(this.pnLoader);
            this.Name = "MainContentHolder";
            this.Size = new System.Drawing.Size(319, 226);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnLoader;
        private System.Windows.Forms.Panel pnMain;
    }
}
