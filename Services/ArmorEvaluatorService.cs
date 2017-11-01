using ArmorOptimizer.Extensions;
using ArmorOptimizer.Models;
using System;

namespace ArmorOptimizer.Services
{
    public class ArmorEvaluatorService
    {
        private readonly ArmorViewModel _baseArmorViewModel;
        private readonly Resists _maxImbueables;
        private readonly int _maxResistBonus;

        public ArmorEvaluatorService(ArmorViewModel armorViewModel, int maxResistBonus)
        {
            if (armorViewModel == null) throw new ArgumentNullException(nameof(armorViewModel));

            PhysicalLowest = armorViewModel.CurrentResists.Physical <= armorViewModel.CurrentResists.Fire
                          && armorViewModel.CurrentResists.Physical <= armorViewModel.CurrentResists.Cold
                          && armorViewModel.CurrentResists.Physical <= armorViewModel.CurrentResists.Poison
                          && armorViewModel.CurrentResists.Physical <= armorViewModel.CurrentResists.Energy;
            FireLowest = armorViewModel.CurrentResists.Fire <= armorViewModel.CurrentResists.Physical
                         && armorViewModel.CurrentResists.Fire <= armorViewModel.CurrentResists.Cold
                         && armorViewModel.CurrentResists.Fire <= armorViewModel.CurrentResists.Poison
                         && armorViewModel.CurrentResists.Fire <= armorViewModel.CurrentResists.Energy;
            ColdLowest = armorViewModel.CurrentResists.Cold <= armorViewModel.CurrentResists.Fire
                         && armorViewModel.CurrentResists.Cold <= armorViewModel.CurrentResists.Physical
                         && armorViewModel.CurrentResists.Cold <= armorViewModel.CurrentResists.Poison
                         && armorViewModel.CurrentResists.Cold <= armorViewModel.CurrentResists.Energy;
            PoisonLowest = armorViewModel.CurrentResists.Poison <= armorViewModel.CurrentResists.Fire
                         && armorViewModel.CurrentResists.Poison <= armorViewModel.CurrentResists.Cold
                         && armorViewModel.CurrentResists.Poison <= armorViewModel.CurrentResists.Physical
                         && armorViewModel.CurrentResists.Poison <= armorViewModel.CurrentResists.Energy;
            EnergyLowest = armorViewModel.CurrentResists.Energy <= armorViewModel.CurrentResists.Fire
                         && armorViewModel.CurrentResists.Energy <= armorViewModel.CurrentResists.Cold
                         && armorViewModel.CurrentResists.Energy <= armorViewModel.CurrentResists.Poison
                         && armorViewModel.CurrentResists.Energy <= armorViewModel.CurrentResists.Physical;
            _baseArmorViewModel = armorViewModel;
            _maxResistBonus = maxResistBonus;
            _maxImbueables = new Resists
            {
                Physical = armorViewModel.BaseResists.Physical + maxResistBonus,
                Fire = armorViewModel.BaseResists.Fire + maxResistBonus,
                Cold = armorViewModel.BaseResists.Cold + maxResistBonus,
                Poison = armorViewModel.BaseResists.Poison + maxResistBonus,
                Energy = armorViewModel.BaseResists.Energy + maxResistBonus,
            };
        }

        public bool ColdLowest { get; }
        public bool EnergyLowest { get; }
        public bool FireLowest { get; }
        public bool PhysicalLowest { get; }
        public bool PoisonLowest { get; }

        public ArmorViewModel ImbueCold()
        {
            var armor = _baseArmorViewModel.Clone();
            var idealMax = armor.CurrentResists.Cold + _maxResistBonus;
            if (idealMax < _maxImbueables.Cold) throw new ArgumentException($"{armor.Slot} '{armor.Id}' does not have correct base material selected.");

            armor.CurrentResists.Cold = Math.Min(idealMax, _maxImbueables.Cold);
            armor.LostResistPoints = idealMax - _maxImbueables.Cold;
            armor.ImbueCount++;

            return armor;
        }

        public ArmorViewModel ImbueEnergy()
        {
            var armor = _baseArmorViewModel.Clone();
            var idealMax = armor.CurrentResists.Energy + _maxResistBonus;
            if (idealMax < _maxImbueables.Energy) throw new ArgumentException($"{armor.Slot} '{armor.Id}' does not have correct base material selected.");

            armor.CurrentResists.Energy = Math.Min(idealMax, _maxImbueables.Energy);
            armor.LostResistPoints = idealMax - _maxImbueables.Energy;
            armor.ImbueCount++;

            return armor;
        }

        public ArmorViewModel ImbueFire()
        {
            var armor = _baseArmorViewModel.Clone();
            var idealMax = armor.CurrentResists.Fire + _maxResistBonus;
            if (idealMax < _maxImbueables.Fire) throw new ArgumentException($"{armor.Slot} '{armor.Id}' does not have correct base material selected.");

            armor.CurrentResists.Fire = Math.Min(idealMax, _maxImbueables.Fire);
            armor.LostResistPoints = idealMax - _maxImbueables.Fire;
            armor.ImbueCount++;

            return armor;
        }

        public ArmorViewModel ImbuePhysical()
        {
            var armor = _baseArmorViewModel.Clone();
            var idealMax = armor.CurrentResists.Physical + _maxResistBonus;
            if (idealMax < _maxImbueables.Physical) throw new ArgumentException($"{armor.Slot} '{armor.Id}' does not have correct base material selected.");

            armor.CurrentResists.Physical = Math.Min(idealMax, _maxImbueables.Physical);
            armor.LostResistPoints = idealMax - _maxImbueables.Physical;
            armor.ImbueCount++;

            return armor;
        }

        public ArmorViewModel ImbuePoison()
        {
            var armor = _baseArmorViewModel.Clone();
            var idealMax = armor.CurrentResists.Poison + _maxResistBonus;
            if (idealMax < _maxImbueables.Poison) throw new ArgumentException($"{armor.Slot} '{armor.Id}' does not have correct base material selected.");

            armor.CurrentResists.Poison = Math.Min(idealMax, _maxImbueables.Poison);
            armor.LostResistPoints = idealMax - _maxImbueables.Poison;
            armor.ImbueCount++;

            return armor;
        }
    }
}