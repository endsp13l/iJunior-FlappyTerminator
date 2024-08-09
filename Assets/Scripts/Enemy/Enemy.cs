using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour, IDamageable, IPoolable
{
    [SerializeField] private int _destroyScore = 2;
    [SerializeField] private int _avoidScore = 1;

    public event Action<int> Killed;
    public event Action<int> Avoided;
    public event Action<Enemy> Destroyed;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            player.Kill();
            Destroy();
        }
    }

    public void Kill()
    {
        Killed?.Invoke(_destroyScore);
        Destroy();
    }

    public void Avoid() => Avoided?.Invoke(_avoidScore);

    public void Destroy() => Destroyed?.Invoke(this);
}