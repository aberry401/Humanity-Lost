using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBoss : Enemy
{
    public GameObject player;
    public GameObject tankGun;
    public float hp;
    public float timer;

    public GameObject bullet;
    public Transform bulletSpawn;

    public GameObject smoke1;
    public GameObject smoke2;

    // Start is called before the first frame update
    void Start()
    {
        hp = 300;
        player = GameObject.FindGameObjectWithTag("Player");
        smoke1.SetActive(false);
        smoke2.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.localScale.x > 0)
        {
            timer += Time.deltaTime;
            var lookPos = player.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2f);

            if (timer > 4)
            {
                Shoot();
                timer = 0;
            }
            
            if(hp < 200)
            {
                smoke1.SetActive(true);
            }
            if (hp < 100)
            {
                smoke2.SetActive(true);
            }





            if (dead)
            {
                Destroy(gameObject);
                PlayerPrefs.SetInt("TankFightDone", 1);

            }

        }
    }



    void Shoot()
    {
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 50)
        {
            Instantiate(bullet, bulletSpawn);
            GetComponent<AudioSource>().Play();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ChipProjectile"))
        {
            Debug.Log("HITTTTT");
            Chip c = other.GetComponent<ChipProjectile>().getAssociatedChip();
            if (c.canDestroy)
            {
                Debug.Log("hit for " + c.getDamage());
                hp -= c.getDamage();
                


                if (hp <= 0)
                {
                    dead = true;
                }
            }
        }
       
    }

}
