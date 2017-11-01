using ArmorOptimizer.Attributes;
using ArmorOptimizer.Autogen;
using ArmorOptimizer.Data.Models;
using ArmorOptimizer.Enums;
using ArmorOptimizer.Extensions;
using ArmorOptimizer.Models;
using ArmorOptimizer.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ArmorOptimizer.Forms
{
    public partial class ArmorPickerForm : Form
    {
        private readonly ArmorDetailsForm _armorDetailsForm = new ArmorDetailsForm();
        private readonly ImporterService _importerService = new ImporterService();
        private List<ArmorViewModel> _armorPieces;

        public ArmorPickerForm()
        {
            InitializeComponent();
        }

        #region Data Files

        private const string ArmsFile = "Data/Arms.json";
        private const string ChestFile = "Data/Chests.json";
        private const string GlovesFile = "Data/Gloves.json";
        private const string HelmFile = "Data/Helms.json";
        private const string LegsFile = "Data/Legs.json";
        private const string ResistConfigurationFile = "Data/ResistConfigurations.json";
        private const string ResourceKindFile = "Data/ResourceKinds.json";

        #endregion Data Files

        #region UI

        private Dictionary<string, IList<ItemViewModel>> _knownItemTypes;
        private int _maxImbues = 2;
        private int _maxIterations = 1000;
        private int _maxResistBonus = 13;
        private Dictionary<long, ResistConfiguration> _resistConfigurations;
        private IList<Resource> _resourceRecords;
        private IList<Resource> _resources;
        private IList<ArmorViewModel> _selectedArmor;
        private string FileToImport => tb_FileToImport.Text;
        private int GoalCold => (int)num_GoalCold.Value;
        private int GoalEnergy => (int)num_GoalEnergy.Value;
        private int GoalFire => (int)num_GoalFire.Value;
        private int GoalPhysical => (int)num_GoalPhysical.Value;
        private int GoalPoison => (int)num_GoalPoison.Value;
        private ArmorViewModel SelectedArms => _selectedArmor.FirstOrDefault(r => r.Slot == SlotTypes.Arms);
        private Resource SelectedArmsResource => (Resource)cbb_ArmsResourceType.SelectedItem;
        private ArmorViewModel SelectedChest => _selectedArmor.FirstOrDefault(r => r.Slot == SlotTypes.Chest);
        private Resource SelectedChestResource => (Resource)cbb_ChestResourceType.SelectedItem;
        private ArmorViewModel SelectedGloves => _selectedArmor.FirstOrDefault(r => r.Slot == SlotTypes.Gloves);
        private Resource SelectedGlovesResource => (Resource)cbb_GlovesResourceType.SelectedItem;
        private ArmorViewModel SelectedHelm => _selectedArmor.FirstOrDefault(r => r.Slot == SlotTypes.Helm);
        private Resource SelectedHelmResource => (Resource)cbb_HelmResourceType.SelectedItem;
        private ArmorViewModel SelectedLegs => _selectedArmor.FirstOrDefault(r => r.Slot == SlotTypes.Legs);
        private Resource SelectedLegsResource => (Resource)cbb_LegsResourceType.SelectedItem;
        private ArmorViewModel SelectedMisc => _selectedArmor.FirstOrDefault(r => r.Slot == SlotTypes.Unknown);

        private void PushSuitToUi(Suit suit)
        {
            _selectedArmor.Clear();
            _selectedArmor.Add(suit.Helm);
            _selectedArmor.Add(suit.Chest);
            _selectedArmor.Add(suit.Arms);
            _selectedArmor.Add(suit.Gloves);
            _selectedArmor.Add(suit.Legs);
        }

        #endregion UI

        #region Event Handlers

        private void ArmorPickerForm_Load(object sender, EventArgs e)
        {
            try
            {
                var resistContext = new DatabaseContext();
                var resistConfigurationRecords = resistContext.ResistConfiguration.Select(r => r).AsNoTracking().ToList();
                _resistConfigurations = resistConfigurationRecords.ToDictionary(record => record.Id);

                var resourceContext = new DatabaseContext();
                _resourceRecords = resourceContext.Resource
                    .Include(r => r.Resist)
                    .Select(r => r).AsNoTracking().ToList();

                _knownItemTypes = new Dictionary<string, IList<ItemViewModel>>();
                var itemContext = new DatabaseContext();
                var typeRecords = itemContext.Item
                    .Include(i => i.ItemType)
                    .Select(i => i).GroupBy(r => r.ItemType).AsNoTracking().ToList();
                foreach (var itemType in typeRecords)
                {
                    var items = new List<ItemViewModel>();
                    foreach (var item in itemType)
                    {
                        var itemViewModel = new ItemViewModel
                        {
                            Slot = (SlotTypes)(int)item.ItemType.Slot,
                            Id = item.Id,
                            Type = item.ItemType.ItemType,
                            BaseResistId = item.BaseResistId,
                            Color = item.Color,
                            ItemType = item.ItemType,
                            ColdResist = item.ColdResist,
                            BaseResist = item.BaseResist,
                            EnergyResist = item.EnergyResist,
                            FireResist = item.FireResist,
                            ItemTypeId = item.ItemTypeId,
                            PhysicalResist = item.PhysicalResist,
                            PoisonResist = item.PoisonResist,
                        };
                        items.Add(itemViewModel);
                    }
                    _knownItemTypes.Add(itemType.Key.ItemType, items);
                }

                BindResourcesToUi();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Failed to load Data Mappings.\r\n\r\nError: {exception}");
                Close();
            }
        }

        private void BindResourcesToUi()
        {
            _resources = _resourceRecords.Select(r => new Resource
            {
                Id = r.Id,
                Name = r.Name,
                ResistId = r.ResistId,
                Resist = _resistConfigurations[r.ResistId],
            }).ToEnumeratedList();

            cbb_HelmResourceType.DisplayMember = "Name";
            cbb_HelmResourceType.DataSource = _resources.ToList();
            cbb_ChestResourceType.DisplayMember = "Name";
            cbb_ChestResourceType.DataSource = _resources.ToList();
            cbb_ArmsResourceType.DisplayMember = "Name";
            cbb_ArmsResourceType.DataSource = _resources.ToList();
            cbb_GlovesResourceType.DisplayMember = "Name";
            cbb_GlovesResourceType.DataSource = _resources.ToList();
            cbb_LegsResourceType.DisplayMember = "Name";
            cbb_LegsResourceType.DataSource = _resources.ToList();
        }

        private void BindSuitToUi()
        {
            _selectedArmor = new List<ArmorViewModel>
            {
                new ArmorViewModel
                {
                    Slot = SlotTypes.Helm,
                    CurrentResists = new Resists(),
                },
                new ArmorViewModel
                {
                    Slot = SlotTypes.Chest,
                    CurrentResists = new Resists(),
                },
                new ArmorViewModel
                {
                    Slot = SlotTypes.Arms,
                    CurrentResists = new Resists(),
                },
                new ArmorViewModel
                {
                    Slot = SlotTypes.Gloves,
                    CurrentResists = new Resists(),
                },
                new ArmorViewModel
                {
                    Slot = SlotTypes.Legs,
                    CurrentResists = new Resists(),
                },
                new ArmorViewModel
                {
                    Slot = SlotTypes.Unknown,
                    CurrentResists = new Resists(),
                },
            };
            dgv_SelectedArmor.AutoGenerateColumns = false;
            var bindingSource = new BindingSource { DataSource = _selectedArmor };
            dgv_SelectedArmor.DataSource = bindingSource;
        }

        private void btn_ArmsDetails_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedArms = SelectedArms;
                if (string.IsNullOrWhiteSpace(selectedArms.Id)) return;

                var basePiece = _armorPieces.FirstOrDefault(p => p.Id == selectedArms.Id);
                if (basePiece == null)
                {
                    MessageBox.Show($@"Failed to locate base piece with Id '{selectedArms.Id}'.");
                    return;
                }

                _armorDetailsForm.PopulateForm(SelectedArmsResource, basePiece, selectedArms);
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
                var selectedChest = SelectedChest;
                if (string.IsNullOrWhiteSpace(selectedChest.Id)) return;

                var basePiece = _armorPieces.FirstOrDefault(p => p.Id == selectedChest.Id);
                if (basePiece == null)
                {
                    MessageBox.Show($@"Failed to locate base piece with Id '{selectedChest.Id}'.");
                    return;
                }

                _armorDetailsForm.PopulateForm(SelectedChestResource, basePiece, selectedChest);
                _armorDetailsForm.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void btn_Configure_Click(object sender, EventArgs e)
        {
            try
            {
                var resistConfigurations = _resistConfigurations.Select(c => c.Value).ToEnumeratedList();
                var resourceRecords = _resourceRecords;
                var armorTypes = _knownItemTypes.SelectMany(t => t.Value);
                var configurationEditor = new ConfigurationEditor(resistConfigurations, resourceRecords, armorTypes);
                var result = configurationEditor.ShowDialog();
                if (result != DialogResult.OK) return;

                var newResists = JsonConvert.SerializeObject(configurationEditor.ResistConfigurations, Formatting.Indented);
                File.WriteAllText(ResistConfigurationFile, newResists);
                _resistConfigurations = configurationEditor.ResistConfigurations.ToDictionary(record => record.Id);

                var newResources = JsonConvert.SerializeObject(configurationEditor.Resources, Formatting.Indented);
                File.WriteAllText(ResourceKindFile, newResources);
                _resourceRecords = configurationEditor.Resources;

                var helmRecords = new List<Item>();
                var chestRecords = new List<Item>();
                var armRecords = new List<Item>();
                var gloveRecords = new List<Item>();
                var legRecords = new List<Item>();
                var combinedTypes = configurationEditor.ArmorTypes;
                foreach (var armorType in combinedTypes)
                {
                    switch (armorType.Slot)
                    {
                        case SlotTypes.Helm:
                            helmRecords.Add(armorType);
                            break;

                        case SlotTypes.Chest:
                            chestRecords.Add(armorType);
                            break;

                        case SlotTypes.Arms:
                            armRecords.Add(armorType);
                            break;

                        case SlotTypes.Gloves:
                            gloveRecords.Add(armorType);
                            break;

                        case SlotTypes.Legs:
                            legRecords.Add(armorType);
                            break;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                var helmRecordData = JsonConvert.SerializeObject(helmRecords, Formatting.Indented);
                File.WriteAllText(HelmFile, helmRecordData);
                var chestRecordData = JsonConvert.SerializeObject(chestRecords, Formatting.Indented);
                File.WriteAllText(ChestFile, chestRecordData);
                var armRecordData = JsonConvert.SerializeObject(armRecords, Formatting.Indented);
                File.WriteAllText(ArmsFile, armRecordData);
                var gloveRecordData = JsonConvert.SerializeObject(gloveRecords, Formatting.Indented);
                File.WriteAllText(GlovesFile, gloveRecordData);
                var legRecordData = JsonConvert.SerializeObject(legRecords, Formatting.Indented);
                File.WriteAllText(LegsFile, legRecordData);

                _knownItemTypes.Clear();
                foreach (var itemType in combinedTypes.GroupBy(r => r.Type))
                {
                    _knownItemTypes.Add(itemType.Key, itemType.ToEnumeratedList());
                }
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
                var selectedGloves = SelectedGloves;
                if (string.IsNullOrWhiteSpace(selectedGloves.Id)) return;

                var basePiece = _armorPieces.FirstOrDefault(p => p.Id == selectedGloves.Id);
                if (basePiece == null)
                {
                    MessageBox.Show($@"Failed to locate base piece with Id '{selectedGloves.Id}'.");
                    return;
                }

                _armorDetailsForm.PopulateForm(SelectedGlovesResource, basePiece, selectedGloves);
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
                var selectedHelm = SelectedHelm;
                if (string.IsNullOrWhiteSpace(selectedHelm.Id)) return;

                var basePiece = _armorPieces.FirstOrDefault(p => p.Id == selectedHelm.Id);
                if (basePiece == null)
                {
                    MessageBox.Show($@"Failed to locate base piece with Id '{selectedHelm.Id}'.");
                    return;
                }

                _armorDetailsForm.PopulateForm(SelectedHelmResource, basePiece, selectedHelm);
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
                var fileText = importerService.ReadFile(FileToImport);
                var lines = importerService.SplitString(fileText, '\n');

                var dict = new Dictionary<string, int>();
                var easyUoRecord = new EasyUoRecord();
                var actualtype = easyUoRecord.GetType();
                foreach (var propertyInfo in actualtype.GetProperties())
                {
                    foreach (ColumnNumber columnNumber in propertyInfo.GetCustomAttributes(typeof(ColumnNumber), false))
                    {
                        dict.Add(propertyInfo.Name, columnNumber.Value);
                        break;
                    }
                }

                var armors = new List<ArmorViewModel>();
                foreach (var line in lines)
                {
                    var data = importerService.SplitString(line, '.');
                    var dataArray = data?.ToArray();
                    if (data == null || dataArray.Length == 0) continue;

                    // Check for Resource based on Color
                    var itemColor = int.Parse(dataArray[dict[nameof(easyUoRecord.Color)]]);
                    var resource = _resources.FirstOrDefault(r => r.Color == itemColor);
                    if (resource == null)
                    {
                        var result = MessageBox.Show($@"Unable find known color for '{itemColor}'." +
                                                     "\r\n\r\nWould you like to skip the record?",
                                                     @"Unknown Color", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes) continue;
                        return;
                    }

                    // This tells us the Base Resists for this item
                    var itemType = dataArray[dict[nameof(easyUoRecord.ItemType)]];
                    var armorRecords = _knownItemTypes[itemType];
                    var baseArmor = armorRecords.FirstOrDefault(r => r.Color == itemColor);
                    if (baseArmor == null)
                    {
                        // Type to Color is a one to many relationship
                        var result = MessageBox.Show($@"Unable find Type '{itemType}' with Color '{itemColor}'." +
                                                     "\r\n\r\nWould you like to skip the record?",
                                                     @"Unexpected item Type/Color combination", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes) continue;
                        return;
                    }

                    // Fetch the basics
                    var currentResists = new Resists
                    {
                        Physical = int.Parse(dataArray[dict[nameof(easyUoRecord.Physical)]]),
                        Fire = int.Parse(dataArray[dict[nameof(easyUoRecord.Fire)]]),
                        Cold = int.Parse(dataArray[dict[nameof(easyUoRecord.Cold)]]),
                        Poison = int.Parse(dataArray[dict[nameof(easyUoRecord.Poison)]]),
                        Energy = int.Parse(dataArray[dict[nameof(easyUoRecord.Energy)]]),
                    };
                    var armor = new ArmorViewModel
                    {
                        CurrentResists = currentResists,
                        Slot = baseArmor.Slot,
                    };
                    armor.Id = dataArray[dict[nameof(armor.Id)]];

                    // Set it's minimums
                    armor.BaseResists = ConvertConfigurationToResist(_resistConfigurations[baseArmor.BaseResistId]);

                    // Check if it's imbued already
                    armor.EvaluateImbuedResists(_maxResistBonus);

                    // Add the bonus to the base if it it's not the default color
                    if (itemColor != baseArmor.Color)
                    {
                        var resourceResists = ConvertConfigurationToResist(_resistConfigurations[resource.ResistId]);
                        armor.CurrentResists.Add(resourceResists);
                    }
                    else
                    {
                        armor.NeedsBonus = true;
                    }

                    armors.Add(armor);
                }

                _armorPieces = armors;
                MessageBox.Show($@"Imported '{_armorPieces.Count}' records.");
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
                var selectedLegs = SelectedLegs;
                if (string.IsNullOrWhiteSpace(selectedLegs.Id)) return;

                var basePiece = _armorPieces.FirstOrDefault(p => p.Id == selectedLegs.Id);
                if (basePiece == null)
                {
                    MessageBox.Show($@"Failed to locate base piece with Id '{selectedLegs.Id}'.");
                    return;
                }

                _armorDetailsForm.PopulateForm(SelectedLegsResource, basePiece, selectedLegs);
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
                Enabled = false;
                var armorPieces = new List<ArmorViewModel>();
                foreach (var armorPiece in _armorPieces)
                {
                    var clonedPiece = armorPiece.Clone();
                    armorPieces.Add(clonedPiece);
                }
                // Sort pieces into usable groups
                var headPieces = new List<ArmorViewModel>();
                var chestPieces = new List<ArmorViewModel>();
                var armPieces = new List<ArmorViewModel>();
                var glovePieces = new List<ArmorViewModel>();
                var legPieces = new List<ArmorViewModel>();
                SortPieces(armorPieces, ref headPieces, ref chestPieces, ref armPieces, ref glovePieces, ref legPieces);

                var suit = new Suit
                {
                    Helm = SelectedHelm,
                    Chest = SelectedChest,
                    Arms = SelectedArms,
                    Gloves = SelectedGloves,
                    Legs = SelectedLegs,
                    Misc = SelectedMisc,
                    CurrentResists = new Resists(),
                    MaxResists = new Resists
                    {
                        Physical = GoalPhysical,
                        Fire = GoalFire,
                        Cold = GoalCold,
                        Poison = GoalPoison,
                        Energy = GoalEnergy,
                    },
                };
                suit.UpdateCurrentResists();
                OptimizeSuit(headPieces, chestPieces, armPieces, glovePieces, legPieces, suit);
                PushSuitToUi(suit);

                var newSuitStringBuilder = new StringBuilder();
                newSuitStringBuilder.AppendLine($"Physical Resist: {suit.CurrentResists.Physical} / {suit.MaxResists.Physical}");
                newSuitStringBuilder.AppendLine($"Fire Resist: {suit.CurrentResists.Fire} / {suit.MaxResists.Fire}");
                newSuitStringBuilder.AppendLine($"Cold Resist: {suit.CurrentResists.Cold} / {suit.MaxResists.Cold}");
                newSuitStringBuilder.AppendLine($"Poison Resist: {suit.CurrentResists.Poison} / {suit.MaxResists.Poison}");
                newSuitStringBuilder.AppendLine($"Energy Resist: {suit.CurrentResists.Energy} / {suit.MaxResists.Energy}");
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
                Enabled = true;
            }
        }

        private void dgv_SelectedArmor_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        #endregion Event Handlers

        public ArmorViewModel CheckForBetterFit(ArmorViewModel currentArmorViewModel, IEnumerable<ArmorViewModel> otherPieces, Suit currentSuit)
        {
            var suitEvaluatorService = new SuitEvaluatorService(currentSuit);
            var candidates = new List<ArmorCandidate>();
            foreach (var candidate in otherPieces)
            {
                var armorCandidate = ConvertToCandidate(currentArmorViewModel, currentSuit, candidate);
                candidates.Add(armorCandidate);
            }

            if (!candidates.Any()) return null;

            var totalLostResistsGroup = candidates.GroupBy(c => c.PhysicalCandidate.Value + c.FireCandidate.Value + c.ColdCandidate.Value + c.PoisonCandidate.Value + c.EnergyCandidate.Value);
            var lowestWastedResists = totalLostResistsGroup.OrderBy(c => c.Key).First();
            foreach (var candidate in lowestWastedResists)
            {
                if (candidate.ArmorViewModel == currentArmorViewModel) continue;

                var armorChanged = false;
                if (suitEvaluatorService.PhysicalLowest)
                {
                    armorChanged = candidate.ArmorViewModel.CurrentResists.Physical > currentArmorViewModel.CurrentResists.Physical;
                }
                else if (suitEvaluatorService.FireLowest)
                {
                    armorChanged = candidate.ArmorViewModel.CurrentResists.Fire > currentArmorViewModel.CurrentResists.Fire;
                }
                else if (suitEvaluatorService.ColdLowest)
                {
                    armorChanged = candidate.ArmorViewModel.CurrentResists.Cold > currentArmorViewModel.CurrentResists.Cold;
                }
                else if (suitEvaluatorService.PoisonLowest)
                {
                    armorChanged = candidate.ArmorViewModel.CurrentResists.Poison > currentArmorViewModel.CurrentResists.Poison;
                }
                else if (suitEvaluatorService.EnergyLowest)
                {
                    armorChanged = candidate.ArmorViewModel.CurrentResists.Energy > currentArmorViewModel.CurrentResists.Energy;
                }

                if (armorChanged) return candidate.ArmorViewModel;
            }

            return null;
        }

        private void AddImbuable(ArmorViewModel armorViewModelPiece, ref List<ArmorViewModel> armorPieces)
        {
            if (_maxImbues < 1) return;

            var armorEvaluatorService = new ArmorEvaluatorService(armorViewModelPiece, _maxResistBonus);

            var physicalImbue = armorEvaluatorService.ImbuePhysical();
            armorPieces.Add(physicalImbue);
            for (var i = 2; i <= _maxImbues; i++)
            {
                if (physicalImbue == null) break;

                var secondaryImbue = PerformFirstLowestImbue(physicalImbue, _maxResistBonus);
                if (secondaryImbue != null)
                {
                    armorPieces.Add(secondaryImbue);
                }
                physicalImbue = secondaryImbue;
            }

            var fireImbue = armorEvaluatorService.ImbueFire();
            armorPieces.Add(fireImbue);
            for (var i = 2; i <= _maxImbues; i++)
            {
                if (fireImbue == null) break;

                var secondaryImbue = PerformFirstLowestImbue(fireImbue, _maxResistBonus);
                if (secondaryImbue != null)
                {
                    armorPieces.Add(secondaryImbue);
                }
                fireImbue = secondaryImbue;
            }

            var energyImbue = armorEvaluatorService.ImbueEnergy();
            armorPieces.Add(energyImbue);
            for (var i = 2; i <= _maxImbues; i++)
            {
                if (energyImbue == null) break;

                var secondaryImbue = PerformFirstLowestImbue(energyImbue, _maxResistBonus);
                if (secondaryImbue != null)
                {
                    armorPieces.Add(secondaryImbue);
                }
                energyImbue = secondaryImbue;
            }

            var coldImbue = armorEvaluatorService.ImbueCold();
            armorPieces.Add(coldImbue);
            for (var i = 2; i <= _maxImbues; i++)
            {
                if (coldImbue == null) break;

                var secondaryImbue = PerformFirstLowestImbue(coldImbue, _maxResistBonus);
                if (secondaryImbue != null)
                {
                    armorPieces.Add(secondaryImbue);
                }
                coldImbue = secondaryImbue;
            }

            var poisonImbue = armorEvaluatorService.ImbuePoison();
            armorPieces.Add(poisonImbue);
            for (var i = 2; i <= _maxImbues; i++)
            {
                if (poisonImbue == null) break;

                var secondaryImbue = PerformFirstLowestImbue(poisonImbue, _maxResistBonus);
                if (secondaryImbue != null)
                {
                    armorPieces.Add(secondaryImbue);
                }
                poisonImbue = secondaryImbue;
            }
        }

        private Resists ConvertConfigurationToResist(ResistConfiguration resistConfiguration)
        {
            return new Resists
            {
                Physical = resistConfiguration.Physical,
                Fire = resistConfiguration.Fire,
                Cold = resistConfiguration.Cold,
                Poison = resistConfiguration.Poison,
                Energy = resistConfiguration.Energy,
            };
        }

        private ArmorCandidate ConvertToCandidate(ArmorViewModel currentArmorViewModel, Suit currentSuit, ArmorViewModel candidate)
        {
            var basePhysical = currentSuit.CurrentResists.Physical - currentArmorViewModel.CurrentResists.Physical;
            var newPhysical = basePhysical + candidate.CurrentResists.Physical - currentSuit.MaxResists.Physical;

            var baseFire = currentSuit.CurrentResists.Fire - currentArmorViewModel.CurrentResists.Fire;
            var newFire = baseFire + candidate.CurrentResists.Fire - currentSuit.MaxResists.Fire;

            var baseCold = currentSuit.CurrentResists.Cold - currentArmorViewModel.CurrentResists.Cold;
            var newCold = baseCold + candidate.CurrentResists.Cold - currentSuit.MaxResists.Cold;

            var basePoison = currentSuit.CurrentResists.Poison - currentArmorViewModel.CurrentResists.Poison;
            var newPoison = basePoison + candidate.CurrentResists.Poison - currentSuit.MaxResists.Poison;

            var baseEnergy = currentSuit.CurrentResists.Energy - currentArmorViewModel.CurrentResists.Energy;
            var newEnergy = baseEnergy + candidate.CurrentResists.Energy - currentSuit.MaxResists.Energy;

            return new ArmorCandidate
            {
                ArmorViewModel = candidate,
                PhysicalCandidate = FormCandidate(newPhysical),
                FireCandidate = FormCandidate(newFire),
                ColdCandidate = FormCandidate(newCold),
                PoisonCandidate = FormCandidate(newPoison),
                EnergyCandidate = FormCandidate(newEnergy),
            };
        }

        private string ExportArmor(string name, ArmorViewModel armorViewModel)
        {
            return $"{name}" +
                   $"\t{armorViewModel.CurrentResists.Physical}\t{armorViewModel.CurrentResists.Fire}\t{armorViewModel.CurrentResists.Cold}" +
                   $"\t{armorViewModel.CurrentResists.Poison}\t{armorViewModel.CurrentResists.Energy}" +
                   $"\t'{armorViewModel.ImbueCount}' Imbues, Lost: {armorViewModel.LostResistPoints}";
        }

        private Candidate FormCandidate(long newResist)
        {
            return new Candidate
            {
                Value = Math.Abs(newResist),
                OverMax = newResist > 0,
                UnderMax = newResist < 0,
            };
        }

        private Resists GetBonus(Resource resourceViewModel)
        {
            return new Resists
            {
                Physical = resourceViewModel.Resist.Physical,
                Fire = resourceViewModel.Resist.Fire,
                Cold = resourceViewModel.Resist.Cold,
                Poison = resourceViewModel.Resist.Poison,
                Energy = resourceViewModel.Resist.Energy,
            };
        }

        private void OptimizeSuit(List<ArmorViewModel> headPieces, List<ArmorViewModel> chestPieces, List<ArmorViewModel> armPieces, List<ArmorViewModel> glovePieces, List<ArmorViewModel> legPieces, Suit suit)
        {
            var foundBetterFit = true;
            var iterations = 1;
            while (foundBetterFit && iterations < _maxIterations)
            {
                Debug.WriteLine($"Starting iteration '{iterations}'.");
                var betterHelm = CheckForBetterFit(suit.Helm, headPieces, suit);
                if (betterHelm != null)
                {
                    Console.WriteLine(@"Changed Helm.");
                    UpdateSlot(betterHelm, suit);
                }

                var betterChest = CheckForBetterFit(suit.Chest, chestPieces, suit);
                if (betterChest != null)
                {
                    Console.WriteLine(@"Changed Chest.");
                    UpdateSlot(betterChest, suit);
                }

                var betterArms = CheckForBetterFit(suit.Arms, armPieces, suit);
                if (betterArms != null)
                {
                    Console.WriteLine(@"Changed Arms.");
                    UpdateSlot(betterArms, suit);
                }

                var betterGloves = CheckForBetterFit(suit.Gloves, glovePieces, suit);
                if (betterGloves != null)
                {
                    Console.WriteLine(@"Changed Gloves.");
                    UpdateSlot(betterGloves, suit);
                }

                var betterLegs = CheckForBetterFit(suit.Legs, legPieces, suit);
                if (betterLegs != null)
                {
                    Console.WriteLine(@"Changed Legs.");
                    UpdateSlot(betterLegs, suit);
                }

                foundBetterFit = betterHelm != null || betterChest != null || betterArms != null || betterGloves != null || betterLegs != null;
                iterations++;
            }
        }

        private ArmorViewModel PerformFirstLowestImbue(ArmorViewModel basePiece, int maxResistBonus)
        {
            var armorEvaluatorService = new ArmorEvaluatorService(basePiece, maxResistBonus);
            if (armorEvaluatorService.PhysicalLowest) return armorEvaluatorService.ImbuePhysical();
            if (armorEvaluatorService.FireLowest) return armorEvaluatorService.ImbueFire();
            if (armorEvaluatorService.EnergyLowest) return armorEvaluatorService.ImbueEnergy();
            if (armorEvaluatorService.ColdLowest) return armorEvaluatorService.ImbueCold();
            if (armorEvaluatorService.PoisonLowest) return armorEvaluatorService.ImbuePoison();

            return null;
        }

        private void SortPieces(IEnumerable<ArmorViewModel> armorPieces, ref List<ArmorViewModel> headPieces, ref List<ArmorViewModel> chestPieces, ref List<ArmorViewModel> armPieces, ref List<ArmorViewModel> glovePieces, ref List<ArmorViewModel> legPieces)
        {
            var selectedHelm = SelectedHelm;
            var helmBonus = GetBonus(SelectedHelmResource);
            var selectedChest = SelectedChest;
            var chestBonus = GetBonus(SelectedChestResource);
            var selectedArms = SelectedArms;
            var armsBonus = GetBonus(SelectedArmsResource);
            var selectedGloves = SelectedGloves;
            var glovesBonus = GetBonus(SelectedGlovesResource);
            var selectedLegs = SelectedLegs;
            var legsBonus = GetBonus(SelectedLegsResource);
            foreach (var armorPiece in armorPieces.ToEnumeratedList())
            {
                var currentResists = armorPiece.CurrentResists;
                var baseResists = armorPiece.BaseResists;
                if (currentResists.Physical == 0 && currentResists.Fire == 0
                    && currentResists.Cold == 0 && currentResists.Poison == 0
                    && currentResists.Energy == 0) continue;

                switch (armorPiece.Slot)
                {
                    case SlotTypes.Helm:
                        if (selectedHelm.Locked) break;

                        if (selectedHelm.NeedsBonus)
                        {
                            baseResists.Add(helmBonus);
                            currentResists.Add(helmBonus);
                            selectedHelm.NeedsBonus = false;
                        }

                        headPieces.Add(armorPiece);
                        if (selectedHelm.ImbueCount < _maxImbues)
                        {
                            AddImbuable(armorPiece, ref headPieces);
                        }
                        break;

                    case SlotTypes.Chest:
                        if (selectedChest.Locked) break;

                        if (selectedChest.NeedsBonus)
                        {
                            baseResists.Add(chestBonus);
                            currentResists.Add(chestBonus);
                            selectedChest.NeedsBonus = false;
                        }

                        chestPieces.Add(armorPiece);

                        if (selectedChest.ImbueCount < _maxImbues)
                        {
                            AddImbuable(armorPiece, ref chestPieces);
                        }
                        break;

                    case SlotTypes.Arms:
                        if (selectedArms.Locked) break;

                        if (selectedArms.NeedsBonus)
                        {
                            baseResists.Add(armsBonus);
                            currentResists.Add(armsBonus);
                            selectedArms.NeedsBonus = false;
                        }

                        armPieces.Add(armorPiece);

                        if (selectedArms.ImbueCount < _maxImbues)
                        {
                            AddImbuable(armorPiece, ref armPieces);
                        }
                        break;

                    case SlotTypes.Gloves:
                        if (selectedGloves.Locked) break;

                        if (selectedGloves.NeedsBonus)
                        {
                            baseResists.Add(glovesBonus);
                            currentResists.Add(glovesBonus);
                            selectedGloves.NeedsBonus = false;
                        }

                        glovePieces.Add(armorPiece);

                        if (selectedGloves.ImbueCount < _maxImbues)
                        {
                            AddImbuable(armorPiece, ref glovePieces);
                        }
                        break;

                    case SlotTypes.Legs:
                        if (selectedLegs.Locked) break;

                        if (selectedLegs.NeedsBonus)
                        {
                            baseResists.Add(legsBonus);
                            currentResists.Add(legsBonus);
                            selectedLegs.NeedsBonus = false;
                        }

                        legPieces.Add(armorPiece);

                        if (selectedLegs.ImbueCount < _maxImbues)
                        {
                            AddImbuable(armorPiece, ref legPieces);
                        }
                        break;

                    default:
                        throw new ArgumentOutOfRangeException($"Armor '{armorPiece.Id}' does not fill a known slot.");
                }
            }
        }

        private void UpdateSlot(ArmorViewModel newPiece, Suit suit)
        {
            switch (newPiece.Slot)
            {
                case SlotTypes.Helm:
                    suit.CurrentResists.Physical = suit.CurrentResists.Physical - suit.Helm.CurrentResists.Physical + newPiece.CurrentResists.Physical;
                    suit.CurrentResists.Fire = suit.CurrentResists.Fire - suit.Helm.CurrentResists.Fire + newPiece.CurrentResists.Fire;
                    suit.CurrentResists.Cold = suit.CurrentResists.Cold - suit.Helm.CurrentResists.Cold + newPiece.CurrentResists.Cold;
                    suit.CurrentResists.Poison = suit.CurrentResists.Poison - suit.Helm.CurrentResists.Poison + newPiece.CurrentResists.Poison;
                    suit.CurrentResists.Energy = suit.CurrentResists.Energy - suit.Helm.CurrentResists.Energy + newPiece.CurrentResists.Energy;
                    suit.Helm = newPiece;
                    break;

                case SlotTypes.Chest:
                    suit.CurrentResists.Physical = suit.CurrentResists.Physical - suit.Chest.CurrentResists.Physical + newPiece.CurrentResists.Physical;
                    suit.CurrentResists.Fire = suit.CurrentResists.Fire - suit.Chest.CurrentResists.Fire + newPiece.CurrentResists.Fire;
                    suit.CurrentResists.Cold = suit.CurrentResists.Cold - suit.Chest.CurrentResists.Cold + newPiece.CurrentResists.Cold;
                    suit.CurrentResists.Poison = suit.CurrentResists.Poison - suit.Chest.CurrentResists.Poison + newPiece.CurrentResists.Poison;
                    suit.CurrentResists.Energy = suit.CurrentResists.Energy - suit.Chest.CurrentResists.Energy + newPiece.CurrentResists.Energy;
                    suit.Chest = newPiece;
                    break;

                case SlotTypes.Arms:
                    suit.CurrentResists.Physical = suit.CurrentResists.Physical - suit.Arms.CurrentResists.Physical + newPiece.CurrentResists.Physical;
                    suit.CurrentResists.Fire = suit.CurrentResists.Fire - suit.Arms.CurrentResists.Fire + newPiece.CurrentResists.Fire;
                    suit.CurrentResists.Cold = suit.CurrentResists.Cold - suit.Arms.CurrentResists.Cold + newPiece.CurrentResists.Cold;
                    suit.CurrentResists.Poison = suit.CurrentResists.Poison - suit.Arms.CurrentResists.Poison + newPiece.CurrentResists.Poison;
                    suit.CurrentResists.Energy = suit.CurrentResists.Energy - suit.Arms.CurrentResists.Energy + newPiece.CurrentResists.Energy;
                    suit.Arms = newPiece;
                    break;

                case SlotTypes.Gloves:
                    suit.CurrentResists.Physical = suit.CurrentResists.Physical - suit.Gloves.CurrentResists.Physical + newPiece.CurrentResists.Physical;
                    suit.CurrentResists.Fire = suit.CurrentResists.Fire - suit.Gloves.CurrentResists.Fire + newPiece.CurrentResists.Fire;
                    suit.CurrentResists.Cold = suit.CurrentResists.Cold - suit.Gloves.CurrentResists.Cold + newPiece.CurrentResists.Cold;
                    suit.CurrentResists.Poison = suit.CurrentResists.Poison - suit.Gloves.CurrentResists.Poison + newPiece.CurrentResists.Poison;
                    suit.CurrentResists.Energy = suit.CurrentResists.Energy - suit.Gloves.CurrentResists.Energy + newPiece.CurrentResists.Energy;
                    suit.Gloves = newPiece;
                    break;

                case SlotTypes.Legs:
                    suit.CurrentResists.Physical = suit.CurrentResists.Physical - suit.Legs.CurrentResists.Physical + newPiece.CurrentResists.Physical;
                    suit.CurrentResists.Fire = suit.CurrentResists.Fire - suit.Legs.CurrentResists.Fire + newPiece.CurrentResists.Fire;
                    suit.CurrentResists.Cold = suit.CurrentResists.Cold - suit.Legs.CurrentResists.Cold + newPiece.CurrentResists.Cold;
                    suit.CurrentResists.Poison = suit.CurrentResists.Poison - suit.Legs.CurrentResists.Poison + newPiece.CurrentResists.Poison;
                    suit.CurrentResists.Energy = suit.CurrentResists.Energy - suit.Legs.CurrentResists.Energy + newPiece.CurrentResists.Energy;
                    suit.Legs = newPiece;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}