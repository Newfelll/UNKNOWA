using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPositionManager : MonoBehaviour
{
    

    public List<SuperPosition> collapsePoints=new List<SuperPosition>();


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public  void Collapse(SuperPosition collapsingPoint)
    {   
        List<SuperPosition> vacantPoints = new List<SuperPosition>();
        foreach (SuperPosition point in collapsePoints)
        {
            if (point.isVacant&&!point.isObjectInView) 
            {
                vacantPoints.Add(point);
            }
        }

        if (vacantPoints.Count > 0)
        {
            int randomIndex = Random.Range(0, vacantPoints.Count);
            SuperPosition randomPoint = vacantPoints[randomIndex];
            randomPoint.pointObject.SetActive(true);
            randomPoint.isVacant = false;
            randomPoint.isMoved = true;

            collapsingPoint.pointObject.SetActive(false);
            collapsingPoint.isVacant = true;
            collapsingPoint.isMoved = true;
        }
        else collapsingPoint.isMoved = false;
    }


}