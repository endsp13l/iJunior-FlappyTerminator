using UnityEngine;

public class Border : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out IPoolable poolable))
            poolable.Destroy();
    }
}