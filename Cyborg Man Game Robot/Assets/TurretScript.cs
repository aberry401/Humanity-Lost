using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : Enemy
{
    public GameObject turrethead;
    public GameObject player;
    public float timer;
    public GameObject bullet;
    public GameObject bulletspawn;
    public string prefToSet;
    public AudioSource a;
    public float hp;

    public GameObject smoke1;
    // Start is called before the first frame update
    void Start()
    {
        smoke1.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player"); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.localScale.x > 0)
        {
            timer += Time.deltaTime;
            if (timer > 3)
            {
                Shoot();
                timer = 0;
            }
            if(hp < 101)
            {

                smoke1.SetActive(true);
            }
            Vector3 targetPostition = new Vector3(player.transform.position.x,
                                        turrethead.transform.position.y,
                                        player.transform.position.z);
            turrethead.transform.LookAt(targetPostition);
            turrethead.transform.rotation = Quaternion.Euler(turrethead.transform.rotation.x, turrethead.transform.rotation.y + 180, turrethead.transform.rotation.z);



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
            a.Play();
            Instantiate(bullet, bulletspawn.transform);
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
