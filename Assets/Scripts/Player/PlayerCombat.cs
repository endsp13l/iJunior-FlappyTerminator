using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _shootDelay = 0.5f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);
    }
}