using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    RoomScript parent;
    //[SerializeField]
    //Vector3 origin = new Vector3(0, 0, 0);
    private void Start()
    {
        parent = gameObject.transform.parent.GetComponent<RoomScript>();
    }
    void OnTriggerExit2D(Collider2D other)
    {
        // Destroy everything that leaves the trigger
        other.transform.SetPositionAndRotation(parent.origin, other.transform.rotation);
    }
}
