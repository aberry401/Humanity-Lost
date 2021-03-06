using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class littleRobotAI : Enemy
{
    NavMeshAgent agent;
    public Transform target;
    ParticleSystem p;
    public float timer = 0;
    public float timer2 = 0;
    bool alive;
    public bool chasingTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        chasingTarget = false;
       
        alive = true;
        p = GetComponentInChildren<ParticleSystem>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("spawnsystem").GetComponent<SpawnScript>().currentTarget;
        agent.destination = target.position;
        if(p != null)
            p.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
        if (chasingTarget)
        {
            target = GameObject.FindGameObjectWithTag("spawnsystem").GetComponent<SpawnScript>().currentTarget;

            agent.SetDestination(target.position);

        }

        if (timer > 0.5f)
        {
            chasingTarget = false;
            if (Vector3.Distance(transform.position, target.position) < 60)
            {

                if (chasingTarget == false)
                {
                    UpdateTargetChase();
                }
                //  Debug.Log(Vector3.Distance(transform.position, target.position));

            }
            else
            {
                if (timer > 1f)
                {
                    UpdateTargetIdle();
                }
            }

        }




    }
    void FixedUpdate()
    {

       
        if (alive == true && timer2 > 1f && chasingTarget == true)
        {
            if (agent.remainingDistance < 1.5f)
            {
               
            }
        }
    }

   
    void UpdateTargetChase()
    {
        agent.Warp(transform.position);
        chasingTarget = true;
        timer = 0;
    }
    void UpdateTargetIdle()
    {
        Vector3 newTarget = new Vector3(transform.position.x + Random.Range(-10, 10), transform.position.y, transform.position.z + Random.Range(-10, 10));

        agent.Warp(transform.position);
        agent.SetDestination(newTarget);
        chasingTarget = false;
        timer = 0;
    }
}
