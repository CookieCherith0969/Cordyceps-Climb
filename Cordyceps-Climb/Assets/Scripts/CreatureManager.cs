using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManager : MonoBehaviour
{
    public static CreatureManager activeManager;
    public List<GameObject> enemies;
    public List<GameObject> infected;
    public GameObject player;
    
    private void Awake()
    {
        activeManager = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Register(GameObject newChild)
    {
        if (enemies.Contains(newChild))
        {
            enemies.Remove(newChild);
        }
        if (infected.Contains(newChild))
        {
            infected.Remove(newChild);
        }
        if (newChild.CompareTag("Enemy"))
        {
            enemies.Add(newChild);
        }
        else if (newChild.CompareTag("Infected"))
        {
            infected.Add(newChild);
        }
        //Debug.Log("Registered " + newChild.name);
    }
    public void RegisterNew(GameObject newChild)
    {
        if (newChild.CompareTag("Enemy"))
        {
            enemies.Add(newChild);
        }
        else if (newChild.CompareTag("Infected"))
        {
            infected.Add(newChild);
        }
        //Debug.Log("Registered " + newChild.name);
    }
    public void Deregister(GameObject child)
    {
        if (enemies.Contains(child))
        {
            enemies.Remove(child);
        }
        if (infected.Contains(child))
        {
            infected.Remove(child);
        }
        //Debug.Log("Deregistered " + child.name);
    }

    public int ClearInfected()
    {
        int totalHealth = 0;
        for (int i = infected.Count - 1; i >= 0; i--)
        {
            if(infected[i] != player)
            {
                totalHealth += ((ICreature)infected[i].GetComponent(typeof(ICreature))).GetHealth();
                infected.RemoveAt(i);
            }
        }
        return totalHealth;
    }
    public GameObject GetPlayer()
    {
        return player;
    }
    
}
