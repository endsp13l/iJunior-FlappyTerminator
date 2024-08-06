using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Camera _camera;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private float _spawnDelay = 1f;

    [SerializeField] private int _poolSize = 10;
    [SerializeField] private int _poolMaxSize = 30;

    private Pool<Enemy> _pool;
    private Coroutine _coroutine;
    private bool _isActive;

    private void Awake()
    {
        _pool = new Pool<Enemy>(_enemyPrefab, _poolSize, _poolMaxSize);
        _pool.Initialize();
    }

    private void OnEnable()
    {
        _isActive = true;
        _coroutine = StartCoroutine(Spawn());

        _pool.Getted += Subscribe;
        _pool.Released += Unsubscribe;
    }

    private void OnDisable()
    {
        _isActive = false;
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _pool.Getted -= Subscribe;
        _pool.Released -= Unsubscribe;

        _pool.Clear();
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);

        while (_isActive)
        {
            if (_pool.Get().TryGetComponent(out Enemy enemy))
            {
                enemy.transform.position = GetRandomPosition();
                yield return wait;
            }
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