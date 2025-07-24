using System;
using DG.Tweening;
using UnityEngine;

namespace FarmingSystem {

    public class Loot : MonoBehaviour {

        public event Action<Loot> Collected;

        public string Id { get; private set; }
        public int Amount { get; private set; }

        private Collider _collider;
        private Tween _moveToCollectorAnimation;

        private void Awake() {
            _collider ??= GetComponentInChildren<Collider>();
        }

        public void Init(string name, int amount) {
            Id = name;
            Amount = amount;
        }

        public void Collect(Vector3 collectorPosition, float duration) {
            _collider.enabled = false;

            _moveToCollectorAnimation = transform.DOJump(collectorPosition, 1f, 1, duration)
                                                 .SetEase(Ease.Flash)
                                                 .OnComplete(() => Take());
        }

        public virtual void Take() {
            _moveToCollectorAnimation?.Kill();
            _moveToCollectorAnimation = null;

            Collected?.Invoke(this);
        }
    }
}
