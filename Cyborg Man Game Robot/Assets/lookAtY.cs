using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtY : MonoBehaviour
{

    Transform target;
    public string targetName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x != 0)
        {
            target = GameObject.Find(targetName).transform;
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        }
    }
}
