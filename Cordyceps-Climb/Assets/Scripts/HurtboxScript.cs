using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtboxScript : MonoBehaviour
{
    public int damage;
    public int infect;
    public bool infected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.transform == transform.parent)
        {
            return;
        }
        ICreature target = (ICreature)collision.gameObject.GetComponent(typeof(ICreature));
        if (target == null)
        {
            return;
        }

        if(infected && ((MonoBehaviour)target).CompareTag("Infected"))
        {
            return;
        }
        if (!infected && ((MonoBehaviour)target).CompareTag("Enemy"))
        {
            return;
        }

        if (infect > 0)
        {
            target.Infect(infect);
        }
        if (damage > 0)
        {
            target.Damage(damage);
        }
    }
}
