using System;
using ArmorOptimizer.Attributes;
using ArmorOptimizer.Enums;

namespace ArmorOptimizer.Models
{
    public class Armor
    {
        [ColumnNumber(4)]
        public int ColdResist { get; set; }

        [ColumnNumber(6)]
        public int EnergyResist { get; set; }

        [ColumnNumber(3)]
        public int FireResist { get; set; }

        [ColumnNumber(1)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public int ImbueCount { get; set; }

        public int LostResistPoints { get; set; }

        [ColumnNumber(2)]
        public int PhysicalResist { get; set; }

        [ColumnNumber(5)]
        public int PoisonResist { get; set; }

        [ColumnNumber(0)]
        public SlotTypes Slot { get; set; }
    }
}