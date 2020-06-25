using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
   public int Attack,Def, Hp;
   public int currentHp;
   public static int TorchHp;

    public Item[] ItemSlots = {null,null,null};
    public Component[] PlayerCore;
    public GameObject[] ChildernCore;

   public static Player instance;

   void Awake()
   {
       //set up player
       Init();    
   }

   public void Init()
   {
       //gameObject.name = "Player";
       PlayerCore[0] = GetComponent<CharacterController>();
       PlayerCore[1] = GetComponent<Invertory>();
       PlayerCore[2] = GetComponent<PlayerControll>();
       PlayerCore[3] = GetComponent<Player>();
       
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
        float calculatedDmg = (float)damage*((100-(float)Def)/100);
        print(calculatedDmg);
        currentHp -= (int)calculatedDmg;

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
        MoveControlls(attacker);
        Destroy(gameObject);
        TorchHp -= 1; 
        
    }
    void MoveControlls(EnemyAttack attacker)
    {
        
        //insert enemies stats into that players
        EnemyStats enemyStats = attacker.GetComponent<EnemyStats>();
        Attack = enemyStats.Attack;
        Def = enemyStats.Def;
        Hp = enemyStats.Hp;

        attacker.gameObject.tag = "Player";
        attacker.gameObject.transform.parent = null;

        //remove new their AI
        Destroy(attacker.gameObject.GetComponent<NavMeshAgent>());
        Destroy(attacker.gameObject.GetComponent<EnemyMovement>());
        Destroy(attacker.gameObject.GetComponent<EnemyAttack>());
        Destroy(attacker.gameObject.GetComponent<EnemyStats>());
        Destroy(attacker.gameObject.GetComponent<Rigidbody>());
        Destroy(attacker.gameObject.transform.GetChild(0).gameObject);
        
        //copy player components into enemy
        for(int a = 0;a<PlayerCore.Length;a++){
            Component sourceComp =  PlayerCore[a];
            //Type type = GetType(sourceComp);
            
            FieldInfo[] sourceFields = sourceComp.GetType().GetFields(BindingFlags.Public | 
                                                        BindingFlags.NonPublic | 
                                                        BindingFlags.Instance);
            
            Component targetComp = attacker.gameObject.AddComponent(PlayerCore[a].GetType()) as Component;
            
            int i = 0;
            for(i = 0; i < sourceFields.Length; i++) {
                var value = sourceFields[i].GetValue(sourceComp);
                sourceFields[i].SetValue(targetComp, value);
                //print( targetComp+" "+sourceFields[i]);
            }
            
            //Destroy(sourceComp);
        }
        //copy childern to enemy
        foreach(GameObject child in ChildernCore)
        {
            Vector3 savePos = child.transform.localPosition;
            Quaternion saveRot = child.transform.localRotation; 
            child.transform.parent = attacker.transform;
            child.transform.localPosition = savePos;
            child.transform.localRotation = saveRot;
            
        }
        attacker.gameObject.GetComponent<Player>().Init();
        attacker.gameObject.GetComponent<PlayerControll>().Init();
    }
}
