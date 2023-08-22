using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteboxScript : MonoBehaviour
{
    public List<ICreature> targets;
    private void Awake()
    {
        targets = new List<ICreature>();

    }
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
        if (collision.gameObject.transform == transform.parent || collision.gameObject.CompareTag("Infected"))
        {
            return;
        }
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            return;
        }

        ICreature creature = (ICreature)collision.gameObject.GetComponent(typeof(ICreature));
        if (creature == null) return;

        targets.Add(creature);
        creature.Lock();
    }
}
