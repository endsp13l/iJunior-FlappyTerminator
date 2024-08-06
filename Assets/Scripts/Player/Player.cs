using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private ScoreCounter _scoreCounter;

    public event Action<int> Killed;

    public void Kill()
    {
        Killed?.Invoke(_scoreCounter.Score);
        Destroy();
    }

    public void Destroy() => Destroy(gameObject);
}