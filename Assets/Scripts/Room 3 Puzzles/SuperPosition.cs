using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SuperPosition : MonoBehaviour
{
    public Camera mainCamera;

    public GameObject pointObject;
    private Transform[] bounds=new Transform[7];
    public Renderer objRenderer;
    public BoxCollider thisBoxCollider;

    public bool insideDoor = false;
    public bool onScreen = false;
    public bool isObjectInView;
    public bool isMoved;
    public bool isVacant;
    public float sphereCastRadius = 0.5f;

    

    List<BoxCollider> boxColliders = new List<BoxCollider>();
    

    private Vector3[] points = new Vector3[8];

    public SuperPositionManager SuperPositionManager;

    RaycastHit hit;

    Vector3 directionToTarget;
    void Awake()
    {   
        SuperPositionManager = GetComponentInParent<SuperPositionManager>();
        mainCamera = Camera.main;
        bounds[0] = this.transform;
        bounds[1] = transform.GetChild(1);
        bounds[2] = transform.GetChild(2);
        bounds[3] = transform.GetChild(3);
        bounds[4] = transform.GetChild(4);
        bounds[5] = transform.GetChild(5);
        bounds[6] = transform.GetChild(6);

        for (int i = 1; i < 5; i++)
        {
            boxColliders.Add(bounds[i].GetComponent<BoxCollider>());
        }

        




        thisBoxCollider = GetComponent<BoxCollider>();
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


    private void Update()
    {
        if (isVacant)
        {
            thisBoxCollider.enabled = false;
            boxColliders.ForEach(boxCollider => boxCollider.enabled = true);
        }
        else
        {
            thisBoxCollider.enabled = true;
            boxColliders.ForEach(boxCollider => boxCollider.enabled = true);
        }
    }
    void FixedUpdate()
    {
        CheckVisibility();

        
    }


    private void CheckVisibility()
    {
       
        
        Vector3 screenPoint;

        foreach (Transform bound in bounds)
        {
            screenPoint = mainCamera.WorldToViewportPoint(bound.position);

            if(screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
            {
                onScreen = true;

                break;
            }
            else
            {
                onScreen = false;
            }
        }



        // onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;



        if (onScreen)
        {
           //Vector3 directionToTarget;

            if (!isVacant) 
            {   

                for(int i=0; i<7; i++)
                {
                    directionToTarget = bounds[i].position - mainCamera.transform.position;
                    if (Physics.Raycast(mainCamera.transform.position, directionToTarget.normalized, out hit))
                    {
                        if (hit.collider.gameObject != this.gameObject && hit.collider.transform.parent.gameObject != this.gameObject)
                        {
                            isObjectInView = false;
                        }
                        else
                        {
                            isObjectInView = true;
                            break;
                        }
                    }
                }
                
                

                 
                 
            }
            else
            {
                for(int i=1; i<7; i++)
                {   
                    directionToTarget = bounds[i].position - mainCamera.transform.position;

                    
                    if(Physics.Raycast(mainCamera.transform.position, directionToTarget.normalized, out hit))
                    {
                        
                        if (hit.collider.gameObject.transform.parent.gameObject !=this.gameObject )
                            {
                            isObjectInView = false;
                            }
                        else
                            {
                            isObjectInView = true;
                            break;
                        
                            }
                    }
                }
            }
        }
        else if(insideDoor) isObjectInView = true;
        else isObjectInView = false;


        if (isObjectInView)
        {   
                isMoved = false;
               
        }
        else if (!isVacant)
        {   Collapse();
                
               
        }
        

    }


    private void Collapse()
    {
        if (isMoved) return;
       
       SuperPositionManager.Collapse(this);  

    }


    public void DisablePuzzle()
    {
        this.enabled = false;
    }



    private void OnTriggerStay(Collider other)
    {   

        
        if (other.gameObject.CompareTag("Player"))
        {
            insideDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            insideDoor = false;
        }
    }

  
}
