using System;

namespace ArmorPicker.Attributes
{
    public sealed class ColumnNumber : Attribute
    {
        public ColumnNumber(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }
}