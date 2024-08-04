using System;

public interface IDamageable
{
    public event Action<int> Killed;

    public void Kill();

    public void Destroy();
}