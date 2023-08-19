using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public Transform origin;
    public bool doReset = true;
    public List<Transform> transitions;
    public List<Transform> northDoors;
    public List<Transform> eastDoors;
    public List<Transform> southDoors;
    public List<Transform> westDoors;
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.Find("Origin");
        foreach (Transform child in gameObject.transform)
        {
            if(child.CompareTag("Transition"))
            {
                transitions.Add(child);
                if(child.rotation.eulerAngles == new Vector3(0, 0, 0))
                {

                }
            }
        }
    }

}
