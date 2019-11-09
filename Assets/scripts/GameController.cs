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
        Time.timeScale += (1f / slowDownDuration) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);





    }

    public static void startSlowMo(float duration,float speed)
    {
        Time.timeScale = speed;
        Time.fixedDeltaTime = Time.timeScale * 0.2f;
        instance.slowDownDuration = duration;
        
        

    }
}

