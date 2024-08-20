using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPositionManager : MonoBehaviour
{
    

    public List<SuperPosition> collapsePoints=new List<SuperPosition>();
    List<SuperPosition> vacantPoints = new List<SuperPosition>();


    void Start()
    { 
        foreach (Transform child in transform)
        {
            collapsePoints.Add(child.GetComponent<SuperPosition>());
        }
    }

    // Update is called once per frame
    

    public  void Collapse(SuperPosition collapsingPoint)
    {   
        vacantPoints.Clear();

        foreach (SuperPosition point in collapsePoints)
        {
            if (point.isVacant&&!point.isObjectInView) 
            {
                vacantPoints.Add(point);
            }
        }

        if (vacantPoints.Count > 0)
        {

            if (collapsingPoint.pointObject == null)
            {   
                
                foreach (SuperPosition point in collapsePoints)
                {
                    point.DisablePuzzle();

                }

                this.enabled = false;

                return;
            }

            collapsingPoint.pointObject.SetActive(false);
            collapsingPoint.isVacant = true;
            collapsingPoint.isMoved = true;


            int randomIndex = Random.Range(0, vacantPoints.Count);
            SuperPosition randomPoint = vacantPoints[randomIndex];
            randomPoint.pointObject.SetActive(true);
            randomPoint.isVacant = false;
            randomPoint.isMoved = true;
           


            
        }
        else collapsingPoint.isMoved = false;
    }


   
}