using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IPoolable
{
    [SerializeField] private int _destroyScore = 2;
    [SerializeField] private int _avoidScore = 1;

    public event Action<int> Killed;
    public event Action<int> Avoided;
    public event Action<GameObject> Destroyed;

    public void Kill()
    {
        Killed?.Invoke(_destroyScore);
        Destroy();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}