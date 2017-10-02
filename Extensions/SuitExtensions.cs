using ArmorOptimizer.Models;

namespace ArmorOptimizer.Extensions
{
    public static class SuitExtensions
    {
        public static void UpdateCurrentResists(this Suit suit)
        {
            suit.CurrentResists = new Resists
            {
                Physical = suit.Helm.CurrentResists.Physical + suit.Chest.CurrentResists.Physical + suit.Arms.CurrentResists.Physical + suit.Gloves.CurrentResists.Physical + suit.Legs.CurrentResists.Physical + suit.Misc.CurrentResists.Physical,
                Fire = suit.Helm.CurrentResists.Fire + suit.Chest.CurrentResists.Fire + suit.Arms.CurrentResists.Fire + suit.Gloves.CurrentResists.Fire + suit.Legs.CurrentResists.Fire + suit.Misc.CurrentResists.Fire,
                Cold = suit.Helm.CurrentResists.Cold + suit.Chest.CurrentResists.Cold + suit.Arms.CurrentResists.Cold + suit.Gloves.CurrentResists.Cold + suit.Legs.CurrentResists.Cold + suit.Misc.CurrentResists.Cold,
                Poison = suit.Helm.CurrentResists.Poison + suit.Chest.CurrentResists.Poison + suit.Arms.CurrentResists.Poison + suit.Gloves.CurrentResists.Poison + suit.Legs.CurrentResists.Poison + suit.Misc.CurrentResists.Poison,
                Energy = suit.Helm.CurrentResists.Energy + suit.Chest.CurrentResists.Energy + suit.Arms.CurrentResists.Energy + suit.Gloves.CurrentResists.Energy + suit.Legs.CurrentResists.Energy + suit.Misc.CurrentResists.Energy,
            };
        }
    }
}