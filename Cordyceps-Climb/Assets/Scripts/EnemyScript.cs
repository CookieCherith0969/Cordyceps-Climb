using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public float health = 100;
    Animator animator;
    public Transform goal;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        animator.SetFloat("Velocity", rb.velocity.magnitude);
        spriteRenderer.flipX = rb.velocity.x < 0;
        
    }

    public void Damage(float amount)
    {
        health -= amount;
        animator.SetFloat("Health", health);

    }
}
