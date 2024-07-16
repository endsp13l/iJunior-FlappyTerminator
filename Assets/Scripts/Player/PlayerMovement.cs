using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _jetForce = 5f;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _maxRotationAngle = 25f;
    [SerializeField] private float _minRotationAngle = -25f;
    [SerializeField] private float _rotationSpeed = 1f;

    private Rigidbody2D _rigidbody;
    private Vector3 _startPosition;
    private Quaternion _minRotation;
    private Quaternion _maxRotation;
    private float _rotationAngle;
    
    private void Start()
    {
        _startPosition = transform.position;
        _minRotation = Quaternion.Euler(0, 0, _minRotationAngle);
        _maxRotation = Quaternion.Euler(0, 0, _maxRotationAngle);
    }
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _rigidbody.velocity = new Vector2(_speed, _jetForce);
            transform.rotation = _maxRotation;
        }
        
        transform.rotation = Quaternion.Lerp(transform.rotation , _minRotation, Time.deltaTime * _rotationSpeed);
    }
}