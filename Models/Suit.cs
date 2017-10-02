namespace ArmorOptimizer.Models
{
    public class Suit
    {
        public Armor Arms { get; set; }
        public Armor Chest { get; set; }
        public Resists CurrentResists { get; set; }
        public Armor Gloves { get; set; }
        public Armor Helm { get; set; }
        public Armor Legs { get; set; }
        public Resists MaxResists { get; set; }
        public Armor Misc { get; set; }
    }
}