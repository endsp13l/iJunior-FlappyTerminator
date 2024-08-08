using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Border : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out IPoolable poolable))
        {
            if (poolable is Enemy)
            {
                other.gameObject.GetComponent<Enemy>().Avoid();
            }

            poolable.Destroy();
        }
    }
}