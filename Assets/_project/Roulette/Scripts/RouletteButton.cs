using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ValeryPopov.RoulettePrototype
{
    public class RouletteButton : MonoBehaviour
    {
        [SerializeField] private Roulette _roulette;
        [SerializeField] private Button _playButton;
        [SerializeField] private BumpAnimator _bumpAnimator;
        [Inject] private Player _player;
        [Inject] private Notification _notification;

        private void Awake()
        {
            _playButton.onClick.AddListener(Play);
        }

        private async void Play()
        {
            if (await _roulette.Play())
                _notification.Show(_roulette.LastItem.Icon, "Collected", _roulette.LastItem.Title);
            else
            {
                if (_roulette.PlayCost > _player.Mana.Value)
                    _bumpAnimator?.Play();
                // TODO fail sfx
            }
        }
    }
}