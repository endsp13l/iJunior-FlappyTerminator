using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Camera _camera;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private float _spawnDelay = 1f;

    private Coroutine _coroutine;
    private bool _isActive;

    private void Start()
    {
        _isActive = true;
        _coroutine = StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        _isActive = false;
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);

        while (_isActive)
        {
            Enemy enemy = Instantiate(_enemy, GetRandomPosition(), Quaternion.identity);
            Subscribe(enemy);
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
}