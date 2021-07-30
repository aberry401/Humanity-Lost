using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollower : MonoBehaviour
{
    public GameObject objectToFollow;

    public float speed = 2.0f;

    void LateUpdate()
    {
        float interpolation = speed * Time.deltaTime;

        Vector3 position = transform.position;
        position.y = Mathf.Lerp(transform.position.y, objectToFollow.transform.position.y + 6, interpolation);
        position.x = Mathf.Lerp(transform.position.x, objectToFollow.transform.position.x, interpolation);
        position.z = Mathf.Lerp(transform.position.z, objectToFollow.transform.position.z - 6, interpolation);


        transform.position = position;
    }
}
