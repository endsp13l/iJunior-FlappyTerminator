using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _shootDelay = 0.5f;

    private Coroutine _coroutine;
    private bool _isActive;
    private bool _isReady;
    
    private void OnEnable()
    {
        _isActive = true;
        _isReady = true;
    }
    
    private void Update()
    {
        if(_isReady == false)
            return;
        
        if (Input.GetMouseButtonDown(0))
            Shoot();
    }
    
    private void Shoot()
    {
        _isReady = false;
        Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);
    
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