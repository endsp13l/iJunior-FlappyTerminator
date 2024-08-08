using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BackgroundMovement : MonoBehaviour
{
    private const int HalfDivider = 2;

    [SerializeField] private Camera _camera;

    private float _defaultStartPositionX;
    private float _defaultEndPositionX;
    private float _startPositionX;
    private float _endPositionX;

    private void Awake()
    {
        _defaultStartPositionX = transform.position.x;
        _defaultEndPositionX = GetComponent<Collider2D>().bounds.size.x / HalfDivider;

        ResetPosition();
    }

    private void FixedUpdate()
    {
        if (_camera.transform.position.x >= _endPositionX)
            ChangePosition(_camera.transform.position.x);

        if (_camera.transform.position.x < _startPositionX)
            ResetPosition();
    }

    private void ResetPosition()
    {
        transform.position = Vector3.zero;
        _endPositionX = _defaultEndPositionX;
        _startPositionX = _defaultStartPositionX;
    }

    private void ChangePosition(float cameraPositionX)
    {
        Vector3 position = transform.position;
        position = new Vector3(cameraPositionX, position.y, position.z);
        transform.position = position;

        _endPositionX += position.x - _startPositionX;
        _startPositionX = position.x;
    }
}