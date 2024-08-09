using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private EnemySpawner _enemySpawner;

    private PlayerMovement _playerMovement;

    public event Action Launched;
    public event Action Ended;

    private void Awake()
    {
        _playerMovement = _player.GetComponent<PlayerMovement>();
        _enemySpawner.gameObject.SetActive(false);
        DeactivatePlayer();
    }

    private void OnEnable() => _player.Killed += End;

    private void Start() => Launched?.Invoke();

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }

    private void OnDisable() => _player.Killed -= End;

    public void Play()
    {
        ActivatePlayer();
        _scoreCounter.Initialize();
        _enemySpawner.gameObject.SetActive(true);
    }

    private void End(int collectedScores)
    {
        Ended?.Invoke();
        _enemySpawner.gameObject.SetActive(false);
        _scoreCounter.SetMaxScore(collectedScores);
        DeactivatePlayer();
    }

    private void DeactivatePlayer()
    {
        _player.GetComponent<Rigidbody2D>().isKinematic = true;
        _player.GetComponent<PlayerCombat>().enabled = false;
        _playerMovement.enabled = false;
    }

    private void ActivatePlayer()
    {
        _player.gameObject.SetActive(true);
        _player.GetComponent<Rigidbody2D>().isKinematic = false;
        _player.GetComponent<PlayerCombat>().enabled = true;
        _playerMovement.enabled = true;
    }
}