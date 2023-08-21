using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    Transform healthBar;
    public int health = 100;
    public int maxHealth = 100;
    public int damage = 10;
    public bool dead = false;
    public bool feeding = false;
    public bool infecting = false;
    public bool locked = false;
    GameObject bitebox;
    BiteboxScript boxScript;
    CreatureManager cm;
    public GameObject sporePack;
    List<GameObject> activeSpores;
    public int maxSpores = 8;
    
    void Awake()
    {
        activeSpores = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bitebox = transform.Find("Hurtbox").gameObject;
        boxScript = bitebox.GetComponent<BiteboxScript>();
        cm = CreatureManager.activeManager;
        cm.RegisterNew(gameObject);
        healthBar = transform.Find("HealthBar");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (locked)
        {
            rb.velocity = Vector2.zero;
            animator.SetFloat("Velocity", 0);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            infecting = true;
            StartCoroutine(Attack(0.610f));
            rb.velocity = Vector2.zero;
            animator.SetFloat("Velocity", 0);
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            feeding = true;
            StartCoroutine(Attack(0.610f));
            rb.velocity = Vector2.zero;
            animator.SetFloat("Velocity", 0);
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            SpawnSpores();
            
            //Spawn lil mushy guys
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
            float newX = Math.Abs(transform.localScale.x) * rb.velocity.x < 0 ? -1 : 1;
            transform.localScale = new Vector3(newX, transform.localScale.y, transform.localScale.z);
        }

        animator.SetFloat("Velocity", rb.velocity.magnitude);
    }

    void OnDestroy()
    {
        cm.Deregister(gameObject);
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
    private IEnumerator Attack(float tickDelay)
    {
        Lock();
        bitebox.SetActive(true);
        animator.SetBool("Attacking", true);
        yield return new WaitForSeconds(0.02f);
        List<ICreature> targets = new(boxScript.targets);
        boxScript.targets.Clear();
        bitebox.SetActive(false);

        if (targets.Count > 0)
        {
            
            List<int> baseHealth = targets.Select(x => x.GetHealth()).ToList();

            while (targets.Count > 0)
            {
                if (animator.GetBool("Hurt"))
                {
                    for (int i = targets.Count - 1; i >= 0; i--) {
                        targets[i].SetHealth(baseHealth[i]);
                        targets[i].ResetInfection();
                        targets[i].Unlock();
                    }
                    break;
                }

                if (feeding)
                {
                    FeedingTick(targets, baseHealth);
                }
                else
                {
                    InfectingTick(targets, baseHealth);
                }
                yield return new WaitForSeconds(tickDelay);
            }
        }
        animator.SetBool("Attacking", false);
        feeding = false;
        infecting = false;
        Unlock();
    }

    void SpawnSpores()
    {
        if (animator.GetBool("Hurt"))
        {
            return;
        }
        Damage(10);
        GameObject newPack = Instantiate(sporePack, transform.position, transform.rotation);
        newPack.transform.parent = transform.parent;
        foreach(Transform spore in newPack.transform)
        {
            activeSpores.Add(spore.gameObject);
        }
        for(int i = activeSpores.Count-1; i >= 0; i--)
        {
            if(activeSpores[i] == null)
            {
                activeSpores.RemoveAt(i);
            }
        }
        while(activeSpores.Count > maxSpores)
        {
            GameObject toRemove = activeSpores[0];
            activeSpores.RemoveAt(0);
            Destroy(toRemove);
        }
        
    }

    private void InfectingTick(List<ICreature> targets, List<int> baseHealth)
    {
        for (int i = targets.Count - 1; i >= 0; i--)
        {
            if ((MonoBehaviour)targets[i] == null)
            {
                targets.RemoveAt(i);
                continue;
            }
            if (targets[i].Infect(damage))
            {
                targets[i].Unlock();
                targets.RemoveAt(i);
                continue;
            }
        }
    }

    private void FeedingTick(List<ICreature> targets, List<int> baseHealth)
    {
        for (int i = targets.Count - 1; i >= 0; i--)
        {
            if ((MonoBehaviour)targets[i] == null)
            {
                targets.RemoveAt(i);
                continue;
            }
            if (targets[i].Damage(damage))
            {
                SetHealth(health + baseHealth[i]);
                
                targets.RemoveAt(i);
            }
        }
    }

    public bool Damage(int amount)
    {
        if (animator.GetBool("Hurt"))
        {
            return false;
        }
        health -= amount;
        if (health < 0) health = 0;
        healthBar.localScale = new Vector3(2 * ((float)health / (float)maxHealth), healthBar.localScale.y, healthBar.localScale.z);
        if (health < 1)
        {
            animator.SetBool("Dead", true);
            dead = true;
            Lock();
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
        if (health < 0) health = 0;
        if (health > maxHealth) health = maxHealth;
        healthBar.localScale = new Vector3(2 * ((float)health / (float)maxHealth), healthBar.localScale.y, healthBar.localScale.z);
    }
    public void ResetInfection()
    {

    }
    public bool IsInfected()
    {
        return true;
    }
}
