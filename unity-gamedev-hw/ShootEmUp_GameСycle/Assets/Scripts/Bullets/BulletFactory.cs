using ShootEmUp;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(BulletFactory), menuName = "Configs/" + nameof(BulletFactory))]
public class BulletFactory : ScriptableObject {
    [SerializeField] private Bullet _prefab;

    public Bullet Get(Transform parent) {
        return Instantiate(_prefab, parent);
    }
}