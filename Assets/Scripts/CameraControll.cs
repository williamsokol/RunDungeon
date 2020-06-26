using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    private Vector3 velocity;
    public  Player  player;
    private Vector3        offset;

    
    
    
    
    public void getCamera()
    {
        
            
            player = Player.instance;
            //offset = player.transform.position - transform.position;
            velocity = Vector3.zero;
            print("offset");

       
    }
    void LateUpdate()
    {
        if(player != null)
            transform.position = Vector3.SmoothDamp(transform.position,player.transform.position - offset,ref velocity,.3f);
    }
}
