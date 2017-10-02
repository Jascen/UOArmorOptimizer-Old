using ArmorOptimizer.Enums;
using ArmorOptimizer.Extensions;
using ArmorOptimizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ArmorOptimizer.Forms
{
    public partial class ConfigurationEditor : Form
    {
        public ConfigurationEditor() : this(new List<ResistConfiguration>(), new List<ResourceRecord>(), new List<ArmorRecord>())
        {
        }

        public ConfigurationEditor(IEnumerable<ResistConfiguration> resistConfigurations, IEnumerable<ResourceRecord> resources, IEnumerable<ArmorRecord> armorTypes)
        {
            InitializeComponent();
            ResistConfigurations = resistConfigurations.ToEnumeratedList();
            Resources = resources.ToEnumeratedList();
            ArmorTypes = armorTypes.ToEnumeratedList();
            SupportedSlotTypes = new List<SlotTypes>
            {
                SlotTypes.Helm,
                SlotTypes.Chest,
                SlotTypes.Arms,
                SlotTypes.Gloves,
                SlotTypes.Legs,
            };
        }

        public IList<ArmorRecord> ArmorTypes { get; }
        public IList<ResistConfiguration> ResistConfigurations { get; }
        public IList<ResourceRecord> Resources { get; }
        public IList<SlotTypes> SupportedSlotTypes { get; }

        private void ArmorTypesErrorHandler(object sender, DataGridViewDataErrorEventArgs e)
        {
            var column = dgv_ArmorTypes.Columns[e.ColumnIndex];
            if (column.DataPropertyName.ToLower() == "slot")
            {
                MessageBox.Show($"{e.Exception.Message}\r\n\r\nSupported Slot Types:\r\n{string.Join("\r\n", SupportedSlotTypes)}.");
            }
            else
            {
                MessageBox.Show(e.Exception.ToString());
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btn_SaveChanges_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void dgv_ArmorTypes_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (((DataGridView)sender).Rows[e.RowIndex].IsNewRow) return;
            if (!((DataGridView)sender).IsCurrentRowDirty) return;

            var armorRecord = (ArmorRecord)((DataGridView)sender).Rows[e.RowIndex].DataBoundItem;

            // Constraint: Must be explicit slot
            if (armorRecord.Slot == SlotTypes.Unknown)
            {
                MessageBox.Show(@"The Slot cannot be 'Unknown'.");
                e.Cancel = true;
            }

            // Constraint: Type and Slot must be unique combinations
            if (ArmorTypes.Where(c => c.Type.ToLower() == armorRecord.Type.ToLower()).Any(c => c.Slot != armorRecord.Slot))
            {
                MessageBox.Show($@"That Type '{armorRecord.Type}' already belongs to another Slot.");
                e.Cancel = true;
            }

            // Constraint: Type and Color must be unique combinations
            if (ArmorTypes.Count(c => c.Type.ToLower() == armorRecord.Type.ToLower() && c.Color == armorRecord.Color) > 1)
            {
                MessageBox.Show($@"There is already a Type '{armorRecord.Type}' and Color '{armorRecord.Color}'.");
                e.Cancel = true;
            }

            // Foreign Key: Resist Id must exist
            if (ResistConfigurations.All(c => c.Id != armorRecord.BaseResistConfigurationId))
            {
                MessageBox.Show($@"The Resist Id '{armorRecord.BaseResistConfigurationId}' does not exist.");
                e.Cancel = true;
            }
        }

        private void dgv_ResistConfigurations_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (((DataGridView)sender).Rows[e.RowIndex].IsNewRow) return;
            if (!((DataGridView)sender).IsCurrentRowDirty) return;

            // Constraint: Must be > 0
            var resistConfiguration = (ResistConfiguration)((DataGridView)sender).Rows[e.RowIndex].DataBoundItem;
            if (resistConfiguration.Id < 1)
            {
                MessageBox.Show("The Id must be greater than 0.");
                e.Cancel = true;
            }

            // Identity
            if (ResistConfigurations.Count(c => c.Id == resistConfiguration.Id) > 1)
            {
                MessageBox.Show($"There is already a record with Id '{resistConfiguration.Id}'.");
                e.Cancel = true;
            }

            // Primary Key on all resists
            if (ResistConfigurations.Count(c =>
            c.Physical == resistConfiguration.Physical
            && c.Fire == resistConfiguration.Fire
            && c.Cold == resistConfiguration.Cold
            && c.Poison == resistConfiguration.Poison
            && c.Energy == resistConfiguration.Energy) > 1)
            {
                MessageBox.Show("This configuration already exists!");
                e.Cancel = true;
            }
        }

        private void dgv_Resources_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (((DataGridView)sender).Rows[e.RowIndex].IsNewRow) return;
            if (!((DataGridView)sender).IsCurrentRowDirty) return;

            // Constraint: Id must be > 0
            var resourceRecord = (ResourceRecord)((DataGridView)sender).Rows[e.RowIndex].DataBoundItem;
            if (resourceRecord.Id < 1)
            {
                MessageBox.Show("The Id must be greater than 0.");
                e.Cancel = true;
            }

            // Identity
            if (Resources.Count(c => c.Id == resourceRecord.Id) > 1)
            {
                MessageBox.Show($"There is already a Resource with Id '{resourceRecord.Id}'.");
                e.Cancel = true;
            }

            // Foreign Key
            if (ResistConfigurations.All(c => c.Id != resourceRecord.BonusResistConfigurationId))
            {
                MessageBox.Show($"The Resist Id '{resourceRecord.BonusResistConfigurationId}' does not exist.");
                e.Cancel = true;
                return;
            }

            // Primary Key
            if (Resources.Count(c => c.BonusResistConfigurationId == resourceRecord.BonusResistConfigurationId) > 1)
            {
                MessageBox.Show("This configuration already exists!");
                e.Cancel = true;
            }
        }

        private void ResistConfigurationEditor_Load(object sender, EventArgs e)
        {
            dgv_ResistConfigurations.AutoGenerateColumns = false;
            var resistBindingSource = new BindingSource { DataSource = ResistConfigurations };
            dgv_ResistConfigurations.DataSource = resistBindingSource;

            dgv_Resources.AutoGenerateColumns = false;
            var resourceBindingSource = new BindingSource { DataSource = Resources };
            dgv_Resources.DataSource = resourceBindingSource;

            dgv_ArmorTypes.AutoGenerateColumns = false;
            var armorTypesBindingSource = new BindingSource { DataSource = ArmorTypes };
            dgv_ArmorTypes.DataSource = armorTypesBindingSource;

            dgv_ArmorTypes.DataError += ArmorTypesErrorHandler;
        }
    }
}