using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //function that decides the bounce value of the object thats colliding
    private void OnCollisionEnter(Collision other)
    {
        GameObject player = other.gameObject;


    }
}
