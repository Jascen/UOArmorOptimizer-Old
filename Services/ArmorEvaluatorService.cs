using System;
using ArmorOptimizer.Models;

namespace ArmorOptimizer.Services
{
    public class ArmorEvaluatorService
    {
        public const int MaxResistPoints = 13;
        private readonly Armor _baseArmor;
        private readonly Armor _maxImbueableResists;

        public ArmorEvaluatorService(Armor armor, Armor maxImbueableResists)
        {
            if (armor == null) throw new ArgumentNullException(nameof(armor));
            if (maxImbueableResists == null) throw new ArgumentNullException(nameof(maxImbueableResists));

            PhysicalLowest = armor.PhysicalResist <= armor.FireResist
                          && armor.PhysicalResist <= armor.ColdResist
                          && armor.PhysicalResist <= armor.PoisonResist
                          && armor.PhysicalResist <= armor.EnergyResist;
            FireLowest = armor.FireResist <= armor.PhysicalResist
                         && armor.FireResist <= armor.ColdResist
                         && armor.FireResist <= armor.PoisonResist
                         && armor.FireResist <= armor.EnergyResist;
            ColdLowest = armor.ColdResist <= armor.FireResist
                         && armor.ColdResist <= armor.PhysicalResist
                         && armor.ColdResist <= armor.PoisonResist
                         && armor.ColdResist <= armor.EnergyResist;
            PoisonLowest = armor.PoisonResist <= armor.FireResist
                         && armor.PoisonResist <= armor.ColdResist
                         && armor.PoisonResist <= armor.PhysicalResist
                         && armor.PoisonResist <= armor.EnergyResist;
            EnergyLowest = armor.EnergyResist <= armor.FireResist
                         && armor.EnergyResist <= armor.ColdResist
                         && armor.EnergyResist <= armor.PoisonResist
                         && armor.EnergyResist <= armor.PhysicalResist;
            _baseArmor = armor;
            _maxImbueableResists = maxImbueableResists;
        }

        public bool ColdLowest { get; }
        public bool EnergyLowest { get; }
        public bool FireLowest { get; }
        public bool PhysicalLowest { get; }
        public bool PoisonLowest { get; }

        public Armor ImbueCold()
        {
            var armor = Clone();
            var idealMax = armor.ColdResist + MaxResistPoints;
            if (idealMax < _maxImbueableResists.ColdResist) throw new ArgumentException($"{armor.Slot} '{armor.Id}' does not have correct bass material selected.");

            armor.ColdResist = Math.Min(idealMax, _maxImbueableResists.ColdResist);
            armor.LostResistPoints = idealMax - _maxImbueableResists.ColdResist;
            armor.ImbueCount++;

            return armor;
        }

        public Armor ImbueEnergy()
        {
            var armor = Clone();
            var idealMax = armor.EnergyResist + MaxResistPoints;
            if (idealMax < _maxImbueableResists.EnergyResist) throw new ArgumentException($"{armor.Slot} '{armor.Id}' does not have correct bass material selected.");

            armor.EnergyResist = Math.Min(idealMax, _maxImbueableResists.EnergyResist);
            armor.LostResistPoints = idealMax - _maxImbueableResists.EnergyResist;
            armor.ImbueCount++;

            return armor;
        }

        public Armor ImbueFire()
        {
            var armor = Clone();
            var idealMax = armor.FireResist + MaxResistPoints;
            if (idealMax < _maxImbueableResists.FireResist) throw new ArgumentException($"{armor.Slot} '{armor.Id}' does not have correct bass material selected.");

            armor.FireResist = Math.Min(idealMax, _maxImbueableResists.FireResist);
            armor.LostResistPoints = idealMax - _maxImbueableResists.FireResist;
            armor.ImbueCount++;

            return armor;
        }

        public Armor ImbuePhysical()
        {
            var armor = Clone();
            var idealMax = armor.PhysicalResist + MaxResistPoints;
            if (idealMax < _maxImbueableResists.PhysicalResist) throw new ArgumentException($"{armor.Slot} '{armor.Id}' does not have correct bass material selected.");

            armor.PhysicalResist = Math.Min(idealMax, _maxImbueableResists.PhysicalResist);
            armor.LostResistPoints = idealMax - _maxImbueableResists.PhysicalResist;
            armor.ImbueCount++;

            return armor;
        }

        public Armor ImbuePoison()
        {
            var armor = Clone();
            var idealMax = armor.PoisonResist + MaxResistPoints;
            if (idealMax < _maxImbueableResists.PoisonResist) throw new ArgumentException($"{armor.Slot} '{armor.Id}' does not have correct bass material selected.");

            armor.PoisonResist = Math.Min(idealMax, _maxImbueableResists.PoisonResist);
            armor.LostResistPoints = idealMax - _maxImbueableResists.PoisonResist;
            armor.ImbueCount++;

            return armor;
        }

        private Armor Clone()
        {
            return new Armor
            {
                Id = _baseArmor.Id,
                PhysicalResist = _baseArmor.PhysicalResist,
                FireResist = _baseArmor.FireResist,
                ColdResist = _baseArmor.ColdResist,
                PoisonResist = _baseArmor.PoisonResist,
                EnergyResist = _baseArmor.EnergyResist,
                Slot = _baseArmor.Slot,
                ImbueCount = _baseArmor.ImbueCount,
                LostResistPoints = _baseArmor.LostResistPoints,
            };
        }
    }
}