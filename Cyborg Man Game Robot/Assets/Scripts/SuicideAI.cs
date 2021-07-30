using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SuicideAI : Enemy
{
    NavMeshAgent agent;
    public Transform target;
    ParticleSystem p;
    public float timer = 0;
    public float timer2 = 0;
    bool alive;
   public bool chasingTarget;
    public GameObject hitbox;
    // Start is called before the first frame update
    void Start()
    {
        chasingTarget = false;
        hitbox.GetComponent<SphereCollider>().radius = 0.1f;
        hitbox.SetActive(false);
        alive = true;
        p = GetComponentInChildren<ParticleSystem>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("spawnsystem").GetComponent<SpawnScript>().currentTarget;
        agent.destination = target.position;
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
            if (Vector3.Distance(transform.position, target.position) < 40)
            {
                if (chasingTarget == false)
                {
                    UpdateTargetChase();
                }
              //  Debug.Log(Vector3.Distance(transform.position, target.position));

            } else
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
      
        if(hitbox.activeSelf == true)
        {
            //Debug.Log("true");
            if (hitbox.GetComponent<SphereCollider>().radius < 4)
            {
                if (timer2 > 1f)
                {
                    hitbox.GetComponent<SphereCollider>().radius *= 1.26f;
                }
            }
        }
        if (alive == true && timer2 > 1f && chasingTarget == true)
        {
            if (agent.remainingDistance < 1.5f)
            {
                alive = false;
                agent.speed = 0.1f;
                Explode();
                Destroy(gameObject, 1.5f);
            }
        }
    }
    
    void Explode()
    {
        p.Play();
        hitbox.SetActive(true);
        timer2 = 0;

    }
    void UpdateTargetChase()
    {
        agent.Warp(transform.position);
        chasingTarget = true;
        timer = 0;
    }
    void UpdateTargetIdle()
    {
        Vector3 newTarget = new Vector3(transform.position.x + Random.Range(-10,10), transform.position.y, transform.position.z + Random.Range(-10, 10));

        agent.Warp(transform.position);
        agent.SetDestination(newTarget);
        chasingTarget = false;
        timer = 0;
    }
}
