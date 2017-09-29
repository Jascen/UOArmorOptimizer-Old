using ArmorOptimizer.Models;

namespace ArmorOptimizer.Extensions
{
    public static class ArmorExtensions
    {
        public static void Add(this Armor baseArmor, params Armor[] bonuses)
        {
            foreach (var bonus in bonuses)
            {
                baseArmor.PhysicalResist += bonus.PhysicalResist;
                baseArmor.FireResist += bonus.FireResist;
                baseArmor.ColdResist += bonus.ColdResist;
                baseArmor.PoisonResist += bonus.PoisonResist;
                baseArmor.EnergyResist += bonus.EnergyResist;
            }
        }
        public static int TotalResists(this Armor baseArmor)
        {
            return baseArmor.PhysicalResist + baseArmor.FireResist + baseArmor.ColdResist + baseArmor.PoisonResist + baseArmor.EnergyResist;
        }
    }
}