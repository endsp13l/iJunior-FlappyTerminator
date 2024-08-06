using UnityEngine;

public class Border : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out IPoolable poolable))
        {
            if (poolable is Enemy)
            {
                other.gameObject.GetComponent<Enemy>().Kill();
            }

            poolable.Destroy();
        }
    }
}