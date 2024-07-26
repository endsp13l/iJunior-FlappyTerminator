using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _spawnDelay = 1f;

    private Coroutine _coroutine;
    private bool _isActive;

    private void OnEnable()
    {
        _isActive = true;
        _coroutine = StartCoroutine(Spawn());
    }

    private void Start()
    {
        _coroutine = StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (_isActive)
        {
            Instantiate(_enemy, GetRandomPosition(), Quaternion.identity);
            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    private void OnDisable()
    {
        _isActive = false;
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private Vector3 GetRandomPosition()
    {
        Vector2 position = _camera.ScreenToWorldPoint(new Vector2(_camera.transform.position.x + Screen.width,
            Random.Range(0, Screen.height)));

        return new Vector3(position.x, position.y, 0);
    }
}