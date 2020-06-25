using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackSpeed;
    public int attackDamage;

    Animator anim;
    GameObject player;
    EnemyStats stats;
    bool playerInRange;
    float timer;

    void Awake()
    {
        player = Player.instance.gameObject;
        stats  = gameObject.GetComponent<EnemyStats>();
        attackSpeed = stats.AttackSpeed;
        attackDamage = stats.Attack;
        
        //anim
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
            print(gameObject +"this one");
            
        }else{print(other);}
    }

    void OnTriggerExit (Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }

    }
    
    void Update ()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if(timer >= attackSpeed && playerInRange && stats.currentHp > 0)
        {
            // ... attack.
            Attack ();
           
        }

        // If the player has zero or less health...
        if(Player.instance.currentHp <= 0)
        {
            // ... tell the animator the player is dead.
            //anim.SetTrigger ("PlayerDead");
        }
    }


    void Attack ()
    {
        // Reset the timer.
        timer = 0f;
        
        print(Player.instance.gameObject);
        // If the player has health to lose...
        if(Player.instance.currentHp > 0)
        {
            // ... damage the player.
            Player.instance.TakeDamage (attackDamage,this);
        }
    }
}