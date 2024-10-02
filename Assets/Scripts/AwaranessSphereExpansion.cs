using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AwaranessSphereExpansion : MonoBehaviour
{   public float radiusExpansionRate = 10f;
    public float multiplier = 2f;
 
    public float timeToExpand = 1f;
    public GameObject particleObject;

    
    
    
   public void ExpandSphere()
    {

        transform.localScale = transform.localScale * radiusExpansionRate;
        particleObject.transform.localScale = particleObject.transform.localScale* radiusExpansionRate;

        radiusExpansionRate += multiplier;
        
        

    }
}
