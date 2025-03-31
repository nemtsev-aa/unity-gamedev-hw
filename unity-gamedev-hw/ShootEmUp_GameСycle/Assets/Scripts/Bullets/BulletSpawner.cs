using ShootEmUp;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour, IGameStartListener, IGameFinishListener {
    private BulletFactory _factory;
    
    private int _initialCount;
    private List<Bullet> _bulletPool = new List<Bullet>();

    [field: SerializeField] public Transform Container { get; private set; }
    [field: SerializeField] public Transform WorldTransform { get; private set; }

    public void Init(BulletFactory bulletFactory, int initialCount) {
        _factory = bulletFactory;
        _initialCount = initialCount;
    }

    public void OnStartGame() {
        CreateBulletPool();
    }

    public void OnFinishGame() {
        ClearBulletPool();
    }

    public Bullet SpawnBullet(Transform parent) {
        return _factory.Get(parent);
    }
    
    private void CreateBulletPool() {
        if (_initialCount <= 0)
            throw new ArgumentNullException($"Initial Bullet Count is null");

        for (var i = 0; i < _initialCount; i++) {
            var bullet = _factory.Get(Container);
            _bulletPool.Add(bullet);
        }
    }

    private void ClearBulletPool() {
        if (_bulletPool != null && _bulletPool.Count > 0) {
            
            foreach (Bullet iBullet in _bulletPool) {
                Destroy(iBullet.gameObject);
            }

            _bulletPool.Clear();
        }
    }
}