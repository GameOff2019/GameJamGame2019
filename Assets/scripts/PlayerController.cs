using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        
        float z = Input.GetAxis("Vertical");
        //right hand rule to get perp vector
        Vector3 movement = Vector3.Cross(Camera.main.transform.forward,Vector3.down)*x+Camera.main.transform.forward*z;
        
        //make it a Unit Vector so that the total magnitude is moveSpeed
        movement.Normalize();



        rb.AddForce(movement.x * moveSpeed, 0, movement.z * moveSpeed);

    }

    private void OnCollisionEnter(Collision other)
    {
        GameController.startSlowMo(2,0.5f);
    }
}
