namespace ArmorOptimizer.Models
{
    public class Suit
    {
        public ArmorViewModel Arms { get; set; }
        public ArmorViewModel Chest { get; set; }
        public Resists CurrentResists { get; set; }
        public ArmorViewModel Gloves { get; set; }
        public ArmorViewModel Helm { get; set; }
        public ArmorViewModel Legs { get; set; }
        public Resists MaxResists { get; set; }
        public ArmorViewModel Misc { get; set; }
    }
}