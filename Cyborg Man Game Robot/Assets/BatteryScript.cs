using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript : MonoBehaviour
{

   public float hp;
    float maxhp;


    public GameObject normalBat;
    public GameObject brokenBat;
    public GameObject explosionGo;
    // Start is called before the first frame update
    void Start()
    {
        brokenBat.SetActive(false);
        explosionGo.SetActive(false);
        normalBat.SetActive(true);
        
       
        maxhp = hp;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hp > 0)
        {
            GetComponent<Renderer>().material.SetColor("_Color", new Color(250 - ((hp / maxhp * 100) * 2.4f), (hp / maxhp * 100) * 2.4f, 20, .3f));
        }
        if (hp <= 0)
        {
            explosionGo.SetActive(true);
            Destroy(gameObject, 1.5f);
            
        }
        if(hp < (maxhp / 2))
        {
            normalBat.SetActive(false);
            brokenBat.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ChipProjectile"))
        {
            Debug.Log("BATTERYHITTTTT");
            Chip c = other.GetComponent<ChipProjectile>().getAssociatedChip();
            if (c.canDestroy)
            {
                Debug.Log("hit for " + c.getDamage());
                hp -= c.getDamage();
                
            }
            
        }
    }
    void OnParticleCollision(GameObject other)
    {
        if (other.tag != "laser")
        {
            hp -= other.gameObject.GetComponent<collisionDetect>().dmg / 2;
        }
    }
    }
