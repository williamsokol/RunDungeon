using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    public Invertory   inventory;
    public Item        item;

    public void DropItem()
    {
        item.DropOutInventory(inventory);
        Destroy(gameObject);
    }
}
