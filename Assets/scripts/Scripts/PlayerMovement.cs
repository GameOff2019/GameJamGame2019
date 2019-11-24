using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = .5f;
    public float jumpHeight = 2;
    public float rotationSpeed = 1;

    private Animator anim;
    private Transform head;
    private Rigidbody rb;
    private Vector3 axisInput;
    private Vector3 mouseInput;
    private Ray ray;
    private RaycastHit hit;

    private bool isGrounded;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        head = transform.GetChild(transform.childCount-1).transform;
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // check grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, .5f);
        // check movement
       
        axisInput.x = Input.GetAxis("Horizontal");
        axisInput.y = Input.GetAxis("Vertical");
        // check rotation
        mouseInput.x = Input.GetAxis("Mouse X");
        // check jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2 * Physics.gravity.y), ForceMode.VelocityChange);
        }
        // set animations
        anim.SetBool("isGrounded", isGrounded);
        if(isGrounded)
            anim.SetFloat("hVelGrounded", axisInput.y);
        else
            anim.SetFloat("hVelGrounded", 0);
    }

    private void FixedUpdate()
    {
       
        // move player
        if(isGrounded)
           rb.AddForce(((head.right * axisInput.x) + (head.forward * axisInput.y)) * speed / Time.fixedDeltaTime);
        else
            rb.AddForce(((head.right * axisInput.x) + (head.forward * axisInput.y)) * (speed/2) / Time.fixedDeltaTime);

        // rotate player in the y
        transform.Rotate(0, mouseInput.x, 0);
    }
}
