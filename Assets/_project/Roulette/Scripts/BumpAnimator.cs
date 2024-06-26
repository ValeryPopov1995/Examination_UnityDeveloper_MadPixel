using DG.Tweening;
using UnityEngine;

namespace ValeryPopov.RoulettePrototype
{
    public class BumpAnimator : MonoBehaviour
    {
        [SerializeField] private float _scaleMultiplayer = 1.5f;
        [SerializeField] private float _duration = .5f;
        private Vector3 _startScale;

        private void Awake()
        {
            _startScale = transform.localScale;
        }

        public async void Play()
        {
            await transform.DOScale(_startScale * _scaleMultiplayer, _duration / 2).AsyncWaitForCompletion();
            await transform.DOScale(_startScale, _duration / 2).AsyncWaitForCompletion();
        }
    }
}