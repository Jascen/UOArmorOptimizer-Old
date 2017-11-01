namespace ArmorOptimizer.Forms
{
    partial class ConfigurationEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationEditor));
            this.dgv_ResistConfigurations = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PhysicalResist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FireResist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColdResist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PoisonResist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EnergyResist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_SaveChanges = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.num_MaxImbues = new System.Windows.Forms.NumericUpDown();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.num_MaxIterations = new System.Windows.Forms.NumericUpDown();
            this.dgv_Resources = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResistName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Color = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResistId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_ArmorTypes = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ArmorColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ArmorSlot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BaseResistConfigurationId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ResistConfigurations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_MaxImbues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_MaxIterations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Resources)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ArmorTypes)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_ResistConfigurations
            // 
            this.dgv_ResistConfigurations.AllowUserToResizeColumns = false;
            this.dgv_ResistConfigurations.AllowUserToResizeRows = false;
            this.dgv_ResistConfigurations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_ResistConfigurations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ResistConfigurations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.PhysicalResist,
            this.FireResist,
            this.ColdResist,
            this.PoisonResist,
            this.EnergyResist});
            this.dgv_ResistConfigurations.Location = new System.Drawing.Point(12, 12);
            this.dgv_ResistConfigurations.Name = "dgv_ResistConfigurations";
            this.dgv_ResistConfigurations.Size = new System.Drawing.Size(306, 150);
            this.dgv_ResistConfigurations.TabIndex = 96;
            this.dgv_ResistConfigurations.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgv_ResistConfigurations_RowValidating);
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
            // btn_SaveChanges
            // 
            this.btn_SaveChanges.Location = new System.Drawing.Point(545, 324);
            this.btn_SaveChanges.Name = "btn_SaveChanges";
            this.btn_SaveChanges.Size = new System.Drawing.Size(85, 23);
            this.btn_SaveChanges.TabIndex = 97;
            this.btn_SaveChanges.Text = "Save Changes";
            this.btn_SaveChanges.UseVisualStyleBackColor = true;
            this.btn_SaveChanges.Click += new System.EventHandler(this.btn_SaveChanges_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(12, 324);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 98;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(324, 12);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(75, 20);
            this.textBox9.TabIndex = 102;
            this.textBox9.TabStop = false;
            this.textBox9.Text = "Max Imbues";
            this.textBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // num_MaxImbues
            // 
            this.num_MaxImbues.Location = new System.Drawing.Point(405, 12);
            this.num_MaxImbues.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.num_MaxImbues.Name = "num_MaxImbues";
            this.num_MaxImbues.Size = new System.Drawing.Size(44, 20);
            this.num_MaxImbues.TabIndex = 101;
            this.num_MaxImbues.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(324, 38);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(75, 20);
            this.textBox8.TabIndex = 100;
            this.textBox8.TabStop = false;
            this.textBox8.Text = "Max Iterations";
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // num_MaxIterations
            // 
            this.num_MaxIterations.Location = new System.Drawing.Point(405, 38);
            this.num_MaxIterations.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.num_MaxIterations.Name = "num_MaxIterations";
            this.num_MaxIterations.Size = new System.Drawing.Size(44, 20);
            this.num_MaxIterations.TabIndex = 99;
            this.num_MaxIterations.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // dgv_Resources
            // 
            this.dgv_Resources.AllowUserToResizeColumns = false;
            this.dgv_Resources.AllowUserToResizeRows = false;
            this.dgv_Resources.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_Resources.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Resources.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.ResistName,
            this.Color,
            this.ResistId});
            this.dgv_Resources.Location = new System.Drawing.Point(12, 168);
            this.dgv_Resources.Name = "dgv_Resources";
            this.dgv_Resources.Size = new System.Drawing.Size(306, 150);
            this.dgv_Resources.TabIndex = 103;
            this.dgv_Resources.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgv_Resources_RowValidating);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 22;
            // 
            // ResistName
            // 
            this.ResistName.DataPropertyName = "Name";
            this.ResistName.HeaderText = "Name";
            this.ResistName.Name = "ResistName";
            this.ResistName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ResistName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ResistName.Width = 41;
            // 
            // Color
            // 
            this.Color.DataPropertyName = "Color";
            this.Color.HeaderText = "UO Color";
            this.Color.Name = "Color";
            this.Color.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Color.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Color.Width = 56;
            // 
            // ResistId
            // 
            this.ResistId.DataPropertyName = "BonusResistConfigurationId";
            this.ResistId.HeaderText = "ResistId";
            this.ResistId.Name = "ResistId";
            this.ResistId.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ResistId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ResistId.Width = 51;
            // 
            // dgv_ArmorTypes
            // 
            this.dgv_ArmorTypes.AllowUserToResizeColumns = false;
            this.dgv_ArmorTypes.AllowUserToResizeRows = false;
            this.dgv_ArmorTypes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_ArmorTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ArmorTypes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.ArmorColor,
            this.ArmorSlot,
            this.BaseResistConfigurationId});
            this.dgv_ArmorTypes.Location = new System.Drawing.Point(324, 168);
            this.dgv_ArmorTypes.Name = "dgv_ArmorTypes";
            this.dgv_ArmorTypes.Size = new System.Drawing.Size(306, 150);
            this.dgv_ArmorTypes.TabIndex = 104;
            this.dgv_ArmorTypes.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgv_ArmorTypes_RowValidating);
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ItemType";
            this.dataGridViewTextBoxColumn2.HeaderText = "Type";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 37;
            // 
            // ArmorColor
            // 
            this.ArmorColor.DataPropertyName = "Color";
            this.ArmorColor.HeaderText = "Color";
            this.ArmorColor.Name = "ArmorColor";
            this.ArmorColor.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ArmorColor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ArmorColor.Width = 37;
            // 
            // ArmorSlot
            // 
            this.ArmorSlot.DataPropertyName = "Slot";
            this.ArmorSlot.HeaderText = "Slot";
            this.ArmorSlot.Name = "ArmorSlot";
            this.ArmorSlot.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ArmorSlot.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ArmorSlot.Width = 31;
            // 
            // BaseResistConfigurationId
            // 
            this.BaseResistConfigurationId.DataPropertyName = "BaseResistConfigurationId";
            this.BaseResistConfigurationId.HeaderText = "Base Resist Id";
            this.BaseResistConfigurationId.Name = "BaseResistConfigurationId";
            this.BaseResistConfigurationId.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.BaseResistConfigurationId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BaseResistConfigurationId.Width = 81;
            // 
            // ConfigurationEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 353);
            this.Controls.Add(this.dgv_ArmorTypes);
            this.Controls.Add(this.dgv_Resources);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.num_MaxImbues);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.num_MaxIterations);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_SaveChanges);
            this.Controls.Add(this.dgv_ResistConfigurations);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigurationEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.ResistConfigurationEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ResistConfigurations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_MaxImbues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_MaxIterations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Resources)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ArmorTypes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_ResistConfigurations;
        private System.Windows.Forms.Button btn_SaveChanges;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn PhysicalResist;
        private System.Windows.Forms.DataGridViewTextBoxColumn FireResist;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColdResist;
        private System.Windows.Forms.DataGridViewTextBoxColumn PoisonResist;
        private System.Windows.Forms.DataGridViewTextBoxColumn EnergyResist;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.NumericUpDown num_MaxImbues;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.NumericUpDown num_MaxIterations;
        private System.Windows.Forms.DataGridView dgv_Resources;
        private System.Windows.Forms.DataGridView dgv_ArmorTypes;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResistName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Color;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResistId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArmorColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArmorSlot;
        private System.Windows.Forms.DataGridViewTextBoxColumn BaseResistConfigurationId;
    }
}