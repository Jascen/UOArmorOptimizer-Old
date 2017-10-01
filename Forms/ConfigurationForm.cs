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
        /// <summary>
        /// For Designer Only
        /// </summary>
        public ConfigurationEditor()
        {
            InitializeComponent();
            ResistConfigurations = new List<ResistConfiguration>();
            Resources = new List<ResourceRecord>();
        }

        public ConfigurationEditor(IEnumerable<ResistConfiguration> resistConfigurations, IEnumerable<ResourceRecord> resources)
        {
            InitializeComponent();
            ResistConfigurations = resistConfigurations.ToEnumeratedList() ?? new List<ResistConfiguration>();
            Resources = resources.ToEnumeratedList() ?? new List<ResourceRecord>();
        }

        public IList<ResistConfiguration> ResistConfigurations { get; }
        public IList<ResourceRecord> Resources { get; }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btn_SaveChanges_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void dgv_ResistConfigurations_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (((DataGridView)sender).Rows[e.RowIndex].IsNewRow) return;
            if (!((DataGridView)sender).IsCurrentRowDirty) return;

            var resistConfiguration = (ResistConfiguration)((DataGridView)sender).Rows[e.RowIndex].DataBoundItem;
            if (resistConfiguration.Id == 0)
            {
                MessageBox.Show("The Id must be greater than 0.");
                e.Cancel = true;
            }

            if (ResistConfigurations.Count(c => c.Id == resistConfiguration.Id) > 1)
            {
                MessageBox.Show($"There is already a record with Id '{resistConfiguration.Id}'.");
                e.Cancel = true;
            }

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

            var resourceRecord = (ResourceRecord)((DataGridView)sender).Rows[e.RowIndex].DataBoundItem;
            if (resourceRecord.Id == 0)
            {
                MessageBox.Show("The Id must be greater than 0.");
                e.Cancel = true;
            }

            if (Resources.Count(c => c.Id == resourceRecord.Id) > 1)
            {
                MessageBox.Show($"There is already a Resource with Id '{resourceRecord.Id}'.");
                e.Cancel = true;
            }

            if (ResistConfigurations.All(c => c.Id != resourceRecord.BonusResistConfigurationId))
            {
                MessageBox.Show($"The Resist Id '{resourceRecord.BonusResistConfigurationId}' does not exist.");
                e.Cancel = true;
            }
            else if (Resources.Count(c => c.BonusResistConfigurationId == resourceRecord.BonusResistConfigurationId) > 1)
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
        }
    }
}