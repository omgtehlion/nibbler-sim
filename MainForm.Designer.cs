using System.ComponentModel;
using System.Windows.Forms;

namespace Simulator
{
    public partial class MainForm : Form
    {

        private Label m_pcLabel;

        private Label m_aLabel;

        private Label label1;

        private Label m_fetchLabel;

        private Label label6;

        private Label label7;

        private Label label10;

        private TextBox m_memoryViewTextBox;

        private Label label11;

        private RadioButton m_cRadioButton;

        private RadioButton m_eRadioButton;

        private Button m_continueButtton;

        private Button m_pauseButton;

        private Button m_resetButton;

        private Button m_stepButton;

        private Button m_microStepButton;

        private Label label13;

        private Label m_phaseLabel;

        private Panel panel1;

        private Label m_microDisassemblyViewTitle;

        private Label m_opLabel;

        private Label label14;

        private ToolTip m_tooltip;

        private FlickerFreeListView m_sourceListView;

        private ImageList m_debuggerImageList;

        private ColumnHeader columnHeader1;

        private FlickerFreeListView m_microSourceListView;

        private ColumnHeader columnHeader2;

        private Button m_clearBreakpointsButton;

        private MenuStrip menuStrip1;

        private ToolStripMenuItem fileToolStripMenuItem;

        private ToolStripMenuItem openProgramROMToolStripMenuItem;

        private ToolStripSeparator toolStripMenuItem1;

        private ToolStripMenuItem exitToolStripMenuItem;

        private ToolStripMenuItem toolStripMenuItem2;

        private CheckBox m_ShowDisassemblyCheckbox;

        private ToolStripMenuItem toolStripMenuItem4;

        private ToolStripMenuItem toolStripMenuItem5;

        private ToolStripMenuItem toolStripMenuItem6;

        private Button m_buttonLeft;

        private Button m_buttonRight;

        private Button m_buttonUp;

        private Button m_buttonDown;

        private Label m_out1Label;

        private Label m_out0Label;

        private Label label5;

        private Label label4;

        private Label m_Xlabel;

        private Label label3;

        private Label label2;

        private Label m_statusLabel;

        private Label m_timeLabel;

        private Button m_resetTimeButton;

        private IContainer components;

