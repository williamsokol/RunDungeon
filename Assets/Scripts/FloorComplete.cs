using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorComplete : MonoBehaviour
{
   
   void OnTriggerEnter(Collider other)
   {
       if(other.gameObject.tag == "Player")
       {
           GameObject.Find("Fade").GetComponent<MainMenu>().FadeToLevel();
           Time.timeScale = 0f;
           
       }
   }

    // Update is called once per frame
    void Update()
    {
        
    }
}
