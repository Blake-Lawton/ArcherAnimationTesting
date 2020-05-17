using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    
    public Animator playerAnimate;
    public Animator abilityAnimate;

    public NavMeshAgent agent;

    public GameObject player;

    public GameObject rOA;

    public bool running;
    
    
    public Camera mainCamera;
    public float speed;


    public float lastBasicAttackTime;
    public float basicAttackSpeed = 1.2f;
    

    // Start is called before the first frame update
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        speed = agent.speed;
        lastBasicAttackTime = -5.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //Raycasting set up for abilities 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);



        //MOVEMENT
        if (Input.GetMouseButton(1) && (Time.time - lastBasicAttackTime > 1.5f))
        {
            //Player Movement
            agent.isStopped = false;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                agent.destination = hit.point;
            }

            

        }
        
        //BASICATTACK
        if (Input.GetMouseButtonDown(0)) 
        {
            
            playerAnimate.SetTrigger("basicAttack");
            
            agent.isStopped = true ;
            agent.ResetPath();

            //player looking at mouse to shoot 
            
            

            float rayLength;

            if (groundPlane.Raycast(ray, out rayLength))
            {
                Vector3 pointToLook = ray.GetPoint(rayLength);
                

                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }


            lastBasicAttackTime = Time.time;



        }

        //DOUBLESTRIKE
        if(Input.GetKeyDown("q"))
        {
            playerAnimate.SetTrigger("doubleStrike");
            
        
            agent.isStopped = true;
            agent.ResetPath();

            //player looking at mouse to shoot
           
            

            float rayLength;

            if (groundPlane.Raycast(ray, out rayLength))
            {
                Vector3 pointToLook = ray.GetPoint(rayLength);


                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }

            
        }

        //BLINK
        if (Input.GetKeyDown("w"))
        {
            
            agent.isStopped = false;
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
               // agent.speed = 99999;
                //agent.destination = hit.point;
                agent.Warp(hit.point);
                

            }

            

            //agent.speed = speed;
        }

        //RainOfArrows
        if(Input.GetKeyDown("r"))
        {
            
            playerAnimate.SetTrigger("rainOfArrows");

            abilityAnimate.SetTrigger("ROA");
            //playerAnimate.PlayInFixedTime("AttackAngled", 0, 1);

            agent.isStopped = true;
            agent.ResetPath();

            //player looking at mouse to shoot



            float rayLength;

            if (groundPlane.Raycast(ray, out rayLength))
            {
                Vector3 pointToLook = ray.GetPoint(rayLength);


                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
                rOA.transform.position = new Vector3(pointToLook.x, transform.position.y + 50, pointToLook.z);
            }
        }



        

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            running = false;
            
            
        }
        else
        {
            running = true;
            

        }

        playerAnimate.SetBool("running", running); 
    }

   
}
