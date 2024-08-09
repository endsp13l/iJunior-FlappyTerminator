using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Camera _camera;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private float _spawnDelay = 1f;

    [SerializeField] private int _poolSize = 5;
    [SerializeField] private int _poolMaxSize = 10;

    private ObjectPool<Enemy> _objectPool;
    private List<Enemy> _enemies = new List<Enemy>();
    private Coroutine _coroutine;
    private bool _isActive;

    private void Awake()
    {
        _objectPool = new ObjectPool<Enemy>(
            createFunc: () => Create(),
            actionOnGet: (obj) => Subscribe(obj),
            actionOnRelease: (obj) => ActionOnRelease(obj),
            actionOnDestroy: (obj) => ActionOnDestroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolSize,
            maxSize: _poolMaxSize);
    }

    private void OnEnable()
    {
        _isActive = true;
        _coroutine = StartCoroutine(Spawn());

        foreach (Enemy enemy in _enemies)
            enemy.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _isActive = false;

        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private Enemy Create()
    {
        Enemy enemy = Instantiate(_enemyPrefab);
        enemy.gameObject.SetActive(false);
        
        enemy.Destroyed += _objectPool.Release;
        
        _enemies.Add(enemy);
        return enemy;
    }

    private void ActionOnRelease(Enemy enemy)
    {
        Unsubscribe(enemy);
        enemy.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(Enemy enemy)
    {
        enemy.Destroyed -= _objectPool.Release;
        _enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);

        while (_isActive)
        {
            Enemy enemy = _objectPool.Get();
            enemy.transform.position = GetRandomPosition();
            enemy.gameObject.SetActive(true);

            yield return wait;
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector2 position = _camera.ScreenToWorldPoint(new Vector2(_camera.transform.position.x + Screen.width,
            Random.Range(0, Screen.height)));

        return new Vector3(position.x, position.y, 0);
    }

    private void Subscribe(Enemy enemy)
    {
        enemy.Killed += _scoreCounter.AddScore;
        enemy.Avoided += _scoreCounter.AddScore;
    }

    private void Unsubscribe(Enemy enemy)
    {
        enemy.Killed -= _scoreCounter.AddScore;
        enemy.Avoided -= _scoreCounter.AddScore;
    }
}