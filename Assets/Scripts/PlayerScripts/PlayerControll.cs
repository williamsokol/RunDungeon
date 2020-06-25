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

    void Awake()
    {
        i = movingSpeed;
    }
    void Start()
    {
        Init();
        //print(charact);   
    }
    public void Init()
    {
        
        cam                 = Camera.main.transform;
        charact             = GetComponent<CharacterController>();
        charact.center      = new Vector3(0,1,0);
        attack              = gameObject.GetComponentInChildren<PlayerAttack>();
        anim                = gameObject.GetComponent<Animator>();
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
        //print("test");
        Ycomponent -= 5 * Time.deltaTime;
        
        

        float H     = Input.GetAxis("Horizontal");
        float Y     = Input.GetAxis("Vertical");

        Vector3 direction           = transform.position + (cam.transform.right * H) + (cam.transform.forward * Y);
        Vector3 convertedDirection  = new Vector3(direction.x, transform.position.y, direction.z);
        transform.LookAt(convertedDirection);


        float inputforce = Vector3.Magnitude(new Vector3(H, 0, Y));

        Vector3 walk = transform.forward * movingSpeed * Time.deltaTime;

        if (inputforce > 0.1f)
        {
            charact.Move(new Vector3(walk.x,Ycomponent,walk.z));
            if (charact.isGrounded ) { Ycomponent = 0f; }
            anim.SetBool("IsRun", true);
        }
        if (inputforce == 0)
        {
            anim.SetBool("IsRun", false);
        }
    }
    

}
