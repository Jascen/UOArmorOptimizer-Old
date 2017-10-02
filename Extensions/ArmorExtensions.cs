using ArmorOptimizer.Models;
using System;

namespace ArmorOptimizer.Extensions
{
    public static class ArmorExtensions
    {
        public static Armor Clone(this Armor armorPiece)
        {
            return new Armor
            {
                Id = armorPiece.Id,
                Slot = armorPiece.Slot,
                ImbueCount = armorPiece.ImbueCount,
                LostResistPoints = armorPiece.LostResistPoints,
                CurrentResists = armorPiece.CurrentResists.Clone(),
                BaseResists = armorPiece.BaseResists.Clone(),
                NeedsBonus = armorPiece.NeedsBonus,
                Locked = armorPiece.Locked,
            };
        }

        public static void EvaluateImbuedResists(this Armor armor, int maxResistImbuePoints)
        {
            armor.ImbueCount = 0;
            armor.LostResistPoints = 0;

            var physicalMax = armor.BaseResists.Physical + maxResistImbuePoints;
            if (armor.CurrentResists.Physical >= physicalMax)
            {
                var idealMax = armor.CurrentResists.Physical + maxResistImbuePoints;
                if (idealMax < physicalMax) throw new ArgumentException($"{armor.Slot} '{armor.Id}' does not have correct base material selected.");

                armor.CurrentResists.Physical = Math.Min(idealMax, physicalMax);
                armor.LostResistPoints += idealMax - physicalMax;
                armor.ImbueCount++;
            }

            var fireMax = armor.BaseResists.Fire + maxResistImbuePoints;
            if (armor.CurrentResists.Fire >= fireMax)
            {
                var idealMax = armor.CurrentResists.Fire + maxResistImbuePoints;
                if (idealMax < fireMax) throw new ArgumentException($"{armor.Slot} '{armor.Id}' does not have correct base material selected.");

                armor.CurrentResists.Fire = Math.Min(idealMax, fireMax);
                armor.LostResistPoints += idealMax - fireMax;
                armor.ImbueCount++;
            }

            var coldMax = armor.BaseResists.Cold + maxResistImbuePoints;
            if (armor.CurrentResists.Cold >= coldMax)
            {
                var idealMax = armor.CurrentResists.Cold + maxResistImbuePoints;
                if (idealMax < coldMax) throw new ArgumentException($"{armor.Slot} '{armor.Id}' does not have correct base material selected.");

                armor.CurrentResists.Cold = Math.Min(idealMax, coldMax);
                armor.LostResistPoints += idealMax - coldMax;
                armor.ImbueCount++;
            }

            var poisonMax = armor.BaseResists.Poison + maxResistImbuePoints;
            if (armor.CurrentResists.Poison >= poisonMax)
            {
                var idealMax = armor.CurrentResists.Poison + maxResistImbuePoints;
                if (idealMax < poisonMax) throw new ArgumentException($"{armor.Slot} '{armor.Id}' does not have correct base material selected.");

                armor.CurrentResists.Poison = Math.Min(idealMax, poisonMax);
                armor.LostResistPoints += idealMax - poisonMax;
                armor.ImbueCount++;
            }

            var energyMax = armor.BaseResists.Energy + maxResistImbuePoints;
            if (armor.CurrentResists.Energy >= energyMax)
            {
                var idealMax = armor.CurrentResists.Energy + maxResistImbuePoints;
                if (idealMax < energyMax) throw new ArgumentException($"{armor.Slot} '{armor.Id}' does not have correct base material selected.");

                armor.CurrentResists.Energy = Math.Min(idealMax, energyMax);
                armor.LostResistPoints += idealMax - energyMax;
                armor.ImbueCount++;
            }
        }

        public static int TotalResists(this Armor armor)
        {
            return armor.CurrentResists.Physical
                + armor.CurrentResists.Fire
                + armor.CurrentResists.Cold
                + armor.CurrentResists.Poison
                + armor.CurrentResists.Energy;
        }
    }
}