using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//script that rotates the spinner 
//the spinner body should be kinematic which will allow it to move at a constant velocity.
public class Spin : MonoBehaviour
{
    [SerializeField] private float angularSpeed;
    private Rigidbody rb;
    void Start()
    { }

    void Update()
    {
        transform.Rotate(new Vector3(0,0,angularSpeed));
    }

    
}
