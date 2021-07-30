using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipPickup : MonoBehaviour
{
    public string ChipName;
    public Chip.BodyParts ChipType;
    public float ShootSpeed;
    public float Damage;
    public string Weapon;
    public string Projectile;
    public bool ProjectileOff;
    public GameObject proj;
    public GameObject wep;
    private ChipInventory inv;

    void Start()
    {
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<ChipInventory>();
        StartCoroutine(InventoryCheck());
    }

    void Update()
    {
        if (wep == null)
        {
            wep = GameObject.Find(Weapon);
            if (wep != null) { Debug.Log(ChipName + " weapon = " + wep.name); }

            if (proj == null)
            {
                proj = GameObject.Find(Projectile);
                Debug.Log("gofindproj: " + GameObject.Find(Projectile));
                if (proj != null) {
                    Debug.Log(ChipName + " projectile = " + proj.name);
                    if (ProjectileOff)
                    {
                        proj.SetActive(false);
                    }
                } else
                {
                    Debug.Log("111: null error finding projectile");
                }
            }

            if(wep != null) { wep.SetActive(false); }
        }
    }

    IEnumerator InventoryCheck()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (Chip c in inv.allChips)
        {
            if (c.getName() == ChipName)
            {
                Debug.Log("deleting pickup for " + ChipName);
                gameObject.SetActive(false);
            }
            else { Debug.Log(c.getName() + " not equal to " + ChipName); }
        }
    }

    public string getName()
    {
        return ChipName;
    }

    public string getType()
    {
        switch (ChipType)
        {
            case Chip.BodyParts.HEAD:
                return "Head";
            case Chip.BodyParts.CHEST:
                return "Chest";
            case Chip.BodyParts.ARM:
                return "Arm";
            case Chip.BodyParts.LEG:
                return "Leg";
            default:
                return "Unspecified";
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            ChipInventory playerInv = coll.GetComponent<ChipInventory>();
            gameObject.SetActive(false);
            playerInv.Add(new Chip(ChipName, ChipType, ShootSpeed, Damage, proj, wep));
        }
    }
}
