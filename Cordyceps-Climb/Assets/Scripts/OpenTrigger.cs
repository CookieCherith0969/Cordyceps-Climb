using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public CreatureManager cm;
    public DoorScript door;
    // Start is called before the first frame update
    void Start()
    {
        door = transform.parent.gameObject.GetComponent<DoorScript>();
    }
    private void OnTriggerEnter2D(Collider2D player)
    {
        //if (cm.enemies.Count == 0) //if no enemies in room.
        //{
        door.Open();
        //}
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
