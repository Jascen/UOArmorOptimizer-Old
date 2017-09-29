using System;

namespace ArmorOptimizer.Models
{
    public class SuitEvaluatorService
    {
        public SuitEvaluatorService(Suit suit)
        {
            if (suit == null) throw new ArgumentNullException(nameof(suit));

            var currentResists = suit.CurrentResists;
            var maxSuitResists = suit.MaxResists;
            var physicalDeficit = maxSuitResists.PhysicalResist - currentResists.PhysicalResist;
            var fireDeficit = maxSuitResists.FireResist - currentResists.FireResist;
            var coldDeficit = maxSuitResists.ColdResist - currentResists.ColdResist;
            var poisonDeficit = maxSuitResists.PoisonResist - currentResists.PoisonResist;
            var energyDeficit = maxSuitResists.EnergyResist - currentResists.EnergyResist;

            PhysicalLowest = physicalDeficit >= fireDeficit && physicalDeficit >= coldDeficit && physicalDeficit >= poisonDeficit && physicalDeficit >= energyDeficit;
            FireLowest = fireDeficit >= physicalDeficit && fireDeficit >= coldDeficit && fireDeficit >= poisonDeficit && fireDeficit >= energyDeficit;
            ColdLowest = coldDeficit >= fireDeficit && coldDeficit >= physicalDeficit && coldDeficit >= poisonDeficit && coldDeficit >= energyDeficit;
            PoisonLowest = poisonDeficit >= fireDeficit && poisonDeficit >= coldDeficit && poisonDeficit >= physicalDeficit && poisonDeficit >= energyDeficit;
            EnergyLowest = energyDeficit >= fireDeficit && energyDeficit >= coldDeficit && energyDeficit >= poisonDeficit && energyDeficit >= physicalDeficit;
        }

        public bool ColdLowest { get; }
        public bool EnergyLowest { get; }
        public bool FireLowest { get; }
        public bool PhysicalLowest { get; }
        public bool PoisonLowest { get; }
    }
}