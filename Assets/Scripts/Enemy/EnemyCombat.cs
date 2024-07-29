using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _shootDelay = 0.5f;
}
