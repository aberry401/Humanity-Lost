using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenuNewGame : MonoBehaviour
{
    private ChipInventory inv;
    private string filePath;
    public InventoryWriter writer;

    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        filePath = Application.dataPath + "\\Chips.txt";

       // GameObject.Find("New DDOL(Clone)").GetComponent<NoDestroy>().isHide = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            File.Create(filePath);
        }
        if (GameObject.Find("New DDOL(Clone)") == null)
        {
            Instantiate(playerPrefab); // set saved vars here; 
        }

       
        //cut1
        PlayerPrefs.SetInt("cut1", 1);
        GameObject.Find("New DDOL(Clone)").GetComponent<NoDestroy>().isHide = false;
        SceneManager.LoadScene("mechanics making");
    }
    public void Continue()
    {
        
        if (!File.Exists(filePath))
        {
            File.Create(filePath);
        }
        if (GameObject.Find("New DDOL(Clone)") == null)
        {
            Instantiate(playerPrefab); // set saved vars here; 
        }
        GameObject.Find("New DDOL(Clone)").GetComponent<NoDestroy>().isHide = false;
        GameObject.Find("samplePlayer").GetComponent<newController>().TurnOffRun();
        writer.PopulateInventory(GameObject.FindGameObjectWithTag("Player").GetComponent<ChipInventory>());
        SceneManager.LoadScene(PlayerPrefs.GetString("currentScene"));
       
    }
}
