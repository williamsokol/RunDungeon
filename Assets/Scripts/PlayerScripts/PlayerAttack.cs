using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public Player player;
    public BoxCollider hitbox;
    void Start()
    {
        hitbox = GetComponent<BoxCollider>();
        player = Player.instance;        
    }

    public void StartAttack()
    {
        
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
           print("hit enemy");
           //they take dmg
            other.gameObject.GetComponent<EnemyStats>().TakeDamage(player.Attack);
       }
   }
}
