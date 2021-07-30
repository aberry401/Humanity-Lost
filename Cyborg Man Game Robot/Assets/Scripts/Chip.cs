using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip
{

    private BodyParts type;
    private string chipName;
    private float shootSpeed;
    private float dmg;

    private GameObject weapon;
    private GameObject projectile;
    public bool canDestroy;
    public bool equipped;

    
    public enum BodyParts
    {
        HEAD,
        CHEST,
        ARM,
        LEG
    }

    public Chip(string n, BodyParts t, float s, float d, GameObject p, GameObject w)
    {
        chipName = n;
        type = t;
        shootSpeed = s;
        dmg = d;
        if (p != null)
        {
            projectile = p;
        }
        if(w != null)
        {
            Debug.Log("Creating chip with weapon " + w);
            weapon = w;
        }
    }

    private void Update()
    {
        if (canDestroy)
        {
          //  gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * 2.5f);
          if(chipName == "Gun")
            projectile.GetComponent<Rigidbody>().AddRelativeForce(Vector3.left * 0.4f);
        }
    }

    private void Start()
    {
        //grabbable = true;
    }

    public BodyParts getType()
    {
        return type;
    }

    public string getName()
    {
        return chipName;
    }

    public float getSpeed()
    {
        return shootSpeed;
    }

    public float getDamage()
    {
        return dmg;
    }

    public GameObject getProjectile()
    {
        return projectile;
    }

    public GameObject getWeapon()
    {
        return weapon;
    }
}
