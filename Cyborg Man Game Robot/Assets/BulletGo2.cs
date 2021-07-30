using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGo2 : MonoBehaviour
{
    public float speed;
    public float delay;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Fire", delay);
      //  speed = GetComponent<ChipProjectile>().getAssociatedChip().getSpeed();
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Fire()
    {
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * speed);
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * Random.Range(-100, 100));
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.right * Random.Range(-100, 100));
        Destroy(gameObject, 5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            Destroy(gameObject);
        }
    }
}
