using Invector.vCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipInventory : MonoBehaviour
{
    private inventoryScreen screen;
    private InventoryWriter writer;
    public Chip headEquipped;
    public Chip chestEquipped;
    public Chip armEquipped;
    public Chip legEquipped;
    public GameObject player;
    public GameObject[] weapons;

    public List<Chip> headParts = new List<Chip>();
    public List<Chip> chestParts = new List<Chip>();
    public List<Chip> armParts = new List<Chip>();
    public List<Chip> legParts = new List<Chip>();
    public List<Chip> allChips = new List<Chip>();

    private float time;

    private void Awake()
    {
        writer = new InventoryWriter();
        screen = GameObject.Find("Inventory Screen").GetComponent<inventoryScreen>();
        time = Time.time;
    }

    private void Update()
    {

    }

    public void Add(Chip c)
    {
        switch (c.getType())
        {
            case Chip.BodyParts.HEAD:
                headParts.Add(c);
                break;
            case Chip.BodyParts.CHEST:
                chestParts.Add(c);
                break;
            case Chip.BodyParts.ARM:
                armParts.Add(c);
                break;
            case Chip.BodyParts.LEG:
                legParts.Add(c);
                break;
        }
        allChips.Add(c);
        screen.AddToDrop(c);
        writer.WriteToFile(c);
    }

    public void AddFromFile(List<Chip> chips)
    {
        foreach(Chip c in chips) {
            switch (c.getType())
            {
                case Chip.BodyParts.HEAD:
                    headParts.Add(c);
                    break;
                case Chip.BodyParts.CHEST:
                    chestParts.Add(c);
                    break;
                case Chip.BodyParts.ARM:
                    armParts.Add(c);
                    break;
                case Chip.BodyParts.LEG:
                    legParts.Add(c);
                    break;
            }
            screen.AddToDrop(c);
            allChips.Add(c);
        }
    }

    //EQUIP METHODS
    public void EquipHead(string target)
    {
        foreach(Chip c in headParts)
        {
            if (c.getName() == target)
            {
                headEquipped = c;
                c.equipped = true;
                c.getWeapon().SetActive(true);
                PlayerPrefs.SetString("HeadChip", c.getName());
            }
        }
    }

    public void EquipChest(string target)
    {
        foreach(Chip c in chestParts)
        {
            if(c.getName() == target)
            {
                chestEquipped = c;
                c.equipped = true;
            //    c.getWeapon().SetActive(true); chest never have weap
                PlayerPrefs.SetString("ChestChip", c.getName());
            }
        }
    }

    public void EquipArm(string target)
    {
        foreach (Chip c in armParts)
        {
            if (c.getName() == target)
            {
                if(armEquipped != null)
                    armEquipped.getWeapon().SetActive(false);
                armEquipped = c;
                c.equipped = true;
                Debug.Log("here is weapon", c.getWeapon());
                c.getWeapon().SetActive(true);
                PlayerPrefs.SetString("ShoulderChip", c.getName());
            }
        }
    }

    public void EquipLeg(string target)
    {
        foreach (Chip c in legParts)
        {
            if (c.getName() == target)
            {
                legEquipped = c;
                c.equipped = true;
                c.getWeapon().SetActive(true);
                PlayerPrefs.SetString("LegChip", c.getName());
            }
        }
    }

    //UNEQUIP METHODS
    public void UnequipHead()
    {
        headEquipped.equipped = false;
        headEquipped = null;
    }

    public void UnequipChest()
    {
        chestEquipped.equipped = false;
        chestEquipped = null;
        PlayerPrefs.SetString("ChestChip", "");
    }

    public void UnequipArm()
    {
        if(armEquipped.getWeapon() != null)
        {
            armEquipped.getWeapon().SetActive(false);
        }
        armEquipped.equipped = false;
        armEquipped = null;
    }

    public void UnequipLeg()
    {
        legEquipped.equipped = false;
        legEquipped = null;
    }

    //USE METHODS
    public void UseHead()
    {
        if(headEquipped != null)
        {

        }
    }

    public void UseChest()
    {
        if (chestEquipped != null)
        {
            
        }
    }

    public void UseArm()
    {
        if (armEquipped != null)
        {
            //if ((armEquipped.getName() == "Spark" | armEquipped.getName() == "Scattershot" | armEquipped.getName() == "Acid Bubbles")
            //    && !armEquipped.getProjectile().activeSelf)
            //{
            //    GameObject spark = armEquipped.getProjectile();
            //    Debug.Log("melee");
            //    spark.GetComponent<ChipProjectile>().AssociateWithChip(armEquipped);
            //    armEquipped.canDestroy = true;
            //    StartCoroutine(UseMelee(spark));
            //}


            if (armEquipped.getName() == "Gun")
            {
                Transform spawn = GameObject.Find("bulletSpawnNew").transform;

                float next = 0.2f;

                Rigidbody b;
                if (Time.time > time + next)
                {
                    //Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.501F, 0.501F, 0));

                    //Vector3 targetPoint;
                    //LayerMask mask = LayerMask.GetMask("bullet");

                    //if (Physics.Raycast(ray, out RaycastHit hit, mask))
                    //{
                    //    targetPoint = hit.point;
                    //}
                    //else
                    //{
                    //    targetPoint = ray.GetPoint(1000);
                    //}

                    GameObject g = Instantiate(armEquipped.getProjectile(), spawn.position, spawn.rotation);
                    g.GetComponent<ChipProjectile>().AssociateWithChip(armEquipped);
                    
                    
                    b = g.GetComponent<Rigidbody>();
                    armEquipped.canDestroy = true;
                    g.SetActive(true);
                    b.useGravity = true;
                    b.velocity = spawn.forward * armEquipped.getSpeed();
                    
                    //  b.transform.position += transform.TransformDirection(Vector3.up * 0.10f);
                    // b.velocity = transform.forward * b.GetComponent<Chip>().shootSpeed;
                    Destroy(g, 2.5f);
                    time = Time.time;
                }
            }
            if (armEquipped.getName() == "lightningGun")
            {
                Transform spawn = GameObject.Find("bulletSpawnNew2").transform;

                float next = 1.2f;

                Rigidbody b;
                if (Time.time > time + next)
                {
                    //Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.501F, 0.501F, 0));

                    //Vector3 targetPoint;
                    //LayerMask mask = LayerMask.GetMask("bullet");

                    //if (Physics.Raycast(ray, out RaycastHit hit, mask))
                    //{
                    //    targetPoint = hit.point;
                    //}
                    //else
                    //{
                    //    targetPoint = ray.GetPoint(1000);
                    //}

                    GameObject g = Instantiate(armEquipped.getProjectile(), spawn.position, spawn.rotation, spawn);
                    g.GetComponent<ChipProjectile>().AssociateWithChip(armEquipped);


                  
                    armEquipped.canDestroy = true;
                   
                  

                    //  b.transform.position += transform.TransformDirection(Vector3.up * 0.10f);
                    // b.velocity = transform.forward * b.GetComponent<Chip>().shootSpeed;
                    Destroy(g, 2.5f);
                    time = Time.time;
                }
            }
            if (armEquipped.getName() == "acidGun")
            {
                Transform spawn = GameObject.Find("bulletSpawnNew3").transform;

                float next = 1.2f;

                Rigidbody b;
                if (Time.time > time + next)
                {
                    //Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.501F, 0.501F, 0));

                    //Vector3 targetPoint;
                    //LayerMask mask = LayerMask.GetMask("bullet");

                    //if (Physics.Raycast(ray, out RaycastHit hit, mask))
                    //{
                    //    targetPoint = hit.point;
                    //}
                    //else
                    //{
                    //    targetPoint = ray.GetPoint(1000);
                    //}

                    GameObject g = Instantiate(armEquipped.getProjectile(), spawn.position, spawn.rotation, spawn);
                    g.GetComponent<ChipProjectile>().AssociateWithChip(armEquipped);



                    armEquipped.canDestroy = true;



                    //  b.transform.position += transform.TransformDirection(Vector3.up * 0.10f);
                    // b.velocity = transform.forward * b.GetComponent<Chip>().shootSpeed;
                    Destroy(g, 2.5f);
                    time = Time.time;
                }
            }
        } else
        {
            Debug.Log("111: null proj when right clicking");
        }
    }

    IEnumerator UseMelee(GameObject obj)
    {
        obj.SetActive(true);
        Debug.Log("player attacking!");
        yield return new WaitForSeconds(1.2f);
        obj.SetActive(false);
    }

    public void UseLeg()
    {
        if(legEquipped != null)
        {
            vThirdPersonController player = GameObject.FindGameObjectWithTag("Player").GetComponent<vThirdPersonController>();
            if (legEquipped.getName() == "Spring Boots")
            {
                
                //player.SetMoveDirectionY(player.GetJumpSpeed() * 3);
                player.jumpHeight = 10;
            } else
            {
                player.jumpHeight = 4;
            }
        }
    }
}
