using ArmorOptimizer.Extensions;
using ArmorOptimizer.Models;
using System;

namespace ArmorOptimizer.Services
{
    public class ArmorEvaluatorService
    {
        private readonly Armor _baseArmor;
        private readonly Resists _maxImbueables;
        private readonly int _maxResistBonus;

        public ArmorEvaluatorService(Armor armor, int maxResistBonus)
        {
            if (armor == null) throw new ArgumentNullException(nameof(armor));

            PhysicalLowest = armor.CurrentResists.Physical <= armor.CurrentResists.Fire
                          && armor.CurrentResists.Physical <= armor.CurrentResists.Cold
                          && armor.CurrentResists.Physical <= armor.CurrentResists.Poison
                          && armor.CurrentResists.Physical <= armor.CurrentResists.Energy;
            FireLowest = armor.CurrentResists.Fire <= armor.CurrentResists.Physical
                         && armor.CurrentResists.Fire <= armor.CurrentResists.Cold
                         && armor.CurrentResists.Fire <= armor.CurrentResists.Poison
                         && armor.CurrentResists.Fire <= armor.CurrentResists.Energy;
            ColdLowest = armor.CurrentResists.Cold <= armor.CurrentResists.Fire
                         && armor.CurrentResists.Cold <= armor.CurrentResists.Physical
                         && armor.CurrentResists.Cold <= armor.CurrentResists.Poison
                         && armor.CurrentResists.Cold <= armor.CurrentResists.Energy;
            PoisonLowest = armor.CurrentResists.Poison <= armor.CurrentResists.Fire
                         && armor.CurrentResists.Poison <= armor.CurrentResists.Cold
                         && armor.CurrentResists.Poison <= armor.CurrentResists.Physical
                         && armor.CurrentResists.Poison <= armor.CurrentResists.Energy;
            EnergyLowest = armor.CurrentResists.Energy <= armor.CurrentResists.Fire
                         && armor.CurrentResists.Energy <= armor.CurrentResists.Cold
                         && armor.CurrentResists.Energy <= armor.CurrentResists.Poison
                         && armor.CurrentResists.Energy <= armor.CurrentResists.Physical;
            _baseArmor = armor;
            _maxResistBonus = maxResistBonus;
            _maxImbueables = new Resists
            {
                Physical = armor.BaseResists.Physical + maxResistBonus,
                Fire = armor.BaseResists.Fire + maxResistBonus,
                Cold = armor.BaseResists.Cold + maxResistBonus,
                Poison = armor.BaseResists.Poison + maxResistBonus,
                Energy = armor.BaseResists.Energy + maxResistBonus,
            };
        }

        public bool ColdLowest { get; }
        public bool EnergyLowest { get; }
        public bool FireLowest { get; }
        public bool PhysicalLowest { get; }
        public bool PoisonLowest { get; }

        public Armor ImbueCold()
        {
            var armor = _baseArmor.Clone();
            var idealMax = armor.CurrentResists.Cold + _maxResistBonus;
            if (idealMax < _maxImbueables.Cold) throw new ArgumentException($"{armor.Slot} '{armor.Id}' does not have correct base material selected.");

            armor.CurrentResists.Cold = Math.Min(idealMax, _maxImbueables.Cold);
            armor.LostResistPoints = idealMax - _maxImbueables.Cold;
            armor.ImbueCount++;

            return armor;
        }

        public Armor ImbueEnergy()
        {
            var armor = _baseArmor.Clone();
            var idealMax = armor.CurrentResists.Energy + _maxResistBonus;
            if (idealMax < _maxImbueables.Energy) throw new ArgumentException($"{armor.Slot} '{armor.Id}' does not have correct base material selected.");

            armor.CurrentResists.Energy = Math.Min(idealMax, _maxImbueables.Energy);
            armor.LostResistPoints = idealMax - _maxImbueables.Energy;
            armor.ImbueCount++;

            return armor;
        }

        public Armor ImbueFire()
        {
            var armor = _baseArmor.Clone();
            var idealMax = armor.CurrentResists.Fire + _maxResistBonus;
            if (idealMax < _maxImbueables.Fire) throw new ArgumentException($"{armor.Slot} '{armor.Id}' does not have correct base material selected.");

            armor.CurrentResists.Fire = Math.Min(idealMax, _maxImbueables.Fire);
            armor.LostResistPoints = idealMax - _maxImbueables.Fire;
            armor.ImbueCount++;

            return armor;
        }

        public Armor ImbuePhysical()
        {
            var armor = _baseArmor.Clone();
            var idealMax = armor.CurrentResists.Physical + _maxResistBonus;
            if (idealMax < _maxImbueables.Physical) throw new ArgumentException($"{armor.Slot} '{armor.Id}' does not have correct base material selected.");

            armor.CurrentResists.Physical = Math.Min(idealMax, _maxImbueables.Physical);
            armor.LostResistPoints = idealMax - _maxImbueables.Physical;
            armor.ImbueCount++;

            return armor;
        }

        public Armor ImbuePoison()
        {
            var armor = _baseArmor.Clone();
            var idealMax = armor.CurrentResists.Poison + _maxResistBonus;
            if (idealMax < _maxImbueables.Poison) throw new ArgumentException($"{armor.Slot} '{armor.Id}' does not have correct base material selected.");

            armor.CurrentResists.Poison = Math.Min(idealMax, _maxImbueables.Poison);
            armor.LostResistPoints = idealMax - _maxImbueables.Poison;
            armor.ImbueCount++;

            return armor;
        }
    }
}