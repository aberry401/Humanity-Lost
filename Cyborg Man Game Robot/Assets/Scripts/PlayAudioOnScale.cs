using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnScale : MonoBehaviour
{
    // Start is called before the first frame update
    bool hasRun;
    void Start()
    {
        hasRun = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.localScale.y > 1 && !hasRun)
        {
            GetComponent<AudioSource>().Play();
            hasRun = true;
        }
    }
}
