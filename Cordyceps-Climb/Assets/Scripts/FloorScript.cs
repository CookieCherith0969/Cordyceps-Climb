using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    RoomScript room;
    //[SerializeField]
    //Vector3 origin = new Vector3(0, 0, 0);
    private void Start()
    {
        room = gameObject.transform.parent.GetComponent<RoomScript>();
    }
    void OnTriggerExit2D(Collider2D other)
    {
        //if (room.doReset)
        //{
        //    other.transform.SetPositionAndRotation(room.origin.position, other.transform.rotation);
        //}
        
    }
}
