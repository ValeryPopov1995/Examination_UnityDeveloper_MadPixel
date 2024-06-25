using System;
using UnityEngine;

namespace ValeryPopov.RoulettePrototype
{
    [Serializable]
    public class Mana
    {
        [field: SerializeField] public int MaxValue { get; private set; } = 50;
        [field: SerializeField] public int Value { get; private set; }
        [field: SerializeField] public int ValueRegeneration { get; private set; } = 1;

        public void Add(int value = 1)
        {
            if (value <= 0) return;
            Value = Mathf.Min(Value + value, MaxValue);
        }

        public void Remove(int value = 1)
        {
            if (value <= 0) return;
            Value = Mathf.Max(Value - value, 0);
        }
    }
}