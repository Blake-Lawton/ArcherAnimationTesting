using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArcherController : MonoBehaviour
{
    // Start is called before the first frame update
    //public Camera mainCamera;
    public NavMeshAgent agent;
    public Animator playerAnimate;

    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerAnimate = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
       // Plane groundPlane = new Plane(Vector3.up, Vector3.zero);



        //MOVEMENT
        if (Input.GetMouseButton(1))
        {
            Debug.Log("wtfisgoingon");
            //Player Movement
            //agent.isStopped = false;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                agent.destination = hit.point;
            }
        }

       if (agent.remainingDistance <= agent.stoppingDistance)
        {
            playerAnimate.SetFloat("AnimDemo", 0);


       }
        else
       {
            playerAnimate.SetFloat("AnimDemo", 1);


        }

       
    }


}
