using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnContact : MonoBehaviour
{
    public GameObject explosion;
    public TrailRenderer trail;
    // Start is called before the first frame update
    void Start()
    {
        explosion.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionEnter(Collision collision)
    {
        explosion.SetActive(true);
        Destroy(gameObject, .1f);
    }
    void OnDestroy()
    {
        explosion.transform.parent = null;
       




        trail.transform.parent = null;
        trail.autodestruct = true;
        trail = null;
    }
}
