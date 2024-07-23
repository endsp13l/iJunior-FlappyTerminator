using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private readonly int _isFlying = Animator.StringToHash("IsFlying");

    private Animator _animator;
    private AudioSource _audioSource;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        _animator.SetBool(_isFlying, _playerMovement.IsFlying);
    }
}