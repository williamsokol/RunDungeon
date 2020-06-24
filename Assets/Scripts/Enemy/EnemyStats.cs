using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
   public int Attack,Def,Hp;
   public float currentHp,AttackSpeed;


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
       
   }
}
