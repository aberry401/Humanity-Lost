using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class newController : MonoBehaviour
{
    public GameObject[] weapons;


    public GameObject mapCam;
    public GameObject mainCam;
    public GameObject hudimg;
    //public int maxHealth;
    public int health;
    public int scrap;
    private float nextHit = 0;

    public bool isPaused;
  
    public bool inMenu = false; 
    public bool dead = false;

    public Image dmgIMG;

    public Text healthText;
    public Text scrapText;
    
    private GameObject menu;
    private ChipInventory inventory;
    public GameObject followTarget;
    private dialougeController d;

    public float timer;

    public GameObject PauseMenu;
    public GameObject Loading;

    public GameObject inventoryhud;

    public GameObject hud;
    public GameObject cam1;
    public GameObject cam2;
    public GameObject mm;
    void Start()
    {
        PlayerPrefs.SetInt("maxhp", 200);
        mainCam = Camera.main.gameObject;
        mapCam.SetActive(false);
        isPaused = false;
        inventory = GetComponent<ChipInventory>();
        d = GetComponent<dialougeController>();
        menu = GameObject.Find("Inventory Screen");
        healthText.text = " " + health;
        dmgIMG.CrossFadeAlpha(0, .1f, true);
    }

    public void TurnOffRun()
    {
        StartCoroutine(TurnOnQuick());
    }
    public IEnumerator TurnOnQuick()
    {
        List<GameObject> weaponsOff = new List<GameObject>();
        foreach (GameObject g in weapons)
        {
            if (!g.activeSelf)
            {
                g.SetActive(true);
                weaponsOff.Add(g);

            }


           
           
            
        }
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject g in weaponsOff)
        {
         
            

            g.SetActive(false); 


        }
    }
    private void OnLevelWasLoaded(int level)
    {
        PlayerPrefs.SetInt("maxhp", 200);
        TurnOffRun();
        if (level != SceneManager.GetSceneByName("menu").buildIndex)
        {
            cam1.SetActive(true);
            cam2.SetActive(true);
            hud.SetActive(true);
            mm.SetActive(true);
        }

            if (level != SceneManager.GetSceneByName("GameOver").buildIndex)
        {

            


            dead = false;
            //health = 100;
          
       //     healthText.text = "Health: " + health;
            dmgIMG.CrossFadeAlpha(0, .1f, true);
            isPaused = false;
            mainCam = Camera.main.gameObject;
            mapCam.SetActive(false);
        }
        else
        {
           // scrap = 0;
            health = 100;
        }
    }

    private void Awake()
    {
        
        isPaused = false;
        scrap = PlayerPrefs.GetInt("scrap");
       
       
    }
    public void Resume()
    {
        isPaused = !isPaused;
    }
    public void BackToMenu()
    {
        
        cam1.SetActive(false);
        cam2.SetActive(false);
        hud.SetActive(false);
        mm.SetActive(false);
       

        inventoryhud.GetComponent<inventoryScreen>().headDrop.ClearOptions();
        inventoryhud.GetComponent<inventoryScreen>().chestDrop.ClearOptions();
        inventoryhud.GetComponent<inventoryScreen>().armDrop.ClearOptions();
        inventoryhud.GetComponent<inventoryScreen>().legDrop.ClearOptions();


        inventoryhud.GetComponent<inventoryScreen>().headDrop.AddOptions(new List<string>() {"None"});
        inventoryhud.GetComponent<inventoryScreen>().chestDrop.AddOptions(new List<string>() { "None" });
        inventoryhud.GetComponent<inventoryScreen>().armDrop.AddOptions(new List<string>() { "None" });
        inventoryhud.GetComponent<inventoryScreen>().legDrop.AddOptions(new List<string>() { "None" });

        GetComponent<ChipInventory>().allChips.Clear();


        GetComponent<ChipInventory>().headEquipped = null;
        GetComponent<ChipInventory>().chestEquipped = null;
        GetComponent<ChipInventory>().armEquipped = null;
        GetComponent<ChipInventory>().legEquipped = null;


        

        foreach (Chip c in GetComponent<ChipInventory>().armParts)
        {
            c.getProjectile().gameObject.SetActive(true);
            Debug.Log("113: here is go that we setactive ", c.getProjectile().gameObject);
        }
        GetComponent<ChipInventory>().headParts.Clear();
        GetComponent<ChipInventory>().chestParts.Clear();
        GetComponent<ChipInventory>().legParts.Clear();
        GetComponent<ChipInventory>().armParts.Clear();


        SceneManager.LoadScene("menu");
        

        //  Destroy(gameObject.transform.root.gameObject);
    }
    void Update()
    {
       
        if(PlayerPrefs.GetString("ChestChip") == "hpup")
        {
            PlayerPrefs.SetInt("maxhp", 300);
        } else
        {
            PlayerPrefs.SetInt("maxhp", 200);
        }





        scrapText.text = " " + scrap;
        if (health <= 0)
        {
            health = 0;
            dead = true;
        }
        Scene s = SceneManager.GetSceneByName("GameOver");
        if (dead && SceneManager.GetActiveScene() != s)
        {
            SceneManager.LoadScene("GameOver");
        }
        if (Input.GetKey(KeyCode.M)){
            hudimg.SetActive(false);
            mainCam.SetActive(false);
            mapCam.SetActive(true);
        } else
        {
            hudimg.SetActive(true);
            mainCam.SetActive(true);
            mapCam.SetActive(false);
        }
        healthText.text = " " + health;

        ////////checking playerprefs



        if ((PlayerPrefs.GetInt("blu1", 0) == 1) && (PlayerPrefs.GetInt("blu2", 0) == 1) && (PlayerPrefs.GetInt("door1", 0) == 1) && (PlayerPrefs.GetInt("door2", 0) == 1))
        {
            PlayerPrefs.SetInt("infoquestdone", 1);



        }
        if ((PlayerPrefs.GetInt("car1", 0) == 1) && (PlayerPrefs.GetInt("car2", 0) == 1) && (PlayerPrefs.GetInt("car3", 0) == 1) && (PlayerPrefs.GetInt("car4", 0) == 1))
        {
            PlayerPrefs.SetInt("carquestdone", 1);



        }
        if ((PlayerPrefs.GetInt("turret1", 0) == 1) && (PlayerPrefs.GetInt("turret2", 0) == 1) && (PlayerPrefs.GetInt("turret3", 0) == 1) && (PlayerPrefs.GetInt("turret4", 0) == 1) && (PlayerPrefs.GetInt("turret5", 0) == 1) && (PlayerPrefs.GetInt("turret6", 0) == 1) && (PlayerPrefs.GetInt("turret7", 0) == 1) && (PlayerPrefs.GetInt("turret8", 0) == 1))
        {
            PlayerPrefs.SetInt("turretquestdone", 1);



        }



        /////////////////////////////


        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayerPrefs.SetInt("spawnhunt1", 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlayerPrefs.SetInt("infoquesttake", 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            PlayerPrefs.SetInt("getfood", 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            PlayerPrefs.SetInt("BossFightTank", 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            PlayerPrefs.SetInt("dung1take", 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SceneManager.LoadScene("mechanics making");
        }


        if (Input.GetKeyDown(KeyCode.B)){
            SceneManager.LoadScene("BossFight");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene("FinaleBoss");
        }
        timer += Time.deltaTime;
        Loading.SetActive(false);
        if (isPaused)
        {
            PauseMenu.SetActive(true);
           // Time.timeScale = 0.001f;
        } else
        {
            PauseMenu.SetActive(false);
           // Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            scrap += 15;
        }
        scrapText.text = " " + scrap;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            inventory.UseChest();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            inventory.UseArm();
        }


       
     //   Debug.Log(PlayerPrefs.GetString("currentScene"));
        if (inMenu)
        {
            menu.gameObject.SetActive(true);
        }
        else
        {
            menu.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inMenu = !inMenu;
        }
        
      
       
    }
    IEnumerator DmgEffect(int dmg)
    {
        if(dmg == 0)
        {
            dmg = 20;
        }
        for (int i = 0; i < dmg / 3; i++)
        {
            dmgIMG.CrossFadeAlpha(.7f, .1f, true);
            yield return new WaitForSeconds(Random.Range(0,.3f));
            dmgIMG.CrossFadeAlpha(0, .1f, true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "laser")
        {
            health -= 30;
            if (health <= 0)
            {
                health = 0;
                dead = true;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("pp" + other.tag);
        if (other.tag == "attack")
        {
            Debug.Log("attacked!");
            if (timer > 1)
            {
                Debug.Log("I've taken dmg!");
                timer = 0;
                AiAttack hit = new AiAttack();

                if (other.transform.root.GetComponent<AiAttack>() != null)
                {
                    hit = other.transform.root.GetComponent<AiAttack>();
                }
                if (other.transform.parent.GetComponent<AiAttack>() != null)
                {
                    hit = other.transform.parent.GetComponent<AiAttack>();
                }
                if(other.transform.GetComponent<AiAttack>() != null)
                {
                    hit = other.transform.GetComponent<AiAttack>();
                }
               
               if(hit.damage == 0)
                {
                    health -= 20;
                }else
                {
                    health -= hit.damage;
                }
                   
                    StartCoroutine(DmgEffect(hit.damage));
                    if (health <= 0)
                    {
                        health = 0;
                        dead = true;
                    }
                
           //     healthText.text = "Health: " + health;

                if (dead)
                {
                    SceneManager.LoadScene("GameOver");
                }
                
            }
        }

        if(other.tag == "scrap")
        {
            other.gameObject.SetActive(false);
            Debug.Log("I touched it");
            scrap += (1 + PlayerPrefs.GetInt("extrascrap",0));
        //    scrapText.text = "Scrap: " + scrap;
            PlayerPrefs.SetInt("scrap", scrap);


            if (PlayerPrefs.GetInt("scrapheals",0) > 0)
            {
                if (health + PlayerPrefs.GetFloat("scrapheals") > PlayerPrefs.GetInt("maxhp",200))
                {
                    health = PlayerPrefs.GetInt("maxhp");

                }
                else
                {
                    health += PlayerPrefs.GetInt("scrapheals");
                }
            }
        }
        if (other.tag == "bigscrap")
        {
            other.gameObject.SetActive(false);
            Debug.Log("I touched it");
            scrap += (5 +( PlayerPrefs.GetInt("extrascrap", 0) * 5 ));




      //      scrapText.text = "Scrap: " + scrap;
            PlayerPrefs.SetInt("scrap", scrap);


            if (PlayerPrefs.GetInt("scrapheals", 0) > 0)
            {
                if (health +( PlayerPrefs.GetInt("scrapheals") * 5)> PlayerPrefs.GetInt("maxhp", 200))
                {
                    health = PlayerPrefs.GetInt("maxhp");

                }
                else
                {
                    health +=( PlayerPrefs.GetInt("scrapheals") * 5 );
                }
            }
        }
        if (other.tag == "HP")
        {
            other.gameObject.SetActive(false);
            Debug.Log("I touched it");
           
            if(health > PlayerPrefs.GetInt("maxhp") - 50)
            {
                health = PlayerPrefs.GetInt("maxhp");

            } else
            {
                health += 50;
            }
        }
        if (other.tag == "tank")
        {
            StartCoroutine(DmgEffect(20));
            health -= 50;
            if (health <= 0)
            {
                health = 0;
                dead = true;
            }
        }
        if (other.tag == "turret")
        {
            StartCoroutine(DmgEffect(20));
            health -= 20;
            if (health <= 0)
            {
                health = 0;
                dead = true;
            }
        }
    }
}