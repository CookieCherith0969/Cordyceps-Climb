using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public float dampTime = 0.15f;
    public Transform target;
    Vector2 velocity = new Vector3(0, 0, 0);
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
        Vector2 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
        Vector2 destination = (Vector2)transform.position + delta;
        transform.position = Vector2.SmoothDamp(transform.position, destination, ref velocity, dampTime);
    }
}
