using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScript : MonoBehaviour
{
    RoomScript parent;
    Transform marker;
    // Start is called before the first frame update
    void Start()
    {
        marker = transform.Find("Marker").transform;
        parent = gameObject.transform.parent.GetComponent<RoomScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(ChangeRoom(0.1f, other));
    }
    private IEnumerator ChangeRoom(float time, Collider2D other)
    {
        
        parent.doReset = false;
        other.transform.SetPositionAndRotation(marker.position, other.transform.rotation);
        yield return new WaitForSeconds(time);
        parent.doReset = true;
        
    }
}
