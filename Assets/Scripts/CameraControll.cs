using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public  Player player;
    private Vector3        offset;

    public void getCamera()
    {
        player = Player.instance;
        offset = player.transform.position - transform.position;
    }
    void LateUpdate()
    {
        if(player != null)
            transform.position = player.transform.position - offset;
    }
}
