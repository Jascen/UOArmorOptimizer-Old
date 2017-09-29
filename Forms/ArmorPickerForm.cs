using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArmorOptimizer.Attributes;
using ArmorOptimizer.Enums;
using ArmorOptimizer.Extensions;
using ArmorOptimizer.Models;
using ArmorOptimizer.Services;

namespace ArmorOptimizer.Forms
{
    public partial class ArmorPickerForm : Form
    {
        private readonly ArmorDetailsForm _armorDetailsForm = new ArmorDetailsForm();
        private List<Armor> _armorPieces;

        public ArmorPickerForm()
        {
            InitializeComponent();
        }

        #region UI

        private Armor _selectedArms;
        private Armor _selectedChest;
        private Armor _selectedGloves;
        private Armor _selectedHelm;
        private Armor _selectedLegs;
        private bool ApplyArmsBonus => cb_ApplyBonusArms.Checked;
        private bool ApplyChestBonus => cb_ApplyBonusChest.Checked;
        private bool ApplyGlovesBonus => cb_ApplyBonusGloves.Checked;
        private bool ApplyHelmBonus => cb_ApplyBonusHelm.Checked;
        private bool ApplyLegsBonus => cb_ApplyBonusLegs.Checked;
        private string FileToImport => tb_FileToImport.Text;
        private bool FindArms => cb_FindArms.Checked;
        private bool FindChest => cb_FindChest.Checked;
        private bool FindGloves => cb_FindGloves.Checked;
        private bool FindHelm => cb_FindHelm.Checked;
        private bool FindLegs => cb_FindLegs.Checked;
        private int MaxImbues => (int)num_MaxImbues.Value;
        private int MaxIterations => (int)num_MaxIterations.Value;
        private ResourceType SelectedArmsResourceType => (ResourceType)cbb_ArmsResourceType.SelectedItem;
        private ResourceType SelectedChestResourceType => (ResourceType)cbb_ChestResourceType.SelectedItem;
        private ResourceType SelectedGlovesResourceType => (ResourceType)cbb_GlovesResourceType.SelectedItem;
        private ResourceType SelectedHelmResourceType => (ResourceType)cbb_HelmResourceType.SelectedItem;
        private ResourceType SelectedLegsResourceType => (ResourceType)cbb_LegsResourceType.SelectedItem;

        public Suit GetSuitFromUi()
        {
            var maxResists = new Armor
            {
                PhysicalResist = (int)num_GoalPhysical.Value,
                FireResist = (int)num_GoalFire.Value,
                ColdResist = (int)num_GoalCold.Value,
                PoisonResist = (int)num_GoalPoison.Value,
                EnergyResist = (int)num_GoalEnergy.Value,
            };
            var suit = new Suit
            {
                MaxResists = maxResists,
                Misc = new Armor
                {
                    PhysicalResist = (int)num_MiscPhysical.Value,
                    FireResist = (int)num_MiscFire.Value,
                    ColdResist = (int)num_MiscCold.Value,
                    PoisonResist = (int)num_MiscPoison.Value,
                    EnergyResist = (int)num_MiscEnergy.Value,
                },
            };
            suit.Helm = FindHelm
                ? new Armor()
                : new Armor
                {
                    Id = _selectedHelm?.Id ?? "",
                    Slot = _selectedHelm?.Slot ?? SlotTypes.Helm,
                    PhysicalResist = _selectedHelm?.PhysicalResist ?? 0,
                    FireResist = _selectedHelm?.FireResist ?? 0,
                    ColdResist = _selectedHelm?.ColdResist ?? 0,
                    PoisonResist = _selectedHelm?.PoisonResist ?? 0,
                    EnergyResist = _selectedHelm?.EnergyResist ?? 0,
                    ImbueCount = _selectedHelm?.ImbueCount ?? 0,
                    LostResistPoints = _selectedHelm?.LostResistPoints ?? 0,
                };
            suit.Chest = FindChest
                ? new Armor()
                : new Armor
                {
                    Id = _selectedChest?.Id ?? "",
                    Slot = _selectedChest?.Slot ?? SlotTypes.Helm,
                    PhysicalResist = _selectedChest?.PhysicalResist ?? 0,
                    FireResist = _selectedChest?.FireResist ?? 0,
                    ColdResist = _selectedChest?.ColdResist ?? 0,
                    PoisonResist = _selectedChest?.PoisonResist ?? 0,
                    EnergyResist = _selectedChest?.EnergyResist ?? 0,
                    ImbueCount = _selectedChest?.ImbueCount ?? 0,
                    LostResistPoints = _selectedChest?.LostResistPoints ?? 0,
                };
            suit.Arms = FindArms
                ? new Armor()
                : new Armor
                {
                    Id = _selectedArms?.Id ?? "",
                    Slot = _selectedArms?.Slot ?? SlotTypes.Helm,
                    PhysicalResist = _selectedArms?.PhysicalResist ?? 0,
                    FireResist = _selectedArms?.FireResist ?? 0,
                    ColdResist = _selectedArms?.ColdResist ?? 0,
                    PoisonResist = _selectedArms?.PoisonResist ?? 0,
                    EnergyResist = _selectedArms?.EnergyResist ?? 0,
                    ImbueCount = _selectedArms?.ImbueCount ?? 0,
                    LostResistPoints = _selectedArms?.LostResistPoints ?? 0,
                };
            suit.Gloves = FindGloves
                ? new Armor()
                : new Armor
                {
                    Id = _selectedGloves?.Id ?? "",
                    Slot = _selectedGloves?.Slot ?? SlotTypes.Helm,
                    PhysicalResist = _selectedGloves?.PhysicalResist ?? 0,
                    FireResist = _selectedGloves?.FireResist ?? 0,
                    ColdResist = _selectedGloves?.ColdResist ?? 0,
                    PoisonResist = _selectedGloves?.PoisonResist ?? 0,
                    EnergyResist = _selectedGloves?.EnergyResist ?? 0,
                    ImbueCount = _selectedGloves?.ImbueCount ?? 0,
                    LostResistPoints = _selectedGloves?.LostResistPoints ?? 0,
                };
            suit.Legs = FindLegs
                ? new Armor()
                : new Armor
                {
                    Id = _selectedLegs?.Id ?? "",
                    Slot = _selectedLegs?.Slot ?? SlotTypes.Helm,
                    PhysicalResist = _selectedLegs?.PhysicalResist ?? 0,
                    FireResist = _selectedLegs?.FireResist ?? 0,
                    ColdResist = _selectedLegs?.ColdResist ?? 0,
                    PoisonResist = _selectedLegs?.PoisonResist ?? 0,
                    EnergyResist = _selectedLegs?.EnergyResist ?? 0,
                    ImbueCount = _selectedLegs?.ImbueCount ?? 0,
                    LostResistPoints = _selectedLegs?.LostResistPoints ?? 0,
                };
            suit.UpdateCurrentResists();

            return suit;
        }

