using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomizer : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] helmets;
    public GameObject[] armors;
    public int[] randomStats = new int[4];
    void Awake()
    {
        int helmRan = Random.Range(0,helmets.Length);
        helmets[helmRan].SetActive(true);

        int armRan = Random.Range(0,armors.Length);
        armors[armRan].SetActive(true);

        for(int i = 0;i>randomStats.Length;i++)
        {
            randomStats[i] =  Random.Range(5,40);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
