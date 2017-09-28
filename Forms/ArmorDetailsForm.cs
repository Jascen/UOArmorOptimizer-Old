using ArmorPicker.Enums;
using ArmorPicker.Models;
using System.Windows.Forms;

namespace ArmorPicker.Forms
{
    public partial class ArmorDetailsForm : Form
    {
        public ArmorDetailsForm()
        {
            InitializeComponent();
        }

        public void PopulateForm(ResourceType resourceType, bool resourceBuffApplied, Armor baseArmor, Armor buffedArmor)
        {
            cbb_ResourceType.Text = resourceType.ToString();
            cb_BonusApplied.Checked = resourceBuffApplied;

            num_BasePhysical.Value = baseArmor.PhysicalResist;
            num_BaseFire.Value = baseArmor.FireResist;
            num_BaseCold.Value = baseArmor.ColdResist;
            num_BasePoison.Value = baseArmor.PoisonResist;
            num_BaseEnergy.Value = baseArmor.EnergyResist;

            num_BuffedPhysical.Value = buffedArmor.PhysicalResist;
            num_BuffedFire.Value = buffedArmor.FireResist;
            num_BuffedCold.Value = buffedArmor.ColdResist;
            num_BuffedPoison.Value = buffedArmor.PoisonResist;
            num_BuffedEnergy.Value = buffedArmor.EnergyResist;
            num_LostResists.Value = buffedArmor.LostResistPoints;
            num_NumberImbues.Value = buffedArmor.ImbueCount;
        }
    }
}