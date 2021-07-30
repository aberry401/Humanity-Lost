using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefsLogic : MonoBehaviour
{
    public GameObject[] barriers;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(PlayerPrefs.GetInt("beforeSewer",0) == 0)
        {
            barriers[0].SetActive(true);
        } else
        {
            barriers[0].SetActive(false);
        }

        


    }
}
