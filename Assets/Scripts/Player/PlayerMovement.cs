using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _jetForce = 5f;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _maxRotationAngle = 15f;
    [SerializeField] private float _minRotationAngle = -15f;
    [SerializeField] private float _rotationSpeed = 1f;

    private Rigidbody2D _rigidbody;
    private Vector3 _startPosition;
    private Quaternion _minRotation;
    private Quaternion _maxRotation;
    private float _rotationAngle;

    public event Action FlightStart;
    public event Action FlightEnd;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _startPosition = transform.position;

        _minRotation = Quaternion.Euler(0, 0, _minRotationAngle);
        _maxRotation = Quaternion.Euler(0, 0, _maxRotationAngle);
    }

    private void OnEnable()
    {
        _rigidbody.velocity = Vector2.zero;
        transform.rotation = Quaternion.identity;
        transform.position = _startPosition;

        Fly();
    }

    private void OnDisable()
    {
        _rigidbody.velocity = Vector2.zero;
        transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Fly();

        if (_rigidbody.velocity.y <= 0)
            FlightEnd?.Invoke();

        transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, Time.deltaTime * _rotationSpeed);
    }

    public void Fly()
    {
        FlightEnd?.Invoke();
        FlightStart?.Invoke();

        _rigidbody.velocity = new Vector2(_speed, _jetForce);
        transform.rotation = _maxRotation;
    }
}