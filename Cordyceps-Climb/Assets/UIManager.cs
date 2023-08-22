using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager activeManager;

    public Text clearedText;
    void Awake()
    {
        activeManager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateClears(int clears)
    {
        clearedText.text = "Cleared Rooms: " + clears;
    }
}
