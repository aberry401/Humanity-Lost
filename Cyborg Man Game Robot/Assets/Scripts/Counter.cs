using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public Transform[] bots;
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        count = bots.Length;
    }

    // Update is called once per frame
    void Update()
    {
        bots = gameObject.GetComponentsInChildren<Transform>();
        count = bots.Length;
    }
}
