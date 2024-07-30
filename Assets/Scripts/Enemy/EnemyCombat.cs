using System;
using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _shootDelay = 0.5f;
    
    private Coroutine _coroutine;
    private bool _isActive;

    public event Action Attack;
    
    private void OnEnable()
    {
        _isActive = true;
        _coroutine = StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        WaitForSeconds wait = new WaitForSeconds(_shootDelay);
        
          while (_isActive)
          {
              yield return wait;

              Attack?.Invoke();
              Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);
          }
    }
    
    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _isActive = false;
    }
}