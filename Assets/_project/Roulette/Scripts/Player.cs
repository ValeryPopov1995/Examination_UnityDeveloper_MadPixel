using System;
using System.Threading.Tasks;
using UnityEngine;

namespace ValeryPopov.RoulettePrototype
{
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public Mana Mana { get; private set; }
        [SerializeField] private int _regenerationDelay = 1;

        private async void OnEnable()
        {
            while (!destroyCancellationToken.IsCancellationRequested)
            {
                Mana.Add();
                await Task.Delay(TimeSpan.FromSeconds(_regenerationDelay));
            }
        }
    }
}