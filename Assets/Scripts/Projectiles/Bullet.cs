using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private float _speed = 20f;

    private void Update()
    {
        transform.Translate(Vector2.right * (_speed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable target))
        {
            target.Destroy();
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}