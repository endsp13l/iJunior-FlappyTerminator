using System;
using UnityEngine;

public interface IPoolable
{
    public event Action<GameObject> Destroyed;
    
    public void Destroy();
    
    public void Clear();
}