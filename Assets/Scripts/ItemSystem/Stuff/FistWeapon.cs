using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class FistWeapon : Item, IItem
{
    public AnimatorController animOver;

    public void UseItem(Invertory inventory)
    {
        inventory.weapon   = gameObject;
        transform.position = inventory.weaponSlot.position;
        transform.SetParent(inventory.weaponSlot);

        Animator anim                  = inventory.GetComponent<Animator>();
        anim.runtimeAnimatorController = animOver;
    }
}
