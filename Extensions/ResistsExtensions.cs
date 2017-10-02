using ArmorOptimizer.Models;

namespace ArmorOptimizer.Extensions
{
    public static class ResistsExtensions
    {
        public static void Add(this Resists baseResists, params Resists[] bonuses)
        {
            foreach (var bonus in bonuses)
            {
                baseResists.Physical += bonus.Physical;
                baseResists.Fire += bonus.Fire;
                baseResists.Cold += bonus.Cold;
                baseResists.Poison += bonus.Poison;
                baseResists.Energy += bonus.Energy;
            }
        }

        public static Resists Clone(this Resists resists)
        {
            return new Resists
            {
                Physical = resists.Physical,
                Fire = resists.Fire,
                Cold = resists.Cold,
                Poison = resists.Poison,
                Energy = resists.Energy,
            };
        }
    }
}