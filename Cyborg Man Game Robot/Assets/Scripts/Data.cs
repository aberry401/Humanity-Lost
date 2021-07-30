using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Data : MonoBehaviour
{
    public GameObject player;

    public GameObject spawn;

   
    // Start is called before the first frame update
    void Awake()
    {
       
        SceneManager.sceneLoaded -= OnSceneLoaded;

        // Add the listener to be called when a scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
        spawn = GameObject.Find("Spawn");
        
        player = GameObject.Find("samplePlayer");
        if (spawn != null && PlayerPrefs.GetFloat((SceneManager.GetActiveScene().name + "y"), -69f) == -69f)
        {
            player.transform.position = spawn.transform.position;
        }
        else
        {
            player.transform.position = new Vector3(PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "x")
                , PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "y")
                , PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "z"));
            Debug.Log("vec3" + new Vector3(PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "x")
                , PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "y")
                , PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "z")) + SceneManager.GetActiveScene().name);
        }
        Debug.Log("startinggg");
    }

    // Update is called once per frame
    public void SavePos()
    {

        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "x", player.transform.position.x);
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "y", player.transform.position.y);
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "z", player.transform.position.z);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Awake();
       
        Debug.Log("Re-Initializing", this);
        
    }
    void FixedUpdate()
    {
        









    }

}
