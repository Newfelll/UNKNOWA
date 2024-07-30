using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AwaranessSphereExpansion : MonoBehaviour
{   public float radiusExpansionRate = 10f;
    public float multiplier = 2f;
 
    public float timeToExpand = 1f;

    
    
    
   public void ExpandSphere()
    {
            
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(transform.localScale.x+radiusExpansionRate,
        transform.localScale.y+radiusExpansionRate, transform.localScale.z+radiusExpansionRate), timeToExpand);


        radiusExpansionRate += multiplier;
        

    }
}
