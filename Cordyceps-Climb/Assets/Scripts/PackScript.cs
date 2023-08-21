using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for(int i = transform.childCount-1; i>=0; i--)
        {
            Transform child = transform.GetChild(i);
            child.parent = transform.parent;
            child.gameObject.SetActive(true);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
