using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTrigger : MonoBehaviour
{
    public CreatureManager cm;
    public DoorScript door;
    // Start is called before the first frame update
    void Start()
    {
        door = transform.parent.gameObject.GetComponent<DoorScript>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("trying");
        door.CloseMushroom();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
