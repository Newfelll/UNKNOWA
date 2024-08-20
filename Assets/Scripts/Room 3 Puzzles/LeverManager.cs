using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverManager : MonoBehaviour
{
    private int leverPressed = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LeverPressed()
    {
        leverPressed++;
        if (leverPressed == 4)
        {
            Debug.Log("All levers pressed");
            //open door
            
        }
    }

   public  void LeverReset()
    {
        leverPressed--;
    }
}
