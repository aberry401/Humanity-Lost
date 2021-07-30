using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefChecker : MonoBehaviour
{
    public string prefToCheck, prefToSet;
    public int checkValue, setValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (PlayerPrefs.GetInt(prefToCheck, 90) == checkValue)
        {
            PlayerPrefs.SetInt(prefToCheck, setValue);
            Debug.Log("aaaa");
        }
    }
   
}
