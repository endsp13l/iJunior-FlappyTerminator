using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
  private Animator _animator;
  private AudioSource _audioSource;
  
  private bool _isAlive = true;

  private void Awake()
  {
    _animator = GetComponent<Animator>();
    _audioSource = GetComponent<AudioSource>();
  }

  public void Destroy()
  {
    throw new System.NotImplementedException();
  }
}
