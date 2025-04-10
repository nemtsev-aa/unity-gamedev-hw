using System;
using UnityEngine;

namespace ShootEmUp {
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Unit : MonoBehaviour, IDisposable {
        public event Action<Unit> Death;

        [SerializeField] private Transform _firePoint;

        protected Rigidbody2D _rigidbody2D;
        protected bool _isInit;

        public HitPointCounter HitPointCounter { get; protected set; }
        public MoveController MoveController { get; protected set; }
        public Weapon Weapon { get; protected set; }
        public Team Team { get; protected set; }

        public virtual void Init(int hitPoints, float speed, bool isPlayerTeam) {
            _rigidbody2D = GetComponent<Rigidbody2D>();

            HitPointCounter = new HitPointCounter(hitPoints);
            MoveController = new MoveController(_rigidbody2D, speed);
            Weapon = new Weapon(_firePoint);
            Team = new Team(isPlayerTeam);

            AddListeners();
            _isInit = true;
        }

        protected virtual void AddListeners() {
            HitPointCounter.HitPointsEmpty += OnHitPointsEmpty;
        }

        protected virtual void RemoveListeners() {
            HitPointCounter.HitPointsEmpty -= OnHitPointsEmpty;
        }

        protected virtual void OnHitPointsEmpty() {
            Death?.Invoke(this);
        }

        public virtual void Dispose() {
            RemoveListeners();
        }
    }
}