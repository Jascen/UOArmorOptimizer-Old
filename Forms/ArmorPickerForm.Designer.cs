namespace ArmorOptimizer.Forms
{
    partial class ArmorPickerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArmorPickerForm));
            this.btn_MakeSuit = new System.Windows.Forms.Button();
            this.btn_ImportFile = new System.Windows.Forms.Button();
            this.tb_FileToImport = new System.Windows.Forms.TextBox();
            this.btn_BrowseFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.num_GoalEnergy = new System.Windows.Forms.NumericUpDown();
            this.num_GoalPoison = new System.Windows.Forms.NumericUpDown();
            this.num_GoalCold = new System.Windows.Forms.NumericUpDown();
            this.num_GoalFire = new System.Windows.Forms.NumericUpDown();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.num_GoalPhysical = new System.Windows.Forms.NumericUpDown();
            this.num_TotalEnergy = new System.Windows.Forms.NumericUpDown();
            this.num_TotalPoison = new System.Windows.Forms.NumericUpDown();
            this.num_TotalCold = new System.Windows.Forms.NumericUpDown();
            this.num_TotalFire = new System.Windows.Forms.NumericUpDown();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.num_TotalPhysical = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tb_ArmorIds = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbb_LegsResourceType = new System.Windows.Forms.ComboBox();
            this.cbb_GlovesResourceType = new System.Windows.Forms.ComboBox();
            this.cbb_ArmsResourceType = new System.Windows.Forms.ComboBox();
            this.cbb_ChestResourceType = new System.Windows.Forms.ComboBox();
            this.btn_ArmsDetails = new System.Windows.Forms.Button();
            this.cbb_HelmResourceType = new System.Windows.Forms.ComboBox();
            this.btn_GlovesDetails = new System.Windows.Forms.Button();
            this.btn_ChestDetails = new System.Windows.Forms.Button();
            this.btn_LegsDetails = new System.Windows.Forms.Button();
            this.btn_HelmDetails = new System.Windows.Forms.Button();
            this.btn_Configure = new System.Windows.Forms.Button();
            this.dgv_SelectedArmor = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Locked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Slot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bonus = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PhysicalResist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FireResist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColdResist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PoisonResist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EnergyResist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.num_GoalEnergy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_GoalPoison)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_GoalCold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_GoalFire)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_GoalPhysical)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_TotalEnergy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_TotalPoison)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_TotalCold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_TotalFire)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_TotalPhysical)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SelectedArmor)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_MakeSuit
            // 
            this.btn_MakeSuit.Enabled = false;
            this.btn_MakeSuit.Location = new System.Drawing.Point(463, 278);
            this.btn_MakeSuit.Name = "btn_MakeSuit";
            this.btn_MakeSuit.Size = new System.Drawing.Size(62, 29);
            this.btn_MakeSuit.TabIndex = 0;
            this.btn_MakeSuit.TabStop = false;
            this.btn_MakeSuit.Text = "Optimize";
            this.btn_MakeSuit.UseVisualStyleBackColor = true;
            this.btn_MakeSuit.Click += new System.EventHandler(this.btn_MakeSuit_Click);
            // 
            // btn_ImportFile
            // 
            this.btn_ImportFile.Location = new System.Drawing.Point(463, 12);
            this.btn_ImportFile.Name = "btn_ImportFile";
            this.btn_ImportFile.Size = new System.Drawing.Size(62, 23);
            this.btn_ImportFile.TabIndex = 2;
            this.btn_ImportFile.TabStop = false;
            this.btn_ImportFile.Text = "Import";
            this.btn_ImportFile.UseVisualStyleBackColor = true;
            this.btn_ImportFile.Click += new System.EventHandler(this.btn_ImportFile_Click);
            // 
            // tb_FileToImport
            // 
            this.tb_FileToImport.Location = new System.Drawing.Point(12, 14);
            this.tb_FileToImport.Name = "tb_FileToImport";
            this.tb_FileToImport.Size = new System.Drawing.Size(413, 20);
            this.tb_FileToImport.TabIndex = 0;
            this.tb_FileToImport.Text = "F:\\Euo\\inventory.txt";
            // 
            // btn_BrowseFile
            // 
            this.btn_BrowseFile.Location = new System.Drawing.Point(431, 12);
            this.btn_BrowseFile.Name = "btn_BrowseFile";
            this.btn_BrowseFile.Size = new System.Drawing.Size(26, 23);
            this.btn_BrowseFile.TabIndex = 1;
            this.btn_BrowseFile.TabStop = false;
            this.btn_BrowseFile.Text = "...";
            this.btn_BrowseFile.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(211, 228);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Physical";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(269, 228);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Fire";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(317, 228);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Cold";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(360, 228);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Poison";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(410, 228);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Energy";
            // 
            // num_GoalEnergy
            // 
            this.num_GoalEnergy.Location = new System.Drawing.Point(247, 3);
            this.num_GoalEnergy.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.num_GoalEnergy.Name = "num_GoalEnergy";
            this.num_GoalEnergy.Size = new System.Drawing.Size(43, 20);
            this.num_GoalEnergy.TabIndex = 42;
            this.num_GoalEnergy.Value = new decimal(new int[] {
            65,
            0,
            0,
            0});
            // 
            // num_GoalPoison
            // 
            this.num_GoalPoison.Location = new System.Drawing.Point(198, 3);
            this.num_GoalPoison.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.num_GoalPoison.Name = "num_GoalPoison";
            this.num_GoalPoison.Size = new System.Drawing.Size(43, 20);
            this.num_GoalPoison.TabIndex = 41;
            this.num_GoalPoison.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // num_GoalCold
            // 
            this.num_GoalCold.Location = new System.Drawing.Point(149, 3);
            this.num_GoalCold.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.num_GoalCold.Name = "num_GoalCold";
            this.num_GoalCold.Size = new System.Drawing.Size(43, 20);
            this.num_GoalCold.TabIndex = 40;
            this.num_GoalCold.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // num_GoalFire
            // 
            this.num_GoalFire.Location = new System.Drawing.Point(100, 3);
            this.num_GoalFire.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.num_GoalFire.Name = "num_GoalFire";
            this.num_GoalFire.Size = new System.Drawing.Size(43, 20);
            this.num_GoalFire.TabIndex = 39;
            this.num_GoalFire.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(3, 3);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(42, 20);
            this.textBox7.TabIndex = 51;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "Goals";
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // num_GoalPhysical
            // 
            this.num_GoalPhysical.Location = new System.Drawing.Point(51, 3);
            this.num_GoalPhysical.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.num_GoalPhysical.Name = "num_GoalPhysical";
            this.num_GoalPhysical.Size = new System.Drawing.Size(43, 20);
            this.num_GoalPhysical.TabIndex = 38;
            this.num_GoalPhysical.Value = new decimal(new int[] {
            85,
            0,
            0,
            0});
            // 
            // num_TotalEnergy
            // 
            this.num_TotalEnergy.Enabled = false;
            this.num_TotalEnergy.Location = new System.Drawing.Point(247, 3);
            this.num_TotalEnergy.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.num_TotalEnergy.Name = "num_TotalEnergy";
            this.num_TotalEnergy.ReadOnly = true;
            this.num_TotalEnergy.Size = new System.Drawing.Size(43, 20);
            this.num_TotalEnergy.TabIndex = 60;
            // 
            // num_TotalPoison
            // 
            this.num_TotalPoison.Enabled = false;
            this.num_TotalPoison.Location = new System.Drawing.Point(198, 3);
            this.num_TotalPoison.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.num_TotalPoison.Name = "num_TotalPoison";
            this.num_TotalPoison.ReadOnly = true;
            this.num_TotalPoison.Size = new System.Drawing.Size(43, 20);
            this.num_TotalPoison.TabIndex = 59;
            // 
            // num_TotalCold
            // 
            this.num_TotalCold.Enabled = false;
            this.num_TotalCold.Location = new System.Drawing.Point(149, 3);
            this.num_TotalCold.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.num_TotalCold.Name = "num_TotalCold";
            this.num_TotalCold.ReadOnly = true;
            this.num_TotalCold.Size = new System.Drawing.Size(43, 20);
            this.num_TotalCold.TabIndex = 58;
            // 
            // num_TotalFire
            // 
            this.num_TotalFire.Enabled = false;
            this.num_TotalFire.Location = new System.Drawing.Point(100, 3);
            this.num_TotalFire.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.num_TotalFire.Name = "num_TotalFire";
            this.num_TotalFire.ReadOnly = true;
            this.num_TotalFire.Size = new System.Drawing.Size(43, 20);
            this.num_TotalFire.TabIndex = 57;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(3, 3);
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.Size = new System.Drawing.Size(42, 20);
            this.textBox10.TabIndex = 61;
            this.textBox10.TabStop = false;
            this.textBox10.Text = "Totals";
            this.textBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // num_TotalPhysical
            // 
            this.num_TotalPhysical.Enabled = false;
            this.num_TotalPhysical.Location = new System.Drawing.Point(51, 3);
            this.num_TotalPhysical.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.num_TotalPhysical.Name = "num_TotalPhysical";
            this.num_TotalPhysical.ReadOnly = true;
            this.num_TotalPhysical.Size = new System.Drawing.Size(43, 20);
            this.num_TotalPhysical.TabIndex = 56;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox10);
            this.panel1.Controls.Add(this.num_TotalEnergy);
            this.panel1.Controls.Add(this.num_TotalPhysical);
            this.panel1.Controls.Add(this.num_TotalPoison);
            this.panel1.Controls.Add(this.num_TotalFire);
            this.panel1.Controls.Add(this.num_TotalCold);
            this.panel1.Location = new System.Drawing.Point(163, 244);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(292, 29);
            this.panel1.TabIndex = 62;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBox7);
            this.panel2.Controls.Add(this.num_GoalPhysical);
            this.panel2.Controls.Add(this.num_GoalFire);
            this.panel2.Controls.Add(this.num_GoalCold);
            this.panel2.Controls.Add(this.num_GoalPoison);
            this.panel2.Controls.Add(this.num_GoalEnergy);
            this.panel2.Location = new System.Drawing.Point(163, 279);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(292, 29);
            this.panel2.TabIndex = 63;
            // 
            // tb_ArmorIds
            // 
            this.tb_ArmorIds.Location = new System.Drawing.Point(163, 314);
            this.tb_ArmorIds.Name = "tb_ArmorIds";
            this.tb_ArmorIds.ReadOnly = true;
            this.tb_ArmorIds.Size = new System.Drawing.Size(292, 20);
            this.tb_ArmorIds.TabIndex = 76;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 19);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(99, 17);
            this.checkBox1.TabIndex = 68;
            this.checkBox1.Text = "Reactive Armor";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Location = new System.Drawing.Point(6, 65);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(106, 17);
            this.checkBox7.TabIndex = 69;
            this.checkBox7.Text = "Magic Reflection";
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Location = new System.Drawing.Point(6, 42);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(74, 17);
            this.checkBox8.TabIndex = 70;
            this.checkBox8.Text = "Protection";
            this.checkBox8.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(51, 17);
            this.radioButton1.TabIndex = 71;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "None";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(110, 17);
            this.radioButton2.TabIndex = 72;
            this.radioButton2.Text = "Vampiric Embrace";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(6, 65);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(56, 17);
            this.radioButton3.TabIndex = 73;
            this.radioButton3.Text = "Wraith";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.checkBox7);
            this.groupBox1.Controls.Add(this.checkBox8);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(163, 340);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(113, 88);
            this.groupBox1.TabIndex = 74;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buffs";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(285, 340);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(118, 88);
            this.groupBox2.TabIndex = 75;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Forms";
            // 
            // cbb_LegsResourceType
            // 
            this.cbb_LegsResourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_LegsResourceType.FormattingEnabled = true;
            this.cbb_LegsResourceType.Location = new System.Drawing.Point(63, 386);
            this.cbb_LegsResourceType.Name = "cbb_LegsResourceType";
            this.cbb_LegsResourceType.Size = new System.Drawing.Size(88, 21);
            this.cbb_LegsResourceType.TabIndex = 7;
            // 
            // cbb_GlovesResourceType
            // 
            this.cbb_GlovesResourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_GlovesResourceType.FormattingEnabled = true;
            this.cbb_GlovesResourceType.Location = new System.Drawing.Point(63, 351);
            this.cbb_GlovesResourceType.Name = "cbb_GlovesResourceType";
            this.cbb_GlovesResourceType.Size = new System.Drawing.Size(88, 21);
            this.cbb_GlovesResourceType.TabIndex = 6;
            // 
            // cbb_ArmsResourceType
            // 
            this.cbb_ArmsResourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_ArmsResourceType.FormattingEnabled = true;
            this.cbb_ArmsResourceType.Location = new System.Drawing.Point(63, 316);
            this.cbb_ArmsResourceType.Name = "cbb_ArmsResourceType";
            this.cbb_ArmsResourceType.Size = new System.Drawing.Size(88, 21);
            this.cbb_ArmsResourceType.TabIndex = 5;
            // 
            // cbb_ChestResourceType
            // 
            this.cbb_ChestResourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_ChestResourceType.FormattingEnabled = true;
            this.cbb_ChestResourceType.Location = new System.Drawing.Point(63, 281);
            this.cbb_ChestResourceType.Name = "cbb_ChestResourceType";
            this.cbb_ChestResourceType.Size = new System.Drawing.Size(88, 21);
            this.cbb_ChestResourceType.TabIndex = 4;
            // 
            // btn_ArmsDetails
            // 
            this.btn_ArmsDetails.Location = new System.Drawing.Point(15, 316);
            this.btn_ArmsDetails.Name = "btn_ArmsDetails";
            this.btn_ArmsDetails.Size = new System.Drawing.Size(42, 23);
            this.btn_ArmsDetails.TabIndex = 78;
            this.btn_ArmsDetails.Text = "Arms";
            this.btn_ArmsDetails.UseVisualStyleBackColor = true;
            this.btn_ArmsDetails.Click += new System.EventHandler(this.btn_ArmsDetails_Click);
            // 
            // cbb_HelmResourceType
            // 
            this.cbb_HelmResourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_HelmResourceType.FormattingEnabled = true;
            this.cbb_HelmResourceType.Location = new System.Drawing.Point(63, 246);
            this.cbb_HelmResourceType.Name = "cbb_HelmResourceType";
            this.cbb_HelmResourceType.Size = new System.Drawing.Size(88, 21);
            this.cbb_HelmResourceType.TabIndex = 3;
            // 
            // btn_GlovesDetails
            // 
            this.btn_GlovesDetails.Location = new System.Drawing.Point(15, 351);
            this.btn_GlovesDetails.Name = "btn_GlovesDetails";
            this.btn_GlovesDetails.Size = new System.Drawing.Size(42, 23);
            this.btn_GlovesDetails.TabIndex = 79;
            this.btn_GlovesDetails.Text = "Gloves";
            this.btn_GlovesDetails.UseVisualStyleBackColor = true;
            this.btn_GlovesDetails.Click += new System.EventHandler(this.btn_GlovesDetails_Click);
            // 
            // btn_ChestDetails
            // 
            this.btn_ChestDetails.Location = new System.Drawing.Point(15, 281);
            this.btn_ChestDetails.Name = "btn_ChestDetails";
            this.btn_ChestDetails.Size = new System.Drawing.Size(42, 23);
            this.btn_ChestDetails.TabIndex = 77;
            this.btn_ChestDetails.Text = "Chest";
            this.btn_ChestDetails.UseVisualStyleBackColor = true;
            this.btn_ChestDetails.Click += new System.EventHandler(this.btn_ChestDetails_Click);
            // 
            // btn_LegsDetails
            // 
            this.btn_LegsDetails.Location = new System.Drawing.Point(15, 386);
            this.btn_LegsDetails.Name = "btn_LegsDetails";
            this.btn_LegsDetails.Size = new System.Drawing.Size(42, 23);
            this.btn_LegsDetails.TabIndex = 80;
            this.btn_LegsDetails.Text = "Legs";
            this.btn_LegsDetails.UseVisualStyleBackColor = true;
            this.btn_LegsDetails.Click += new System.EventHandler(this.btn_LegsDetails_Click);
            // 
            // btn_HelmDetails
            // 
            this.btn_HelmDetails.Location = new System.Drawing.Point(15, 246);
            this.btn_HelmDetails.Name = "btn_HelmDetails";
            this.btn_HelmDetails.Size = new System.Drawing.Size(42, 23);
            this.btn_HelmDetails.TabIndex = 76;
            this.btn_HelmDetails.Text = "Helm";
            this.btn_HelmDetails.UseVisualStyleBackColor = true;
            this.btn_HelmDetails.Click += new System.EventHandler(this.btn_HelmDetails_Click);
            // 
            // btn_Configure
            // 
            this.btn_Configure.Location = new System.Drawing.Point(463, 244);
            this.btn_Configure.Name = "btn_Configure";
            this.btn_Configure.Size = new System.Drawing.Size(62, 23);
            this.btn_Configure.TabIndex = 76;
            this.btn_Configure.Text = "Configure";
            this.btn_Configure.UseVisualStyleBackColor = true;
            this.btn_Configure.Click += new System.EventHandler(this.btn_Configure_Click);
            // 
            // dgv_SelectedArmor
            // 
            this.dgv_SelectedArmor.AllowUserToAddRows = false;
            this.dgv_SelectedArmor.AllowUserToDeleteRows = false;
            this.dgv_SelectedArmor.AllowUserToResizeColumns = false;
            this.dgv_SelectedArmor.AllowUserToResizeRows = false;
            this.dgv_SelectedArmor.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_SelectedArmor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_SelectedArmor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Locked,
            this.Slot,
            this.Bonus,
            this.PhysicalResist,
            this.FireResist,
            this.ColdResist,
            this.PoisonResist,
            this.EnergyResist});
            this.dgv_SelectedArmor.Location = new System.Drawing.Point(12, 41);
            this.dgv_SelectedArmor.Name = "dgv_SelectedArmor";
            this.dgv_SelectedArmor.Size = new System.Drawing.Size(511, 179);
            this.dgv_SelectedArmor.TabIndex = 97;
            this.dgv_SelectedArmor.Validating += new System.ComponentModel.CancelEventHandler(this.dgv_SelectedArmor_Validating);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Id.Width = 22;
            // 
            // Locked
            // 
            this.Locked.DataPropertyName = "Locked";
            this.Locked.HeaderText = "Locked";
            this.Locked.Name = "Locked";
            this.Locked.Width = 49;
            // 
            // Slot
            // 
            this.Slot.DataPropertyName = "Slot";
            this.Slot.HeaderText = "Slot";
            this.Slot.Name = "Slot";
            this.Slot.ReadOnly = true;
            this.Slot.Width = 50;
            // 
            // Bonus
            // 
            this.Bonus.DataPropertyName = "NeedsBonus";
            this.Bonus.HeaderText = "Bonus";
            this.Bonus.Name = "Bonus";
            this.Bonus.Width = 43;
            // 
            // PhysicalResist
            // 
            this.PhysicalResist.DataPropertyName = "Physical";
            this.PhysicalResist.HeaderText = "Physical";
            this.PhysicalResist.Name = "PhysicalResist";
            this.PhysicalResist.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PhysicalResist.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PhysicalResist.Width = 52;
            // 
            // FireResist
            // 
            this.FireResist.DataPropertyName = "Fire";
            this.FireResist.HeaderText = "Fire";
            this.FireResist.Name = "FireResist";
            this.FireResist.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FireResist.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FireResist.Width = 30;
            // 
            // ColdResist
            // 
            this.ColdResist.DataPropertyName = "Cold";
            this.ColdResist.HeaderText = "Cold";
            this.ColdResist.Name = "ColdResist";
            this.ColdResist.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColdResist.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColdResist.Width = 34;
            // 
            // PoisonResist
            // 
            this.PoisonResist.DataPropertyName = "Poison";
            this.PoisonResist.HeaderText = "Poison";
            this.PoisonResist.Name = "PoisonResist";
            this.PoisonResist.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PoisonResist.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PoisonResist.Width = 45;
            // 
            // EnergyResist
            // 
            this.EnergyResist.DataPropertyName = "Energy";
            this.EnergyResist.HeaderText = "Energy";
            this.EnergyResist.Name = "EnergyResist";
            this.EnergyResist.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.EnergyResist.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.EnergyResist.Width = 46;
            // 
            // ArmorPickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 436);
            this.Controls.Add(this.tb_ArmorIds);
            this.Controls.Add(this.btn_LegsDetails);
            this.Controls.Add(this.btn_HelmDetails);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btn_GlovesDetails);
            this.Controls.Add(this.btn_MakeSuit);
            this.Controls.Add(this.dgv_SelectedArmor);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_ArmsDetails);
            this.Controls.Add(this.btn_Configure);
            this.Controls.Add(this.btn_ChestDetails);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbb_HelmResourceType);
            this.Controls.Add(this.cbb_ChestResourceType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbb_ArmsResourceType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbb_GlovesResourceType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbb_LegsResourceType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_BrowseFile);
            this.Controls.Add(this.tb_FileToImport);
            this.Controls.Add(this.btn_ImportFile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ArmorPickerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ABC Ultima Online Suit Crafter - By Tsai";
            this.Load += new System.EventHandler(this.ArmorPickerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.num_GoalEnergy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_GoalPoison)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_GoalCold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_GoalFire)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_GoalPhysical)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_TotalEnergy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_TotalPoison)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_TotalCold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_TotalFire)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_TotalPhysical)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SelectedArmor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_MakeSuit;
        private System.Windows.Forms.Button btn_ImportFile;
        private System.Windows.Forms.TextBox tb_FileToImport;
        private System.Windows.Forms.Button btn_BrowseFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown num_GoalEnergy;
        private System.Windows.Forms.NumericUpDown num_GoalPoison;
        private System.Windows.Forms.NumericUpDown num_GoalCold;
        private System.Windows.Forms.NumericUpDown num_GoalFire;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.NumericUpDown num_GoalPhysical;
        private System.Windows.Forms.NumericUpDown num_TotalEnergy;
        private System.Windows.Forms.NumericUpDown num_TotalPoison;
        private System.Windows.Forms.NumericUpDown num_TotalCold;
        private System.Windows.Forms.NumericUpDown num_TotalFire;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.NumericUpDown num_TotalPhysical;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.CheckBox checkBox8;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tb_ArmorIds;
        private System.Windows.Forms.ComboBox cbb_LegsResourceType;
        private System.Windows.Forms.ComboBox cbb_GlovesResourceType;
        private System.Windows.Forms.ComboBox cbb_ArmsResourceType;
        private System.Windows.Forms.ComboBox cbb_ChestResourceType;
        private System.Windows.Forms.Button btn_ArmsDetails;
        private System.Windows.Forms.ComboBox cbb_HelmResourceType;
        private System.Windows.Forms.Button btn_GlovesDetails;
        private System.Windows.Forms.Button btn_ChestDetails;
        private System.Windows.Forms.Button btn_LegsDetails;
        private System.Windows.Forms.Button btn_HelmDetails;
        private System.Windows.Forms.Button btn_Configure;
        private System.Windows.Forms.DataGridView dgv_SelectedArmor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Locked;
        private System.Windows.Forms.DataGridViewTextBoxColumn Slot;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Bonus;
        private System.Windows.Forms.DataGridViewTextBoxColumn PhysicalResist;
        private System.Windows.Forms.DataGridViewTextBoxColumn FireResist;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColdResist;
        private System.Windows.Forms.DataGridViewTextBoxColumn PoisonResist;
        private System.Windows.Forms.DataGridViewTextBoxColumn EnergyResist;
    }
}

