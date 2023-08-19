using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public Transform origin;
    public bool doReset = true;
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
            if(child.CompareTag("Door"))
            {
                float angle = child.rotation.eulerAngles.z;
                if (angle < 45 && angle > -45)
                {
                    eastDoors.Add(child);
                }
                else if (angle > 135 || angle < -135)
                {
                    westDoors.Add(child);
                }
                else if (angle > 45)
                {
                    northDoors.Add(child);
                }
                else
                {
                    southDoors.Add(child);
                }
            }
        }
    }

}
