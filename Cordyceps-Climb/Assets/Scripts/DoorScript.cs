using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField]
    private bool isClosed = false;
    Rigidbody2D rb;
    Animator animator;
    RoomScript oldRoom;
    Transform trigger;
    Direction exitDirection = Direction.Right;
    bool loadedRoom = false;
    RoomManager rm;
    // Start is called before the first frame update
    enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }
    public PrefabListObject roomList;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rm = RoomManager.activeManager;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void Close()
    {
        if (isClosed)
        {
            return;
        }

        isClosed = true;
        rb.simulated = true;
        animator.SetBool("isClosed", true);
    }
    public void CloseMushroom()
    {
        if (isClosed)
        {
            return;
        }

        isClosed = true;
        rb.simulated = true;
        animator.SetBool("isMushroom", true);
        rm.loadNextRoom();
    }
    public void Open()
    {
        if (!isClosed)
        {
            return;
        }

        isClosed = false;
        rb.simulated = false;
        animator.SetBool("isClosed", false);
        
    }
    /*
    void LoadNewRoom()
    {
        if (!loadedRoom) { 
            GameObject newRoom = transform.parent.gameObject;
            RoomScript newScript = newRoom.GetComponent<RoomScript>();
            Transform newDoor = newScript.eastDoors[0];
            
            switch (exitDirection)
            {
                case Direction.Up:
                    newRoom = Instantiate(roomList.upEntrance[Random.Range(0, roomList.upEntrance.Length)]);
                    newRoom.transform.parent = transform.parent.parent;
                    newScript = newRoom.GetComponent<RoomScript>();
                    newDoor = newScript.southDoors[Random.Range(0, newScript.southDoors.Count)];
                    break;
                case Direction.Right:
                    newRoom = Instantiate(roomList.rightEntrance[Random.Range(0, roomList.rightEntrance.Length)]);
                    newRoom.transform.parent = transform.parent.parent;
                    newScript = newRoom.GetComponent<RoomScript>();
                    newDoor = newScript.westDoors[Random.Range(0, newScript.westDoors.Count)];
                    break;
                case Direction.Down:
                    newRoom = Instantiate(roomList.downEntrance[Random.Range(0, roomList.downEntrance.Length)]);
                    newRoom.transform.parent = transform.parent.parent;
                    newScript = newRoom.GetComponent<RoomScript>();
                    newDoor = newScript.northDoors[Random.Range(0, newScript.northDoors.Count)];
                    break;
                case Direction.Left:
                    newRoom = Instantiate(roomList.leftEntrance[Random.Range(0, roomList.leftEntrance.Length)]);
                    newRoom.transform.parent = transform.parent.parent;
                    newScript = newRoom.GetComponent<RoomScript>();
                    newDoor = newScript.eastDoors[Random.Range(0, newScript.eastDoors.Count)];
                    break;
                    //default:
                    //    
                    //    break;
            }
            Vector3 delta = trigger.position - newDoor.position;
            newRoom.transform.position += delta;
            loadedRoom = true;
            
        }
    }
    */
}
