using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public Room startRoomPrefab,endRoomPrefab;
    public List<Room> roomPrefabs = new List<Room>();
    public Vector2 iterationRange = new Vector2(3,10);

    List<DoorWay> availableDoorways = new List<DoorWay>();

    public StartRoom startRoom;
    public EndRoom endRoom; 

    List<Room> placedRooms = new List<Room>();

    LayerMask roomLayerMask;

    void Start()
    {
        roomLayerMask = LayerMask.GetMask("Room");
        StartCoroutine("GenerateLevel");   
    }

    IEnumerator GenerateLevel()
    {
        WaitForSeconds startup = new WaitForSeconds(1);
        WaitForFixedUpdate interval = new WaitForFixedUpdate();

        yield return startup;

        // places the start room
        PlaceStartRoom();
        yield return interval;

        // 
        int iterations = Random.Range((int)iterationRange.x,(int)iterationRange.y);

        for(int i =0;i < iterations;i++)
        {
            // place random middle rooms
            PlaceRooms();
            yield return interval;
        }         


        //places the end room
        PlaceEndRoom();
        yield return interval;

        print("level generation done");

        yield return new WaitForSeconds(3);
        ResetLevelGenerator();
    }

    void PlaceStartRoom()
    {
        //instanciate start room
        startRoom = Instantiate(startRoomPrefab) as StartRoom;
        startRoom.transform.parent = this.transform;

        //add room to the doorways list
        AddDoorwaysToList(startRoom, ref availableDoorways);

        //position Room
        startRoom.transform.position =  Vector3.zero;
        startRoom.transform.rotation = Quaternion.identity;

        print("place start room");
    }
    void AddDoorwaysToList(Room room, ref List<DoorWay> List)
    {
        foreach (DoorWay doorWay in room.doorWays)
        {
            int r = Random.Range(0,List.Count);
            List.Insert(r, doorWay);
        }
    }
    void PlaceRooms()
    {
        print("placed a random room from list");
    }
    void PlaceEndRoom()
    {
        print("place endroom");
    }
    void ResetLevelGenerator()
    {
        print("reset level builder");

        StopCoroutine("GenerateLevel");

        //Delete all rooms
        if(startRoom)
        {
            Destroy(startRoom.gameObject);
        }
        if(endRoom){
            Destroy(endRoom.gameObject);
        }

        foreach(Room room in placedRooms)
        {
            Destroy (room.gameObject);
        }
        //clear lists
        placedRooms.Clear();
        availableDoorways.Clear();

        //reset Coroutine
        StartCoroutine("GenerateLevel");
    }

    

}
