using ArmorOptimizer.Models;
using System.Windows.Forms;
using ArmorOptimizer.Data.Models;

namespace ArmorOptimizer.Forms
{
    public partial class ArmorDetailsForm : Form
    {
        public ArmorDetailsForm()
        {
            InitializeComponent();
        }

        public void PopulateForm(Resource resourceViewModel, ArmorViewModel baseArmorViewModel, ArmorViewModel buffedArmorViewModel)
        {
            cbb_ResourceType.Text = resourceViewModel.ToString();
            cb_BonusApplied.Checked = !buffedArmorViewModel.NeedsBonus;

            num_BasePhysical.Value = baseArmorViewModel.CurrentResists.Physical;
            num_BaseFire.Value = baseArmorViewModel.CurrentResists.Fire;
            num_BaseCold.Value = baseArmorViewModel.CurrentResists.Cold;
            num_BasePoison.Value = baseArmorViewModel.CurrentResists.Poison;
            num_BaseEnergy.Value = baseArmorViewModel.CurrentResists.Energy;

            num_BuffedPhysical.Value = buffedArmorViewModel.CurrentResists.Physical;
            num_BuffedFire.Value = buffedArmorViewModel.CurrentResists.Fire;
            num_BuffedCold.Value = buffedArmorViewModel.CurrentResists.Cold;
            num_BuffedPoison.Value = buffedArmorViewModel.CurrentResists.Poison;
            num_BuffedEnergy.Value = buffedArmorViewModel.CurrentResists.Energy;
            num_LostResists.Value = buffedArmorViewModel.LostResistPoints;
            num_NumberImbues.Value = buffedArmorViewModel.ImbueCount;
        }
    }
}