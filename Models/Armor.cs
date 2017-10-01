using ArmorOptimizer.Enums;

namespace ArmorOptimizer.Models
{
    public class Armor
    {
        public Resists BaseResists { get; set; }
        public Resists CurrentResists { get; set; }
        public string Id { get; set; }
        public int ImbueCount { get; set; }
        public bool Locked { get; set; }
        public int LostResistPoints { get; set; }
        public bool NeedsBonus { get; set; }
        public SlotTypes Slot { get; set; }
    }
}