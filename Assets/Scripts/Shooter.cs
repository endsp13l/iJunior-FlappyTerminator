using System;
using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _shootPoint;

    [SerializeField] private int _poolSize = 10;
    [SerializeField] private int _poolMaxSize = 30;

    private Pool<Bullet> _pool;

    private void Awake()
    {
        _pool = new Pool<Bullet>(_bulletPrefab, _poolSize, _poolMaxSize);
        _pool.Initialize();
    }
    
    private void OnDisable() => _pool.Clear();

    protected void Shoot() => SetStartPosition(_pool.Get());

    private void SetStartPosition(GameObject bullet)
    {
        bullet.transform.position = _shootPoint.position;
        bullet.transform.rotation = _shootPoint.rotation;
    }
}