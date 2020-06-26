using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackSpeed;
    public int attackDamage;

    public MusicPlayer sound;
    Animator anim;
    GameObject player;
    EnemyStats stats;
    bool playerInRange;
    float timer;

    void Awake()
    {
        player = Player.instance.gameObject;
        //print(player);
        stats  = gameObject.GetComponent<EnemyStats>();
        attackSpeed = stats.AttackSpeed;
        attackDamage = stats.Attack;
        
        //anim
        anim = gameObject.GetComponent<Animator>();
        sound = GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>();
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            //print("test");
            playerInRange = true;
            //print(gameObject +"this one");
            
        }else{}
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
        
        anim.SetInteger("IsFight", 0);
        

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if(timer >= attackSpeed && playerInRange && stats.currentHp > 0)
        {
            // ... attack.
            
            Attack ();
           // print("test");
            int IsFight = Random.Range(1, 4);
            anim.SetInteger("IsFight", IsFight);
           
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
            sound.AttackedSFX();
        }
    }
}