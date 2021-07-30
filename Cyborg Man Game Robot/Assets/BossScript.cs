using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
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

    public Text hpText;


    public bool batteryup;


    public GameObject doorway;
    // Start is called before the first frame update
    void Start()
    {
        hpText.text = "";
        batteryup = false;
        batteryCount = 6;
        robotCount = 0;
        StartCoroutine(StartFight());
        index = 0;
        waveGone = true;
        doorway.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hpText.text = "";
        for (int i = 0; i < batteryCount; i++)
        {
            hpText.text += "[]   ";
        }
        if (BatteriesGone())
        {
            //PLAY CUTSCENE HERE & RETURN TO MAIN SCENE AFTER, CHANGE PLAYERPREF VARS//

            Debug.Log("you win boss fight!");
            doorway.SetActive(true);
            PlayerPrefs.SetInt("boss1done", 1);


        }
        robotCount = 0;
        foreach (GameObject g in spawnedRobots)
        {
            if(g != null)
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

        if (robotCount < 1)
        {
            waveGone = true;
            

        } else
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
                    batteryParent.transform.position

                        = batterySpawns[Random.Range(0, batterySpawns.Length)].position;
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
