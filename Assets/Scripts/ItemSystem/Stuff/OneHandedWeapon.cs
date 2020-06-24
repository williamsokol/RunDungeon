using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneHandedWeapon : Item, IItem
{
    public AnimatorOverrideController animOver;

    public void UseItem(Invertory inventory)
    {
        inventory.weapon   = gameObject;
        transform.SetParent(inventory.weaponSlot);
        transform.localPosition = new Vector3(0,0,0);
        transform.localRotation = Quaternion.Euler(0, 0, 0);

        Animator anim                  = inventory.GetComponent<Animator>();
        anim.runtimeAnimatorController = animOver;
    }
}
