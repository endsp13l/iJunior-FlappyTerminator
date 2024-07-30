using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
}