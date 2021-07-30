using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionDetect : MonoBehaviour
{
    public float dmg;
    public string tag;

    public float isp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "enemy")
        {

            if (other.tag == tag)
            {
                Debug.Log("partcol: " + other.name);

                other.GetComponent<NewAI>().hp -= dmg;
                if(isp == 1)
                {
                    other.GetComponent<NewAI>().runPoison();
                }

            }
        }



        else if (other.tag == "Player")
        {

            Debug.Log("partcol: " + other.name);
            if (other.tag == tag)
            {

                other.GetComponent<newController>().health -= (int)dmg;
                if (other.GetComponent<newController>().health <= 0)
                {
                    other.GetComponent<newController>().health = 0;
                    other.GetComponent<newController>().dead = true;
                }

            }
        } else
        {
            if (other.tag == "battery")
            {
                if (other.tag == tag)
                {
                    Debug.Log("partcol: " + other.name);
                    if (other.GetComponent<BatteryScript>() != null)
                    {
                        other.GetComponent<BatteryScript>().hp -= dmg;
                    }
                }
            }
        }
    }
    }

    
