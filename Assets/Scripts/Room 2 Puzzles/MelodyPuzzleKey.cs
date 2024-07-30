using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MelodyPuzzleKey:MonoBehaviour  
{
    public int solvedCount=0;
    public int puzzleCount;
    public GameObject key;


    private void Start()
    {
       
        
    }
    public  void Check()
    {
        solvedCount++;
        if (solvedCount == puzzleCount)
        {
            
           key.SetActive(true);
            
        }
    }
}
