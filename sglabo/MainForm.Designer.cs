namespace sglabo
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.captureButton = new System.Windows.Forms.Button();
            this.detectColorButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.activateButton1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.refleshButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.startButton = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // captureButton
            // 
            this.captureButton.Location = new System.Drawing.Point(459, 12);
            this.captureButton.Name = "captureButton";
            this.captureButton.Size = new System.Drawing.Size(75, 23);
            this.captureButton.TabIndex = 0;
            this.captureButton.Text = "Capture";
            this.captureButton.UseVisualStyleBackColor = true;
            this.captureButton.Click += new System.EventHandler(this.captureButton_Click);
            // 
            // detectColorButton
            // 
            this.detectColorButton.Location = new System.Drawing.Point(459, 41);
            this.detectColorButton.Name = "detectColorButton";
            this.detectColorButton.Size = new System.Drawing.Size(75, 23);
            this.detectColorButton.TabIndex = 2;
            this.detectColorButton.Text = "Detect";
            this.detectColorButton.UseVisualStyleBackColor = true;
            this.detectColorButton.Click += new System.EventHandler(this.detectColorButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 264);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(546, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(32, 17);
            this.statusLabel.Text = "gl hf";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.activateButton1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(440, 43);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "proc1";
            // 
            // activateButton1
            // 
            this.activateButton1.Location = new System.Drawing.Point(285, 12);
            this.activateButton1.Name = "activateButton1";
            this.activateButton1.Size = new System.Drawing.Size(75, 23);
            this.activateButton1.TabIndex = 8;
            this.activateButton1.Text = "Activate";
            this.activateButton1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "戦士",
            "騎士",
            "盗賊",
            "忍者",
            "格闘",
            "幻闘",
            "精霊",
            "召喚",
            "黒印",
            "錬金",
            "守護",
            "次元"});
            this.comboBox1.Location = new System.Drawing.Point(201, 14);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(66, 20);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.Text = "Job";
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(6, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(180, 16);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // refleshButton
            // 
            this.refleshButton.Location = new System.Drawing.Point(459, 70);
            this.refleshButton.Name = "refleshButton";
            this.refleshButton.Size = new System.Drawing.Size(75, 23);
            this.refleshButton.TabIndex = 5;
            this.refleshButton.Text = "Reflesh";
            this.refleshButton.UseVisualStyleBackColor = true;
            this.refleshButton.Click += new System.EventHandler(this.refleshButton_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(459, 100);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 6;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 286);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.refleshButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.detectColorButton);
            this.Controls.Add(this.captureButton);
            this.Name = "MainForm";
            this.Text = "sglabo";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button captureButton;
        private System.Windows.Forms.Button detectColorButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button activateButton1;
        private System.Windows.Forms.Button refleshButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button startButton;
    }
}

