using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BackgroundMovement : MonoBehaviour
{
    private const int HalfDivider = 2;

    [SerializeField] private Camera _camera;

    private float _startPositionX;
    private float _endPositionX;

    private void Awake()
    {
        _startPositionX = transform.position.x;
        _endPositionX = GetComponent<Collider2D>().bounds.size.x / HalfDivider;
    }

    private void FixedUpdate()
    {
        if (_camera.transform.position.x >= _endPositionX)
            ChangePosition(_camera.transform.position.x);
    }

    private void ChangePosition(float cameraPositionX)
    {
        transform.position = new Vector3(cameraPositionX, transform.position.y, transform.position.z);

        _endPositionX += transform.position.x - _startPositionX;
        _startPositionX = transform.position.x;
    }
}