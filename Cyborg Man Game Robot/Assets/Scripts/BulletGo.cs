using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGo : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        //speed = GetComponent<ChipProjectile>().getAssociatedChip().getSpeed();
        //GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * speed);
        if (speed != 0)
        {
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * Random.Range(-100, 100));
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.right * Random.Range(-100, 100));
        }
        //   Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "player")
        {
            Destroy(gameObject);
        }
    }
}
