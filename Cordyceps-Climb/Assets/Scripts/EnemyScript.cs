using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour, ICreature
{
    public int maxHealth = 100;
    public int health = 100;
    public float speed = 4;
    public float maxSpeed = 5;
    Animator animator;
    public Transform target;
    ICreature targetScript;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    GameObject hurtbox;
    BoxCollider2D hurtboxCollider;
    public bool infected = false;
    CreatureManager cm;
    public bool locked = false;
    Transform healthBar;
    HurtboxScript hurtboxScript;
    int infectionCounter = 0;
    bool aggressive = true;
    public float maxRange;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hurtbox = transform.Find("Hurtbox").gameObject;
        hurtboxCollider = hurtbox.GetComponent<BoxCollider2D>();
        hurtboxScript = hurtbox.GetComponent<HurtboxScript>();
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
            if (!infected)
            {
                GetTargetFromList(cm.infected);
            }
            else
            {
                GetTargetFromList(cm.enemies);
            }
            
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
        
        if (locked || target == null || animator.GetBool("Attacking"))
        {
            rb.velocity = Vector2.zero;
            animator.SetFloat("Velocity", 0);
            return;
        }
        float xDist = hurtbox.transform.position.x - target.position.x;
        float yDist = hurtbox.transform.position.y - target.position.y;
        if (targetScript.IsInfected() != infected && Math.Abs(xDist) < hurtboxCollider.size.x/2 && Math.Abs(yDist) < hurtboxCollider.size.y / 2)
        {
            rb.velocity = Vector2.zero;
            StartCoroutine(Attack(0.1f, 0.3f));
            return;
        }

        Vector2 delta = target.position - transform.position;
        delta.Normalize();
        rb.velocity -= 0.2f*rb.velocity*Time.deltaTime;
        rb.velocity += delta * speed * Time.deltaTime;

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        animator.SetFloat("Velocity", rb.velocity.magnitude);

        float newX = Math.Abs(transform.localScale.x) * rb.velocity.x < 0 ? -1 : 1;
        transform.localScale = new Vector3(newX, transform.localScale.y, transform.localScale.z);
    }

    void OnDestroy()
    {
        cm.Deregister(gameObject);
    }

    private IEnumerator Attack(float delay, float duration)
    {
        animator.SetBool("Attacking", true);
        yield return new WaitForSeconds(delay);
        if (locked)
        {
            animator.SetBool("Attacking", false);
            yield break;
        }
        hurtbox.SetActive(true);
        yield return new WaitForSeconds(duration);
        hurtbox.SetActive(false);
        animator.SetBool("Attacking", false);
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
        healthBar.localScale = new Vector3(2 * ((float)health / (float)maxHealth), healthBar.localScale.y, healthBar.localScale.z);
        if (health < 1)
        {
            animator.SetBool("Dead", true);
            return true;
        }
        return false;
    }
    public bool Infect(int amount)
    {
        if (infected)
        {
            return false;
        }
        health -= amount;
        infectionCounter += amount;
        if (health < 0)
        {
            infectionCounter += health;
            health = 0;
        }
        healthBar.localScale = new Vector3(2 * ((float)health / (float)maxHealth), healthBar.localScale.y, healthBar.localScale.z);
        if (health < 1)
        {
            animator.SetBool("Infected", true);
            infected = true;
            hurtboxScript.infected = true;
            gameObject.tag = "Infected";

            cm.Register(gameObject);

            SetHealth(infectionCounter);
            infectionCounter = 0;
            healthBar.GetComponent<SpriteRenderer>().color = Color.red;

            target = null;
            GetTargetFromList(cm.enemies);

            return true;
            
        }
        return false;
    }
    public void SetHealth(int amount)
    {
        health = amount;
        if (health > maxHealth) health = maxHealth;
        if (health < 0) health = 0;
        healthBar.localScale = new Vector3(2 * ((float)health / (float)maxHealth), healthBar.localScale.y, healthBar.localScale.z);

    }
    public void ResetInfection()
    {
        infectionCounter = 0;
    }
    public bool IsInfected()
    {
        return infected;
    }
}
