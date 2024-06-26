using UnityEngine;

namespace ValeryPopov.RoulettePrototype
{
    public class GameLogicFake : MonoBehaviour
    {
        [SerializeField] private Roulette _roulette;
        [SerializeField] private RouletteItem[] _items;

        private void Start()
        {
            _roulette.SetItems(_items);
        }
    }
}