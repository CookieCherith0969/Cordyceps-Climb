using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public Transform origin;
    public bool doReset = true;
    public List<Transform> transitions;
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.Find("Origin");
        foreach (Transform child in gameObject.transform)
        {
            if(child.tag == "Transition")
            {
                transitions.Add(child);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
