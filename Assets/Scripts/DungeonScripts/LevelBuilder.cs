using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelBuilder : MonoBehaviour
{
    public Room startRoomPrefab,endRoomPrefab;
    public Player playerPrefab;
    public List<Room> roomPrefabs = new List<Room>();
    public Vector2 iterationRange = new Vector2(3,10);

    public List<Item> itemsPrefabs = new List<Item>();
    public List<Transform> SpawnSpots = new List<Transform>();
    

    List<DoorWay> availableDoorways = new List<DoorWay>();

    public StartRoom startRoom;
    public EndRoom endRoom; 
    
    public NavMeshSurface EnemyPathing;

    List<Room> placedRooms = new List<Room>();

    LayerMask roomLayerMask;
    int floorCount = 0;

    Player player;
    GameObject manager;

    void Start()
    {
        EnemyPathing = GetComponent<NavMeshSurface>();
        roomLayerMask = LayerMask.GetMask("Room");
        StartCoroutine("GenerateLevel");
        manager = GameObject.Find("GameManager");
        player = Player.instance; 
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

        // place item
        if(itemsPrefabs.Count >= 1)
        {
            Transform itemSpot = SpawnSpots[Random.Range(0,SpawnSpots.Count)].transform;
            //Instantiate(itemsPrefabs[Random.Range(0,itemsPrefabs.Count) ],itemSpot.position, itemSpot.rotation,manager.transform);
        }
        yield return interval;

        // Build navMesh
        EnemyPathing.BuildNavMesh();
        yield return interval;

        print("level generation done");

        //place player
        LoadPlayer();
        yield return interval;
        player.gameObject.GetComponent<CharacterController>().enabled = true;

        //yield return new WaitForSeconds(3);
        //ResetLevelGenerator();
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

        // print("place start room");
    }
    void AddDoorwaysToList(Room room, ref List<DoorWay> List)
    {
        foreach (DoorWay doorWay in room.doorWays)
        {
            int r = Random.Range(0,List.Count);
            List.Insert(r, doorWay);
        }
    }
    void AddSpawnSpotsToList(Room room)
    {
        //Item floorItem = Instantiate(itemsPrefabs[Random.Range(0,itemsPrefabs.Count)]) as Item;
        foreach(Transform spot in room.spawnPlaces)
        {
            SpawnSpots.Add(spot);
        }
    }
    void PlaceRooms()
    {
        //instantiate room
    
        Room currentRoom = Instantiate(roomPrefabs[Random.Range(0,roomPrefabs.Count)]) as Room;
        currentRoom.transform.parent = this.transform;

        //create doorway list to loop over
        List<DoorWay> AllAvailableDoorways = new List<DoorWay> (availableDoorways);
        List<DoorWay> CurrentRoomDoorways = new List<DoorWay>();
        AddDoorwaysToList(currentRoom, ref CurrentRoomDoorways);

        AddSpawnSpotsToList(currentRoom);

        // add new doorways to list of available doorways
        AddDoorwaysToList(currentRoom, ref availableDoorways);

        bool roomPlaced = false;

        //try all available doorways
        foreach (DoorWay availableDoorway in AllAvailableDoorways)
        {
            //try all available doorways  in the current room
            foreach(DoorWay currentDoorway in CurrentRoomDoorways)
            {
                //position room
                PositionRoomAtDoorway(ref currentRoom, currentDoorway, availableDoorway);

                //check room overlaps
                if (CheckRoomOverlap(currentRoom)){
                    continue;
                }

                roomPlaced = true;

                //add room to list
                placedRooms.Add(currentRoom);

                //remove occupied doorways
                currentDoorway.gameObject.SetActive(false);
                availableDoorways.Remove(currentDoorway);

                availableDoorway.gameObject.SetActive(false);
                availableDoorways.Remove(availableDoorway);

                // exit loop if room is placed
                break;
            }
            // see if the room is placed
            if(roomPlaced){
                break;
            }
        }
        if (!roomPlaced)
        {
            Destroy(currentRoom.gameObject);
            ResetLevelGenerator();
        }
        //print("placed a random room from list");
    }
    
    void PositionRoomAtDoorway (ref Room room, DoorWay roomDoorway, DoorWay targetDoorway)
    {
        // zero out room's position and rotation
        room.transform.position = Vector3.zero;
        room.transform.rotation = Quaternion.identity;

        // rotate room to match previous doorway orientation
        Vector3 targetDoorwayEuler = targetDoorway.transform.eulerAngles;
        Vector3 roomDoorwayEuler = roomDoorway.transform.eulerAngles;
        float deltaAngle = Mathf.DeltaAngle(roomDoorwayEuler.y,targetDoorwayEuler.y);
        Quaternion CurrentRoomTargetRotation = Quaternion.AngleAxis(deltaAngle, Vector3.up);
        room.transform.rotation = CurrentRoomTargetRotation * Quaternion.Euler(0,180f,0);

        // postion room
        Vector3 roomPositionOffset = roomDoorway.transform.position - room.transform.position;
        room.transform.position = targetDoorway.transform.position - roomPositionOffset;
    }

    bool CheckRoomOverlap(Room room)
    {
        Bounds bounds = room.RoomBounds;
        bounds.center = room.transform.position; 
        bounds.Expand(-0.1f);

        Collider[] colliders = Physics.OverlapBox(bounds.center,bounds.size/2, room.transform.rotation,roomLayerMask);
        //ignore collision with the current room
        if (colliders.Length > 0) 
        {
            foreach (Collider c in colliders)
            {
                if(c.transform.parent.gameObject.Equals(room.gameObject))
                {
                    print("all good");
                    continue;
                }else {
                    //print("overlap detected");
                    return true;
                }
            }
        }
        return false;
    }

    void PlaceEndRoom()
    {
        endRoom = Instantiate(endRoomPrefab) as EndRoom;
        endRoom.transform.parent = this.transform;

        //create doorway list to loop over
        List<DoorWay> AllAvailableDoorways = new List<DoorWay> (availableDoorways);
        DoorWay doorway = endRoom.doorWays [0];

        bool roomPlaced = false;

        //try all available doorways
        foreach (DoorWay availableDoorway in AllAvailableDoorways) {

            //position room
            Room room = (Room)endRoom;
            PositionRoomAtDoorway(ref room, doorway, availableDoorway);
            //check room overlaps
            if (CheckRoomOverlap(endRoom)){
                continue;
            }

            roomPlaced = true;
        
            //remove occupied doorways
            doorway.gameObject.SetActive(false);
            availableDoorways.Remove(doorway);

            availableDoorway.gameObject.SetActive(false);
            availableDoorways.Remove(availableDoorway);

            // exit loop if room is placed
            break;
        }

        if (!roomPlaced)
        {
            ResetLevelGenerator();
        }

    }
    public void ResetLevelGenerator()
    {
        //print("reset level builder");

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
        foreach(Transform child in manager.transform)
        {
            //wipe all enemyies and items
            Destroy(child.gameObject);
        }
        //clear lists
        placedRooms.Clear();
        availableDoorways.Clear();
        SpawnSpots.Clear();


        //reset Coroutine
        StartCoroutine("GenerateLevel");
    }
    void LoadPlayer()
    {
        if(floorCount == 0){
            player = Instantiate(playerPrefab) as Player;
            player.transform.position = startRoom.playerStart.position;
            player.transform.rotation = startRoom.playerStart.rotation;
        }else
        {
            //stops the player from gettting draged down in the void
            player = Player.instance;
            player.gameObject.GetComponent<CharacterController>().enabled = false;
            player.gameObject.transform.position = startRoom.playerStart.position;
        }
        floorCount++;
        Player.TorchHp++;
    }

    

}
