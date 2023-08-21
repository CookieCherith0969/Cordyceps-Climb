using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager activeManager;
    public List<GameObject> activeRooms;
    public List<GameObject> rooms;
    float offset = 0;
    // Start is called before the first frame update
    void Awake()
    {
        activeManager = this;
    }
    void Start()
    {
        
    }
    public void loadNextRoom()
    {
        GameObject oldRoom = activeRooms[0];
        activeRooms.RemoveAt(0);
        Destroy(oldRoom);
        Vector3 offsetVector = new Vector3(0, offset, 0);
        GameObject nextRoom = Instantiate(rooms[Random.Range(0, rooms.Count)], transform.position+offsetVector, transform.rotation);
        nextRoom.transform.parent = transform;
        offset += nextRoom.GetComponent<RoomScript>().height;
        activeRooms.Add(nextRoom);
        RoomScript currentRoom = activeRooms[1].GetComponent<RoomScript>();
        currentRoom.ReleaseEnemies();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
