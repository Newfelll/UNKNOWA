using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightSenseManager : MonoBehaviour
{   
    

    public GameObject growObject;
    public GameObject shrinkObject;

    public LineRenderer growLine, shrinkLine;
    public GameObject growIndicator, shrinkIndicator;

    public GameObject door;

    public Vector2 growMagnitudeThreshold;
    public Vector2 shrinkMagnitudeThreshold;
    public Vector3 doorInitialPosition;
    public Vector3 doorDesiredPosition;

    bool shrink, grow;
    public bool puzzleActive;


    Material mat;


    MaterialPropertyBlock mpb;
    public MaterialPropertyBlock Mpb
    {
        get
        {
            if (mpb == null)
            {
                mpb = new MaterialPropertyBlock();
            }
            return mpb;
        }
    }
    void Start()
    {   puzzleActive = true;
        doorInitialPosition = door.transform.localScale;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Visible.isPuzzleActive)
        {


            if (puzzleActive)
            {
                CheckWeights();
            }
        }
    }


    void CheckWeights() 
    {     
        Debug.Log(growObject.transform.localScale.magnitude);
        Debug.Log(shrinkObject.transform.localScale.magnitude);
       if(growObject.transform.localScale.magnitude>growMagnitudeThreshold.x && growObject.transform.localScale.magnitude<growMagnitudeThreshold.y)
        {    
            grow = true;
            Mpb.SetColor("_EmissionColor", Color.green*4);

           growLine.material.EnableKeyword("_EMISSION");
           growIndicator.GetComponent<Renderer>().SetPropertyBlock(Mpb);
       }
       else
        {   
            Mpb.SetColor("_EmissionColor", Color.green);
            grow = false;
            growLine.material.DisableKeyword("_EMISSION");
            growIndicator.GetComponent<Renderer>().SetPropertyBlock(Mpb);
       }

       if(shrinkObject.transform.localScale.magnitude>shrinkMagnitudeThreshold.x&& shrinkObject.transform.localScale.magnitude<shrinkMagnitudeThreshold.y)
        {       
                Mpb.SetColor("_EmissionColor", Color.green*4);
                shrink = true;
                shrinkLine.material.EnableKeyword("_EMISSION");
                shrinkIndicator.GetComponent<Renderer>().SetPropertyBlock(Mpb);
          }
          else
            
        {
                 Mpb.SetColor("_EmissionColor", Color.green);
                shrink = false;
                shrinkLine.material.DisableKeyword("_EMISSION");
                shrinkIndicator.GetComponent<Renderer>().SetPropertyBlock(Mpb);
        }
    
          if(shrink && grow)
        {
                door.transform.localScale=Vector3.MoveTowards(door.transform.localScale,doorDesiredPosition,Time.deltaTime);
          }
          else
        {
                door.transform.localScale =Vector3.MoveTowards(door.transform.localScale,doorInitialPosition,Time.deltaTime);
          }
    }

    public void OpenDoor()
    {   puzzleActive = false;

        door.transform.localScale = doorDesiredPosition;
    }

  
}
