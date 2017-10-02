using ArmorOptimizer.Enums;

namespace ArmorOptimizer.Models
{
    public class ArmorRecord
    {
        /// <summary>
        /// The resists prior to Exceptional bonus
        /// </summary>
        public int BaseResistConfigurationId { get; set; }

        /// <summary>
        /// The item's Color
        /// </summary>
        public int Color { get; set; }

        /// <summary>
        /// The slot number.  This is populated by the App.
        /// </summary>
        public SlotTypes Slot { get; set; }

        /// <summary>
        /// The item type (three characters)
        /// </summary>
        public string Type { get; set; }
    }
}