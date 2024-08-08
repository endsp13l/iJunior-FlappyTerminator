using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(EnemyCombat))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class EnemyView : MonoBehaviour
{
    private const string AttackTrigger = "Attack";

    [SerializeField] private AudioSource _shootSound;
    [SerializeField] private AudioClip _deathSound;

    private Enemy _enemy;
    private EnemyCombat _enemyCombat;
    private Animator _animator;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _enemyCombat = GetComponent<EnemyCombat>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _enemyCombat.Attack += OnShoot;
        _enemy.Killed += OnDie;
    }

    private void OnDisable()
    {
        _enemyCombat.Attack -= OnShoot;
        _enemy.Killed += OnDie;
    }

    private void OnShoot()
    {
        _animator.Play(AttackTrigger);
        _shootSound.Play();
    }

    private void OnDie(int score) => AudioSource.PlayClipAtPoint(_deathSound, transform.position);
}