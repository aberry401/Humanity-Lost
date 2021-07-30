using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class inventoryScreen : MonoBehaviour
{
    public Dropdown headDrop, chestDrop, armDrop, legDrop;
    private ChipInventory inv;



    // Start is called before the first frame update
    void Awake()
    {
        
        SceneManager.activeSceneChanged += OnSceneLoaded;
        headDrop = GameObject.Find("headDrop").GetComponent<Dropdown>();
        chestDrop = GameObject.Find("chestDrop").GetComponent<Dropdown>();
        armDrop = GameObject.Find("armDrop").GetComponent<Dropdown>();
        legDrop = GameObject.Find("legDrop").GetComponent<Dropdown>();

        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<ChipInventory>();
    }
    private void OnSceneLoaded(Scene s1, Scene s2)
    {
        //string chip;

        //chip = headDrop.options[headDrop.value].text;
      
        //    inv.UnequipHead();
       
        //    inv.EquipHead(chip);
       
           
        //        chip = chestDrop.options[chestDrop.value].text;
       
        //    inv.UnequipChest();
       
        //    inv.EquipChest(chip);
       
           
        //        chip = armDrop.options[armDrop.value].text;
       
        //    inv.UnequipArm();
       
        //    inv.EquipArm(chip);
       
            
        //        chip = legDrop.options[legDrop.value].text;
        
        //    inv.UnequipLeg();
       
        //    inv.EquipLeg(chip);



    }
    public void AddToDrop(Chip c)
    {
        List<string> newOption = new List<string>(1);
        switch (c.getType())
        {
            case Chip.BodyParts.HEAD:
                newOption.Add(c.getName());
                headDrop.AddOptions(newOption);
                break;

            case Chip.BodyParts.CHEST:
                newOption.Add(c.getName());
                chestDrop.AddOptions(newOption);
                break;

            case Chip.BodyParts.ARM:
                newOption.Add(c.getName());
                armDrop.AddOptions(newOption);
                break;

            case Chip.BodyParts.LEG:
                newOption.Add(c.getName());
                legDrop.AddOptions(newOption);
                break;
        }
    }

    public void changeEquip(string box)
    {
        string chip;
        switch (box)
        {
            case "head":
                chip = headDrop.options[headDrop.value].text;
                if (chip == "None")
                    inv.UnequipHead();
                else
                    inv.EquipHead(chip);
                break;
            case "chest":
                chip = chestDrop.options[chestDrop.value].text;
                if (chip == "None")
                    inv.UnequipChest();
                else
                    inv.EquipChest(chip);
                break;
            case "arm":
                chip = armDrop.options[armDrop.value].text;
                if (chip == "None")
                    inv.UnequipArm();
                else
                    inv.EquipArm(chip);
                break;
            case "leg":
                chip = legDrop.options[legDrop.value].text;
                if (chip == "None")
                    inv.UnequipLeg();
                else
                    inv.EquipLeg(chip);
                break;
        }
    }
}
