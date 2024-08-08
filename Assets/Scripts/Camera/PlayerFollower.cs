using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _xOffset = 6f;

    private Vector3 _startPosition;

    private void Awake() => _startPosition = transform.position;

    private void LateUpdate()
    {
        if (_player == null)
            return;

        Vector3 position = transform.position;
        position.x = _player.transform.position.x + _xOffset;
        transform.position = position;
    }
}