namespace VideoDirectXPlayer
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.bt_open = new System.Windows.Forms.Button();
            this.bt_play = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.bt_pre = new System.Windows.Forms.Button();
            this.bt_next = new System.Windows.Forms.Button();
            this.tb_srtcontent = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(552, 346);
            this.panel1.TabIndex = 0;
            // 
            // bt_open
            // 
            this.bt_open.Location = new System.Drawing.Point(22, 380);
            this.bt_open.Name = "bt_open";
            this.bt_open.Size = new System.Drawing.Size(80, 38);
            this.bt_open.TabIndex = 1;
            this.bt_open.Text = "打开";
            this.bt_open.Click += new System.EventHandler(this.bt_open_Click_1);
            // 
            // bt_play
            // 
            this.bt_play.Location = new System.Drawing.Point(125, 380);
            this.bt_play.Name = "bt_play";
            this.bt_play.Size = new System.Drawing.Size(81, 38);
            this.bt_play.TabIndex = 1;
            this.bt_play.Text = "播放";
            this.bt_play.Click += new System.EventHandler(this.bt_play_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "avi";
            this.openFileDialog1.Filter = "视频文件|*.avi;*.mp4";
            this.openFileDialog1.Title = "请选择播放的AVI文件";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 353);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(540, 21);
            this.textBox1.TabIndex = 2;
            // 
            // bt_pre
            // 
            this.bt_pre.Location = new System.Drawing.Point(244, 380);
            this.bt_pre.Name = "bt_pre";
            this.bt_pre.Size = new System.Drawing.Size(81, 38);
            this.bt_pre.TabIndex = 1;
            this.bt_pre.Text = "快退";
            this.bt_pre.Click += new System.EventHandler(this.bt_pre_Click_1);
            // 
            // bt_next
            // 
            this.bt_next.Location = new System.Drawing.Point(351, 380);
            this.bt_next.Name = "bt_next";
            this.bt_next.Size = new System.Drawing.Size(80, 38);
            this.bt_next.TabIndex = 1;
            this.bt_next.Text = "快进";
            this.bt_next.Click += new System.EventHandler(this.bt_next_Click);
            // 
            // tb_srtcontent
            // 
            this.tb_srtcontent.Location = new System.Drawing.Point(36, 447);
            this.tb_srtcontent.Multiline = true;
            this.tb_srtcontent.Name = "tb_srtcontent";
            this.tb_srtcontent.Size = new System.Drawing.Size(484, 52);
            this.tb_srtcontent.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(552, 511);
            this.Controls.Add(this.tb_srtcontent);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.bt_next);
            this.Controls.Add(this.bt_open);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bt_pre);
            this.Controls.Add(this.bt_play);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Visual C#中使用DriectX实现媒体播放";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bt_open;
        private System.Windows.Forms.Button bt_play;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button bt_pre;
        private System.Windows.Forms.Button bt_next;
        private System.Windows.Forms.TextBox tb_srtcontent;
    }
}

