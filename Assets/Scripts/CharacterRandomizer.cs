using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRandomizer : MonoBehaviour
{
    public GameObject[] armors;
    public GameObject[] helmets;

    public void Awake()
    {
        ChangeCloth();
    }

    public void ChangeCloth()
    {
        foreach (GameObject armor in armors)
        {
            armor.SetActive(false);
        }
        foreach (GameObject helmet in helmets)
        {
            helmet.SetActive(false);
        }

        int armorRand = Random.Range(0, armors.Length);
        int helmetRand = Random.Range(0, helmets.Length);

        armors[armorRand].SetActive(true);
        helmets[helmetRand].SetActive(true);
    }
}
