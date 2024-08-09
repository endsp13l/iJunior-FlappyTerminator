using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Ground : Border
{
    protected new void OnCollisionEnter2D(Collision2D other)
    {
        CheckOtherObject(other);

        if (other.gameObject.TryGetComponent(out Player player))
            player.Kill();
    }
}