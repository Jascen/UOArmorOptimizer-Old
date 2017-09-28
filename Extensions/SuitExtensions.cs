using ArmorPicker.Models;

namespace ArmorPicker.Extensions
{
    public static class SuitExtensions
    {
        public static void UpdateCurrentResists(this Suit suit)
        {
            var currentResists = new Armor
            {
                PhysicalResist = suit.Helm.PhysicalResist + suit.Chest.PhysicalResist + suit.Arms.PhysicalResist + suit.Gloves.PhysicalResist + suit.Legs.PhysicalResist + suit.Misc.PhysicalResist,
                FireResist = suit.Helm.FireResist + suit.Chest.FireResist + suit.Arms.FireResist + suit.Gloves.FireResist + suit.Legs.FireResist + suit.Misc.FireResist,
                ColdResist = suit.Helm.ColdResist + suit.Chest.ColdResist + suit.Arms.ColdResist + suit.Gloves.ColdResist + suit.Legs.ColdResist + suit.Misc.ColdResist,
                PoisonResist = suit.Helm.PoisonResist + suit.Chest.PoisonResist + suit.Arms.PoisonResist + suit.Gloves.PoisonResist + suit.Legs.PoisonResist + suit.Misc.PoisonResist,
                EnergyResist = suit.Helm.EnergyResist + suit.Chest.EnergyResist + suit.Arms.EnergyResist + suit.Gloves.EnergyResist + suit.Legs.EnergyResist + suit.Misc.EnergyResist,
            };
            suit.CurrentResists = currentResists;
        }
    }
}