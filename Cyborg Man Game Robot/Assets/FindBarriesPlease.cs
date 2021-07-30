using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindBarriesPlease : MonoBehaviour
{

    public GameObject[] g;


    // Start is called before the first frame update
    void Start()
    {
        g = GameObject.FindGameObjectsWithTag("barrier");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
