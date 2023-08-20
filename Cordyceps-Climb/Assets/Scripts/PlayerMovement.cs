using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, ICreature
{
    //private float horizontal;
    //private float vertical;
    [SerializeField] private float movementSpeed = 5f;
    //private bool isFacingRight = true;
    private Rigidbody2D rb;
    private Vector2 movementDirection;
    public Animator animator;
    SpriteRenderer sr;
    public int health = 100;
    public int damage = 5;
    public bool dead = false;
    public bool feeding = false;
    public bool infecting = false;
    public bool locked = false;
    GameObject bitebox;
    BiteboxScript boxScript;
    CreatureManager cm;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bitebox = transform.Find("Hurtbox").gameObject;
        boxScript = bitebox.GetComponent<BiteboxScript>();
        cm = transform.parent.GetComponent<CreatureManager>();
        cm.RegisterNew(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(!locked)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                infecting = true;
                StartCoroutine(EnableBitebox(0.2f,1f));
                rb.velocity = Vector2.zero;
                animator.SetFloat("Velocity", 0);
                return;
            }
            else if (Input.GetKey(KeyCode.X))
            {
                feeding = true;
                StartCoroutine(EnableBitebox(0.2f, 1f));
                rb.velocity = Vector2.zero;
                animator.SetFloat("Velocity", 0);
                return;
            }
            else if (Input.GetKey(KeyCode.C))
            {

            }
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
            if (rb.velocity.x != 0)
            {
                transform.localScale = new Vector3(Math.Abs(transform.localScale.x) * rb.velocity.x < 0 ? -1 : 1, transform.localScale.y, transform.localScale.z);

            }

            animator.SetFloat("Velocity", rb.velocity.magnitude);
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetFloat("Velocity", 0);
        }
        
    }
    public void Lock()
    {
        locked = true;
    }
    public void Free()
    {
        locked = false;
    }
    public int GetHealth()
    {
        return health;
    }
    private IEnumerator EnableBitebox(float delay, float time)
    {
        bitebox.SetActive(true);
        animator.SetBool("Attacking", true);
        ICreature target = this;
        yield return new WaitForSeconds(delay);
        if (boxScript.bit)
        {
            animator.speed = 0;
            target = boxScript.target;
            int baseHealth = target.GetHealth();
            while(target.GetHealth() > 0)
            {
                if (!bitebox.activeSelf)
                {
                    target.SetHealth(baseHealth);
                    break;
                }
                if (feeding)
                {
                    if (target.Damage(damage))
                    {
                        health += baseHealth;
                        break;
                    }
                }
                else
                {
                    if (target.Infect(damage))
                    {
                        target.SetHealth(baseHealth);
                        break;
                    }
                }
                yield return new WaitForSeconds(time);
            }
            
        }
        animator.speed = 1;
        feeding = false;
        infecting = false;
        boxScript.bit = false;
        target.Free();
        bitebox.SetActive(false);

    }
    public bool Damage(int amount)
    {
        if (animator.GetBool("Hurt"))
        {
            return false;
        }
        health -= amount;
        if (health < 1)
        {
            animator.SetBool("Dead", true);
            dead = true;
            cm.Deregister(gameObject);
            return true;
        }
        animator.SetBool("Hurt", true);
        return false;
    }
    public bool Infect(int amount)
    {
        return false;
    }
    public void SetHealth(int amount)
    {
        health = amount;
    }
}
