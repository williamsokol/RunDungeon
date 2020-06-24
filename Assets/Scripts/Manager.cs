using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public int spawnRate = 6;
    public int spawnAmonut;
    public Transform enemy;

    public LevelBuilder levelBuilder;
    // Start is called before the first frame update
    void Start()
    {
       
        levelBuilder = GameObject.Find("LevelBuilder").GetComponent<LevelBuilder>();
        InvokeRepeating("SpawnEnemies",8,spawnRate);
    }

    void SpawnEnemies()
    {
        int r = Random.Range(0,levelBuilder.SpawnSpots.Count);
        
        Instantiate(enemy,levelBuilder.SpawnSpots[r].transform.position,Quaternion.identity);
    }
}
