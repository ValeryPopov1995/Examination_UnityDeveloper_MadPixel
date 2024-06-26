using DG.Tweening;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ValeryPopov.RoulettePrototype
{
    public class Notification : MonoBehaviour
    {
        private const float _moveDuration = .5f;

        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _text;
        [Space]
        [SerializeField] private int _showDuration = 5;
        [SerializeField] private Vector3 _showPosition;
        private Vector3 _startPosition;

        public async void Show(Sprite icon = default, string title = default, string text = default)
        {
            _icon.sprite = icon;
            _icon.color = icon ? Color.white : Color.clear;
            _title.text = title;
            _text.text = text;

            transform.DOLocalMove(_showPosition, _moveDuration);
            await Task.Delay(TimeSpan.FromSeconds(_showDuration));

            if (destroyCancellationToken.IsCancellationRequested) return;
            transform.DOLocalMove(_startPosition, _moveDuration);

            // TODO play audio
        }
    }
}