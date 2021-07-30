using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceRobotAI : Enemy
{
    NavMeshAgent agent;
    public Transform target;
    public bool chasingTarget;
    public GameObject spawnLoc, prefab;
    float timer, timer2, timer3;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        timer2 = 0;
        Instantiate(prefab, spawnLoc.transform);
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("spawnsystem").GetComponent<SpawnScript>().currentTarget;
        agent.destination = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (chasingTarget)
        {
            target = GameObject.FindGameObjectWithTag("spawnsystem").GetComponent<SpawnScript>().currentTarget;

            agent.SetDestination(target.position);

        }
        Vector3 newTarget = new Vector3(transform.position.x + Random.Range(-10, 10), transform.position.y, transform.position.z + Random.Range(-10, 10));

        if (timer3 > 0.5f)
        {
            chasingTarget = false;
            if (Vector3.Distance(transform.position, target.position) > 30)
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


        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
        timer3 += Time.deltaTime;
        if (timer > 3)
        {
            GameObject g = (Instantiate(prefab, spawnLoc.transform));
            g.transform.parent = GameObject.Find("empty").transform;
            Destroy(g, 35f);
            timer = 0;
        }

        if (timer2 > 1)
        {
            UpdateTargetIdle();
            timer2 = 0;
        }
    }
    void UpdateTargetIdle()
    {
        Vector3 newTarget = new Vector3(transform.position.x + Random.Range(-10, 10), transform.position.y, transform.position.z + Random.Range(-10, 10));

        agent.Warp(transform.position);
        agent.SetDestination(newTarget);
        chasingTarget = false;
        timer3 = 0;
    }
    void UpdateTargetChase()
    {
        agent.Warp(transform.position);
        chasingTarget = true;
        timer = 0;

    }





}
