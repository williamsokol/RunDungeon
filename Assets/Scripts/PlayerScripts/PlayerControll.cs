using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    private Transform           cam;
    private CharacterController charact;
    public  Animator            anim;
    public PlayerAttack attack;

    public float Ycomponent;
    public float movingSpeed, i;

    void Start()
    {
        i = movingSpeed;
        cam                 = Camera.main.transform;
        charact             = GetComponent<CharacterController>();
        attack              = gameObject.GetComponentInChildren<PlayerAttack>();
        
    }

    void Update()
    {
        Move();
        GetActiveButtons();
    }

    void GetActiveButtons()
    {
        if (anim.GetInteger("IsFight") != 0)
        {
            anim.SetInteger("IsFight", 0);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            // do an Attack
            int IsFight = Random.Range(1, 4);
            anim.SetInteger("IsFight", IsFight);
            movingSpeed = 0;
            attack.EndAttack();
            attack.StartAttack();
            
        }else if(anim.GetCurrentAnimatorStateInfo(0).IsName("FightPose")){
           //revert to non attack mode
            movingSpeed = i;
            attack.EndAttack();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            anim.SetInteger("IsFight", 4);
        }
        if (Input.GetButtonDown("Fire3"))
        {
            anim.SetInteger("IsFight", 5);
        }
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

        if (inputforce > 0.1f)
        {
            charact.Move(transform.forward * movingSpeed * Time.deltaTime);
            anim.SetBool("IsRun", true);
        }
        if (inputforce == 0)
        {
            anim.SetBool("IsRun", false);
        }

        //charact.Move(new Vector3(0, 0, 0) * Time.deltaTime);
    }

}
