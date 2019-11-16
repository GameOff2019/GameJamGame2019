using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    Transform cam;
    float pitch;
    float rotateSpeed = 2;
    float viewRange = 40;
   
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // get head movment
        pitch -= Input.GetAxis("Mouse Y") * rotateSpeed;
        // clamp from flipping
        pitch = Mathf.Clamp(pitch, -viewRange+20, viewRange);
        // rotate camera in x
        cam.localEulerAngles = new Vector3(pitch, 0, 0);


    }
}
