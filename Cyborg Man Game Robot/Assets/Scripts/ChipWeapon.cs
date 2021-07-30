using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChipWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        SceneManager.activeSceneChanged += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene s1, Scene s2)
    {
     //   gameObject.SetActive(true);
      //  StartCoroutine(TurnItOff());
    }

    IEnumerator TurnItOff()
    {
        yield return new WaitForSeconds(0.5f);
      //  gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