        private void UpdateUiItems()
        {
            num_HelmPhysical.Value = _selectedHelm?.PhysicalResist ?? 0;
            num_HelmFire.Value = _selectedHelm?.FireResist ?? 0;
            num_HelmCold.Value = _selectedHelm?.ColdResist ?? 0;
            num_HelmPoison.Value = _selectedHelm?.PoisonResist ?? 0;
            num_HelmEnergy.Value = _selectedHelm?.EnergyResist ?? 0;
            tb_HelmId.Text = _selectedHelm?.Id ?? Guid.NewGuid().ToString();

            num_ChestPhysical.Value = _selectedChest?.PhysicalResist ?? 0;
            num_ChestFire.Value = _selectedChest?.FireResist ?? 0;
            num_ChestCold.Value = _selectedChest?.ColdResist ?? 0;
            num_ChestPoison.Value = _selectedChest?.PoisonResist ?? 0;
            num_ChestEnergy.Value = _selectedChest?.EnergyResist ?? 0;
            tb_ChestId.Text = _selectedChest?.Id ?? Guid.NewGuid().ToString();

            num_ArmsPhysical.Value = _selectedArms?.PhysicalResist ?? 0;
            num_ArmsFire.Value = _selectedArms?.FireResist ?? 0;
            num_ArmsCold.Value = _selectedArms?.ColdResist ?? 0;
            num_ArmsPoison.Value = _selectedArms?.PoisonResist ?? 0;
            num_ArmsEnergy.Value = _selectedArms?.EnergyResist ?? 0;
            tb_ArmsId.Text = _selectedArms?.Id ?? Guid.NewGuid().ToString();

            num_GlovesPhysical.Value = _selectedGloves?.PhysicalResist ?? 0;
            num_GlovesFire.Value = _selectedGloves?.FireResist ?? 0;
            num_GlovesCold.Value = _selectedGloves?.ColdResist ?? 0;
            num_GlovesPoison.Value = _selectedGloves?.PoisonResist ?? 0;
            num_GlovesEnergy.Value = _selectedGloves?.EnergyResist ?? 0;
            tb_GlovesId.Text = _selectedGloves?.Id ?? Guid.NewGuid().ToString();

            num_LegsPhysical.Value = _selectedLegs?.PhysicalResist ?? 0;
            num_LegsFire.Value = _selectedLegs?.FireResist ?? 0;
            num_LegsCold.Value = _selectedLegs?.ColdResist ?? 0;
            num_LegsPoison.Value = _selectedLegs?.PoisonResist ?? 0;
            num_LegsEnergy.Value = _selectedLegs?.EnergyResist ?? 0;
            tb_LegsId.Text = _selectedLegs?.Id ?? Guid.NewGuid().ToString();
        }

        private void UpdateUiSuit(Suit suit)
        {
            if (suit.Helm.TotalResists() > 0)
            {
                _selectedHelm = suit.Helm;
            }

            if (suit.Chest.TotalResists() > 0)
            {
                _selectedChest = suit.Chest;
            }

            if (suit.Arms.TotalResists() > 0)
            {
                _selectedArms = suit.Arms;
            }

            if (suit.Gloves.TotalResists() > 0)
            {
                _selectedGloves = suit.Gloves;
            }

            if (suit.Legs.TotalResists() > 0)
            {
                _selectedLegs = suit.Legs;
            }

            UpdateUiItems();
            suit.UpdateCurrentResists();
            num_TotalPhysical.Value = suit.CurrentResists?.PhysicalResist ?? 0;
            num_TotalFire.Value = suit.CurrentResists?.FireResist ?? 0;
            num_TotalCold.Value = suit.CurrentResists?.ColdResist ?? 0;
            num_TotalPoison.Value = suit.CurrentResists?.PoisonResist ?? 0;
            num_TotalEnergy.Value = suit.CurrentResists?.EnergyResist ?? 0;
            tb_ArmorIds.Text = $"{suit.Helm.Id}_{suit.Chest.Id}_{suit.Arms.Id}_{suit.Gloves.Id}_{suit.Legs.Id}";
        }

        #endregion UI

        #region Event Handlers

        private void ArmorPickerForm_Load(object sender, EventArgs e)
        {
            LoadResourceTypes();
        }

