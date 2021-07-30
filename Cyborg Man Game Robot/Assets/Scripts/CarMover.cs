using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarMover : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;
    public GameObject spawnsystem;
    public NavMeshPath path;
    // Start is called before the first frame update
    void Start()
    {
        agent.enabled = true;
        spawnsystem = GameObject.Find("SpawnSystem");
        target = FindWaypoint();
       
        path = new NavMeshPath();
        StartCoroutine(Idle());
        agent.Warp(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.isOnNavMesh)
        {
           
            agent.transform.position = target.position;
            agent.enabled = false;
            agent.enabled = true;
        }
    }
    IEnumerator Idle()
    {

       
            Debug.Log("idling");
            float waypointDist = Vector3.Distance(transform.position, target.position);
            //Debug.Log(waypointDist);

            
                Debug.Log("new waypouint idle");
                target = FindWaypoint();

                
                agent.CalculatePath(target.position, path);
                agent.SetPath(path);


            

            yield return new WaitForSeconds(3f);

            StartCoroutine(Idle());

        
    }
    Transform FindWaypoint()
    {
        Debug.Log("finding waypoint" + spawnsystem.GetComponent<SpawnScript>().carwaypoints
            [Random.Range(0, spawnsystem.GetComponent<SpawnScript>().carwaypoints.Count - 1)]);
        return spawnsystem.GetComponent<SpawnScript>().carwaypoints
            [Random.Range(0, spawnsystem.GetComponent<SpawnScript>().carwaypoints.Count - 1)];
    }
}
