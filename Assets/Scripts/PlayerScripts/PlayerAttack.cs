using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public MusicPlayer sound;
    public Player player;
    public BoxCollider hitbox;
    void Start()
    {
        sound = GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>();
       
        hitbox = GetComponent<BoxCollider>();
        player = Player.instance;        
    }

    public void StartAttack()
    {
        sound.AttackSFX();
        hitbox.enabled = true;
    }
    public void EndAttack()
    {
        
        hitbox.enabled = false;
    }



   void OnTriggerEnter(Collider other)
   {
      
       if(other.gameObject.tag == "Enemy")
       {
           print(Player.Attack);
           //they take dmg
            other.gameObject.GetComponent<EnemyStats>().TakeDamage(Player.Attack);
       }
   }
}
