
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RandomDialog : MonoBehaviour
{

    [System.Serializable]
    public class LinesList
    {
        public string[] lines;
    }
    public LinesList[] linesListGeneric;

    public LinesList[] linesListBeforeBoss1;

    public LinesList[] linesListAfterBoss1;

    public LinesList[] linesListBeforeBoss2;

    public enum Lists {GENERIC,B4BOSS1,AFTERBOSS1,AFTERBOSS2 }

    private bool isGeneric;

    [Tooltip("how often a random npc is generic /n 0-100 as percentage value")]
    
    public float genericRate;

    [Tooltip("how often a non generic npc gives info /n 0-100 as percentage value")]
    public float questInfoRate;
    // Start is called before the first frame update

    private int questIndex;
    void Start()
    {
        //INSERT LOGIC TO SEE WHERE IN GAME WE ARE
        if (PlayerPrefs.GetInt("boss1done") == 0)
        {
            questIndex = (int)Lists.B4BOSS1;
        }
        if (PlayerPrefs.GetInt("boss1done") == 1)
        {
            questIndex = (int)Lists.AFTERBOSS1;
        }
        if (PlayerPrefs.GetInt("TankFightDone") == 1)
        {
            questIndex = (int)Lists.AFTERBOSS2;
        }



        if (Random.Range(0, 100) < genericRate)
        {
            isGeneric = true;
        }
        //logic for whenever you talk to guy if generic they can say random stuff but maybe the clues are the same until scene is loaded and unloaded
        //this way a random person talking about cheese doesn't next line talk about quest, or maybe we do want that 
        
        GetComponent<dialouge>().lines = GetRandomLine((int)Lists.GENERIC);
    }

        // Update is called once per frame
        void Update()
    {
        
    }
    string[] GetRandomLine(int index)
    {
        //make this a switch statement
        if (index == (int)Lists.GENERIC)
        {
            return linesListGeneric[Random.Range(0, linesListGeneric.Length)].lines;
        }
        else
        {
            return linesListGeneric[Random.Range(0, linesListGeneric.Length)].lines;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform target = other.gameObject.transform;
            Vector3 targetPostition = new Vector3(target.position.x,
                                       this.transform.position.y,
                                       target.position.z);
            this.transform.LookAt(targetPostition);

            if (isGeneric)
            {
                GetComponent<dialouge>().lines = GetRandomLine((int)Lists.GENERIC);
            } else
            {
                if (Random.Range(0, 100) < questInfoRate)
                {
                    GetComponent<dialouge>().lines = GetRandomLine(questIndex);
                }
            }

        }
    }
    }
