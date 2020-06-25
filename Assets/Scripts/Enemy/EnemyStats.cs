using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
   public int Attack,Def,Hp;
   public float currentHp,AttackSpeed;

    void Start()
    {
        //randomize enemy stats;
        Attack = Random.Range(5,20);
        Def = Random.Range (5,40);
        Hp = Random.Range (5,40);
    }

   public void TakeDamage(int damage)
   {
       currentHp -= damage;

       if (currentHp <= 0)
       {
           Die();
       }
   }
   void Die()
   {
       gameObject.GetComponentInChildren<BoxCollider>().enabled = false;
       Destroy (gameObject, 1f);
       //play death anim
   }
}
