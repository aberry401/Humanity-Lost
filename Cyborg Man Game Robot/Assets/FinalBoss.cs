using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalBoss : MonoBehaviour
{
    public GameObject[] robots;
    public Transform[] spawnpoints;
    public Transform[] batterySpawns;
    public GameObject[] batteries;
    public GameObject batteryParent;
    List<GameObject> spawnedRobots = new List<GameObject>();

    public AudioSource openingLine;
    public AudioSource spawnSound;

    public int index;
    public bool waveGone;

    public int robotCount;
    public int batteryCount;

    public GameObject[] BatteriesDisplay;

    public GameObject aiming;
    public GameObject laser;
    public GameObject line;

    public GameObject bat1;
    public GameObject bat2;


    public bool batteryup;

    public bool laserOn;
    public bool lineOn;


    public GameObject doorway;
    // Start is called before the first frame update
    void Start()
    {
        bat1.SetActive(true);
        bat2.SetActive(true);
        lineOn = false;
        laserOn = false;
        laser.SetActive(false);
        line.SetActive(false);
        batteryup = false;
        batteryCount = 10;
        robotCount = 0;
        StartCoroutine(StartFight());
        index = 0;
        waveGone = true;
        doorway.SetActive(false);


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(batteryCount < 7)
        {
            bat1.SetActive(false);
        }
        if (batteryCount < 3)
        {
            bat2.SetActive(false);
        }
        aiming.GetComponent<LookAtPlayer>().LookAt(GameObject.Find("samplePlayer"));
        foreach (GameObject g in BatteriesDisplay)
        {
            g.SetActive(false);
        }
        for (int i = 0; i < batteryCount; i++)
        {
            BatteriesDisplay[i].SetActive(true);
        }
        if (BatteriesGone())
        {
            //PLAY CUTSCENE HERE & RETURN TO MAIN SCENE AFTER, CHANGE PLAYERPREF VARS//

            Debug.Log("you win boss fight!");
            doorway.SetActive(true);
            PlayerPrefs.SetInt("boss2done", 1);


        }
        robotCount = 0;
        foreach (GameObject g in spawnedRobots)
        {
            if (g != null)
            {
                robotCount++;
            }
        }

        batteryCount = 0;
        foreach (GameObject g in batteries)
        {
            if (g != null)
            {
                batteryCount++;
            }
        }

        if (robotCount < 1 && !laserOn && !lineOn)
        {
            waveGone = true;


        }
        else
        {
            waveGone = false;
        }

        if (waveGone)
        {

            StartCoroutine(SpawnWave(index));
            waveGone = false;
        }


    }

    bool BatteriesGone()
    {
        bool dead = true;
        foreach (GameObject g in batteries)
        {
            if (g != null)
            {
                dead = false;
            }
        }
        return dead;
    }

    IEnumerator SpawnWave(int index)
    {
        int x;
        x = Random.Range(0, 100);
        if (x < 33)
        {


            for (int i = 0; i < 6 + index; i++)
            {
                spawnSound.Play();
                GameObject g = Instantiate(robots[Random.Range(0, robots.Length)],
                    spawnpoints[Random.Range(0, spawnpoints.Length)]);
                spawnedRobots.Add(g);

                yield return new WaitForSeconds(Random.Range(0f, 2f));
            }

            yield return new WaitForSeconds(Random.Range(0f, 5f));
            StartCoroutine(SpawnBattery());
        }
        else if (x < 66)
        {
            laserOn = true;
            yield return new WaitForSeconds(Random.Range(1f, 4f));
           
            StartCoroutine(SpawnBattery());
            yield return new WaitForSeconds(Random.Range(.5f, 1f));
            laser.SetActive(true);
            yield return new WaitForSeconds(Random.Range(5f, 6f));
            laser.SetActive(false);
           
            yield return new WaitForSeconds(Random.Range(.5f, 1f));
            laser.SetActive(true);
            yield return new WaitForSeconds(Random.Range(5f, 6f));
            laser.SetActive(false);
           
            yield return new WaitForSeconds(Random.Range(.5f, 1f));
            laser.SetActive(true);
            yield return new WaitForSeconds(Random.Range(5f, 6f));
            laser.SetActive(false);
          
            yield return new WaitForSeconds(Random.Range(.5f, 1f));
            laser.SetActive(true);
            yield return new WaitForSeconds(Random.Range(5f, 6f));
            laser.SetActive(false);
           
            yield return new WaitForSeconds(Random.Range(.5f, 1f));
            laser.SetActive(true);
            yield return new WaitForSeconds(Random.Range(5f, 6f));
            laser.SetActive(false);
            laserOn = false;

        }
        else
        {
            lineOn = true;
            yield return new WaitForSeconds(Random.Range(1f, 4f));
            StartCoroutine(SpawnBattery());

            line.SetActive(true);
            yield return new WaitForSeconds(Random.Range(10f, 12f));
            line.SetActive(false);
            
            lineOn = false;


        }
    }


    IEnumerator SpawnBattery()
    {
        if (!batteryup)
        {
            batteryup = true;
            if (batteryCount >= 1)
            {
                int x = batteryCount - 1;
                if (batteries[x] != null)
                {
                    
                        int y = Random.Range(0, batterySpawns.Length);
                    batteryParent.transform.position
                        = batterySpawns[y].position;
                    batteryParent.transform.rotation

                        = batterySpawns[y].rotation;
                    batteries[x].GetComponent<Animator>().SetTrigger("up");


                    yield return new WaitForSeconds(10f);
                    batteryup = false;
                }
                else
                {
                    StartCoroutine(SpawnBattery());
                }
            }
        }
    }




    IEnumerator StartFight()
    {
        yield return new WaitForSeconds(1f);
        openingLine.Play();
        yield return new WaitForSeconds(5f);

    }

}
