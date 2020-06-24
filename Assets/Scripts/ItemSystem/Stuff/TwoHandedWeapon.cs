using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoHandedWeapon : Item, IItem
{
    public AnimatorOverrideController animOver;

    public void UseItem(Invertory inventory)
    {
        inventory.weapon   = gameObject;
        transform.position = inventory.weaponSlot.position;
        transform.SetParent(inventory.weaponSlot);

        Animator anim                  = inventory.GetComponent<Animator>();
        anim.runtimeAnimatorController = animOver;
    }
}
