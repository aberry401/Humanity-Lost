using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnScript : MonoBehaviour
{
    public GameObject[] robotsList;
    public Transform[] spawnPoints;
    public List<Transform> waypoints = new List<Transform>();
    public List<Transform> carwaypoints = new List<Transform>();
    public Transform currentTarget;
    public GameObject waypointsObj;
    public GameObject carwaypointsObj;
    public GameObject cutscene;
    public float timer; 
    public int totalCount;
    // Start is called before the first frame update
    void Start()
    {
        totalCount = 0;
        SetCurrentTarget(GameObject.FindGameObjectWithTag("Player").transform);
        foreach (Transform child in waypointsObj.transform)
        {
            if (child.tag == "waypoint")
            {
                waypoints.Add(child.gameObject.transform);
            }
        }
        if (carwaypointsObj != null)
        {
            foreach (Transform child in carwaypointsObj.transform)
            {
                if (child.tag == "waypoint")
                {
                    carwaypoints.Add(child.gameObject.transform);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }
    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name != "BossFight" && SceneManager.GetActiveScene().name != "FinaleBoss")
        {
            totalCount = 0;
            if (spawnPoints.Length > 0)
            {
                foreach (Transform t in spawnPoints)
                {
                    totalCount += t.gameObject.GetComponent<Counter>().count;
                }
                if (totalCount < 5500 && timer > .5f)
                {
                    if (!cutscene.GetComponent<Camera>().enabled)
                    {
                        Spawn();
                        timer = 0;
                    }
                }
            }
            SetCurrentTarget(GameObject.FindGameObjectWithTag("Player").transform);

            //if (Input.GetKeyDown(KeyCode.Q)) 
            //{

            //        int x = Random.Range(0, robotsList.Length);
            //        int y = Random.Range(0, spawnPoints.Length);
            //        GameObject g = Instantiate(robotsList[x], spawnPoints[y]);


            //}
        }
    }
    void Spawn()
    {
        int x = Random.Range(0, robotsList.Length);
        int y = Random.Range(0, spawnPoints.Length);
        GameObject g = Instantiate(robotsList[x], spawnPoints[y]);

    }
    public void SetCurrentTarget(Transform t) //If at some point in the future we want robots attacking someone other than the player
    {
        currentTarget = t;
    }

}
