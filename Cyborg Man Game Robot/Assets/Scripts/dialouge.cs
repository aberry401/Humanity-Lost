using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class dialouge : MonoBehaviour
{

    public bool noDestroy = false;



    [Tooltip("lines to be read - each line is a single box and a step in dialog")]
    public string[] lines;
    [Tooltip("dont need to assign this, helper variable for controller")]
    public GameObject g;
    [Tooltip("object that the functions are moving/ doing things to(either on the object on in a child)")]
    
    public GameObject obj;

   
    [Tooltip("ChangeVar = does it change a playerPref variable?")]
    public bool changeVar;
    [Tooltip("ObjectFunction = does it run a function from Objectfunctions.cs?")]
    public bool objectFunction;
    [Space(10)]
    [Tooltip("varToChange1 - what var to change(use 1 if not using buttons) ")]
    
    public string varToChange1, varValue1, functionIndex1;
    [Tooltip("varValue1 - what var changes to(use 1 if not using buttons) ")]
    public string varToChange2, varValue2, functionIndex2;
    [Tooltip("leave blank if no buttons, or type the options and they will execute corresponding var or functions")]
    [Space(10)]
    public string button1Text, button2Text;

    [Tooltip("what playerpref value has to be set to 1(and not 0) in order for this character to spawn/despawn?")]
    public string deleteAfterPref;
    public string spawnAfterPref;

    public Vector3 startingScale; // instead of despawning we make the scale 0 so the scripts keep their stuff
    [Space(10)]

    [Tooltip("what to say when a player can't do the action because of missing story or items")]
    public string cantDoLine;
    [Space(10)]
    [Tooltip("should the dialog give the player scrap when it finishes? this is also in objfunctions")]
    public bool giveScrap;
    public int scrapAmmnt;



    private void OnLevelWasLoaded(int level)
    {

        if (noDestroy)
        {
         //   startingScale = gameObject.transform.localScale;
            if (spawnAfterPref != "")
            {
                gameObject.transform.localScale = new Vector3(0, 0, 0);
                if (PlayerPrefs.GetInt(spawnAfterPref, 0) == 1)
                {
                    gameObject.transform.localScale = new Vector3(startingScale.x, startingScale.y, startingScale.z);
                }
            }
        }
       
    }





        // Start is called before the first frame update
        void Start()
    {
        startingScale = gameObject.transform.localScale;
        if (spawnAfterPref != "")
        {
            gameObject.transform.localScale = new Vector3(0, 0, 0);
            if (PlayerPrefs.GetInt(spawnAfterPref, 0) == 1)
            {
                gameObject.transform.localScale = new Vector3(startingScale.x, startingScale.y, startingScale.z);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (deleteAfterPref != "")
        {
            if (PlayerPrefs.GetInt(deleteAfterPref, 0) == 1)
            {
                if (!noDestroy)
                {
                    Destroy(gameObject);
                } else
                {
                    gameObject.SetActive(false); 
                }
            }
        }

        if (spawnAfterPref != "")
        {
            if (PlayerPrefs.GetInt(spawnAfterPref, 0) == 1)
            {
                gameObject.transform.localScale = new Vector3(startingScale.x, startingScale.y, startingScale.z);
            }
        }
    }
}
