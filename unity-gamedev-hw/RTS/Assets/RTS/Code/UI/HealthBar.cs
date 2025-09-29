using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Client.Components.Health;
using Leopotam.EcsLite.Entities;

public class HealthBar : MonoBehaviour {
    [SerializeField] private Image _filler;
    [SerializeField] private TMP_Text _healthText;

    private Entity _entity;
    private int _currentHealth;
    private int _maxHealth;

    public void Init(Entity entity) {
        _entity = entity;
        _currentHealth = _maxHealth = _entity.GetData<Health>().MaxValue;

        UpdateCompanents();
    }

    private void LateUpdate() {
        if (_entity == null || _entity.IsAlive() == false)
            return;

        if (_entity.TryGetData(out Health health) && _currentHealth != health.Value) {
            _currentHealth = health.Value;

            UpdateCompanents();
        }

        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, Vector3.up);
    }

    private void UpdateCompanents() {

        if (_healthText != null) {
            _healthText.text = $"{_currentHealth}/{_maxHealth}";
            _filler.fillAmount = (float)_currentHealth / _maxHealth;
        }
    }
}
