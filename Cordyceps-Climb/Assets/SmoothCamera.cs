using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public float dampTime = 0.15f;
    public Transform target;
    Vector3 velocity = new Vector3(0, 0, 0);
    new Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 point = camera.WorldToViewportPoint(target.position);
        Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
        Vector3 destination = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
    }
}
