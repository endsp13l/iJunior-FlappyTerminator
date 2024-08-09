using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Shooter : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _shootPoint;

    [SerializeField] private int _poolSize = 10;
    [SerializeField] private int _poolMaxSize = 30;

    private ObjectPool<Bullet> _objectPool;
    private List<Bullet> _bullets = new List<Bullet>();

    private void Awake()
    {
        _objectPool = new ObjectPool<Bullet>(
            createFunc: () => CreateBullet(),
            actionOnGet: (obj) => RestartBullet(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => ActionOnDestroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolSize,
            maxSize: _poolMaxSize);
    }

    protected void ClearBullets()
    {
        foreach (Bullet bullet in _bullets)
            bullet.gameObject.SetActive(false);
    }

    protected void Shoot() => _objectPool.Get();

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(_bulletPrefab);
        _bullets.Add(bullet);

        bullet.Destroyed += _objectPool.Release;

        bullet.gameObject.SetActive(false);
        return bullet;
    }

    private void ActionOnDestroy(Bullet bullet)
    {
        bullet.Destroyed -= _objectPool.Release;
        _bullets.Remove(bullet);
        Destroy(bullet.gameObject);
    }

    private void RestartBullet(Bullet bullet)
    {
        bullet.transform.position = _shootPoint.position;
        bullet.transform.rotation = _shootPoint.rotation;

        bullet.gameObject.SetActive(true);
    }
}