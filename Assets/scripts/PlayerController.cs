using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] private bool startedJump=false;

    private Rigidbody rb;
    private Animator _animator;
    public bool isJumping;
    private Vector3 axisInput;
    private RaycastHit hit;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        


    }

    private void Update()
    {
        axisInput.x = Input.GetAxis("Horizontal");
        
        axisInput.z = Input.GetAxis("Vertical");
        // check grounded
        isJumping = !Physics.Raycast(transform.position, Vector3.down, out hit, .5f);
        if (isJumping)
        {
            _animator.SetInteger("velGrounded", 0);
        }
        else
        {
            _animator.SetInteger("velGrounded", (int) (axisInput.z + axisInput.x));
        }
        
        _animator.SetBool("isJumping", isJumping);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //right hand rule to get perp vector
        Vector3 movement = Vector3.Cross(Camera.main.transform.forward,Vector3.down)*axisInput.x+Camera.main.transform.forward*axisInput.z;
        
        //make it a Unit Vector so that the total magnitude is moveSpeed
        movement.Normalize();
        // check jump
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            startedJump = true;
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2 * Physics.gravity.y), ForceMode.VelocityChange);
        }

        float dir = 0;
        int l = 0;
        if (axisInput.x != 0)
        {
            l += 1;
            dir += Mathf.Sign(axisInput.x) * 90;
        }
        if (axisInput.z != 0)
        {
            l += 1;
            dir +=  axisInput.z<0? 180:0;
        }
        //integer division by zero lol
        if (l > 0)
        {
            dir /= l;
        }
        transform.rotation=Quaternion.Euler(new Vector3(0,Camera.main.transform.rotation.eulerAngles.y+dir,0));
        rb.AddForce(movement.x * moveSpeed, 0, movement.z * moveSpeed);
        
        

        
        
        
        //set a maxSpeed so that movement doesnt go out of control
        //rb.velocity=new Vector3(Mathf.Clamp(rb.velocity.x,-5,5),Mathf.Clamp(rb.velocity.y,-5,5),Mathf.Clamp(rb.velocity.z,-5,5));

    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Trampoline")&&startedJump)
        {
            Destroy(gameObject);
            
        }

    }
}
