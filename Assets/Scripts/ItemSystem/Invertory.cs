using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invertory : MonoBehaviour
{
    public Transform shieldSlot;
    public Transform weaponSlot;

    public GameObject weapon;

    public Transform crate;

    public List<GameObject> backpack;

    public static Invertory instance;

    void Awake(){
        instance = this;
        InvertoryUI Inven = GameObject.Find("InGameScreen").GetComponent<InvertoryUI>();
        Inven.GetInventory();
    }
}
