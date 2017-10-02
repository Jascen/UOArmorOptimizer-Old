using ArmorOptimizer.Models;
using System;

namespace ArmorOptimizer.Services
{
    public class SuitEvaluatorService
    {
        public SuitEvaluatorService(Suit suit)
        {
            if (suit == null) throw new ArgumentNullException(nameof(suit));

            var currentResists = suit.CurrentResists;
            var maxSuitResists = suit.MaxResists;
            var physicalDeficit = maxSuitResists.Physical - currentResists.Physical;
            var fireDeficit = maxSuitResists.Fire - currentResists.Fire;
            var coldDeficit = maxSuitResists.Cold - currentResists.Cold;
            var poisonDeficit = maxSuitResists.Poison - currentResists.Poison;
            var energyDeficit = maxSuitResists.Energy - currentResists.Energy;

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