using System;
using UnityEngine;
public class LeverTimer : MonoBehaviour
{
    private float timer;

    [SerializeField] private float levelTime;

    private void Update()
    {
        if (timer < levelTime)
        {
            timer += Time.deltaTime;
            
        }
        else
        {
            //restart level
        }
        
    }
}
