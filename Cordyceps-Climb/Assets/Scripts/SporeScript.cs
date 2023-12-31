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
    ICreature targetScript;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    GameObject hurtbox;
    public bool infected = true;
    CreatureManager cm;
    public bool locked = false;
    Transform healthBar;
    bool aggressive = true;
    public float maxRange;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hurtbox = transform.Find("Hurtbox").gameObject;
        cm = CreatureManager.activeManager;
        cm.RegisterNew(gameObject);
        StartCoroutine(SetTarget());
        healthBar = transform.Find("HealthBar");
    }
    private IEnumerator SetTarget()
    {
        while (true)
        {
            target = null;
            GetTargetFromList(cm.enemies);

            if (target == null)
            {
                target = cm.player.transform;
            }

            targetScript = (ICreature)target.gameObject.GetComponent(typeof(ICreature));
            yield return new WaitForSeconds(1);
        }
    }

    private void GetTargetFromList(List<GameObject> possibleTargets)
    {
        float closeDistance = maxRange;
        foreach (GameObject possibleTarget in possibleTargets)
        {
            float distance = Vector2.Distance(transform.position, possibleTarget.transform.position);
            if (distance < closeDistance)
            {
                target = possibleTarget.transform;
                closeDistance = distance;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (target == null || locked)
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
        if (!targetScript.IsInfected() && !animator.GetBool("Attacking") && Vector2.Distance(hurtbox.transform.position, target.position) < 1)
        {
            rb.velocity = Vector2.zero;
            
            //cm.Deregister(gameObject);
            StartCoroutine(Attack(0.1f));
        }
        else if (animator.GetBool("Attacking"))
        {
            rb.velocity = Vector2.zero;
        }
        animator.SetFloat("Velocity", rb.velocity.magnitude);

    }

    void OnDestroy()
    {
        cm.Deregister(gameObject);
    }

    private IEnumerator Attack(float delay)
    {
        animator.SetBool("Attacking", true);
        yield return new WaitForSeconds(delay);
        hurtbox.SetActive(true);
        //
    }
    public void Lock()
    {
        locked = true;
    }
    public void Unlock()
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
        if (health < 0) health = 0;
        healthBar.localScale = new Vector3(1 * ((float)health / (float)maxHealth), healthBar.localScale.y, healthBar.localScale.z);
        if (health < 1)
        {
            animator.SetBool("Dead", true);
            //cm.Deregister(gameObject);
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
        if (health < 0) health = 0;
        if (health > maxHealth) health = maxHealth;
    }
    public void ResetInfection()
    {

    }
    public bool IsInfected()
    {
        return infected;
    }
}
