using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoDestroy : MonoBehaviour
{
    public GameObject hide;
    public bool isHide;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (isHide)
        {
            hide.SetActive(false);

        } else
        {
            hide.SetActive(true);
        }
        
    }
}
