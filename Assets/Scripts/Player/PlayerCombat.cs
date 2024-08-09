using System;
using System.Collections;
using UnityEngine;

public class PlayerCombat : Shooter
{
    [SerializeField] private float _shootDelay = 0.5f;

    private Coroutine _coroutine;
    private bool _isActive;
    private bool _isReady;

    public event Action Attack;

    private void OnEnable()
    {
        ClearBullets();
        
        _isActive = true;
        _isReady = true;
    }

    private void Update()
    {
        if (_isReady == false || _isActive == false)
            return;

        if (Input.GetMouseButtonDown(0))
            TryShoot();
    }

    private void OnDisable() => _isActive = false;

    private void TryShoot()
    {
        _isReady = false;
        Shoot();
        Attack?.Invoke();
        _coroutine = StartCoroutine(ShootDelay());
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(_shootDelay);
        _isReady = true;

        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }
}