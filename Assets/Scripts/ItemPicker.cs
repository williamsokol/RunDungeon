using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    public Invertory inventory;

    public void OnTriggerStay(Collider other)
    {
        if (other.GetComponentInParent<Item>())
        {
            print("itemNearby");
        }
    }
}
