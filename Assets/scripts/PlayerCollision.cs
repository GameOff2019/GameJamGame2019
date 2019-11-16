using System;
using UnityEngine;
 

public class PlayerCollision : MonoBehaviour
{
    private PlayerController pc;
    private Animator _animator;
    private bool onGround;
    public bool onTramp;
    void Start()
    {
        pc = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
        if(Physics.BoxCast(transform.position,new Vector3(5,5,5),Vector3.down,Quaternion.identity))
        {
            onGround = false;
            
        }else
        {
            onGround = true;
        }

        _animator.SetBool("isJumping",!onGround);
    }

    private void Update()
    {
        
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
            default:
                //Destroy(gameObject);
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
