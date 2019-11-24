using UnityEngine;
using System.Collections;
 
public class cameraFollow : MonoBehaviour {
    public GameObject player;
    private float x = 0.0f;
    private float y = 0.0f;

    public int mouseXSpeedMod;
    public int mouseYSpeedMod;
 
    public float MaxViewDistance = 15f;
    public float MinViewDistance = 1f;
    public int ZoomRate = 20;
    private int lerpRate = 5;
    private float distance = 3f;
    private float desireDistance;
    private float correctedDistance;
    private float currentDistance;
 
    public float cameraTargetHeight = 1.0f;
 
    //checks if first person mode is on
    private bool click = false;
    //stores cameras distance from player
    private float curDist = 0;
 
    // Use this for initialization
    void Start () {
        Vector3 Angles = transform.eulerAngles;
        x = Angles.x;
        y = Angles.y;
        currentDistance = distance;
        desireDistance = distance;
        correctedDistance = distance;
    }
   
    // Update is called once per frame
    void LateUpdate () {
        
        x += Input.GetAxis("Mouse X") * mouseXSpeedMod;
        y -= Input.GetAxis("Mouse Y") * mouseYSpeedMod;
 
        y = ClampAngle (y, -25, 25);
        Quaternion rotation = Quaternion.Euler (y,x,0);
 
        desireDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * ZoomRate * Mathf.Abs(desireDistance);
        desireDistance = Mathf.Clamp (desireDistance, MinViewDistance, MaxViewDistance);
        correctedDistance = desireDistance;
 
        Vector3 position = player.transform.position - (rotation * Vector3.forward * desireDistance);
 
        RaycastHit collisionHit;
        Vector3 cameraTargetPosition = new Vector3 (player.transform.position.x, player.transform.position.y + cameraTargetHeight, player.transform.position.z);
 
        bool isCorrected = false;
        if (Physics.Linecast (cameraTargetPosition, position, out collisionHit)) {
            position = collisionHit.point;
            correctedDistance = Vector3.Distance(cameraTargetPosition,position);
            isCorrected = true;
        }
 
        //?
        //condicion ? first_expresion : second_expresion;
        //(input > 0) ? isPositive : isNegative;
 
        currentDistance = !isCorrected || correctedDistance > currentDistance ? Mathf.Lerp(currentDistance,correctedDistance,Time.deltaTime * ZoomRate) : correctedDistance;
 
        position = player.transform.position - (rotation * Vector3.forward * currentDistance + new Vector3 (0, -cameraTargetHeight, 0));
 
        transform.rotation = rotation;
        transform.position = position;
 
        //player.transform.rotation = rotation;
 
        float cameraX = transform.rotation.x;
        //checks if right mouse button is pushed
        if(Input.GetMouseButton(1))
        {
            //sets CHARACTERS x rotation to match cameras x rotation
            player.transform.eulerAngles = new Vector3(cameraX,transform.eulerAngles.y,transform.eulerAngles.z);
        }
        //checks if middle mouse button is pushed down
        if(Input.GetMouseButtonDown(2))
        {
            //if middle mouse button is pressed 1st time set click to true and camera in front of player and save cameras position before mmb.
            //if mmb is pressed again set camera back to it's position before we clicked mmb 1st time and set click to false
            if(click == false)
            {
                click = true;
                curDist = distance;
                distance = distance - distance - 1;
            }
            else
            {
                distance = curDist;
                click = false;
            }
        }
 
    }
 
    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) {
            angle += 360;      
        }
        if (angle > 360) {
            angle -= 360;      
        }
        return Mathf.Clamp (angle,min,max);
    }
}