using UnityEngine;

[RequireComponent(typeof(EnemyCombat))]
[RequireComponent(typeof(Animator))]
public class EnemyView : MonoBehaviour
{
   private EnemyCombat _enemyCombat;
   private Animator _animator;
   
   private void OnEnable()
   {
       _enemyCombat.Attack += OnShoot;
   }
   
   private void OnDisable()
   {
       _enemyCombat.Attack -= OnShoot;
   }
   
   private void OnShoot()
   {
       _animator.SetBool("IsShooting", true);
       _animator.SetBool("IsShooting", false);
       
      
   }
   
   private void Awake()
   {
       _enemyCombat = GetComponent<EnemyCombat>();
       _animator = GetComponent<Animator>();
   }
}
