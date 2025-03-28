using System;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

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

        public void SetVelocity(Vector2 velocity) {
            _rigidbody2D.linearVelocity = velocity;
        }

        public void SetPhysicsLayer(int physicsLayer) {
            gameObject.layer = physicsLayer;
        }

        public void SetPosition(Vector3 position) {
            transform.position = position;
        }

        public void SetColor(Color color) {
            _spriteRenderer.color = color;
        }

        public void SetDamage(int damage) {
            Damage = damage;
        }

        public void SetIsPlayer(bool isPlayer) {
            IsPlayer = isPlayer;
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