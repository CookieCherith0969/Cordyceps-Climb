using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SporeScript : MonoBehaviour, ICreature
{
    public int maxHealth = 10;
    public int health = 10;
    public float speed = 4;
    public float maxSpeed = 3;
    Animator animator;
    public Transform target;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    GameObject hurtbox;
    public bool infected = true;
    CreatureManager cm;
    public bool locked = false;
    Transform healthBar;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hurtbox = transform.Find("Hurtbox").gameObject;
        cm = transform.parent.GetComponent<CreatureManager>();
        cm.RegisterNew(gameObject);
        StartCoroutine(SetTarget());
        healthBar = transform.Find("HealthBar");
    }
    private IEnumerator SetTarget()
    {
        float closeDistance;
        float distance;
        while (true)
        {
            if (cm.enemies.Count > 0)
            {
                target = cm.enemies[0].transform;
                closeDistance = Vector2.Distance(transform.position, target.position);

                foreach (GameObject enemy in cm.enemies)
                {
                    distance = Vector2.Distance(transform.position, enemy.transform.position);
                    if (distance < closeDistance)
                    {
                        target = enemy.transform;
                        closeDistance = distance;

                    }
                }
            }
            else
            {
                target = null;
            }
            yield return new WaitForSeconds(1);
        }


    }
    // Update is called once per frame
    void Update()
    {
        if (!locked)
        {
            if (target == null)
            {
                rb.velocity = Vector2.zero;
                animator.SetFloat("Velocity", 0);
                return;
            }
            Vector2 delta = target.position - transform.position;
            delta.Normalize();
            rb.velocity += delta * speed * Time.deltaTime;
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
            transform.localScale = new Vector3(Math.Abs(transform.localScale.x) * rb.velocity.x < 0 ? -1 : 1, transform.localScale.y, transform.localScale.z);
            if (!animator.GetBool("Attacking") && Vector2.Distance(hurtbox.transform.position, target.position) < 1)
            {
                rb.velocity = Vector2.zero;
                animator.SetBool("Attacking", true);
                cm.Deregister(gameObject);
                StartCoroutine(EnableHurtbox(0.2f));
            }
            else if (animator.GetBool("Attacking"))
            {
                rb.velocity = Vector2.zero;
            }
            animator.SetFloat("Velocity", rb.velocity.magnitude);
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetFloat("Velocity", 0);
        }

    }
    private IEnumerator EnableHurtbox(float delay)
    {

        yield return new WaitForSeconds(delay);
        hurtbox.SetActive(true);

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

    public bool Damage(int amount)
    {
        health -= amount;
        healthBar.localScale = new Vector3(1 * ((float)health / (float)maxHealth), healthBar.localScale.y, healthBar.localScale.z);
        if (health < 1)
        {
            animator.SetBool("Dead", true);
            cm.Deregister(gameObject);
            return true;
        }
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
