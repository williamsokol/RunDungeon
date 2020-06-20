using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    private Transform           cam;
    private CharacterController charact;
    public  Animator            anim;

    public float Ycomponent;
    public float movingSpeed;

    void Start()
    {
        cam                 = Camera.main.transform;
        charact             = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
        GetActiveButtons();
    }

    void GetActiveButtons()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //int IsFight = Random.Range(1, 4);
            //anim.SetInteger("IsFight", IsFight);
        }
        //if (Input.GetButtonDown("Fire2")) { anim.SetInteger("IsFight", 4); }
    }
    void Move()
    {
        Ycomponent += Physics.gravity.y * Time.deltaTime;
        if (charact.isGrounded & charact.velocity.y <= 0) { Ycomponent = 0f; }

        float H     = Input.GetAxis("Horizontal");
        float Y     = Input.GetAxis("Vertical");

        Vector3 direction           = transform.position + (cam.transform.right * H) + (cam.transform.forward * Y);
        Vector3 convertedDirection  = new Vector3(direction.x, transform.position.y, direction.z);
        transform.LookAt(convertedDirection);


        float inputforce = Vector3.Magnitude(new Vector3(H, 0, Y));
        if (inputforce > 0.7f && charact.isGrounded)
        {
            charact.Move(transform.forward * movingSpeed * Time.deltaTime);
            anim.SetBool("IsRun", true);
        }
        if (inputforce == 0) { anim.SetBool("IsRun", false); }

        charact.Move(new Vector3(0, Ycomponent, 0) * Time.deltaTime);
    }
}
