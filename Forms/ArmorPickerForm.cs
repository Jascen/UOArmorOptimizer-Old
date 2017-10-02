using ArmorOptimizer.Attributes;
using ArmorOptimizer.Enums;
using ArmorOptimizer.Extensions;
using ArmorOptimizer.Models;
using ArmorOptimizer.Services;
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
        private List<Armor> _armorPieces;

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

        private Dictionary<string, IList<ArmorRecord>> _knownItemTypes;
        private int _maxImbues = 2;
        private int _maxIterations = 1000;
        private int _maxResistBonus = 13;
        private Dictionary<int, ResistConfiguration> _resistConfigurations;
        private IList<ResourceRecord> _resourceRecords;
        private IList<Resource> _resources;
        private IList<Armor> _selectedArmor;
        private string FileToImport => tb_FileToImport.Text;
        private int GoalCold => (int)num_GoalCold.Value;
        private int GoalEnergy => (int)num_GoalEnergy.Value;
        private int GoalFire => (int)num_GoalFire.Value;
        private int GoalPhysical => (int)num_GoalPhysical.Value;
        private int GoalPoison => (int)num_GoalPoison.Value;
        private Armor SelectedArms => _selectedArmor.FirstOrDefault(r => r.Slot == SlotTypes.Arms);
        private Resource SelectedArmsResource => (Resource)cbb_ArmsResourceType.SelectedItem;
        private Armor SelectedChest => _selectedArmor.FirstOrDefault(r => r.Slot == SlotTypes.Chest);
        private Resource SelectedChestResource => (Resource)cbb_ChestResourceType.SelectedItem;
        private Armor SelectedGloves => _selectedArmor.FirstOrDefault(r => r.Slot == SlotTypes.Gloves);
        private Resource SelectedGlovesResource => (Resource)cbb_GlovesResourceType.SelectedItem;
        private Armor SelectedHelm => _selectedArmor.FirstOrDefault(r => r.Slot == SlotTypes.Helm);
        private Resource SelectedHelmResource => (Resource)cbb_HelmResourceType.SelectedItem;
        private Armor SelectedLegs => _selectedArmor.FirstOrDefault(r => r.Slot == SlotTypes.Legs);
        private Resource SelectedLegsResource => (Resource)cbb_LegsResourceType.SelectedItem;
        private Armor SelectedMisc => _selectedArmor.FirstOrDefault(r => r.Slot == SlotTypes.Unknown);

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
                // Set default values and bind to DGV
                BindSuitToUi();

                // Import Resist Configurations
                var resistConfigurationRecords = _importerService.ImportJsonFile<IEnumerable<ResistConfiguration>>(ResistConfigurationFile).ToEnumeratedList();
                if (resistConfigurationRecords.IsNullOrEmpty())
                {
                    MessageBox.Show(@"Failed to import Resist Configurations.  Closing!");
                    Close();
                }

                // Re-map it to be more easily accessible
                _resistConfigurations = resistConfigurationRecords.ToDictionary(record => record.Id);

                // Import Item Types
                _knownItemTypes = new Dictionary<string, IList<ArmorRecord>>();
                LoadArmorMappings(ref _knownItemTypes);

                // Import Resources
                _resourceRecords = _importerService.ImportJsonFile<IEnumerable<ResourceRecord>>(ResourceKindFile).ToEnumeratedList();
                if (_resourceRecords.IsNullOrEmpty())
                {
                    MessageBox.Show(@"Failed to import Resource Types.  Closing!");
                    Close();
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
                BonusResistConfigurationId = r.BonusResistConfigurationId,
                BonusResistConfiguration = _resistConfigurations[r.BonusResistConfigurationId],
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
            _selectedArmor = new List<Armor>
            {
                new Armor
                {
                    Slot = SlotTypes.Helm,
                    CurrentResists = new Resists(),
                },
                new Armor
                {
                    Slot = SlotTypes.Chest,
                    CurrentResists = new Resists(),
                },
                new Armor
                {
                    Slot = SlotTypes.Arms,
                    CurrentResists = new Resists(),
                },
                new Armor
                {
                    Slot = SlotTypes.Gloves,
                    CurrentResists = new Resists(),
                },
                new Armor
                {
                    Slot = SlotTypes.Legs,
                    CurrentResists = new Resists(),
                },
                new Armor
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

                var helmRecords = new List<ArmorRecord>();
                var chestRecords = new List<ArmorRecord>();
                var armRecords = new List<ArmorRecord>();
                var gloveRecords = new List<ArmorRecord>();
                var legRecords = new List<ArmorRecord>();
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

                var armors = new List<Armor>();
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
                    var armor = new Armor
                    {
                        CurrentResists = currentResists,
                        Slot = baseArmor.Slot,
                    };
                    armor.Id = dataArray[dict[nameof(armor.Id)]];

                    // Set it's minimums
                    armor.BaseResists = _resistConfigurations[baseArmor.BaseResistConfigurationId];

                    // Check if it's imbued already
                    armor.EvaluateImbuedResists(_maxResistBonus);

                    // Add the bonus to the base if it it's not the default color
                    if (itemColor != baseArmor.Color)
                    {
                        var resourceResists = _resistConfigurations[resource.BonusResistConfigurationId];
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
                var armorPieces = new List<Armor>();
                foreach (var armorPiece in _armorPieces)
                {
                    var clonedPiece = armorPiece.Clone();
                    armorPieces.Add(clonedPiece);
                }
                // Sort pieces into usable groups
                var headPieces = new List<Armor>();
                var chestPieces = new List<Armor>();
                var armPieces = new List<Armor>();
                var glovePieces = new List<Armor>();
                var legPieces = new List<Armor>();
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

        private void LoadArmorMappings(ref Dictionary<string, IList<ArmorRecord>> knownItemTypes)
        {
            var typeRecords = new List<ArmorRecord>();
            var helmRecords = _importerService.ImportJsonFile<IEnumerable<ArmorRecord>>(HelmFile).ToEnumeratedList();
            foreach (var armorRecord in helmRecords)
            {
                armorRecord.Slot = SlotTypes.Helm;
            }
            typeRecords.AddRange(helmRecords);

            var chestRecords = _importerService.ImportJsonFile<IEnumerable<ArmorRecord>>(ChestFile).ToEnumeratedList();
            foreach (var armorRecord in chestRecords)
            {
                armorRecord.Slot = SlotTypes.Chest;
            }
            typeRecords.AddRange(chestRecords);

            var armRecords = _importerService.ImportJsonFile<IEnumerable<ArmorRecord>>(ArmsFile).ToEnumeratedList();
            foreach (var armorRecord in armRecords)
            {
                armorRecord.Slot = SlotTypes.Arms;
            }
            typeRecords.AddRange(armRecords);

            var gloveRecords = _importerService.ImportJsonFile<IEnumerable<ArmorRecord>>(GlovesFile).ToEnumeratedList();
            foreach (var armorRecord in gloveRecords)
            {
                armorRecord.Slot = SlotTypes.Gloves;
            }
            typeRecords.AddRange(gloveRecords);

            var legRecords = _importerService.ImportJsonFile<IEnumerable<ArmorRecord>>(LegsFile).ToEnumeratedList();
            foreach (var armorRecord in legRecords)
            {
                armorRecord.Slot = SlotTypes.Legs;
            }
            typeRecords.AddRange(legRecords);

            foreach (var itemType in typeRecords.GroupBy(r => r.Type))
            {
                knownItemTypes.Add(itemType.Key, itemType.ToEnumeratedList());
            }
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
                    armorChanged = candidate.Armor.CurrentResists.Physical > currentArmor.CurrentResists.Physical;
                }
                else if (suitEvaluatorService.FireLowest)
                {
                    armorChanged = candidate.Armor.CurrentResists.Fire > currentArmor.CurrentResists.Fire;
                }
                else if (suitEvaluatorService.ColdLowest)
                {
                    armorChanged = candidate.Armor.CurrentResists.Cold > currentArmor.CurrentResists.Cold;
                }
                else if (suitEvaluatorService.PoisonLowest)
                {
                    armorChanged = candidate.Armor.CurrentResists.Poison > currentArmor.CurrentResists.Poison;
                }
                else if (suitEvaluatorService.EnergyLowest)
                {
                    armorChanged = candidate.Armor.CurrentResists.Energy > currentArmor.CurrentResists.Energy;
                }

                if (armorChanged) return candidate.Armor;
            }

            return null;
        }

        private void AddImbuable(Armor armorPiece, ref List<Armor> armorPieces)
        {
            if (_maxImbues < 1) return;

            var armorEvaluatorService = new ArmorEvaluatorService(armorPiece, _maxResistBonus);

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

        private ArmorCandidate ConvertToCandidate(Armor currentArmor, Suit currentSuit, Armor candidate)
        {
            var basePhysical = currentSuit.CurrentResists.Physical - currentArmor.CurrentResists.Physical;
            var newPhysical = basePhysical + candidate.CurrentResists.Physical - currentSuit.MaxResists.Physical;

            var baseFire = currentSuit.CurrentResists.Fire - currentArmor.CurrentResists.Fire;
            var newFire = baseFire + candidate.CurrentResists.Fire - currentSuit.MaxResists.Fire;

            var baseCold = currentSuit.CurrentResists.Cold - currentArmor.CurrentResists.Cold;
            var newCold = baseCold + candidate.CurrentResists.Cold - currentSuit.MaxResists.Cold;

            var basePoison = currentSuit.CurrentResists.Poison - currentArmor.CurrentResists.Poison;
            var newPoison = basePoison + candidate.CurrentResists.Poison - currentSuit.MaxResists.Poison;

            var baseEnergy = currentSuit.CurrentResists.Energy - currentArmor.CurrentResists.Energy;
            var newEnergy = baseEnergy + candidate.CurrentResists.Energy - currentSuit.MaxResists.Energy;

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
            return $"{name}" +
                   $"\t{armor.CurrentResists.Physical}\t{armor.CurrentResists.Fire}\t{armor.CurrentResists.Cold}" +
                   $"\t{armor.CurrentResists.Poison}\t{armor.CurrentResists.Energy}" +
                   $"\t'{armor.ImbueCount}' Imbues, Lost: {armor.LostResistPoints}";
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

        private Resists GetBonus(Resource resource)
        {
            return new Resists
            {
                Physical = resource.BonusResistConfiguration.Physical,
                Fire = resource.BonusResistConfiguration.Fire,
                Cold = resource.BonusResistConfiguration.Cold,
                Poison = resource.BonusResistConfiguration.Poison,
                Energy = resource.BonusResistConfiguration.Energy,
            };
        }

        private void OptimizeSuit(List<Armor> headPieces, List<Armor> chestPieces, List<Armor> armPieces, List<Armor> glovePieces, List<Armor> legPieces, Suit suit)
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

        private Armor PerformFirstLowestImbue(Armor basePiece, int maxResistBonus)
        {
            var armorEvaluatorService = new ArmorEvaluatorService(basePiece, maxResistBonus);
            if (armorEvaluatorService.PhysicalLowest) return armorEvaluatorService.ImbuePhysical();
            if (armorEvaluatorService.FireLowest) return armorEvaluatorService.ImbueFire();
            if (armorEvaluatorService.EnergyLowest) return armorEvaluatorService.ImbueEnergy();
            if (armorEvaluatorService.ColdLowest) return armorEvaluatorService.ImbueCold();
            if (armorEvaluatorService.PoisonLowest) return armorEvaluatorService.ImbuePoison();

            return null;
        }

        private void SortPieces(IEnumerable<Armor> armorPieces, ref List<Armor> headPieces, ref List<Armor> chestPieces, ref List<Armor> armPieces, ref List<Armor> glovePieces, ref List<Armor> legPieces)
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

        private void UpdateSlot(Armor newPiece, Suit suit)
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