using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    private AudioSource aSource;
    // Start is called before the first frame update
    void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Rigidbody>().velocity.magnitude < 0.5f)
        {
            aSource.enabled = false;
        } else
        {
            aSource.enabled = true;
        }
    }
    void step()
    {
        aSource.pitch = Random.Range(0.8f, 1.1f);
        aSource.Play();
    }
}
