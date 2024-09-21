using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWayBehaviour : MonoBehaviour
{
    
    private int index = 0;
    public int keyCount;
  


   public void EnableKeys()
    {
        transform.GetChild(index).gameObject.SetActive(true);
        

        index++;

        if (index ==keyCount )
        {   
            transform.GetChild (0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);

            transform.GetChild(index).gameObject.SetActive(true);
        }
    }
  
}
