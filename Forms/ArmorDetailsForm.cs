using ArmorOptimizer.Models;
using System.Windows.Forms;

namespace ArmorOptimizer.Forms
{
    public partial class ArmorDetailsForm : Form
    {
        public ArmorDetailsForm()
        {
            InitializeComponent();
        }

        public void PopulateForm(Resource resource, Armor baseArmor, Armor buffedArmor)
        {
            cbb_ResourceType.Text = resource.ToString();
            cb_BonusApplied.Checked = !buffedArmor.NeedsBonus;

            num_BasePhysical.Value = baseArmor.CurrentResists.Physical;
            num_BaseFire.Value = baseArmor.CurrentResists.Fire;
            num_BaseCold.Value = baseArmor.CurrentResists.Cold;
            num_BasePoison.Value = baseArmor.CurrentResists.Poison;
            num_BaseEnergy.Value = baseArmor.CurrentResists.Energy;

            num_BuffedPhysical.Value = buffedArmor.CurrentResists.Physical;
            num_BuffedFire.Value = buffedArmor.CurrentResists.Fire;
            num_BuffedCold.Value = buffedArmor.CurrentResists.Cold;
            num_BuffedPoison.Value = buffedArmor.CurrentResists.Poison;
            num_BuffedEnergy.Value = buffedArmor.CurrentResists.Energy;
            num_LostResists.Value = buffedArmor.LostResistPoints;
            num_NumberImbues.Value = buffedArmor.ImbueCount;
        }
    }
}