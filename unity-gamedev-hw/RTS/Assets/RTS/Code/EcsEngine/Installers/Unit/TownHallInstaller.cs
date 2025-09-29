using Client.Components;
using Client.Components.Health;
using Client.Components.Movement;
using Client.Components.Targeting;
using Client.Components.Teams;
using Client.Components.Visual;
using Leopotam.EcsLite.Entities;
using TMPro;
using UnityEngine;
using R3;
using Sirenix.OdinInspector;

namespace Client.Installer {

    public sealed class TownHallInstaller : EntityInstaller {
        public Observable<Unit> IsDestroyed => _isDestroyed;

        [SerializeField] private TeamTypes _teamType;
        [Header("Model Settings")]
        [SerializeField] private float _modelRadius;

        [Header("Health Settings")]
        [SerializeField] private int _health = 100;
        [SerializeField] private TMP_Text _healthText;

        private Entity _entity;
        private int _currentHealth;
        private Subject<Unit> _isDestroyed = new Subject<Unit>();

        protected override void Install(Entity entity) {
            _entity = entity;
            _currentHealth = _health;

            entity.AddData(new UnitSpawner { });

            // Basic components
            entity.AddData(new ModelRadius { Value = _modelRadius });
            entity.AddData(new Position { Value = transform.position });
            entity.AddData(new TransformView { Value = transform });

            // Health and visuals
            entity.AddData(new Health { Value = _health, MaxValue = _health });
            entity.AddData(new DamageableTag());

            // Team affiliation
            entity.AddData(new Team { Value = _teamType });

            // Добавляем компоненты для таргетирования зданий
            entity.AddData(new TargetableComponent {
                IsActive = true,
                ThreatLevel = 1.0f, // Высокий приоритет
                Type = TargetType.Building
            });

            UpdateHealthDisplay();
        }

        private void Update() {

            if (_entity == null || !_entity.IsAlive())
                return;

            if (_entity.TryGetData(out Health health) && _currentHealth != health.Value) {
                _currentHealth = health.Value;
                
                UpdateHealthDisplay();
            }
        }

        private void UpdateHealthDisplay() {
            
            if (_healthText != null) {
                _healthText.text = $"{_currentHealth}/{_entity.GetData<Health>().MaxValue}";
            }
        }

        protected override void Dispose(Entity entity) {

            // Помечаем, как нетаргетируемый
            if (entity.TryGetData(out TargetableComponent targetable)) {
                targetable.IsActive = false;
                entity.SetData(targetable);
            }

            Debug.Log($"TownHallInstaller: Dispose {entity.Id} {_teamType}");

            // Cleanup any resources if needed
            if (_healthText != null) 
                Destroy(_healthText.gameObject);

            _isDestroyed.OnNext(default);
        }

        //[Button]
        //private void ShowModelRaduis() {
        //    ShowVisionArea(transform.position, _modelRadius, Color.yellow);
        //}

        //private void ShowVisionArea(Vector3 center, float radius, Color color, int segments = 12) {
        //    float angle = 0f;
        //    Vector3 lastPoint = center + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;

        //    for (int i = 1; i <= segments; i++) {
        //        angle = i * Mathf.PI * 2f / segments;
        //        Vector3 nextPoint = center + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
        //        Debug.DrawLine(lastPoint, nextPoint, color, 0.1f);
        //        lastPoint = nextPoint;
        //    }
        //}
    }
}
