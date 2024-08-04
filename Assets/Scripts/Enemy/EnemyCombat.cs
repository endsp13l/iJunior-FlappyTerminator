using System;
using System.Collections;
using UnityEngine;

public class EnemyCombat : Shooter
{
    [SerializeField] private float _shootDelay = 0.5f;

    private Coroutine _coroutine;
    private bool _isActive;

    public event Action Attack;

    private void OnEnable()
    {
        _isActive = true;
        _coroutine = StartCoroutine(Fight());
    }

    private IEnumerator Fight()
    {
        WaitForSeconds wait = new WaitForSeconds(_shootDelay);

        while (_isActive)
        {
            yield return wait;

            Attack?.Invoke();
            Shoot();
        }
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _isActive = false;
    }
}