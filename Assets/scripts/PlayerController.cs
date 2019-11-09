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
        Vector3 movement = new Vector3(x, 0, z);
        //make it a Unit Vector so that the total magnitude is moveSpeed
        movement.Normalize();
        
        
        
        rb.AddForce(movement.x*moveSpeed,0,movement.z*moveSpeed);
        
        
        
        
        

    }
}
