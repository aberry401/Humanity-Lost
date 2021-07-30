using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InventoryWriter : MonoBehaviour
{
    public void WriteToFile(Chip c)
    {
        string filePath = Application.dataPath + "\\Chips.txt";

        if (!File.Exists(filePath))
            File.Create(filePath);
        string projName;
        string weapName;

        if (c.getProjectile() == null)
            projName = "NOPROJ";
        else
            projName = c.getProjectile().name;

        if (c.getWeapon() == null)
            weapName = "NOWEAP";
        else
            weapName = c.getWeapon().name;

        Debug.Log("Saving " + c.getName());
        Debug.Log(c.getSpeed());
        Debug.Log(c.getDamage());
        Debug.Log(projName);
        Debug.Log(weapName);

        File.AppendAllText(filePath, c.getName() + "," + c.getType() + "," + c.getSpeed() + "," + c.getDamage() + ","
            + projName + "," + weapName + '\n');
    }

    public void PopulateInventory(ChipInventory inv)
    {
        string currentLine;
        string[] splitLine = new string[6];
        string name;
        Chip.BodyParts chipType;
        float speed;
        float damage;
        string projToFind;
        string weapToFind;
        GameObject projectile;
        GameObject weapon;
        Chip c;
        List<Chip> chipsToAdd = new List<Chip>();
        string filePath = Application.dataPath + "\\Chips.txt";
        if (!File.Exists(filePath))
            File.Create(filePath);

        StreamReader sr = new StreamReader(filePath);

        while(!sr.EndOfStream)
        {
            currentLine = sr.ReadLine();
            splitLine = currentLine.Split(',');
            name = splitLine[0];
            switch (splitLine[1])
            {
                case "HEAD":
                    chipType = Chip.BodyParts.HEAD;
                    break;
                 case "CHEST":
                    chipType = Chip.BodyParts.CHEST;
                    break;
                case "ARM":
                    chipType = Chip.BodyParts.ARM;
                    break;
                case "LEG":
                    chipType = Chip.BodyParts.LEG;
                    break;
                default:
                    chipType = Chip.BodyParts.HEAD;
                    break;
            }

            try { speed = float.Parse(splitLine[2]); }
            catch { speed = 0; }

            try { damage = float.Parse(splitLine[3]); }
            catch { damage = 0; }

            projToFind = splitLine[4];
            weapToFind = splitLine[5];

            if (projToFind != "NOPROJ")
                try { projectile = GameObject.Find(projToFind); }
                catch { projectile = null; }
            else
                projectile = null;

            if (weapToFind != "NOWEAP")
                try { weapon = GameObject.Find(weapToFind); }
                catch { weapon = null; }
            else
                weapon = null;

            c = new Chip(name, chipType, speed, damage, projectile, weapon);
            Debug.Log("112: " , weapon);
            chipsToAdd.Add(c);
           
        }
        sr.Close();
        inv.AddFromFile(chipsToAdd);
    }
}
