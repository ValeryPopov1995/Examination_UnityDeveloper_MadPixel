using UnityEngine;

namespace ValeryPopov.RoulettePrototype
{
    [CreateAssetMenu(menuName = "Scriptables/Roulette/RouletteItem")]
    public class RouletteItem : ScriptableObject
    {
        [field: SerializeField] public string Title { get; private set; } = "test title";
        [field: SerializeField] public string Discription { get; private set; } = "test description";
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public byte Chance { get; private set; } = 150;
    }
}