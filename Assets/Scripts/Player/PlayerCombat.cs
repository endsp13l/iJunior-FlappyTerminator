using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _shootDelay = 0.5f;

    private Coroutine _shootCoroutine;

    private void OnEnable()
    {
        _shootCoroutine = StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        WaitForSeconds delay = new WaitForSeconds(_shootDelay);

        while (true)
        {
            yield return delay;
            Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);
        }
    }

    private void OnDisable()
    {
        if (_shootCoroutine != null)
            StopCoroutine(_shootCoroutine);
    }
}