namespace WebCrawler
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tBsearchtxt = new System.Windows.Forms.TextBox();
            this.btsearch = new System.Windows.Forms.Button();
            this.lVEmailUrl = new System.Windows.Forms.ListView();
            this.email = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sourceURL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lVUrl = new System.Windows.Forms.ListView();
            this.checkedURL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tBsearchtxt
            // 
            this.tBsearchtxt.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tBsearchtxt.Location = new System.Drawing.Point(37, 48);
            this.tBsearchtxt.Multiline = true;
            this.tBsearchtxt.Name = "tBsearchtxt";
            this.tBsearchtxt.Size = new System.Drawing.Size(629, 50);
            this.tBsearchtxt.TabIndex = 0;
            // 
            // btsearch
            // 
            this.btsearch.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btsearch.Location = new System.Drawing.Point(672, 48);
            this.btsearch.Name = "btsearch";
            this.btsearch.Size = new System.Drawing.Size(90, 50);
            this.btsearch.TabIndex = 1;
            this.btsearch.Text = "搜索";
            this.btsearch.UseVisualStyleBackColor = true;
            this.btsearch.Click += new System.EventHandler(this.btsearch_Click);
            // 
            // lVEmailUrl
            // 
            this.lVEmailUrl.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.email,
            this.sourceURL});
            this.lVEmailUrl.GridLines = true;
            this.lVEmailUrl.HideSelection = false;
            this.lVEmailUrl.Location = new System.Drawing.Point(37, 132);
            this.lVEmailUrl.Name = "lVEmailUrl";
            this.lVEmailUrl.Size = new System.Drawing.Size(725, 459);
            this.lVEmailUrl.TabIndex = 2;
            this.lVEmailUrl.UseCompatibleStateImageBehavior = false;
            this.lVEmailUrl.View = System.Windows.Forms.View.Details;
            // 
            // email
            // 
            this.email.Text = "email";
            this.email.Width = 350;
            // 
            // sourceURL
            // 
            this.sourceURL.Text = "sourceURL";
            this.sourceURL.Width = 375;
            // 
            // lVUrl
            // 
            this.lVUrl.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.checkedURL});
            this.lVUrl.GridLines = true;
            this.lVUrl.HideSelection = false;
            this.lVUrl.Location = new System.Drawing.Point(761, 132);
            this.lVUrl.Name = "lVUrl";
            this.lVUrl.Size = new System.Drawing.Size(382, 459);
            this.lVUrl.TabIndex = 3;
            this.lVUrl.UseCompatibleStateImageBehavior = false;
            this.lVUrl.View = System.Windows.Forms.View.Details;
            // 
            // checkedURL
            // 
            this.checkedURL.Text = "checkedURL";
            this.checkedURL.Width = 400;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox1.Location = new System.Drawing.Point(869, 69);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(134, 29);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "百度搜索";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox2.Location = new System.Drawing.Point(1009, 69);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(134, 29);
            this.checkBox2.TabIndex = 5;
            this.checkBox2.Text = "必应搜索";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 628);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.lVUrl);
            this.Controls.Add(this.lVEmailUrl);
            this.Controls.Add(this.btsearch);
            this.Controls.Add(this.tBsearchtxt);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tBsearchtxt;
        private System.Windows.Forms.Button btsearch;
        private System.Windows.Forms.ListView lVEmailUrl;
        private System.Windows.Forms.ColumnHeader email;
        private System.Windows.Forms.ColumnHeader sourceURL;
        private System.Windows.Forms.ListView lVUrl;
        private System.Windows.Forms.ColumnHeader checkedURL;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}

