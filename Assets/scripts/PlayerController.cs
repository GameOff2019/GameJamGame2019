using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    public int maxJumps=2;

    private Rigidbody rb;
    private PlayerCollision pCol;
    public int numJumps;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pCol = GetComponent<PlayerCollision>();


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
        if (pCol.onTramp)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                rb.AddForce(new Vector3(0,moveSpeed,0),ForceMode.Impulse);
            }
            
            
        }
        if (numJumps>0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                rb.AddForce(new Vector3(0,moveSpeed/2,0),ForceMode.Impulse);
                numJumps -= 1;
            }
        }


        
        
        rb.AddForce(movement.x * moveSpeed, 0, movement.z * moveSpeed);
        
        
        //set a maxSpeed so that movement doesnt go out of control
        //rb.velocity=new Vector3(Mathf.Clamp(rb.velocity.x,-5,5),Mathf.Clamp(rb.velocity.y,-5,5),Mathf.Clamp(rb.velocity.z,-5,5));

    }

    
}
