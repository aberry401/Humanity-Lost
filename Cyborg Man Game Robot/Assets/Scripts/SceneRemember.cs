using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneRemember : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name != PlayerPrefs.GetString("currentScene", "menu"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("currentScene", "menu"));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name != "menu" && SceneManager.GetActiveScene().name != "GameOver")
        {
            PlayerPrefs.SetString("currentScene", SceneManager.GetActiveScene().name);
        }
    }
}
