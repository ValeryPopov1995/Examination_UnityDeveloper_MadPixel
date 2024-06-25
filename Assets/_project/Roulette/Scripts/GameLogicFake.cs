using UnityEngine;
using Zenject;

namespace ValeryPopov.RoulettePrototype
{
    public class GameLogicFake : MonoBehaviour
    {
        [SerializeField] private Roulette _roulette;
        [SerializeField] private RouletteItem[] _items;
        [Inject] private Notification _notification;

        private async void Start()
        {
            _roulette.SetItems(_items);
            if (await _roulette.Play())
                _notification.Show(_roulette.LastItem.Icon, "Collected", _roulette.LastItem.Title);
        }
    }
}