        private ToolStripSeparator toolStripMenuItem3;


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProgramROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_pcLabel = new System.Windows.Forms.Label();
            this.m_aLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_fetchLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.m_memoryViewTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.m_cRadioButton = new System.Windows.Forms.RadioButton();
            this.m_eRadioButton = new System.Windows.Forms.RadioButton();
            this.m_continueButtton = new System.Windows.Forms.Button();
            this.m_pauseButton = new System.Windows.Forms.Button();
            this.m_resetButton = new System.Windows.Forms.Button();
            this.m_stepButton = new System.Windows.Forms.Button();
            this.m_microStepButton = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.m_phaseLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_out1Label = new System.Windows.Forms.Label();
            this.m_out0Label = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_Xlabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_opLabel = new System.Windows.Forms.Label();
            this.m_microDisassemblyViewTitle = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.m_tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.m_debuggerImageList = new System.Windows.Forms.ImageList(this.components);
            this.m_clearBreakpointsButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.m_ShowDisassemblyCheckbox = new System.Windows.Forms.CheckBox();
            this.m_buttonLeft = new System.Windows.Forms.Button();
            this.m_buttonRight = new System.Windows.Forms.Button();
            this.m_buttonUp = new System.Windows.Forms.Button();
            this.m_buttonDown = new System.Windows.Forms.Button();
            this.m_statusLabel = new System.Windows.Forms.Label();
            this.m_timeLabel = new System.Windows.Forms.Label();
            this.m_resetTimeButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.m_microSourceListView = new FlickerFreeListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_sourceListView = new FlickerFreeListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openProgramROMToolStripMenuItem,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openProgramROMToolStripMenuItem
            // 
            this.openProgramROMToolStripMenuItem.Name = "openProgramROMToolStripMenuItem";
            this.openProgramROMToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.openProgramROMToolStripMenuItem.Text = "Open Program File...";
            this.openProgramROMToolStripMenuItem.Click += new System.EventHandler(this.openProgramROMToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(220, 22);
            this.toolStripMenuItem2.Text = "Open Symbol File...";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(217, 6);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(220, 22);
            this.toolStripMenuItem4.Text = "Set Program Start Address...";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(220, 22);
            this.toolStripMenuItem5.Text = "Set Memory Breakpoint...";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(220, 22);
            this.toolStripMenuItem6.Text = "Clear Memory Breakpoint";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(217, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // m_pcLabel
            // 
            this.m_pcLabel.AutoSize = true;
            this.m_pcLabel.BackColor = System.Drawing.SystemColors.Window;
            this.m_pcLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_pcLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_pcLabel.Location = new System.Drawing.Point(149, 9);
            this.m_pcLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_pcLabel.Name = "m_pcLabel";
            this.m_pcLabel.Size = new System.Drawing.Size(34, 18);
            this.m_pcLabel.TabIndex = 3;
            this.m_pcLabel.Text = "ABC";
            // 
            // m_aLabel
            // 
            this.m_aLabel.AutoSize = true;
            this.m_aLabel.BackColor = System.Drawing.SystemColors.Window;
            this.m_aLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_aLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_aLabel.Location = new System.Drawing.Point(24, 9);
            this.m_aLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_aLabel.Name = "m_aLabel";
            this.m_aLabel.Size = new System.Drawing.Size(18, 18);
            this.m_aLabel.TabIndex = 6;
            this.m_aLabel.Text = "A";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "A";
            // 
            // m_fetchLabel
            // 
            this.m_fetchLabel.AutoSize = true;
            this.m_fetchLabel.BackColor = System.Drawing.SystemColors.Window;
            this.m_fetchLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_fetchLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_fetchLabel.Location = new System.Drawing.Point(46, 36);
            this.m_fetchLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_fetchLabel.Name = "m_fetchLabel";
            this.m_fetchLabel.Size = new System.Drawing.Size(26, 18);
            this.m_fetchLabel.TabIndex = 14;
            this.m_fetchLabel.Text = "AB";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(263, 39);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Instruction";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(120, 12);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "PC";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(10, 383);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(28, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "LCD";
            // 
            // m_memoryViewTextBox
            // 
            this.m_memoryViewTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.m_memoryViewTextBox.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_memoryViewTextBox.Location = new System.Drawing.Point(13, 138);
            this.m_memoryViewTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.m_memoryViewTextBox.Multiline = true;
            this.m_memoryViewTextBox.Name = "m_memoryViewTextBox";
            this.m_memoryViewTextBox.ReadOnly = true;
            this.m_memoryViewTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_memoryViewTextBox.Size = new System.Drawing.Size(379, 120);
            this.m_memoryViewTextBox.TabIndex = 20;
            this.m_memoryViewTextBox.Text = "NNN 0 1 2 3 4 5 6 7 8 9 A B C D E F";
            this.m_memoryViewTextBox.WordWrap = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(10, 121);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Memory View";
            // 
            // m_cRadioButton
            // 
            this.m_cRadioButton.AutoCheck = false;
            this.m_cRadioButton.AutoSize = true;
            this.m_cRadioButton.Location = new System.Drawing.Point(287, 10);
            this.m_cRadioButton.Margin = new System.Windows.Forms.Padding(4);
            this.m_cRadioButton.Name = "m_cRadioButton";
            this.m_cRadioButton.Size = new System.Drawing.Size(32, 17);
            this.m_cRadioButton.TabIndex = 22;
            this.m_cRadioButton.TabStop = true;
            this.m_cRadioButton.Text = "C";
            this.m_cRadioButton.UseVisualStyleBackColor = true;
            // 
            // m_eRadioButton
            // 
            this.m_eRadioButton.AutoCheck = false;
            this.m_eRadioButton.AutoSize = true;
            this.m_eRadioButton.Location = new System.Drawing.Point(327, 10);
            this.m_eRadioButton.Margin = new System.Windows.Forms.Padding(4);
            this.m_eRadioButton.Name = "m_eRadioButton";
            this.m_eRadioButton.Size = new System.Drawing.Size(32, 17);
            this.m_eRadioButton.TabIndex = 24;
            this.m_eRadioButton.TabStop = true;
            this.m_eRadioButton.Text = "Z";
            this.m_eRadioButton.UseVisualStyleBackColor = true;
            // 
            // m_continueButtton
            // 
            this.m_continueButtton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_continueButtton.Image = ((System.Drawing.Image)(resources.GetObject("m_continueButtton.Image")));
            this.m_continueButtton.Location = new System.Drawing.Point(13, 479);
            this.m_continueButtton.Margin = new System.Windows.Forms.Padding(4);
            this.m_continueButtton.Name = "m_continueButtton";
            this.m_continueButtton.Size = new System.Drawing.Size(43, 28);
            this.m_continueButtton.TabIndex = 32;
            this.m_continueButtton.UseVisualStyleBackColor = true;
            this.m_continueButtton.Click += new System.EventHandler(this.m_continueButtton_Click);
            // 
            // m_pauseButton
            // 
            this.m_pauseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_pauseButton.Image = ((System.Drawing.Image)(resources.GetObject("m_pauseButton.Image")));
            this.m_pauseButton.Location = new System.Drawing.Point(64, 479);
            this.m_pauseButton.Margin = new System.Windows.Forms.Padding(4);
            this.m_pauseButton.Name = "m_pauseButton";
            this.m_pauseButton.Size = new System.Drawing.Size(43, 28);
            this.m_pauseButton.TabIndex = 33;
            this.m_pauseButton.UseVisualStyleBackColor = true;
            this.m_pauseButton.Click += new System.EventHandler(this.m_pauseButton_Click);
            // 
            // m_resetButton
            // 
            this.m_resetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_resetButton.Image = ((System.Drawing.Image)(resources.GetObject("m_resetButton.Image")));
            this.m_resetButton.Location = new System.Drawing.Point(217, 479);
            this.m_resetButton.Margin = new System.Windows.Forms.Padding(4);
            this.m_resetButton.Name = "m_resetButton";
            this.m_resetButton.Size = new System.Drawing.Size(43, 28);
            this.m_resetButton.TabIndex = 34;
            this.m_resetButton.UseVisualStyleBackColor = true;
            this.m_resetButton.Click += new System.EventHandler(this.m_resetButton_Click);
            // 
            // m_stepButton
            // 
            this.m_stepButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_stepButton.Image = ((System.Drawing.Image)(resources.GetObject("m_stepButton.Image")));
            this.m_stepButton.Location = new System.Drawing.Point(166, 479);
            this.m_stepButton.Margin = new System.Windows.Forms.Padding(4);
            this.m_stepButton.Name = "m_stepButton";
            this.m_stepButton.Size = new System.Drawing.Size(43, 28);
            this.m_stepButton.TabIndex = 37;
            this.m_stepButton.UseVisualStyleBackColor = true;
            this.m_stepButton.Click += new System.EventHandler(this.m_stepButton_Click);
            // 
            // m_microStepButton
            // 
            this.m_microStepButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_microStepButton.Image = ((System.Drawing.Image)(resources.GetObject("m_microStepButton.Image")));
            this.m_microStepButton.Location = new System.Drawing.Point(115, 479);
            this.m_microStepButton.Margin = new System.Windows.Forms.Padding(4);
            this.m_microStepButton.Name = "m_microStepButton";
            this.m_microStepButton.Size = new System.Drawing.Size(43, 28);
            this.m_microStepButton.TabIndex = 38;
            this.m_microStepButton.UseVisualStyleBackColor = true;
            this.m_microStepButton.Click += new System.EventHandler(this.m_microStepButton_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(201, 12);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 13);
            this.label13.TabIndex = 40;
            this.label13.Text = "Phase";
            // 
            // m_phaseLabel
            // 
            this.m_phaseLabel.AutoSize = true;
            this.m_phaseLabel.BackColor = System.Drawing.SystemColors.Window;
            this.m_phaseLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_phaseLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_phaseLabel.Location = new System.Drawing.Point(246, 9);
            this.m_phaseLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_phaseLabel.Name = "m_phaseLabel";
            this.m_phaseLabel.Size = new System.Drawing.Size(18, 18);
            this.m_phaseLabel.TabIndex = 39;
            this.m_phaseLabel.Text = "A";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.m_out1Label);
            this.panel1.Controls.Add(this.m_out0Label);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.m_Xlabel);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.m_opLabel);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.m_phaseLabel);
            this.panel1.Controls.Add(this.m_aLabel);
            this.panel1.Controls.Add(this.m_eRadioButton);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.m_cRadioButton);
            this.panel1.Controls.Add(this.m_pcLabel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.m_fetchLabel);
            this.panel1.Location = new System.Drawing.Point(13, 46);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(379, 67);
            this.panel1.TabIndex = 41;
            // 
            // m_out1Label
            // 
            this.m_out1Label.AutoSize = true;
            this.m_out1Label.BackColor = System.Drawing.SystemColors.Window;
            this.m_out1Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_out1Label.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_out1Label.Location = new System.Drawing.Point(204, 36);
            this.m_out1Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_out1Label.Name = "m_out1Label";
            this.m_out1Label.Size = new System.Drawing.Size(18, 18);
            this.m_out1Label.TabIndex = 49;
            this.m_out1Label.Text = "A";
            // 
            // m_out0Label
            // 
            this.m_out0Label.AutoSize = true;
            this.m_out0Label.BackColor = System.Drawing.SystemColors.Window;
            this.m_out0Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_out0Label.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_out0Label.Location = new System.Drawing.Point(126, 36);
            this.m_out0Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_out0Label.Name = "m_out0Label";
            this.m_out0Label.Size = new System.Drawing.Size(18, 18);
            this.m_out0Label.TabIndex = 48;
            this.m_out0Label.Text = "A";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(172, 39);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 47;
            this.label5.Text = "Out1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(94, 39);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 46;
            this.label4.Text = "Out0";
            // 
            // m_Xlabel
            // 
            this.m_Xlabel.AutoSize = true;
            this.m_Xlabel.BackColor = System.Drawing.SystemColors.Window;
            this.m_Xlabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_Xlabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_Xlabel.Location = new System.Drawing.Point(84, 9);
            this.m_Xlabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_Xlabel.Name = "m_Xlabel";
            this.m_Xlabel.Size = new System.Drawing.Size(18, 18);
            this.m_Xlabel.TabIndex = 45;
            this.m_Xlabel.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(62, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 44;
            this.label3.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 39);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 43;
            this.label2.Text = "Fetch";
            // 
            // m_opLabel
            // 
            this.m_opLabel.AutoSize = true;
            this.m_opLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_opLabel.Location = new System.Drawing.Point(321, 39);
            this.m_opLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_opLabel.Name = "m_opLabel";
            this.m_opLabel.Size = new System.Drawing.Size(33, 13);
            this.m_opLabel.TabIndex = 42;
            this.m_opLabel.Text = "NOP";
            // 
            // m_microDisassemblyViewTitle
            // 
            this.m_microDisassemblyViewTitle.AutoSize = true;
            this.m_microDisassemblyViewTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_microDisassemblyViewTitle.Location = new System.Drawing.Point(10, 266);
            this.m_microDisassemblyViewTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_microDisassemblyViewTitle.Name = "m_microDisassemblyViewTitle";
            this.m_microDisassemblyViewTitle.Size = new System.Drawing.Size(120, 13);
            this.m_microDisassemblyViewTitle.TabIndex = 43;
            this.m_microDisassemblyViewTitle.Text = "Micro-Disassembly View";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(405, 29);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 13);
            this.label14.TabIndex = 45;
            this.label14.Text = "Source View";
            // 
            // m_debuggerImageList
            // 
            this.m_debuggerImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_debuggerImageList.ImageStream")));
            this.m_debuggerImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.m_debuggerImageList.Images.SetKeyName(0, "arrow");
            this.m_debuggerImageList.Images.SetKeyName(1, "breakpoint");
            this.m_debuggerImageList.Images.SetKeyName(2, "breakpoint with arrow");
            this.m_debuggerImageList.Images.SetKeyName(3, "stackframe");
            this.m_debuggerImageList.Images.SetKeyName(4, "breakpoint with stackframe");
            // 
            // m_clearBreakpointsButton
            // 
            this.m_clearBreakpointsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_clearBreakpointsButton.Location = new System.Drawing.Point(763, 479);
            this.m_clearBreakpointsButton.Margin = new System.Windows.Forms.Padding(4);
            this.m_clearBreakpointsButton.Name = "m_clearBreakpointsButton";
            this.m_clearBreakpointsButton.Size = new System.Drawing.Size(146, 28);
            this.m_clearBreakpointsButton.TabIndex = 48;
            this.m_clearBreakpointsButton.Text = "Delete All Breakpoints";
            this.m_clearBreakpointsButton.UseVisualStyleBackColor = true;
            this.m_clearBreakpointsButton.Click += new System.EventHandler(this.m_clearBreakpointsButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(922, 24);
            this.menuStrip1.TabIndex = 49;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // m_ShowDisassemblyCheckbox
            // 
            this.m_ShowDisassemblyCheckbox.AutoSize = true;
            this.m_ShowDisassemblyCheckbox.Location = new System.Drawing.Point(480, 28);
            this.m_ShowDisassemblyCheckbox.Margin = new System.Windows.Forms.Padding(4);
            this.m_ShowDisassemblyCheckbox.Name = "m_ShowDisassemblyCheckbox";
            this.m_ShowDisassemblyCheckbox.Size = new System.Drawing.Size(114, 17);
            this.m_ShowDisassemblyCheckbox.TabIndex = 50;
            this.m_ShowDisassemblyCheckbox.Text = "Show Disassembly";
            this.m_ShowDisassemblyCheckbox.UseVisualStyleBackColor = true;
            this.m_ShowDisassemblyCheckbox.CheckedChanged += new System.EventHandler(this.m_ShowDisassemblyCheckbox_CheckedChanged);
            // 
            // m_buttonLeft
            // 
            this.m_buttonLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_buttonLeft.Image = ((System.Drawing.Image)(resources.GetObject("m_buttonLeft.Image")));
            this.m_buttonLeft.Location = new System.Drawing.Point(408, 479);
            this.m_buttonLeft.Margin = new System.Windows.Forms.Padding(4);
            this.m_buttonLeft.Name = "m_buttonLeft";
            this.m_buttonLeft.Size = new System.Drawing.Size(31, 28);
            this.m_buttonLeft.TabIndex = 51;
            this.m_buttonLeft.UseVisualStyleBackColor = true;
            this.m_buttonLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_buttonLeft_MouseDown);
            this.m_buttonLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_buttonLeft_MouseUp);
            // 
            // m_buttonRight
            // 
            this.m_buttonRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_buttonRight.Image = ((System.Drawing.Image)(resources.GetObject("m_buttonRight.Image")));
            this.m_buttonRight.Location = new System.Drawing.Point(447, 479);
            this.m_buttonRight.Margin = new System.Windows.Forms.Padding(4);
            this.m_buttonRight.Name = "m_buttonRight";
            this.m_buttonRight.Size = new System.Drawing.Size(31, 28);
            this.m_buttonRight.TabIndex = 52;
            this.m_buttonRight.UseVisualStyleBackColor = true;
            this.m_buttonRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_buttonRight_MouseDown);
            this.m_buttonRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_buttonRight_MouseUp);
            // 
            // m_buttonUp
            // 
            this.m_buttonUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_buttonUp.Image = ((System.Drawing.Image)(resources.GetObject("m_buttonUp.Image")));
            this.m_buttonUp.Location = new System.Drawing.Point(547, 479);
            this.m_buttonUp.Margin = new System.Windows.Forms.Padding(4);
            this.m_buttonUp.Name = "m_buttonUp";
            this.m_buttonUp.Size = new System.Drawing.Size(31, 28);
            this.m_buttonUp.TabIndex = 53;
            this.m_buttonUp.UseVisualStyleBackColor = true;
            this.m_buttonUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_buttonUp_MouseDown);
            this.m_buttonUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_buttonUp_MouseUp);
            // 
            // m_buttonDown
            // 
            this.m_buttonDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_buttonDown.Image = ((System.Drawing.Image)(resources.GetObject("m_buttonDown.Image")));
            this.m_buttonDown.Location = new System.Drawing.Point(508, 479);
            this.m_buttonDown.Margin = new System.Windows.Forms.Padding(4);
            this.m_buttonDown.Name = "m_buttonDown";
            this.m_buttonDown.Size = new System.Drawing.Size(31, 28);
            this.m_buttonDown.TabIndex = 54;
            this.m_buttonDown.UseVisualStyleBackColor = true;
            this.m_buttonDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_buttonDown_MouseDown);
            this.m_buttonDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_buttonDown_MouseUp);
            // 
            // m_statusLabel
            // 
            this.m_statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_statusLabel.AutoSize = true;
            this.m_statusLabel.Location = new System.Drawing.Point(10, 523);
            this.m_statusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_statusLabel.Name = "m_statusLabel";
            this.m_statusLabel.Size = new System.Drawing.Size(130, 13);
            this.m_statusLabel.TabIndex = 55;
            this.m_statusLabel.Text = "Running - 123 clocks/sec";
            // 
            // m_timeLabel
            // 
            this.m_timeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_timeLabel.AutoSize = true;
            this.m_timeLabel.Location = new System.Drawing.Point(723, 523);
            this.m_timeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_timeLabel.Name = "m_timeLabel";
            this.m_timeLabel.Size = new System.Drawing.Size(69, 13);
            this.m_timeLabel.TabIndex = 56;
            this.m_timeLabel.Text = "3,000,000 us";
            // 
            // m_resetTimeButton
            // 
            this.m_resetTimeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_resetTimeButton.Location = new System.Drawing.Point(842, 515);
            this.m_resetTimeButton.Margin = new System.Windows.Forms.Padding(4);
            this.m_resetTimeButton.Name = "m_resetTimeButton";
            this.m_resetTimeButton.Size = new System.Drawing.Size(67, 28);
            this.m_resetTimeButton.TabIndex = 57;
            this.m_resetTimeButton.Text = "Clear";
            this.m_resetTimeButton.UseVisualStyleBackColor = true;
            this.m_resetTimeButton.Click += new System.EventHandler(this.m_resetTimeButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Location = new System.Drawing.Point(13, 404);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(379, 67);
            this.pictureBox1.TabIndex = 58;
            this.pictureBox1.TabStop = false;
            // 
            // radioButton1
            // 
            this.radioButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(56, 381);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(54, 17);
            this.radioButton1.TabIndex = 59;
            this.radioButton1.Text = "2-Line";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(116, 381);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(54, 17);
            this.radioButton2.TabIndex = 60;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "4-Line";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(176, 383);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(35, 13);
            this.linkLabel1.TabIndex = 61;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Reset";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // m_microSourceListView
            // 
            this.m_microSourceListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.m_microSourceListView.AutoArrange = false;
            this.m_microSourceListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.m_microSourceListView.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_microSourceListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_microSourceListView.HideSelection = false;
            this.m_microSourceListView.LabelWrap = false;
            this.m_microSourceListView.Location = new System.Drawing.Point(13, 283);
            this.m_microSourceListView.Margin = new System.Windows.Forms.Padding(4);
            this.m_microSourceListView.MultiSelect = false;
            this.m_microSourceListView.Name = "m_microSourceListView";
            this.m_microSourceListView.ShowGroups = false;
            this.m_microSourceListView.Size = new System.Drawing.Size(379, 92);
            this.m_microSourceListView.SmallImageList = this.m_debuggerImageList;
            this.m_microSourceListView.TabIndex = 47;
            this.m_microSourceListView.UseCompatibleStateImageBehavior = false;
            this.m_microSourceListView.View = System.Windows.Forms.View.Details;
            this.m_microSourceListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.m_microSourceListView_ItemSelectionChanged);
            this.m_microSourceListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.m_microSourceListView_MouseClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 450;
            // 
            // m_sourceListView
            // 
            this.m_sourceListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_sourceListView.AutoArrange = false;
            this.m_sourceListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_sourceListView.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_sourceListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_sourceListView.HideSelection = false;
            this.m_sourceListView.LabelWrap = false;
            this.m_sourceListView.Location = new System.Drawing.Point(408, 46);
            this.m_sourceListView.Margin = new System.Windows.Forms.Padding(4);
            this.m_sourceListView.MultiSelect = false;
            this.m_sourceListView.Name = "m_sourceListView";
            this.m_sourceListView.ShowGroups = false;
            this.m_sourceListView.Size = new System.Drawing.Size(501, 425);
            this.m_sourceListView.SmallImageList = this.m_debuggerImageList;
            this.m_sourceListView.TabIndex = 46;
            this.m_sourceListView.UseCompatibleStateImageBehavior = false;
            this.m_sourceListView.View = System.Windows.Forms.View.Details;
            this.m_sourceListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.m_sourceListView_ItemSelectionChanged);
            this.m_sourceListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.m_sourceListView_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 360;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 552);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.m_resetTimeButton);
            this.Controls.Add(this.m_timeLabel);
            this.Controls.Add(this.m_statusLabel);
            this.Controls.Add(this.m_buttonDown);
            this.Controls.Add(this.m_buttonUp);
            this.Controls.Add(this.m_buttonRight);
            this.Controls.Add(this.m_buttonLeft);
            this.Controls.Add(this.m_ShowDisassemblyCheckbox);
            this.Controls.Add(this.m_clearBreakpointsButton);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.m_microSourceListView);
            this.Controls.Add(this.m_sourceListView);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.m_microDisassemblyViewTitle);
            this.Controls.Add(this.m_microStepButton);
            this.Controls.Add(this.m_stepButton);
            this.Controls.Add(this.m_resetButton);
            this.Controls.Add(this.m_pauseButton);
            this.Controls.Add(this.m_continueButtton);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.m_memoryViewTextBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(790, 591);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nibbler";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private PictureBox pictureBox1;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private LinkLabel linkLabel1;
    }
}
