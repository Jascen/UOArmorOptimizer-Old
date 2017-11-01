using ArmorOptimizer.Models;
using System;

namespace ArmorOptimizer.Extensions
{
    public static class ArmorExtensions
    {
        public static ArmorViewModel Clone(this ArmorViewModel armorViewModelPiece)
        {
            return new ArmorViewModel
            {
                Id = armorViewModelPiece.Id,
                Slot = armorViewModelPiece.Slot,
                ImbueCount = armorViewModelPiece.ImbueCount,
                LostResistPoints = armorViewModelPiece.LostResistPoints,
                CurrentResists = armorViewModelPiece.CurrentResists.Clone(),
                BaseResists = armorViewModelPiece.BaseResists.Clone(),
                NeedsBonus = armorViewModelPiece.NeedsBonus,
                Locked = armorViewModelPiece.Locked,
            };
        }

        public static void EvaluateImbuedResists(this ArmorViewModel armorViewModel, long maxResistImbuePoints)
        {
            armorViewModel.ImbueCount = 0;
            armorViewModel.LostResistPoints = 0;

            var physicalMax = armorViewModel.BaseResists.Physical + maxResistImbuePoints;
            if (armorViewModel.CurrentResists.Physical >= physicalMax)
            {
                var idealMax = armorViewModel.CurrentResists.Physical + maxResistImbuePoints;
                if (idealMax < physicalMax) throw new ArgumentException($"{armorViewModel.Slot} '{armorViewModel.Id}' does not have correct base material selected.");

                armorViewModel.CurrentResists.Physical = Math.Min(idealMax, physicalMax);
                armorViewModel.LostResistPoints += idealMax - physicalMax;
                armorViewModel.ImbueCount++;
            }

            var fireMax = armorViewModel.BaseResists.Fire + maxResistImbuePoints;
            if (armorViewModel.CurrentResists.Fire >= fireMax)
            {
                var idealMax = armorViewModel.CurrentResists.Fire + maxResistImbuePoints;
                if (idealMax < fireMax) throw new ArgumentException($"{armorViewModel.Slot} '{armorViewModel.Id}' does not have correct base material selected.");

                armorViewModel.CurrentResists.Fire = Math.Min(idealMax, fireMax);
                armorViewModel.LostResistPoints += idealMax - fireMax;
                armorViewModel.ImbueCount++;
            }

            var coldMax = armorViewModel.BaseResists.Cold + maxResistImbuePoints;
            if (armorViewModel.CurrentResists.Cold >= coldMax)
            {
                var idealMax = armorViewModel.CurrentResists.Cold + maxResistImbuePoints;
                if (idealMax < coldMax) throw new ArgumentException($"{armorViewModel.Slot} '{armorViewModel.Id}' does not have correct base material selected.");

                armorViewModel.CurrentResists.Cold = Math.Min(idealMax, coldMax);
                armorViewModel.LostResistPoints += idealMax - coldMax;
                armorViewModel.ImbueCount++;
            }

            var poisonMax = armorViewModel.BaseResists.Poison + maxResistImbuePoints;
            if (armorViewModel.CurrentResists.Poison >= poisonMax)
            {
                var idealMax = armorViewModel.CurrentResists.Poison + maxResistImbuePoints;
                if (idealMax < poisonMax) throw new ArgumentException($"{armorViewModel.Slot} '{armorViewModel.Id}' does not have correct base material selected.");

                armorViewModel.CurrentResists.Poison = Math.Min(idealMax, poisonMax);
                armorViewModel.LostResistPoints += idealMax - poisonMax;
                armorViewModel.ImbueCount++;
            }

            var energyMax = armorViewModel.BaseResists.Energy + maxResistImbuePoints;
            if (armorViewModel.CurrentResists.Energy >= energyMax)
            {
                var idealMax = armorViewModel.CurrentResists.Energy + maxResistImbuePoints;
                if (idealMax < energyMax) throw new ArgumentException($"{armorViewModel.Slot} '{armorViewModel.Id}' does not have correct base material selected.");

                armorViewModel.CurrentResists.Energy = Math.Min(idealMax, energyMax);
                armorViewModel.LostResistPoints += idealMax - energyMax;
                armorViewModel.ImbueCount++;
            }
        }

        public static long TotalResists(this ArmorViewModel armorViewModel)
        {
            return armorViewModel.CurrentResists.Physical
                + armorViewModel.CurrentResists.Fire
                + armorViewModel.CurrentResists.Cold
                + armorViewModel.CurrentResists.Poison
                + armorViewModel.CurrentResists.Energy;
        }
    }
}