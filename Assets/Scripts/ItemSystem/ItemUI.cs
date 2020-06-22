using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    public Invertory   inventory;
    public Item        item;
    public GameObject  flag;

    public void DropItem()
    {
        item.DropOutInventory(inventory);
        Destroy(gameObject);
    }
    public void UseItem()
    {
        flag.SetActive(true);
        item.UseThisItem(inventory);
    }
}
