using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerView : MonoBehaviour
{
    private readonly int _isFlying = Animator.StringToHash("IsFlying");

    [SerializeField] private AudioSource _shootSound;
    [SerializeField] private AudioSource _jetSound;

    private Sprite _defaultSprite;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private PlayerMovement _playerMovement;
    private PlayerCombat _playerCombat;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultSprite = _spriteRenderer.sprite;
        _animator = GetComponent<Animator>();
        _shootSound = GetComponent<AudioSource>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerCombat = GetComponent<PlayerCombat>();
    }

    private void OnEnable()
    {
        _playerCombat.Attack += OnShoot;
        _playerMovement.FlightStart += OnFlightStart;
        _playerMovement.FlightEnd += OnFlightEnd;
    }

    private void OnDisable()
    {
        _spriteRenderer.sprite = _defaultSprite;
        _playerCombat.Attack -= OnShoot;
        _playerMovement.FlightStart -= OnFlightStart;
        _playerMovement.FlightEnd -= OnFlightEnd;
    }

    public void ShowFlightAnimation() => _animator.SetBool(_isFlying, true);

    private void OnShoot() => _shootSound.Play();

    private void OnFlightStart()
    {
        _animator.SetBool(_isFlying, true);
        _jetSound.Play();
    }

    private void OnFlightEnd()
    {
        _animator.SetBool(_isFlying, false);
        _jetSound.Stop();
    }
}