using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopItem : MonoBehaviour
{
    public string itemName;
    public int quantity;
    public int price;
    public Chip chip;

    public bool isChip;
    public Chip.BodyParts part;
    public float shootSpeed;
    public float dmg;
    public GameObject projectile;
    public GameObject weapon;

    private void Start()
    {
        if(isChip)
        {
            chip = new Chip(itemName, part, shootSpeed, dmg, projectile, weapon);
        }
    }

}
