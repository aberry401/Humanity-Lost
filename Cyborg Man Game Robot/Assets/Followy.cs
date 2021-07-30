using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Followy : MonoBehaviour
{
    private float rotationY = 0f;
    private float sensitivityZ = 2f;
    public float limits;
    public float offset;
    public float invert;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotationY += Input.GetAxis("Mouse Y") * sensitivityZ;
        rotationY = Mathf.Clamp(rotationY, -1 * limits, limits);
        transform.localEulerAngles = new Vector3(rotationY * -1 * invert + offset , transform.localEulerAngles.y, -transform.localEulerAngles.z);

    }
}
