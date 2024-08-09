using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyCombat : Shooter
{
    [SerializeField] private float _minShootDelay = 1f;
    [SerializeField] private float _maxShootDelay = 3f;

    private Coroutine _coroutine;
    private bool _isActive;

    public event Action Attack;

    private void OnEnable()
    {
        ClearBullets();
        
        _isActive = true;
        _coroutine = StartCoroutine(Fight());
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _isActive = false;
    }

    private IEnumerator Fight()
    {
        WaitForSeconds wait = new WaitForSeconds(GetRandomDelay());

        while (_isActive)
        {
            yield return wait;

            Attack?.Invoke();
            Shoot();
        }
    }

    private float GetRandomDelay() => Random.Range(_minShootDelay, _maxShootDelay);
}