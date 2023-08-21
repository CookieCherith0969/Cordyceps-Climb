using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTrigger : MonoBehaviour
{
    CreatureManager cm;
    public DoorScript door;
    // Start is called before the first frame update
    void Start()
    {
        cm = CreatureManager.activeManager;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != cm.player)
        {
            return;
        }
        door.CloseMushroom();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
