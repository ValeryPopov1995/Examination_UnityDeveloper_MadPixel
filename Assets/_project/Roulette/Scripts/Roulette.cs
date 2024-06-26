using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace ValeryPopov.RoulettePrototype
{
    public class Roulette : MonoBehaviour
    {
        public RouletteItem LastItem { get; private set; }
        public bool CanPlay { get; private set; }
        public bool IsPlaing { get; private set; }

        [field: SerializeField] public int PlayCost { get; private set; } = 1;
        [SerializeField] private Transform _circle;
        [SerializeField] private Transform _iconsParent;
        [SerializeField] private Image _iconPrefab; // TODO addressables
        [SerializeField] private int _iconPointRadius = 90;
        [SerializeField] private int _minRotations = 5;
        [SerializeField] private int _duration = 5;
        [SerializeField] private AnimationCurve _rotationSpeed01;
        [Inject] private Player _player;
        private IEnumerable<RouletteItem> _items;

        internal bool SetItems(IEnumerable<RouletteItem> items)
        {
            if (IsPlaing) return false;

            _items = items
                .Where(i => i != null);

            if (!_items.Any()) return false;

            if (_iconsParent.childCount > 0)
            {
                var icons = new Transform[_iconsParent.childCount];
                for (int i = 0; i < _iconsParent.childCount; i++)
                    icons[i] = _iconsParent.GetChild(i);
                for (int i = 0; i < _iconsParent.childCount; i++)
                    Destroy(icons[i].gameObject);
            }

            float angleStep = 360f / _items.Count();
            for (int i = 0; i < _items.Count(); i++)
            {
                float angle = i * angleStep;
                float radians = angle * Mathf.Deg2Rad;
                float x = _iconPointRadius * Mathf.Cos(radians);
                float y = _iconPointRadius * Mathf.Sin(radians);

                var icon = Instantiate(_iconPrefab, _iconsParent);
                icon.sprite = _items.ElementAt(i).Icon;
                icon.transform.localPosition = new Vector3(x, y, 0);
                icon.transform.localRotation = Quaternion.Euler(0, 0, angle);
            }

            CanPlay = true;
            return true;
        }

        internal async Task<bool> Play()
        {
            if (!CanPlay || IsPlaing) return false;
            if (_player.Mana.Value < PlayCost) return false;

            IsPlaing = true;
            _player.Mana.Remove(PlayCost);
            int target = GetItemIndexByChance(_items);
            float startAngle = _circle.localEulerAngles.z;
            float targetAngle = 360f * target / _items.Count();
            Debug.Log(targetAngle);
            float rotationAngle = 360 * _minRotations - targetAngle;

            float progress = 0;
            while (progress < 1 && !destroyCancellationToken.IsCancellationRequested)
            {
                float z = Mathf.Lerp(startAngle, rotationAngle, _rotationSpeed01.Evaluate(progress));
                _circle.localEulerAngles = new(0, 0, z);
                progress += Time.deltaTime / _duration;
                await Task.Yield();
            }

            LastItem = _items.ElementAt(target);
            IsPlaing = false;
            return true;
        }

        private int GetItemIndexByChance(IEnumerable<RouletteItem> items)
        {
            int full = items.Sum(i => i.Chance);
            int random = Random.Range(0, full);
            int current = 0;
            int index = 0;
            foreach (var item in items)
            {
                current += item.Chance;
                if (random < current)
                    return index;
                index++;
            }

            throw new Exception("Can't get item by chance");
        }
    }
}