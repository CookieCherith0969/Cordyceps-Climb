using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICreature
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="amount"></param>
    /// <returns>If the Creature died</returns>
    public bool Damage(int amount);
    public bool Infect(int amount);
    public void SetHealth(int amount);
    public int GetHealth();
    public void Lock();
    public void Unlock();
    public void ResetInfection();

}
