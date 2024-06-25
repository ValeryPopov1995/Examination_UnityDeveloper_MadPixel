using System;
using UnityEngine;
using UnityEngine.UI;

namespace ValeryPopov.Roulette
{
    public class Roulette
    {

    }

    public class RouletteController : MonoBehaviour
    {

    }

    public class RouletteView : MonoBehaviour
    {

    }

    public class RouletteItems : ScriptableObject
    {

    }

    public class RouletteItem : ScriptableObject
    {
        [field: SerializeField] public Image Title { get; private set; }
        [field: SerializeField] public Image Icon { get; private set; }
    }

    [Serializable]
    public class Mana
    {
        [field: SerializeField] public int MaxValue { get; private set; } = 50;
        [field: SerializeField] public int Value { get; private set; }
        [field: SerializeField] public int ValueRegeneration { get; private set; } = 1;
    }
}