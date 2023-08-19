using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //private float horizontal;
    //private float vertical;
    [SerializeField] private float movementSpeed = 5f;
    //private bool isFacingRight = true;
    private Rigidbody2D rb;
    private Vector2 movementDirection;
    public Animator animator;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        float horizontal = 0;
        float vertical = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontal = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontal = -1;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            vertical = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            vertical = -1;
        }
        

        movementDirection = new Vector2(horizontal, vertical).normalized;
        
        rb.velocity = movementDirection * movementSpeed;
        if(rb.velocity.x != 0)
        {
            sr.flipX = rb.velocity.x < 0;
        }
        
        animator.SetFloat("Velocity", rb.velocity.magnitude);
    }

    private void FixedUpdate()
    {
        
    }
}
