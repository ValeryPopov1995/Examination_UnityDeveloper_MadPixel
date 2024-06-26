using TMPro;
using UnityEngine;
using Zenject;

namespace ValeryPopov.RoulettePrototype
{
    public class ManaCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [Inject] private Player _player;

        private void OnEnable()
        {
            _player.Mana.OnUpdate += UpdateText;
            UpdateText();
        }

        private void OnDisable()
        {
            _player.Mana.OnUpdate -= UpdateText;
        }

        private void UpdateText()
        {
            _text.text = $"{_player.Mana.Value} / {_player.Mana.MaxValue}";
        }
    }
}