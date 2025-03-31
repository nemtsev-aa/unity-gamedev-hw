using System;
using UnityEngine;

namespace ShootEmUp {
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Bullet : MonoBehaviour {
        public event Action<Bullet, Unit> OnCollisionEntered;

        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;

        public bool IsPlayer { get; private set; }
        public int Damage { get; private set; }

        public void Init(BulletSystem.Args args) {
            _rigidbody2D ??= GetComponent<Rigidbody2D>();
            _spriteRenderer ??= GetComponentInChildren<SpriteRenderer>();

            transform.position = args.Position;
            _rigidbody2D.linearVelocity = args.Velocity;
            _spriteRenderer.color = args.Color;
            gameObject.layer = args.PhysicsLayer;
            Damage = args.Damage;
            IsPlayer = args.IsPlayer;

            SetLayerToAllChildren(transform, args.PhysicsLayer);
        }

        public void SetSleepState(bool status) {
            _rigidbody2D.simulated = !status;
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            var unit = collision.gameObject.GetComponent<Unit>();
            OnCollisionEntered?.Invoke(this, unit);
        }

        private void SetLayerToAllChildren(Transform transform, int physicsLayer) {
            transform.gameObject.layer = physicsLayer;

            for (int i = 0; i < transform.childCount; i++) {
                Transform child = transform.GetChild(i);
                SetLayerToAllChildren(child, physicsLayer);
            }
        }
    }
}