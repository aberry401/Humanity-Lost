using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsMove : MonoBehaviour
{
    public Rigidbody w1;
    public Rigidbody w2;
    bool hasrun;
    // Start is called before the first frame update
    void Start()
    {
        hasrun = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(hasrun == true)
        {
            MoveWalls();
        }
       
        if(w1.position.z < -30)
        {
            GameObject.Find("samplePlayer").GetComponent<newController>().health -= 2000;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            hasrun = true;
        }
    }

    void MoveWalls()
    {
       
            w1.AddForce(Vector3.back * 10);
            w2.AddForce(Vector3.forward * 10);
        
    }
}
