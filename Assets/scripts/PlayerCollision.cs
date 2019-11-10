using UnityEngine;
 

public class PlayerCollision : MonoBehaviour
{
    public bool onTramp = false;
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        { 
            case "Trampoline": 
                onTramp = true;
                GameController.startSlowMo(0.9f,0.2f);
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
