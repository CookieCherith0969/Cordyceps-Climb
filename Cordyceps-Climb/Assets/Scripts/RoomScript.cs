using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public float height = 0;
    public List<GameObject> enemies;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ReleaseEnemies()
    {
        foreach(GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }
    }
    public void ClearInfected()
    {
        foreach (GameObject enemy in enemies)
        {
            ICreature enemyScript = (ICreature)enemy.GetComponent(typeof(ICreature));
            enemyScript.Lock();
            enemyScript.SetHealth(0);
        }
    }
}
