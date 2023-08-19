using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScript : MonoBehaviour
{
    RoomScript parent;
    public RoomScript target;

    public Vector3 markerParent;
    public Vector3 markerTarget;
    // Start is called before the first frame update
    void Start()
    {
        markerParent = transform.Find("MarkerParent").transform.position;
        markerTarget = transform.Find("MarkerTarget").transform.position;
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
        
        Vector3 otherPosition = other.transform.position;
        if (Vector3.Distance(otherPosition, markerParent) > Vector3.Distance(otherPosition, markerTarget))
        {
            target.doReset = false;
            other.transform.SetPositionAndRotation(markerParent, other.transform.rotation);
            yield return new WaitForSeconds(time);
            target.doReset = true;
        }
        else
        {
            parent.doReset = false;
            other.transform.SetPositionAndRotation(markerTarget, other.transform.rotation);
            yield return new WaitForSeconds(time);
            parent.doReset = true;
        }
        
    }
}
