using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mouseFollower : MonoBehaviour
{
    private float rotationY = 0f;
    private float rotationX = 0f;
    private float sensitivity = 2f;
    public GameObject followTarget;
    private dialougeController d;
    private newController player;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        d = GetComponent<dialougeController>();
        player = GameObject.Find("samplePlayer").GetComponent<newController>();
    }

    // Update is called once per frame
    void Update()
    {

        //Cursor.lockState = CursorLockMode.Locked;
        //--------------MouseLook and player rotation--------------//
        if (!d.talking)
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivity;
            rotationY = Mathf.Clamp(rotationY, -70, 70);

            rotationX += Input.GetAxis("Mouse X") * sensitivity;



           // followTarget.transform.eulerAngles = new Vector3(-rotationY, rotationX, followTarget.transform.eulerAngles.z);


            if (!Input.GetKey(KeyCode.LeftAlt))
            {
           //     transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotationX, transform.localEulerAngles.z);
                //TODO - change this to vector3.lerp by saving previous values or rework with animation
            }

            //  transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), rotationX, 0) * Time.deltaTime * speed);
            // transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);

            //--------------MouseLook and player rotation--------------//
        }
        if (Input.GetKey(KeyCode.Escape) || d.talking || player.inMenu || player.dead || SceneManager.GetActiveScene().name == "menu" || player.isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    
}

