using ArmorOptimizer.Data.Models;
using ArmorOptimizer.Enums;

namespace ArmorOptimizer.Models
{
    public class ItemViewModel : Item
    {
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