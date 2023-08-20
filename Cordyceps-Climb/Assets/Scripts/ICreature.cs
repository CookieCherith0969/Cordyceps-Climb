using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICreature
{
    public bool Damage(int amount);
    public bool Infect(int amount);
    public void SetHealth(int amount);
    public int GetHealth();
    public void Lock();
    public void Free();
}
