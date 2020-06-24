using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   public int Attack,Def, Hp;
   public int currentHp;

    public Item[] ItemSlots = {null,null,null};
        

   public static Player instance;

   void Awake()
   {
       currentHp = Hp;
       instance = this;
       GameObject.Find("FollowCamera").GetComponent<CameraControll>().getCamera();
   }

   public void UpdateItemStats(Item newItem)
   {
        print(ItemSlots[1]);    
        if(ItemSlots[newItem.itemType] != null)
        {
            Item oldItem = ItemSlots[newItem.itemType];
            Attack -= oldItem.Attack;
            Def -= oldItem.Def;
            Hp -= oldItem.Hp;
        }

        Attack += newItem.Attack;
        Def += newItem.Def;
        Hp += newItem.Hp;

        ItemSlots[newItem.itemType] = newItem;
   }

   public void DroppedWornItem(Item DroppedItem)
   {
       // see if dropped it was one equiped
        if(DroppedItem != ItemSlots[DroppedItem.itemType])
        {
            return;
        }
        // if it is do:
        Attack -= DroppedItem.Attack;
        Def -= DroppedItem.Def;
        Hp -= DroppedItem.Hp;

        ItemSlots[DroppedItem.itemType] = null;

   }

    public void TakeDamage(int damage)
    {
        currentHp -= damage*((Def-100)/100);
    }

}
