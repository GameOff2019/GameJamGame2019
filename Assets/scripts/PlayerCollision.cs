using UnityEngine;
 

public class PlayerCollision : MonoBehaviour
{
    private PlayerController pc;
    public bool onTramp;
    void Start()
    {
        pc = GetComponent<PlayerController>();

    }
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        { 
            case "Trampoline": 
                onTramp = true;
                pc.numJumps = pc.maxJumps;
                GameController.startSlowMo(0.9f,0.7f);
                break;
            case "Item":
                gameObject.GetComponent<Item>().Collect();
                break;
        }
        
        
        
    }

    private void OnCollisionExit(Collision other)
    {
        switch (other.gameObject.tag)
        { 
            case "Trampoline": 
                onTramp = false;
                break;
            
        }
        
            
    }
        
}
