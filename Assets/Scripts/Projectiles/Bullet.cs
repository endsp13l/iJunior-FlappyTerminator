using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private float _speed = 20f;

    public event Action<GameObject> Destroyed;

    private void Update()
    {
        transform.Translate(Vector2.right * (_speed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable target))
        {
            target.Kill();
            Destroy();
        }
    }

    private void OnBecameInvisible() => Destroy();

    private void Destroy() => Destroyed?.Invoke(gameObject);
}