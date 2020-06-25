using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Invertory   inventory;
    public Item        item;
    public GameObject  flag;

    public Text _name;
    public Text _attack;
    public Text _defend;


    public void DropItem()
    {
        item.DropOutInventory(inventory);
        Destroy(gameObject);
    }
    public void UseItem()
    {
        List<IItem> items = new List<IItem>();
        foreach (GameObject itm in inventory.backpack)
        {
            IItem _itm = itm.GetComponent<IItem>();
            items.Add(_itm);
        }
        foreach (IItem _item in items)
        {
            _item.HideItem(inventory);
        }

        ItemUI[] itemsUI = FindObjectsOfType<ItemUI>();
        foreach(ItemUI itemUI in itemsUI)
        {
            flag.SetActive(false);
        }


        flag.SetActive(true);
        item.UseThisItem(inventory);
    }
}
