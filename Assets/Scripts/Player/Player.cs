using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private ScoreCounter _scoreCounter;

    private Animator _animator;
    private AudioSource _audioSource;

    private bool _isAlive = true;
    public event Action<int> Killed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void Kill() => Killed?.Invoke(_scoreCounter.Score);

    public void Destroy()
    {
        Destroy(gameObject);
    }
}