using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
   public int Attack,Def, Hp;
   public int currentHp;

    public Item[] ItemSlots = {null,null,null};
    public Component[] PlayerCore;

   public static Player instance;

   void Awake()
   {
       currentHp = Hp;
       instance = this;
       GameObject.Find("FollowCamera").GetComponent<CameraControll>().getCamera();
   }

   public void UpdateItemStats(Item newItem)
   {
        //print(ItemSlots[1]);    
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

    public void TakeDamage(int damage,EnemyAttack attcker)
    {
        currentHp -= damage*((100-Def)/100);

        if (currentHp <= 0)
        {
            Die(attcker);
        } 
    }
    void Die(EnemyAttack attacker)
    {
        //swap into the body of the enemy that killed you
        //(insert death anim)
        
        //Component component = PlayerControll;
        MoveComponent(attacker);
        
    }
    void MoveComponent(EnemyAttack attacker)
    {
        
        Component sourceComp =  PlayerCore[0];
        //Type type = GetType(sourceComp);
        
        FieldInfo[] sourceFields = sourceComp.GetType().GetFields(BindingFlags.Public | 
                                                       BindingFlags.NonPublic | 
                                                       BindingFlags.Instance);
        
        Component targetComp = attacker.gameObject.AddComponent(PlayerCore.GetType()) as Component;
        int i = 0;
        for(i = 0; i < sourceFields.Length; i++) {
            var value = sourceFields[i].GetValue(sourceComp);
        sourceFields[i].SetValue(targetComp, value);
        }
        
         Destroy(sourceComp);
    
    }

}
