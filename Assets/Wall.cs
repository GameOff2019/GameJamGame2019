using System;
using UnityEngine;
//each level will be a separate scene

public class Wall : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        //Destroy(other.gameObject);
        //Restart Level
    }
}
