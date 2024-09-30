using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Vector3 moveDirection;
    public float moveSpeed = 5f;
    public int startingPosSign = 1;
    Transform playerParent;
    
    void Awake()
    {    
        moveDirection = Vector3.forward*startingPosSign;
    }

    // Update is called once per frame
   

    private void OnTriggerEnter(Collider other)
    {   if (other.gameObject.CompareTag( "MovingPlatform"))
        {   
          
            moveDirection *= -1;
            
        }
     
    
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerParent=collision.transform.parent;
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(playerParent);
        }
    }


    private void FixedUpdate()
    {
        transform.position += moveDirection * moveSpeed ;
    }
}
