using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    CreatureManager cm;
    public DoorScript door;
    // Start is called before the first frame update
    void Start()
    {
        cm = CreatureManager.activeManager;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject != cm.player)
        {
            return;
        }
        if (cm.enemies.Count == 0) //if no enemies in room.
        {
            door.Open();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
