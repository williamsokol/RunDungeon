using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public DoorWay[] doorWays;
    public Transform[] spawnPlaces;
    public MeshCollider meshCollider;

    public  Bounds RoomBounds
    {
        get {return meshCollider.bounds;}
    }
     
}
