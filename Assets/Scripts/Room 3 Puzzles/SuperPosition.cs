using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPosition : MonoBehaviour
{
    public Camera mainCamera;
    public Transform player;
    public GameObject pointObject;
    private Transform thisObj;

    public bool isObjectInView;
    public bool isMoved;
    public bool isVacant;

   
    public SuperPositionManager SuperPositionManager;

    void Start()
    {   
        SuperPositionManager = GetComponentInParent<SuperPositionManager>();

        mainCamera = Camera.main;
        thisObj = transform;

        pointObject=transform.GetChild(0).gameObject;

        if (pointObject.active)
        {
            isVacant = false;
        }
        else
        {
            isVacant = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckVisibility();
    }


    private void CheckVisibility()
    {


        Vector3 screenPoint = mainCamera.WorldToViewportPoint(thisObj.position);

        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
      
            isObjectInView = onScreen;

            if (isObjectInView)
            {   
                isMoved = false;
                Debug.Log("Object is in camera view.");
            }
            else if (!isVacant)
            {   Collapse();
                
                Debug.Log("Object is not in camera view.");
            }
        

    }


    private void Collapse()
    {
        if (isMoved) return;

       SuperPositionManager.Collapse(this);  

    }
}