        private void btn_ArmsDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_selectedArms?.Id)) return;

                var basePiece = _armorPieces.FirstOrDefault(p => p.Id == _selectedArms.Id);
                if (basePiece == null)
                {
                    MessageBox.Show($"Failed to locate base piece with Id '{_selectedArms.Id}'.");
                    return;
                }

                _armorDetailsForm.PopulateForm(SelectedArmsResourceType, ApplyArmsBonus, basePiece, _selectedArms);
                _armorDetailsForm.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void btn_ChestDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_selectedChest?.Id)) return;

                var basePiece = _armorPieces.FirstOrDefault(p => p.Id == _selectedChest.Id);
                if (basePiece == null)
                {
                    MessageBox.Show($"Failed to locate base piece with Id '{_selectedChest.Id}'.");
                    return;
                }

                _armorDetailsForm.PopulateForm(SelectedChestResourceType, ApplyChestBonus, basePiece, _selectedChest);
                _armorDetailsForm.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void btn_GlovesDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_selectedGloves?.Id)) return;

                var basePiece = _armorPieces.FirstOrDefault(p => p.Id == _selectedGloves.Id);
                if (basePiece == null)
                {
                    MessageBox.Show($"Failed to locate base piece with Id '{_selectedGloves.Id}'.");
                    return;
                }

                _armorDetailsForm.PopulateForm(SelectedGlovesResourceType, ApplyGlovesBonus, basePiece, _selectedGloves);
                _armorDetailsForm.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void btn_HelmDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_selectedHelm?.Id)) return;

                var basePiece = _armorPieces.FirstOrDefault(p => p.Id == _selectedHelm.Id);
                if (basePiece == null)
                {
                    MessageBox.Show($"Failed to locate base piece with Id '{_selectedHelm.Id}'.");
                    return;
                }

                _armorDetailsForm.PopulateForm(SelectedHelmResourceType, ApplyHelmBonus, basePiece, _selectedHelm);
                _armorDetailsForm.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void btn_ImportFile_Click(object sender, EventArgs e)
        {
            try
            {
                var importerService = new ImporterService();
                var fileText = importerService.ImportFile(FileToImport);
                var lines = importerService.SplitString(fileText, '\n');

                var dict = new Dictionary<string, int>();
                var actualtype = typeof(Armor);
                foreach (var propertyInfo in actualtype.GetProperties())
                {
                    foreach (ColumnNumber columnNumber in propertyInfo.GetCustomAttributes(typeof(ColumnNumber), false))
                    {
                        dict.Add(propertyInfo.Name, columnNumber.Value);
                        break;
                    }
                }

                var armors = new List<Armor>();
                foreach (var line in lines)
                {
                    var data = importerService.SplitString(line, '.');
                    var dataArray = data?.ToArray();
                    if (data == null || dataArray.Length == 0) continue;

                    var armor = new Armor();
                    armor.Slot = (SlotTypes)int.Parse(dataArray[dict[nameof(armor.Slot)]]);
                    armor.Id = dataArray[dict[nameof(armor.Id)]];
                    armor.PhysicalResist = int.Parse(dataArray[dict[nameof(armor.PhysicalResist)]]);
                    armor.FireResist = int.Parse(dataArray[dict[nameof(armor.FireResist)]]);
                    armor.ColdResist = int.Parse(dataArray[dict[nameof(armor.ColdResist)]]);
                    armor.PoisonResist = int.Parse(dataArray[dict[nameof(armor.PoisonResist)]]);
                    armor.EnergyResist = int.Parse(dataArray[dict[nameof(armor.EnergyResist)]]);
                    armors.Add(armor);
                }

                _armorPieces = armors;
                MessageBox.Show($"Imported '{_armorPieces.Count}' records.");
                btn_MakeSuit.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_LegsDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_selectedLegs?.Id)) return;

                var basePiece = _armorPieces.FirstOrDefault(p => p.Id == _selectedLegs.Id);
                if (basePiece == null)
                {
                    MessageBox.Show($"Failed to locate base piece with Id '{_selectedLegs.Id}'.");
                    return;
                }

                _armorDetailsForm.PopulateForm(SelectedLegsResourceType, ApplyLegsBonus, basePiece, _selectedLegs);
                _armorDetailsForm.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void btn_MakeSuit_Click(object sender, EventArgs e)
        {
            try
            {
                btn_MakeSuit.Enabled = false;
                var armorPieces = new List<Armor>();
                foreach (var armorPiece in _armorPieces)
                {
                    var clonedPiece = new Armor
                    {
                        Id = armorPiece.Id,
                        Slot = armorPiece.Slot,
                        ImbueCount = armorPiece.ImbueCount,
                        LostResistPoints = armorPiece.LostResistPoints,
                        PhysicalResist = armorPiece.PhysicalResist,
                        FireResist = armorPiece.FireResist,
                        ColdResist = armorPiece.ColdResist,
                        PoisonResist = armorPiece.PoisonResist,
                        EnergyResist = armorPiece.EnergyResist,
                    };
                    armorPieces.Add(clonedPiece);
                }
                var imbueBonuses = new Armor
                {
                    PhysicalResist = 13,
                    FireResist = 13,
                    ColdResist = 13,
                    PoisonResist = 13,
                    EnergyResist = 13,
                };
                Armor helmBonus, helmMax, chestBonus, chestMax, armsBonus, armsMax, glovesBonus, glovesMax, legsBonus, legsMax;
                LookupBonuses(SelectedHelmResourceType, imbueBonuses, out helmBonus, out helmMax, true);
                LookupBonuses(SelectedChestResourceType, imbueBonuses, out chestBonus, out chestMax);
                LookupBonuses(SelectedArmsResourceType, imbueBonuses, out armsBonus, out armsMax);
                LookupBonuses(SelectedGlovesResourceType, imbueBonuses, out glovesBonus, out glovesMax);
                LookupBonuses(SelectedLegsResourceType, imbueBonuses, out legsBonus, out legsMax);

                // Sort pieces into usable groups
                var headPieces = new List<Armor>();
                var chestPieces = new List<Armor>();
                var armPieces = new List<Armor>();
                var glovePieces = new List<Armor>();
                var legPieces = new List<Armor>();
                SortPieces(armorPieces, helmBonus, helmMax, chestBonus, chestMax, armsBonus, armsMax, glovesBonus, glovesMax, legsBonus, legsMax, ref headPieces, ref chestPieces, ref armPieces, ref glovePieces, ref legPieces);

                var suit = GetSuitFromUi();
                CheckPieces(headPieces, chestPieces, armPieces, glovePieces, legPieces, suit);
                UpdateUiSuit(suit);

                var newSuitStringBuilder = new StringBuilder();
                newSuitStringBuilder.AppendLine($"Physical Resist: {suit.CurrentResists.PhysicalResist} / {suit.MaxResists.PhysicalResist}");
                newSuitStringBuilder.AppendLine($"Fire Resist: {suit.CurrentResists.FireResist} / {suit.MaxResists.FireResist}");
                newSuitStringBuilder.AppendLine($"Cold Resist: {suit.CurrentResists.ColdResist} / {suit.MaxResists.ColdResist}");
                newSuitStringBuilder.AppendLine($"Poison Resist: {suit.CurrentResists.PoisonResist} / {suit.MaxResists.PoisonResist}");
                newSuitStringBuilder.AppendLine($"Energy Resist: {suit.CurrentResists.EnergyResist} / {suit.MaxResists.EnergyResist}");
                MessageBox.Show($"Suit Resists:\r\n\r\n{newSuitStringBuilder}");

                var armorStringBuilder = new StringBuilder();
                armorStringBuilder.AppendLine(ExportArmor("Helm", suit.Helm));
                armorStringBuilder.AppendLine(ExportArmor("Chest", suit.Chest));
                armorStringBuilder.AppendLine(ExportArmor("Arms", suit.Arms));
                armorStringBuilder.AppendLine(ExportArmor("Gloves", suit.Gloves));
                armorStringBuilder.AppendLine(ExportArmor("Legs", suit.Legs));
                MessageBox.Show($"Individual Resists:\r\n\r\n{armorStringBuilder}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btn_MakeSuit.Enabled = true;
            }
        }

        private void cb_FindArms_CheckedChanged(object sender, EventArgs e)
        {
            num_ArmsPhysical.Enabled = !cb_FindArms.Checked;
            num_ArmsFire.Enabled = !cb_FindArms.Checked;
            num_ArmsCold.Enabled = !cb_FindArms.Checked;
            num_ArmsPoison.Enabled = !cb_FindArms.Checked;
            num_ArmsEnergy.Enabled = !cb_FindArms.Checked;
        }

        private void cb_FindChest_CheckedChanged(object sender, EventArgs e)
        {
            num_ChestPhysical.Enabled = !cb_FindChest.Checked;
            num_ChestFire.Enabled = !cb_FindChest.Checked;
            num_ChestCold.Enabled = !cb_FindChest.Checked;
            num_ChestPoison.Enabled = !cb_FindChest.Checked;
            num_ChestEnergy.Enabled = !cb_FindChest.Checked;
        }

        private void cb_FindGloves_CheckedChanged(object sender, EventArgs e)
        {
            num_GlovesPhysical.Enabled = !cb_FindGloves.Checked;
            num_GlovesFire.Enabled = !cb_FindGloves.Checked;
            num_GlovesCold.Enabled = !cb_FindGloves.Checked;
            num_GlovesPoison.Enabled = !cb_FindGloves.Checked;
            num_GlovesEnergy.Enabled = !cb_FindGloves.Checked;
        }

        private void cb_FindHelm_CheckedChanged(object sender, EventArgs e)
        {
            num_HelmPhysical.Enabled = !cb_FindHelm.Checked;
            num_HelmFire.Enabled = !cb_FindHelm.Checked;
            num_HelmCold.Enabled = !cb_FindHelm.Checked;
            num_HelmPoison.Enabled = !cb_FindHelm.Checked;
            num_HelmEnergy.Enabled = !cb_FindHelm.Checked;
        }

        private void cb_FindLegs_CheckedChanged(object sender, EventArgs e)
        {
            num_LegsPhysical.Enabled = !cb_FindLegs.Checked;
            num_LegsFire.Enabled = !cb_FindLegs.Checked;
            num_LegsCold.Enabled = !cb_FindLegs.Checked;
            num_LegsPoison.Enabled = !cb_FindLegs.Checked;
            num_LegsEnergy.Enabled = !cb_FindLegs.Checked;
        }

        private void num_ArmsCold_ValueChanged(object sender, EventArgs e)
        {
            _selectedArms.ColdResist = (int)((NumericUpDown)sender).Value;
        }

        private void num_ArmsEnergy_ValueChanged(object sender, EventArgs e)
        {
            _selectedArms.EnergyResist = (int)((NumericUpDown)sender).Value;
        }

        private void num_ArmsFire_ValueChanged(object sender, EventArgs e)
        {
            _selectedArms.FireResist = (int)((NumericUpDown)sender).Value;
        }

        private void num_ArmsPhysical_ValueChanged(object sender, EventArgs e)
        {
            _selectedArms.PhysicalResist = (int)((NumericUpDown)sender).Value;
        }

        private void num_ArmsPoison_ValueChanged(object sender, EventArgs e)
        {
            _selectedArms.PoisonResist = (int)((NumericUpDown)sender).Value;
        }

        private void num_ChestCold_ValueChanged(object sender, EventArgs e)
        {
            _selectedChest.ColdResist = (int)((NumericUpDown)sender).Value;
        }

        private void num_ChestEnergy_ValueChanged(object sender, EventArgs e)
        {
            _selectedChest.EnergyResist = (int)((NumericUpDown)sender).Value;
        }

        private void num_ChestFire_ValueChanged(object sender, EventArgs e)
        {
            _selectedChest.FireResist = (int)((NumericUpDown)sender).Value;
        }

        private void num_ChestPhysical_ValueChanged(object sender, EventArgs e)
        {
            _selectedChest.PhysicalResist = (int)((NumericUpDown)sender).Value;
        }

        private void num_ChestPoison_ValueChanged(object sender, EventArgs e)
        {
            _selectedChest.PoisonResist = (int)((NumericUpDown)sender).Value;
        }

        private void num_GlovesCold_ValueChanged(object sender, EventArgs e)
        {
            _selectedGloves.ColdResist = (int)((NumericUpDown)sender).Value;
        }

        private void num_GlovesEnergy_ValueChanged(object sender, EventArgs e)
        {
            _selectedGloves.EnergyResist = (int)((NumericUpDown)sender).Value;
        }

        private void num_GlovesFire_ValueChanged(object sender, EventArgs e)
        {
            _selectedGloves.FireResist = (int)((NumericUpDown)sender).Value;
        }

        private void num_GlovesPhysical_ValueChanged(object sender, EventArgs e)
        {
            _selectedGloves.PhysicalResist = (int)((NumericUpDown)sender).Value;
        }

        private void num_GlovesPoison_ValueChanged(object sender, EventArgs e)
        {
            _selectedGloves.PoisonResist = (int)((NumericUpDown)sender).Value;
        }

        private void num_HelmCold_ValueChanged(object sender, EventArgs e)
        {
            _selectedHelm.ColdResist = (int)((NumericUpDown)sender).Value;
        }

        private void num_HelmEnergy_ValueChanged(object sender, EventArgs e)
        {
            _selectedHelm.EnergyResist = (int)((NumericUpDown)sender).Value;
        }

        private void num_HelmFire_ValueChanged(object sender, EventArgs e)
        {
            _selectedHelm.FireResist = (int)((NumericUpDown)sender).Value;
        }

        private void num_HelmPhysical_ValueChanged(object sender, EventArgs e)
        {
            _selectedHelm.PhysicalResist = (int)((NumericUpDown)sender).Value;
        }

        private void num_HelmPoison_ValueChanged(object sender, EventArgs e)
        {
            _selectedHelm.PoisonResist = (int)((NumericUpDown)sender).Value;
        }

        private void num_LegsCold_ValueChanged(object sender, EventArgs e)
        {
            _selectedLegs.ColdResist = (int)((NumericUpDown)sender).Value;
        }

        private void num_LegsEnergy_ValueChanged(object sender, EventArgs e)
        {
            _selectedLegs.EnergyResist = (int)((NumericUpDown)sender).Value;
        }

        private void num_LegsFire_ValueChanged(object sender, EventArgs e)
        {
            _selectedLegs.FireResist = (int)((NumericUpDown)sender).Value;
        }

        private void num_LegsPhysical_ValueChanged(object sender, EventArgs e)
        {
            _selectedLegs.PhysicalResist = (int)((NumericUpDown)sender).Value;
        }

        private void num_LegsPoison_ValueChanged(object sender, EventArgs e)
        {
            _selectedLegs.PoisonResist = (int)((NumericUpDown)sender).Value;
        }

        #endregion Event Handlers

        public Armor CheckForBetterFit(Armor currentArmor, IEnumerable<Armor> otherPieces, Suit currentSuit)
        {
            var suitEvaluatorService = new SuitEvaluatorService(currentSuit);
            var candidates = new List<ArmorCandidate>();
            foreach (var candidate in otherPieces)
            {
                var armorCandidate = ConvertToCandidate(currentArmor, currentSuit, candidate);
                candidates.Add(armorCandidate);
            }

            if (!candidates.Any()) return null;

            var totalLostResistsGroup = candidates.GroupBy(c => c.PhysicalCandidate.Value + c.FireCandidate.Value + c.ColdCandidate.Value + c.PoisonCandidate.Value + c.EnergyCandidate.Value);
            var lowestWastedResists = totalLostResistsGroup.OrderBy(c => c.Key).First();
            foreach (var candidate in lowestWastedResists)
            {
                if (candidate.Armor == currentArmor) continue;

                var armorChanged = false;
                if (suitEvaluatorService.PhysicalLowest)
                {
                    armorChanged = candidate.Armor.PhysicalResist > currentArmor.PhysicalResist;
                }
                else if (suitEvaluatorService.FireLowest)
                {
                    armorChanged = candidate.Armor.FireResist > currentArmor.FireResist;
                }
                else if (suitEvaluatorService.ColdLowest)
                {
                    armorChanged = candidate.Armor.ColdResist > currentArmor.ColdResist;
                }
                else if (suitEvaluatorService.PoisonLowest)
                {
                    armorChanged = candidate.Armor.PoisonResist > currentArmor.PoisonResist;
                }
                else if (suitEvaluatorService.EnergyLowest)
                {
                    armorChanged = candidate.Armor.EnergyResist > currentArmor.EnergyResist;
                }

                if (armorChanged) return candidate.Armor;
            }

            return null;
        }

        private void AddImbuable(Armor armorPiece, Armor maxImbueable, ref List<Armor> armorPieces)
        {
            if (MaxImbues < 1) return;

            var armorEvaluatorService = new ArmorEvaluatorService(armorPiece, maxImbueable);

            var physicalImbue = armorEvaluatorService.ImbuePhysical();
            armorPieces.Add(physicalImbue);
            for (var i = 2; i <= MaxImbues; i++)
            {
                if (physicalImbue == null) break;

                var secondaryImbue = PerformFirstLowestImbue(maxImbueable, physicalImbue);
                if (secondaryImbue != null)
                {
                    armorPieces.Add(secondaryImbue);
                }
                physicalImbue = secondaryImbue;
            }

            var fireImbue = armorEvaluatorService.ImbueFire();
            armorPieces.Add(fireImbue);
            for (var i = 2; i <= MaxImbues; i++)
            {
                if (fireImbue == null) break;

                var secondaryImbue = PerformFirstLowestImbue(maxImbueable, fireImbue);
                if (secondaryImbue != null)
                {
                    armorPieces.Add(secondaryImbue);
                }
                fireImbue = secondaryImbue;
            }

            var energyImbue = armorEvaluatorService.ImbueEnergy();
            armorPieces.Add(energyImbue);
            for (var i = 2; i <= MaxImbues; i++)
            {
                if (energyImbue == null) break;

                var secondaryImbue = PerformFirstLowestImbue(maxImbueable, energyImbue);
                if (secondaryImbue != null)
                {
                    armorPieces.Add(secondaryImbue);
                }
                energyImbue = secondaryImbue;
            }

            var coldImbue = armorEvaluatorService.ImbueCold();
            armorPieces.Add(coldImbue);
            for (var i = 2; i <= MaxImbues; i++)
            {
                if (coldImbue == null) break;

                var secondaryImbue = PerformFirstLowestImbue(maxImbueable, coldImbue);
                if (secondaryImbue != null)
                {
                    armorPieces.Add(secondaryImbue);
                }
                coldImbue = secondaryImbue;
            }

            var poisonImbue = armorEvaluatorService.ImbuePoison();
            armorPieces.Add(poisonImbue);
            for (var i = 2; i <= MaxImbues; i++)
            {
                if (poisonImbue == null) break;

                var secondaryImbue = PerformFirstLowestImbue(maxImbueable, poisonImbue);
                if (secondaryImbue != null)
                {
                    armorPieces.Add(secondaryImbue);
                }
                poisonImbue = secondaryImbue;
            }
        }

        private void CheckPieces(List<Armor> headPieces, List<Armor> chestPieces, List<Armor> armPieces, List<Armor> glovePieces, List<Armor> legPieces, Suit suit)
        {
            var foundBetterFit = true;
            var iterations = 1;
            while (foundBetterFit && iterations < MaxIterations)
            {
                Debug.WriteLine($"Starting iteration '{iterations}'.");
                var betterHelm = CheckForBetterFit(suit.Helm, headPieces, suit);
                if (betterHelm != null)
                {
                    Console.WriteLine("Changed Helm.");
                    UpdateSlot(betterHelm, suit);
                }

                var betterChest = CheckForBetterFit(suit.Chest, chestPieces, suit);
                if (betterChest != null)
                {
                    Console.WriteLine("Changed Chest.");
                    UpdateSlot(betterChest, suit);
                }

                var betterArms = CheckForBetterFit(suit.Arms, armPieces, suit);
                if (betterArms != null)
                {
                    Console.WriteLine("Changed Arms.");
                    UpdateSlot(betterArms, suit);
                }

                var betterGloves = CheckForBetterFit(suit.Gloves, glovePieces, suit);
                if (betterGloves != null)
                {
                    Console.WriteLine("Changed Gloves.");
                    UpdateSlot(betterGloves, suit);
                }

                var betterLegs = CheckForBetterFit(suit.Legs, legPieces, suit);
                if (betterLegs != null)
                {
                    Console.WriteLine("Changed Legs.");
                    UpdateSlot(betterLegs, suit);
                }

                foundBetterFit = betterHelm != null || betterChest != null || betterArms != null || betterGloves != null || betterLegs != null;
                iterations++;
            }
        }

        private ArmorCandidate ConvertToCandidate(Armor currentArmor, Suit currentSuit, Armor candidate)
        {
            var basePhysical = currentSuit.CurrentResists.PhysicalResist - currentArmor.PhysicalResist;
            var newPhysical = basePhysical + candidate.PhysicalResist - currentSuit.MaxResists.PhysicalResist;

            var baseFire = currentSuit.CurrentResists.FireResist - currentArmor.FireResist;
            var newFire = baseFire + candidate.FireResist - currentSuit.MaxResists.FireResist;

            var baseCold = currentSuit.CurrentResists.ColdResist - currentArmor.ColdResist;
            var newCold = baseCold + candidate.ColdResist - currentSuit.MaxResists.ColdResist;

            var basePoison = currentSuit.CurrentResists.PoisonResist - currentArmor.PoisonResist;
            var newPoison = basePoison + candidate.PoisonResist - currentSuit.MaxResists.PoisonResist;

            var baseEnergy = currentSuit.CurrentResists.EnergyResist - currentArmor.EnergyResist;
            var newEnergy = baseEnergy + candidate.EnergyResist - currentSuit.MaxResists.EnergyResist;

            return new ArmorCandidate
            {
                Armor = candidate,
                PhysicalCandidate = FormCandidate(newPhysical),
                FireCandidate = FormCandidate(newFire),
                ColdCandidate = FormCandidate(newCold),
                PoisonCandidate = FormCandidate(newPoison),
                EnergyCandidate = FormCandidate(newEnergy),
            };
        }

        private string ExportArmor(string name, Armor armor)
        {
            return $"{name}\t{armor.PhysicalResist}\t{armor.FireResist}\t{armor.ColdResist}\t{armor.PoisonResist}\t{armor.EnergyResist}\t'{armor.ImbueCount}' Imbues, Lost: {armor.LostResistPoints}";
        }

        private Candidate FormCandidate(int newResist)
        {
            return new Candidate
            {
                Value = Math.Abs(newResist),
                OverMax = newResist > 0,
                UnderMax = newResist < 0,
            };
        }

        private Armor GetBasicResists(ResourceType resourceType, bool headSlot = false)
        {
            switch (resourceType)
            {
                case ResourceType.Oak:
                case ResourceType.Heartwood:
                    if (headSlot)
                    {
                        return new Armor
                        {
                            PhysicalResist = 5,
                            FireResist = 1,
                            ColdResist = 2,
                            PoisonResist = 2,
                            EnergyResist = 5,
                        };
                    }

                    return new Armor
                    {
                        PhysicalResist = 7,
                        FireResist = 3,
                        ColdResist = 2,
                        PoisonResist = 3,
                        EnergyResist = 2,
                    };

                case ResourceType.Spined:
                case ResourceType.Horned:
                case ResourceType.Barbed:
                    return new Armor
                    {
                        PhysicalResist = 2,
                        FireResist = 4,
                        ColdResist = 3,
                        PoisonResist = 3,
                        EnergyResist = 3,
                    };

                case ResourceType.Iron:
                case ResourceType.Verite:
                case ResourceType.Valorite:
                    if (headSlot)
                    {
                        return new Armor
                        {
                            PhysicalResist = 7,
                            FireResist = 2,
                            ColdResist = 2,
                            PoisonResist = 2,
                            EnergyResist = 2,
                        };
                    }

                    return new Armor
                    {
                        PhysicalResist = 5,
                        FireResist = 3,
                        ColdResist = 2,
                        PoisonResist = 3,
                        EnergyResist = 2,
                    };

                default:
                    throw new ArgumentOutOfRangeException(nameof(resourceType), resourceType, null);
            }
        }

        private Armor GetBonus(ResourceType resourceType)
        {
            switch (resourceType)
            {
                case ResourceType.Heartwood:
                    return new Armor
                    {
                        PhysicalResist = 0,
                        FireResist = 3,
                        ColdResist = 2,
                        PoisonResist = 7,
                        EnergyResist = 2,
                    };

                case ResourceType.Oak:
                    return new Armor
                    {
                        PhysicalResist = 1,
                        FireResist = 3,
                        ColdResist = 0,
                        PoisonResist = 2,
                        EnergyResist = 3,
                    };

                case ResourceType.Spined:
                    return new Armor
                    {
                        PhysicalResist = 5,
                        FireResist = 0,
                        ColdResist = 0,
                        PoisonResist = 0,
                        EnergyResist = 0,
                    };

                case ResourceType.Barbed:
                    return new Armor
                    {
                        PhysicalResist = 2,
                        FireResist = 1,
                        ColdResist = 2,
                        PoisonResist = 3,
                        EnergyResist = 4,
                    };

                case ResourceType.Iron:
                    return new Armor();

                case ResourceType.Verite:
                    return new Armor
                    {
                        PhysicalResist = 3,
                        FireResist = 3,
                        ColdResist = 2,
                        PoisonResist = 3,
                        EnergyResist = 1,
                    };

                case ResourceType.Horned:
                case ResourceType.Valorite:
                default:
                    throw new ArgumentOutOfRangeException(nameof(resourceType), resourceType, null);
            }
        }

        private void LoadResourceTypes()
        {
            var resourceTypes = new List<ResourceType>
            {
                ResourceType.Oak,
                ResourceType.Heartwood,
                ResourceType.Spined,
                ResourceType.Horned,
                ResourceType.Barbed,
                ResourceType.Iron,
                ResourceType.Verite,
                ResourceType.Valorite
            };
            resourceTypes.Sort();
            cbb_HelmResourceType.DataSource = resourceTypes.ToList();
            cbb_ChestResourceType.DataSource = resourceTypes.ToList();
            cbb_ArmsResourceType.DataSource = resourceTypes.ToList();
            cbb_LegsResourceType.DataSource = resourceTypes.ToList();

            var gloveResourceTypes = new List<ResourceType>
            {
                ResourceType.Oak,
                ResourceType.Heartwood,
                ResourceType.Spined,
                ResourceType.Horned,
                ResourceType.Barbed,
            };
            gloveResourceTypes.Sort();
            cbb_GlovesResourceType.DataSource = gloveResourceTypes;
        }

        private void LookupBonuses(ResourceType resourceType, Armor imbueBonuses, out Armor bonusResist, out Armor maxResist, bool headPiece = false)
        {
            var baseResist = GetBasicResists(resourceType, headPiece);
            bonusResist = GetBonus(resourceType);
            maxResist = new Armor();
            maxResist.Add(baseResist, bonusResist, imbueBonuses);
        }

        private Armor PerformFirstLowestImbue(Armor bonusHead, Armor basePiece)
        {
            var armorEvaluatorService = new ArmorEvaluatorService(basePiece, bonusHead);
            if (armorEvaluatorService.PhysicalLowest) return armorEvaluatorService.ImbuePhysical();
            if (armorEvaluatorService.FireLowest) return armorEvaluatorService.ImbueFire();
            if (armorEvaluatorService.EnergyLowest) return armorEvaluatorService.ImbueEnergy();
            if (armorEvaluatorService.ColdLowest) return armorEvaluatorService.ImbueCold();
            if (armorEvaluatorService.PoisonLowest) return armorEvaluatorService.ImbuePoison();

            return null;
        }

        private void SortPieces(List<Armor> armorPieces, Armor helmBonus, Armor helmMax, Armor chestBonus, Armor chestMax, Armor armsBonus, Armor armsMax, Armor glovesBonus, Armor glovesMax, Armor legsBonus, Armor legsMax, ref List<Armor> headPieces, ref List<Armor> chestPieces, ref List<Armor> armPieces, ref List<Armor> glovePieces, ref List<Armor> legPieces)
        {
            foreach (var armorPiece in armorPieces)
            {
                if (armorPiece.PhysicalResist == 0 && armorPiece.FireResist == 0 && armorPiece.ColdResist == 0 && armorPiece.PoisonResist == 0 && armorPiece.EnergyResist == 0) continue;

                switch (armorPiece.Slot)
                {
                    case SlotTypes.Helm:
                        if (!FindHelm) break;

                        if (ApplyHelmBonus)
                        {
                            armorPiece.Add(helmBonus);
                        }

                        headPieces.Add(armorPiece);
                        AddImbuable(armorPiece, helmMax, ref headPieces);
                        break;

                    case SlotTypes.Chest:
                        if (!FindChest) break;

                        if (ApplyChestBonus)
                        {
                            armorPiece.Add(chestBonus);
                        }

                        chestPieces.Add(armorPiece);
                        AddImbuable(armorPiece, chestMax, ref chestPieces);
                        break;

                    case SlotTypes.Arms:
                        if (!FindArms) break;

                        if (ApplyArmsBonus)
                        {
                            armorPiece.Add(armsBonus);
                        }

                        armPieces.Add(armorPiece);
                        AddImbuable(armorPiece, armsMax, ref armPieces);
                        break;

                    case SlotTypes.Gloves:
                        if (!FindGloves) break;

                        if (ApplyGlovesBonus)
                        {
                            armorPiece.Add(glovesBonus);
                        }

                        glovePieces.Add(armorPiece);
                        AddImbuable(armorPiece, glovesMax, ref glovePieces);
                        break;

                    case SlotTypes.Legs:
                        if (!FindLegs) break;

                        if (ApplyLegsBonus)
                        {
                            armorPiece.Add(legsBonus);
                        }

                        legPieces.Add(armorPiece);
                        AddImbuable(armorPiece, legsMax, ref legPieces);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void UpdateSlot(Armor newPiece, Suit suit)
        {
            switch (newPiece.Slot)
            {
                case SlotTypes.Helm:
                    suit.CurrentResists.PhysicalResist = suit.CurrentResists.PhysicalResist - suit.Helm.PhysicalResist + newPiece.PhysicalResist;
                    suit.CurrentResists.FireResist = suit.CurrentResists.FireResist - suit.Helm.FireResist + newPiece.FireResist;
                    suit.CurrentResists.ColdResist = suit.CurrentResists.ColdResist - suit.Helm.ColdResist + newPiece.ColdResist;
                    suit.CurrentResists.PoisonResist = suit.CurrentResists.PoisonResist - suit.Helm.PoisonResist + newPiece.PoisonResist;
                    suit.CurrentResists.EnergyResist = suit.CurrentResists.EnergyResist - suit.Helm.EnergyResist + newPiece.EnergyResist;
                    suit.Helm = newPiece;
                    break;

                case SlotTypes.Chest:
                    suit.CurrentResists.PhysicalResist = suit.CurrentResists.PhysicalResist - suit.Chest.PhysicalResist + newPiece.PhysicalResist;
                    suit.CurrentResists.FireResist = suit.CurrentResists.FireResist - suit.Chest.FireResist + newPiece.FireResist;
                    suit.CurrentResists.ColdResist = suit.CurrentResists.ColdResist - suit.Chest.ColdResist + newPiece.ColdResist;
                    suit.CurrentResists.PoisonResist = suit.CurrentResists.PoisonResist - suit.Chest.PoisonResist + newPiece.PoisonResist;
                    suit.CurrentResists.EnergyResist = suit.CurrentResists.EnergyResist - suit.Chest.EnergyResist + newPiece.EnergyResist;
                    suit.Chest = newPiece;
                    break;

                case SlotTypes.Arms:
                    suit.CurrentResists.PhysicalResist = suit.CurrentResists.PhysicalResist - suit.Arms.PhysicalResist + newPiece.PhysicalResist;
                    suit.CurrentResists.FireResist = suit.CurrentResists.FireResist - suit.Arms.FireResist + newPiece.FireResist;
                    suit.CurrentResists.ColdResist = suit.CurrentResists.ColdResist - suit.Arms.ColdResist + newPiece.ColdResist;
                    suit.CurrentResists.PoisonResist = suit.CurrentResists.PoisonResist - suit.Arms.PoisonResist + newPiece.PoisonResist;
                    suit.CurrentResists.EnergyResist = suit.CurrentResists.EnergyResist - suit.Arms.EnergyResist + newPiece.EnergyResist;
                    suit.Arms = newPiece;
                    break;

                case SlotTypes.Gloves:
                    suit.CurrentResists.PhysicalResist = suit.CurrentResists.PhysicalResist - suit.Gloves.PhysicalResist + newPiece.PhysicalResist;
                    suit.CurrentResists.FireResist = suit.CurrentResists.FireResist - suit.Gloves.FireResist + newPiece.FireResist;
                    suit.CurrentResists.ColdResist = suit.CurrentResists.ColdResist - suit.Gloves.ColdResist + newPiece.ColdResist;
                    suit.CurrentResists.PoisonResist = suit.CurrentResists.PoisonResist - suit.Gloves.PoisonResist + newPiece.PoisonResist;
                    suit.CurrentResists.EnergyResist = suit.CurrentResists.EnergyResist - suit.Gloves.EnergyResist + newPiece.EnergyResist;
                    suit.Gloves = newPiece;
                    break;

                case SlotTypes.Legs:
                    suit.CurrentResists.PhysicalResist = suit.CurrentResists.PhysicalResist - suit.Legs.PhysicalResist + newPiece.PhysicalResist;
                    suit.CurrentResists.FireResist = suit.CurrentResists.FireResist - suit.Legs.FireResist + newPiece.FireResist;
                    suit.CurrentResists.ColdResist = suit.CurrentResists.ColdResist - suit.Legs.ColdResist + newPiece.ColdResist;
                    suit.CurrentResists.PoisonResist = suit.CurrentResists.PoisonResist - suit.Legs.PoisonResist + newPiece.PoisonResist;
                    suit.CurrentResists.EnergyResist = suit.CurrentResists.EnergyResist - suit.Legs.EnergyResist + newPiece.EnergyResist;
                    suit.Legs = newPiece;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}