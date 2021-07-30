using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinner : MonoBehaviour
{
    public float speedx = 360;
    public float speedy = 360;
    public float speedz = 360;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(speedx * Time.deltaTime, speedy * Time.deltaTime, speedz * Time.deltaTime);
    }
}
