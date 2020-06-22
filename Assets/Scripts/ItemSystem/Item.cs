﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject pikColliderBox;
    public GameObject model;

    public void AddInInventory(Invertory inventory)
    {
        inventory.backpack.Add(gameObject);
        transform.position = inventory.crate.position;
        transform.SetParent(inventory.crate);
        pikColliderBox.SetActive(false);
        model.SetActive(false);
    }

    public void DropOutInventory(Invertory inventory)
    {
        inventory.backpack.Remove(gameObject);
        transform.SetParent(null);
        pikColliderBox.SetActive(true);
        model.SetActive(true);

        Ray        ray = new Ray(transform.position + transform.forward * 2f, Vector3.down);
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(ray, out hit);

        if (hit.collider)
        {
            transform.position = hit.point;
        }
        if (!hit.collider)
        {
            print("Can't find point"); 
        }
    }

    public void UseThisItem(Invertory inventory)
    {
        transform.GetComponent<IItem>().UseItem(inventory);
        model.SetActive(true);
    }
}
