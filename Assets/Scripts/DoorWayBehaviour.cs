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
            for (int i = 0; i < keyCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            transform.GetChild(keyCount).gameObject.SetActive(true);
        }
    }
  
}
