using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class controller : MonoBehaviour
{
    Rigidbody player;
    public float speed, sprintSpeed;
    private ChipInventory inventory;
    public GameObject followTarget;
    public Animator anim;
    

    
    public float timer;
    public bool stepping;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<ChipInventory>();
        player = GetComponent<Rigidbody>();
       
        
    }

    // Update is called once per frame
    void Update()
    {


        



    }

    void FixedUpdate()
    {
        //  Debug.Log(player.velocity);
        stepping = false;
        if (Input.GetKey(KeyCode.LeftControl))
        {
            inventory.UseChest();
        }
          
        //Vector3 fwd = feetCol.transform.TransformDirection(Vector3.forward);
        //Debug.DrawRay(feetCol.transform.position, fwd * 0.1f, Color.green);

        //Vector3 fwd2 = highcol.transform.TransformDirection(Vector3.forward);
        //Debug.DrawRay(highcol.transform.position, fwd2 * 0.1f, Color.blue);

        //Vector3 fwd3 = highcol.transform.TransformDirection(Vector3.forward);
        //Debug.DrawRay(highcol.transform.position, fwd2 * 0.2f, Color.red);

        //if (Physics.Raycast(feetCol.transform.position, fwd, 0.1f) && !Physics.Raycast(highcol.transform.position, fwd2, 0.1f) &&!Physics.Raycast(highcol.transform.position, fwd2, 0.2f) && Input.GetKey(KeyCode.W))
        //{
        //    //transform.position = new Vector3 (transform.position.x, transform.position.y + 0.2f, transform.position.z);
        //    if (player.velocity.y < 1)
        //    {
        //        player.AddRelativeForce(Vector3.up * speed / 12);
        //    }
        //    Debug.Log("1");
        //    stepping = true;
        //}
        ////--------------------------------------------------------------------//
        // fwd = feetCol2.transform.TransformDirection(Vector3.forward);
        //Debug.DrawRay(feetCol2.transform.position, fwd * 0.1f, Color.green);

        // fwd2 = highcol2.transform.TransformDirection(Vector3.forward);
        //Debug.DrawRay(highcol2.transform.position, fwd2 * 0.1f, Color.blue);

        // fwd3 = highcol2.transform.TransformDirection(Vector3.forward);
        //Debug.DrawRay(highcol2.transform.position, fwd2 * 0.2f, Color.red);

        //if (Physics.Raycast(feetCol2.transform.position, fwd, 0.1f) && !Physics.Raycast(highcol2.transform.position, fwd2, 0.1f) && !Physics.Raycast(highcol2.transform.position, fwd2, 0.2f) && Input.GetKey(KeyCode.D))
        //{
        //    // transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        //    if (player.velocity.y < 1)
        //    {
        //        player.AddRelativeForce(Vector3.up * speed / 12);
        //    }
        //    Debug.Log("2");
        //    stepping = true;
        //}

        ////--------------------------------------------------------------------//
        //fwd = feetCol3.transform.TransformDirection(Vector3.forward);
        //Debug.DrawRay(feetCol3.transform.position, fwd * 0.1f, Color.green);

        //fwd2 = highcol3.transform.TransformDirection(Vector3.forward);
        //Debug.DrawRay(highcol3.transform.position, fwd2 * 0.1f, Color.blue);

        //fwd3 = highcol3.transform.TransformDirection(Vector3.forward);
        //Debug.DrawRay(highcol3.transform.position, fwd2 * 0.2f, Color.red);

        //if (Physics.Raycast(feetCol3.transform.position, fwd, 0.1f) && !Physics.Raycast(highcol3.transform.position, fwd2, 0.1f) && !Physics.Raycast(highcol3.transform.position, fwd2, 0.2f) && Input.GetKey(KeyCode.A))
        //{
        //    // transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        //    if (player.velocity.y < 1)
        //    {
        //        player.AddRelativeForce(Vector3.up * speed / 12);
        //    }
        //    Debug.Log("3");
        //    stepping = true;
        //}

        ////--------------------------------------------------------------------//
        //fwd = feetCol4.transform.TransformDirection(Vector3.forward);
        //Debug.DrawRay(feetCol4.transform.position, fwd * 0.1f, Color.green);

        //fwd2 = highCol4.transform.TransformDirection(Vector3.forward);
        //Debug.DrawRay(highCol4.transform.position, fwd2 * 0.1f, Color.blue);

        //fwd3 = highCol4.transform.TransformDirection(Vector3.forward);
        //Debug.DrawRay(highCol4.transform.position, fwd2 * 0.2f, Color.red);

        //if (Physics.Raycast(feetCol4.transform.position, fwd, 0.1f) && !Physics.Raycast(highCol4.transform.position, fwd2, 0.1f) && !Physics.Raycast(highCol4.transform.position, fwd2, 0.2f) && Input.GetKey(KeyCode.S))
        //{
        //    //transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        //    if (player.velocity.y < 1)
        //    {
        //        player.AddRelativeForce(Vector3.up * speed / 12);
        //    }
        //    Debug.Log("4");
        //    stepping = true;
        //}

     



        // Debug.Log(player.velocity.magnitude);
        if (player.velocity.magnitude < 12)
        {
            if (Input.GetKey(KeyCode.W))
            {
                player.AddRelativeForce(Vector3.forward * speed * Time.deltaTime);
                anim.SetBool("forward", true);
            }
            if (Input.GetKey(KeyCode.A))
            {
                player.AddRelativeForce(Vector3.left * speed * Time.deltaTime);
                anim.SetBool("left", true);
            }
            if (Input.GetKey(KeyCode.S))
            {

                player.AddRelativeForce(Vector3.back * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                player.AddRelativeForce(Vector3.right * speed * Time.deltaTime);
                anim.SetBool("right", true);
            }

            
        }
        if (Input.GetKey(KeyCode.Space))
        {

            player.AddRelativeForce(Vector3.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            inventory.UseChest();
        }
        if (!Input.GetKey(KeyCode.W))
        {
            anim.SetBool("forward", false);
        }
        if (!Input.GetKey(KeyCode.A))
        {
            anim.SetBool("left", false);
        }
        if (!Input.GetKey(KeyCode.D))
        {
            anim.SetBool("right", false);
        }
    }

}
