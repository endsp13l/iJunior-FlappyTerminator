using UnityEngine;

[RequireComponent(typeof(EnemyCombat))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class EnemyView : MonoBehaviour
{
    private const string AttackTrigger = "Attack";

    private EnemyCombat _enemyCombat;
    private Animator _animator;
    private AudioSource _shootSound;

    private void OnEnable() => _enemyCombat.Attack += OnShoot;

    private void Awake()
    {
        _enemyCombat = GetComponent<EnemyCombat>();
        _animator = GetComponent<Animator>();
        _shootSound = GetComponent<AudioSource>();
    }

    private void OnDisable() => _enemyCombat.Attack -= OnShoot;

    private void OnShoot()
    {
        _animator.Play(AttackTrigger);
        _shootSound.Play();
    }
}