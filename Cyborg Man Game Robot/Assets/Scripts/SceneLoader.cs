using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public string SceneName;
    public bool changeVar;
    public string varToChange;
    public int valueToSet;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
          
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.F))
        {
            if (other.CompareTag("Player"))
            {
                GameObject.Find("Data").GetComponent<Data>().SavePos();
                SceneManager.LoadScene(SceneName);
                if (changeVar)
                {
                    PlayerPrefs.SetInt(varToChange, valueToSet);
                }
            }
        }
    }
}
