using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SuperPosedPairs : MonoBehaviour
{
    public List<bool> Vacant = new List<bool>();

    private void OnDrawGizmosSelected()
    {   
       
        foreach (Transform child in transform)
        {
            Vector3 managerPos = transform.position;
            Vector3 childPos = child.transform.position;
            float halfHeight = (managerPos.y - childPos.y) / 2;
            Vector3 offset=Vector3.up*halfHeight;

            
            Gizmos.color = Color.green;
            Gizmos.DrawLine(managerPos, childPos);
            
            
        }



        for (int i = 0; i < Vacant.Count; i++)
        {
            if (Vacant[i])
            {
                transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);
            }
            else transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(true);

        }
    }

    
    

}
