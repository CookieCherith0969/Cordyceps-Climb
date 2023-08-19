using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float speed = 8f;
    private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        if (Input.GetKey("up"))
        {
            transform.position = transform.position + new Vector3(0, speed * Time.deltaTime, 0);
        }

        if (Input.GetKey("down"))
        {
            transform.position = transform.position + new Vector3(0, -speed * Time.deltaTime, 0);
        }

        if (Input.GetKey("right"))
        {
            transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey("left"))
        {
            transform.position = transform.position + new Vector3(-speed * Time.deltaTime, 0, 0);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
