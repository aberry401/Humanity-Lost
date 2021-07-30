using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objFunctions : MonoBehaviour
{
    public GameObject temp;
    public string requiredPref = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(PlayerPrefs.GetInt("lamp1"));
            Debug.Log(PlayerPrefs.GetInt("door1"));
            Debug.Log("---");
        }

    }
    public void RunFunction(int index)
    {
        if (requiredPref == "" || PlayerPrefs.GetInt(requiredPref, 0) == 1)
        {
            switch (index)
            {
                case 0:
                    GoToggle();
                    break;
                case 1:
                    OpenDoor();
                    break;
                case 2:
                    OpenShop();
                    break;
                case 3:
                    BankScrap();
                    break;
                case 4:
                    TakeScrap();
                    break;
                case 5:
                    Heal();
                    break;
                case 6:
                   BuyFood();
                    break;
                case 7:
                    GiveScrap();
                    break;
                case 8:
                    CheckInfoQuest();
                    break;
            }
        }
       
        //return;
    }


    void CheckInfoQuest()
    {
        

    }
        void GiveScrap()
    {
        temp = GameObject.Find("samplePlayer");
        temp.GetComponent<newController>().scrap += GetComponent<dialouge>().scrapAmmnt;
        GetComponent<dialouge>().scrapAmmnt = 0;
    }
    void Heal()
    {
        temp = GameObject.Find("samplePlayer");
        temp.GetComponent<newController>().health = PlayerPrefs.GetInt("maxhp");
    }
    void BuyFood()
    {
        temp = GameObject.Find("samplePlayer");
        if(temp.GetComponent<newController>().scrap >= 65)
        {
            PlayerPrefs.SetInt("getfood", 1);
            temp.GetComponent<newController>().scrap -= 65;
        }

    }
        void BankScrap()
    {
        temp = GameObject.Find("samplePlayer");
        PlayerPrefs.SetInt("bankScrap", temp.GetComponent<newController>().scrap);
        temp.GetComponent<newController>().scrap = 0;
    }
    void TakeScrap()
    {
        temp = GameObject.Find("samplePlayer");
        if (PlayerPrefs.GetInt("bankScrap") > 0)
        {
            if(PlayerPrefs.GetInt("bankScrap") < 10)
            {
                temp.GetComponent<newController>().scrap += PlayerPrefs.GetInt("bankScrap");
                PlayerPrefs.SetInt("bankScrap", 0);
              
            } else
            {
                
                PlayerPrefs.SetInt("bankScrap", PlayerPrefs.GetInt("bankScrap") - 10);
                temp.GetComponent<newController>().scrap += 10;
            }
        }
       
    }
    void OpenShop()
    {
        Debug.Log("Opening Shop");
        temp.GetComponent<Shop>().Open();
    }

    void GoToggle()
    {
        if (!TryGetComponent(out dialouge d))
        {
            temp = GetComponentInParent<dialouge>().obj;

        } else
        {
            temp = GetComponent<dialouge>().obj;    
        }

        temp.SetActive(!temp.activeSelf);
    }

    void OpenDoor()
    {
        if (temp == null)
        {
            temp = GetComponent<dialouge>().obj;
            temp.GetComponent<AudioSource>().Play();
        }

        DoorSwing door = temp.gameObject.GetComponent<DoorSwing>();

        if (door == null)
            Debug.Log("door null");

        if (!door.locked && !door.isOpen)
        {
            temp.transform.Rotate(new Vector3(0, 90, 0));
            door.isOpen = true;
        }
        else if(!door.locked)
        {
            temp.transform.Rotate(new Vector3(0, -90, 0));
            door.isOpen = false;
        }
    }
}
