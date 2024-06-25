using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace ValeryPopov.RoulettePrototype
{
    public class Roulette : MonoBehaviour
    {
        public RouletteItem LastItem { get; private set; }
        [SerializeField] private int _playCost = 1;
        [SerializeField] private int _fadeRotationCircles = 5;
        [SerializeField] private Transform _circle;
        [Inject] private Player _player;
        private IEnumerable<RouletteItem> _items;
        private float _circleRotationDuration = 1;

        internal void SetItems(IEnumerable<RouletteItem> items)
        {
            _circle.localRotation = Quaternion.identity;

            _items = items
                .Where(i => i != null);

            if (!_items.Any())
                throw new Exception("Have no items to play");
        }

        internal async Task<bool> Play()
        {
            if (_player.Mana.Value <= _playCost)
            {
                // TODO play locked audio
                return false;
            }

            _player.Mana.Remove(_playCost);
            var target = GetItemIndexByChance(_items);
            float targetAngle = 360 * _items.Count() / target;
            await _circle.DOLocalRotate(new(0, 0, targetAngle + 360 * 5), 5).AsyncWaitForCompletion();
            LastItem = _items.ElementAt(target);
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