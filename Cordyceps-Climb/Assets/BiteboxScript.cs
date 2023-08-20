using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteboxScript : MonoBehaviour
{
    public ICreature target;
    public bool bit = false;
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
        target = (ICreature)collision.gameObject.GetComponent(typeof(ICreature));
        bit = true;
        target.Lock();
    }
}
