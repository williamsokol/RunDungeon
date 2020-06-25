using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class TwoHandedWeapon : Item, IItem
{
    public AnimatorOverrideController animOver;
    public AnimatorController         fistContrl;

    public void HideItem(Invertory inventory)
    {
        inventory.weapon = null;
        transform.SetParent(inventory.crate);
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.Euler(0, 0, 0);

        Animator anim = inventory.GetComponent<Animator>();
        anim.runtimeAnimatorController = fistContrl;
    }

    public void UseItem(Invertory inventory)
    {
        inventory.weapon   = gameObject;
        transform.SetParent(inventory.weaponSlot);
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        Animator anim                  = inventory.GetComponent<Animator>();
        anim.runtimeAnimatorController = animOver;
    }
}
