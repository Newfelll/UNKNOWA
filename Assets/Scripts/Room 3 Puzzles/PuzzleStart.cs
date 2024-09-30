using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleStart:MonoBehaviour  
{
    public bool puzzleActive = false;
   
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Visible.isPuzzleActive = true;
        }
        
        
    }

   void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Visible.isPuzzleActive = false;
        }
    }
}
