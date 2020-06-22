using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject colliderBox;
    public GameObject model;

    public void AddInInventory(Invertory inventory)
    {
        inventory.backpack.Add(gameObject);
        transform.position = inventory.crate.position;
        transform.SetParent(inventory.crate);
        colliderBox.SetActive(false);
        model.SetActive(false);
    }

    public void DropOutInventory(Invertory inventory)
    {
        inventory.backpack.Remove(gameObject);
        transform.SetParent(null);
        transform.position += Vector3.forward * 3f;
        colliderBox.SetActive(true);
        model.SetActive(true);
    }
}
