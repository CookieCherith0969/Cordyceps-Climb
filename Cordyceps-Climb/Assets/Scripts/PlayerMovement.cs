using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    [SerializeField] private float movementSpeed = 5f;
    private bool isFacingRight = true;
    private Rigidbody2D rb;
    private Vector2 movementDirection;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if(Mathf.Abs(horizontal) > 0)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontal));
            if(horizontal > 0)
            {
                horizontal = 1f;
            }
            else
            {
                horizontal = -1f;
            }
        }else if(Mathf.Abs(vertical) > 0)
        {
            animator.SetFloat("Speed", Mathf.Abs(vertical));
            if (vertical > 0)
            {
                vertical = 1f;
            }
            else
            {
                vertical = -1f;
            }
        }
        else
        {
            animator.SetFloat("Speed",0f);
        }

        movementDirection = new Vector2(horizontal, vertical).normalized;

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = movementDirection * movementSpeed;
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
