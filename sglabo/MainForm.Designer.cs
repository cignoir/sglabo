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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.captureButton = new System.Windows.Forms.Button();
            this.detectColorButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.activateButton1 = new System.Windows.Forms.Button();
            this.jobSelector1 = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.refleshButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.startButton = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.areaSelector = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.activateButton2 = new System.Windows.Forms.Button();
            this.jobSelector2 = new System.Windows.Forms.ComboBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.activateButton3 = new System.Windows.Forms.Button();
            this.jobSelector3 = new System.Windows.Forms.ComboBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.activateButton4 = new System.Windows.Forms.Button();
            this.jobSelector4 = new System.Windows.Forms.ComboBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.activateButton5 = new System.Windows.Forms.Button();
            this.jobSelector5 = new System.Windows.Forms.ComboBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.captureWindow = new System.Windows.Forms.Button();
            this.codeLabel1 = new System.Windows.Forms.Label();
            this.codeLabel2 = new System.Windows.Forms.Label();
            this.codeLabel3 = new System.Windows.Forms.Label();
            this.codeLabel4 = new System.Windows.Forms.Label();
            this.codeLabel5 = new System.Windows.Forms.Label();
            this.mapPictureBox = new System.Windows.Forms.PictureBox();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // captureButton
            // 
            this.captureButton.Location = new System.Drawing.Point(532, 10);
            this.captureButton.Name = "captureButton";
            this.captureButton.Size = new System.Drawing.Size(94, 23);
            this.captureButton.TabIndex = 0;
            this.captureButton.Text = "CaptureMap";
            this.captureButton.UseVisualStyleBackColor = true;
            this.captureButton.Click += new System.EventHandler(this.captureButton_Click);
            // 
            // detectColorButton
            // 
            this.detectColorButton.Location = new System.Drawing.Point(532, 99);
            this.detectColorButton.Name = "detectColorButton";
            this.detectColorButton.Size = new System.Drawing.Size(94, 23);
            this.detectColorButton.TabIndex = 2;
            this.detectColorButton.Text = "Detect";
            this.detectColorButton.UseVisualStyleBackColor = true;
            this.detectColorButton.Click += new System.EventHandler(this.detectColorButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 306);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(637, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.activateButton1);
            this.groupBox1.Controls.Add(this.jobSelector1);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(379, 43);
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
            this.activateButton1.Click += new System.EventHandler(this.activateButton1_Click);
            // 
            // jobSelector1
            // 
            this.jobSelector1.FormattingEnabled = true;
            this.jobSelector1.Items.AddRange(new object[] {
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
            this.jobSelector1.Location = new System.Drawing.Point(201, 14);
            this.jobSelector1.Name = "jobSelector1";
            this.jobSelector1.Size = new System.Drawing.Size(66, 20);
            this.jobSelector1.TabIndex = 7;
            this.jobSelector1.Text = "精霊";
            this.jobSelector1.SelectedIndexChanged += new System.EventHandler(this.jobSelector1_SelectedIndexChanged);
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
            this.refleshButton.Location = new System.Drawing.Point(532, 128);
            this.refleshButton.Name = "refleshButton";
            this.refleshButton.Size = new System.Drawing.Size(94, 23);
            this.refleshButton.TabIndex = 5;
            this.refleshButton.Text = "RefleshList";
            this.refleshButton.UseVisualStyleBackColor = true;
            this.refleshButton.Click += new System.EventHandler(this.refleshButton_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(531, 156);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(94, 23);
            this.startButton.TabIndex = 6;
            this.startButton.Text = "Start (ALT+S)";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // timer2
            // 
            this.timer2.Interval = 60000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(451, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(75, 19);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "573,248,40,40";
            // 
            // areaSelector
            // 
            this.areaSelector.FormattingEnabled = true;
            this.areaSelector.Items.AddRange(new object[] {
            "ルデンヌ大森林",
            "イレネイド山脈",
            "ナビア北限地帯",
            "シュロ陸峡",
            "グランドロン"});
            this.areaSelector.Location = new System.Drawing.Point(54, 12);
            this.areaSelector.Name = "areaSelector";
            this.areaSelector.Size = new System.Drawing.Size(121, 20);
            this.areaSelector.TabIndex = 8;
            this.areaSelector.Text = "ナビア北限地帯";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "エリア";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.activateButton2);
            this.groupBox2.Controls.Add(this.jobSelector2);
            this.groupBox2.Controls.Add(this.pictureBox2);
            this.groupBox2.Location = new System.Drawing.Point(12, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(379, 43);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "proc2";
            // 
            // activateButton2
            // 
            this.activateButton2.Location = new System.Drawing.Point(285, 12);
            this.activateButton2.Name = "activateButton2";
            this.activateButton2.Size = new System.Drawing.Size(75, 23);
            this.activateButton2.TabIndex = 8;
            this.activateButton2.Text = "Activate";
            this.activateButton2.UseVisualStyleBackColor = true;
            this.activateButton2.Click += new System.EventHandler(this.activateButton2_Click);
            // 
            // jobSelector2
            // 
            this.jobSelector2.FormattingEnabled = true;
            this.jobSelector2.Items.AddRange(new object[] {
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
            this.jobSelector2.Location = new System.Drawing.Point(201, 14);
            this.jobSelector2.Name = "jobSelector2";
            this.jobSelector2.Size = new System.Drawing.Size(66, 20);
            this.jobSelector2.TabIndex = 7;
            this.jobSelector2.Text = "戦士";
            this.jobSelector2.SelectedIndexChanged += new System.EventHandler(this.jobSelector2_SelectedIndexChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.ErrorImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(6, 18);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(180, 16);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.activateButton3);
            this.groupBox3.Controls.Add(this.jobSelector3);
            this.groupBox3.Controls.Add(this.pictureBox3);
            this.groupBox3.Location = new System.Drawing.Point(12, 149);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(379, 43);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "proc3";
            // 
            // activateButton3
            // 
            this.activateButton3.Location = new System.Drawing.Point(285, 12);
            this.activateButton3.Name = "activateButton3";
            this.activateButton3.Size = new System.Drawing.Size(75, 23);
            this.activateButton3.TabIndex = 8;
            this.activateButton3.Text = "Activate";
            this.activateButton3.UseVisualStyleBackColor = true;
            this.activateButton3.Click += new System.EventHandler(this.activateButton3_Click);
            // 
            // jobSelector3
            // 
            this.jobSelector3.FormattingEnabled = true;
            this.jobSelector3.Items.AddRange(new object[] {
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
            this.jobSelector3.Location = new System.Drawing.Point(201, 14);
            this.jobSelector3.Name = "jobSelector3";
            this.jobSelector3.Size = new System.Drawing.Size(66, 20);
            this.jobSelector3.TabIndex = 7;
            this.jobSelector3.Text = "守護";
            this.jobSelector3.SelectedIndexChanged += new System.EventHandler(this.jobSelector3_SelectedIndexChanged);
            // 
            // pictureBox3
            // 
            this.pictureBox3.ErrorImage = null;
            this.pictureBox3.Location = new System.Drawing.Point(6, 18);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(180, 16);
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.activateButton4);
            this.groupBox4.Controls.Add(this.jobSelector4);
            this.groupBox4.Controls.Add(this.pictureBox4);
            this.groupBox4.Location = new System.Drawing.Point(12, 198);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(379, 43);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "proc4";
            // 
            // activateButton4
            // 
            this.activateButton4.Location = new System.Drawing.Point(285, 12);
            this.activateButton4.Name = "activateButton4";
            this.activateButton4.Size = new System.Drawing.Size(75, 23);
            this.activateButton4.TabIndex = 8;
            this.activateButton4.Text = "Activate";
            this.activateButton4.UseVisualStyleBackColor = true;
            this.activateButton4.Click += new System.EventHandler(this.activateButton4_Click);
            // 
            // jobSelector4
            // 
            this.jobSelector4.FormattingEnabled = true;
            this.jobSelector4.Items.AddRange(new object[] {
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
            this.jobSelector4.Location = new System.Drawing.Point(201, 14);
            this.jobSelector4.Name = "jobSelector4";
            this.jobSelector4.Size = new System.Drawing.Size(66, 20);
            this.jobSelector4.TabIndex = 7;
            this.jobSelector4.Text = "黒印";
            this.jobSelector4.SelectedIndexChanged += new System.EventHandler(this.jobSelector4_SelectedIndexChanged);
            // 
            // pictureBox4
            // 
            this.pictureBox4.ErrorImage = null;
            this.pictureBox4.Location = new System.Drawing.Point(6, 18);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(180, 16);
            this.pictureBox4.TabIndex = 5;
            this.pictureBox4.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.activateButton5);
            this.groupBox5.Controls.Add(this.jobSelector5);
            this.groupBox5.Controls.Add(this.pictureBox5);
            this.groupBox5.Location = new System.Drawing.Point(12, 247);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(379, 43);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "proc5";
            // 
            // activateButton5
            // 
            this.activateButton5.Location = new System.Drawing.Point(285, 12);
            this.activateButton5.Name = "activateButton5";
            this.activateButton5.Size = new System.Drawing.Size(75, 23);
            this.activateButton5.TabIndex = 8;
            this.activateButton5.Text = "Activate";
            this.activateButton5.UseVisualStyleBackColor = true;
            this.activateButton5.Click += new System.EventHandler(this.activateButton5_Click);
            // 
            // jobSelector5
            // 
            this.jobSelector5.FormattingEnabled = true;
            this.jobSelector5.Items.AddRange(new object[] {
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
            this.jobSelector5.Location = new System.Drawing.Point(201, 14);
            this.jobSelector5.Name = "jobSelector5";
            this.jobSelector5.Size = new System.Drawing.Size(66, 20);
            this.jobSelector5.TabIndex = 7;
            this.jobSelector5.Text = "盗賊";
            this.jobSelector5.SelectedIndexChanged += new System.EventHandler(this.jobSelector5_SelectedIndexChanged);
            // 
            // pictureBox5
            // 
            this.pictureBox5.ErrorImage = null;
            this.pictureBox5.Location = new System.Drawing.Point(6, 18);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(180, 16);
            this.pictureBox5.TabIndex = 5;
            this.pictureBox5.TabStop = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.radioButton5);
            this.groupBox6.Controls.Add(this.radioButton4);
            this.groupBox6.Controls.Add(this.radioButton3);
            this.groupBox6.Controls.Add(this.radioButton2);
            this.groupBox6.Controls.Add(this.radioButton1);
            this.groupBox6.Location = new System.Drawing.Point(401, 50);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(25, 240);
            this.groupBox6.TabIndex = 14;
            this.groupBox6.TabStop = false;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(6, 214);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(14, 13);
            this.radioButton5.TabIndex = 17;
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(6, 165);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(14, 13);
            this.radioButton4.TabIndex = 16;
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(6, 116);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(14, 13);
            this.radioButton3.TabIndex = 15;
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 67);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(14, 13);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 17);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(14, 13);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(407, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "C";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.checkBox5);
            this.groupBox7.Controls.Add(this.checkBox4);
            this.groupBox7.Controls.Add(this.checkBox3);
            this.groupBox7.Controls.Add(this.checkBox2);
            this.groupBox7.Controls.Add(this.checkBox1);
            this.groupBox7.Location = new System.Drawing.Point(432, 50);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(25, 240);
            this.groupBox7.TabIndex = 16;
            this.groupBox7.TabStop = false;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Checked = true;
            this.checkBox5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox5.Location = new System.Drawing.Point(6, 214);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(15, 14);
            this.checkBox5.TabIndex = 20;
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Checked = true;
            this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox4.Location = new System.Drawing.Point(6, 165);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(15, 14);
            this.checkBox4.TabIndex = 19;
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(6, 116);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(15, 14);
            this.checkBox3.TabIndex = 18;
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(6, 67);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 17;
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(6, 17);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(438, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "A";
            // 
            // captureWindow
            // 
            this.captureWindow.Location = new System.Drawing.Point(532, 40);
            this.captureWindow.Name = "captureWindow";
            this.captureWindow.Size = new System.Drawing.Size(93, 23);
            this.captureWindow.TabIndex = 18;
            this.captureWindow.Text = "CaptureWindow";
            this.captureWindow.UseVisualStyleBackColor = true;
            this.captureWindow.Click += new System.EventHandler(this.captureWindow_Click);
            // 
            // codeLabel1
            // 
            this.codeLabel1.AutoSize = true;
            this.codeLabel1.Location = new System.Drawing.Point(472, 67);
            this.codeLabel1.Name = "codeLabel1";
            this.codeLabel1.Size = new System.Drawing.Size(0, 12);
            this.codeLabel1.TabIndex = 19;
            // 
            // codeLabel2
            // 
            this.codeLabel2.AutoSize = true;
            this.codeLabel2.Location = new System.Drawing.Point(472, 117);
            this.codeLabel2.Name = "codeLabel2";
            this.codeLabel2.Size = new System.Drawing.Size(0, 12);
            this.codeLabel2.TabIndex = 20;
            // 
            // codeLabel3
            // 
            this.codeLabel3.AutoSize = true;
            this.codeLabel3.Location = new System.Drawing.Point(472, 166);
            this.codeLabel3.Name = "codeLabel3";
            this.codeLabel3.Size = new System.Drawing.Size(0, 12);
            this.codeLabel3.TabIndex = 21;
            // 
            // codeLabel4
            // 
            this.codeLabel4.AutoSize = true;
            this.codeLabel4.Location = new System.Drawing.Point(472, 215);
            this.codeLabel4.Name = "codeLabel4";
            this.codeLabel4.Size = new System.Drawing.Size(0, 12);
            this.codeLabel4.TabIndex = 22;
            // 
            // codeLabel5
            // 
            this.codeLabel5.AutoSize = true;
            this.codeLabel5.Location = new System.Drawing.Point(472, 264);
            this.codeLabel5.Name = "codeLabel5";
            this.codeLabel5.Size = new System.Drawing.Size(0, 12);
            this.codeLabel5.TabIndex = 23;
            // 
            // mapPictureBox
            // 
            this.mapPictureBox.Location = new System.Drawing.Point(532, 194);
            this.mapPictureBox.Name = "mapPictureBox";
            this.mapPictureBox.Size = new System.Drawing.Size(94, 96);
            this.mapPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mapPictureBox.TabIndex = 24;
            this.mapPictureBox.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 328);
            this.Controls.Add(this.mapPictureBox);
            this.Controls.Add(this.codeLabel5);
            this.Controls.Add(this.codeLabel4);
            this.Controls.Add(this.codeLabel3);
            this.Controls.Add(this.codeLabel2);
            this.Controls.Add(this.codeLabel1);
            this.Controls.Add(this.captureWindow);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.areaSelector);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.refleshButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.detectColorButton);
            this.Controls.Add(this.captureButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "sglabo";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapPictureBox)).EndInit();
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
        private System.Windows.Forms.ComboBox jobSelector1;
        private System.Windows.Forms.Button activateButton1;
        private System.Windows.Forms.Button refleshButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox areaSelector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button activateButton2;
        private System.Windows.Forms.ComboBox jobSelector2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button activateButton3;
        private System.Windows.Forms.ComboBox jobSelector3;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button activateButton4;
        private System.Windows.Forms.ComboBox jobSelector4;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button activateButton5;
        private System.Windows.Forms.ComboBox jobSelector5;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button captureWindow;
        private System.Windows.Forms.Label codeLabel1;
        private System.Windows.Forms.Label codeLabel2;
        private System.Windows.Forms.Label codeLabel3;
        private System.Windows.Forms.Label codeLabel4;
        private System.Windows.Forms.Label codeLabel5;
        private System.Windows.Forms.PictureBox mapPictureBox;
    }
}

