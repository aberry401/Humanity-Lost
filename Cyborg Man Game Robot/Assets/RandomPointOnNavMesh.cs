// RandomPointOnNavMesh
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class RandomPointOnNavMesh : MonoBehaviour
{
    public GameObject spawnsystem;
    public Transform target;
    void Start()
    {
        StartCoroutine(Starting());
    }

    IEnumerator Starting()
    {
        yield return new WaitForSeconds(1f);
        target = spawnsystem.GetComponent<SpawnScript>().waypoints
           [Random.Range(0, spawnsystem.GetComponent<SpawnScript>().waypoints.Count - 1)];
        transform.position = new Vector3(target.position.x, transform.position.y,target.position.z);
    }
  
}