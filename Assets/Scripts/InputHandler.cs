using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Button _jumpButton;
    [SerializeField] private Button _attackButton;
    
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerCombat _playerCombat;

    private void OnEnable()
    {
        _jumpButton.onClick.AddListener(() => _playerMovement.Fly());
        _attackButton.onClick.AddListener(() => _playerCombat.TryShoot());
    }
    
    private void OnDisable()
    {
        _jumpButton.onClick.RemoveListener(() => _playerMovement.Fly());
        _attackButton.onClick.RemoveListener(() => _playerCombat.TryShoot());
    }
}