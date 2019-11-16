using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public float slowDownFactor;
    public float slowDownDuration;
    public bool foundSecret;
    public int lettersCollected;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

    }

    // Update is called once per frame
    void Update()
    {

        if (slowDownDuration > 0)
        {
            slowDownDuration -= Time.unscaledDeltaTime;
            
        }
        else
        {
            Time.timeScale = 1;
        }






    }

    public static void startSlowMo(float duration,float speed)
    {
        Time.timeScale = speed;

        instance.slowDownDuration = duration;
        
        

    }
}

