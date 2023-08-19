using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PrefabListObject", order = 1)]
public class PrefabListObject : ScriptableObject
{
    public GameObject[] allRooms;
    public GameObject[] upEntrance;
    public GameObject[] rightEntrance;
    public GameObject[] downEntrance;
    public GameObject[] leftEntrance;
}
