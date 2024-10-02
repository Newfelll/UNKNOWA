using UnityEngine;
using UnityEngine.UI;

public class Visible : MonoBehaviour
{
    public static bool isPuzzleActive;

    public Camera mainCamera;
    public Transform player;
    private GameObject indicator;
    public GameObject growingIndicator;
    public GameObject shrinkingIndicator;
    public enum ScaleAxis { XYZ,X, Y, Z };
    public ScaleAxis scaleAxis;


    
    private Transform thisObj;
    private bool isObjectInView;


    
    [SerializeField] private float growScaleMultiplier = 1;
    [SerializeField] private float growSpeed = 1f;
    [SerializeField] private float shrinkScaleMultiplier = 0.1f;

    [SerializeField] private bool isGrowing;
    [SerializeField] private float maxScaleMultiplier = 1.5f;
    [SerializeField] private float uiDistance = 10f;


    public Vector3 puzzleSolvedScale;
    Vector3 baseScale;
    Vector3 maxScale;
    Vector3 screenPoint;
    Vector3 screenUiPoint;
    public float magnitude;
    




    void Start()
    {   mainCamera = Camera.main;
        thisObj = transform;
        baseScale = thisObj.localScale;
        maxScale = baseScale * maxScaleMultiplier;

        if (isGrowing)
        {
            indicator= growingIndicator;

        }
        else indicator= shrinkingIndicator;

        player= GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {   if (isPuzzleActive)
        {

            magnitude = thisObj.localScale.magnitude;
            maxScale = baseScale * maxScaleMultiplier;
            CheckVisibility();


            if (isObjectInView && Vector3.Distance(player.transform.position, thisObj.position) < uiDistance)
            {
                screenUiPoint = mainCamera.WorldToScreenPoint(thisObj.position);

                indicator.SetActive(true);
                indicator.transform.position = screenUiPoint;
                if (isGrowing)
                {
                    Grow();

                }
                else
                {
                    Shrink();
                }
            }
            else
            {
                indicator.SetActive(false); 
               
            }
        }
    else
        {   
            indicator.SetActive(false);
        }
    }


    private void CheckVisibility()
    {
       
        
           Vector3 screenPoint = mainCamera.WorldToViewportPoint(thisObj.position);
           

            isObjectInView = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

               
           
    }


    private void Grow()
    {
        float distance = Vector3.Distance(player.position, thisObj.position);
        Vector3 scale = growScaleMultiplier * baseScale / distance;
        if (scale.magnitude > maxScale.magnitude)
        {
            scale = Vector3.ClampMagnitude(scale, maxScale.magnitude);
        }


        switch (scaleAxis)
        {
            case ScaleAxis.XYZ:
                thisObj.localScale = Vector3.MoveTowards(thisObj.localScale, scale, growSpeed);
                break;

            case ScaleAxis.X:
                scale = new Vector3(scale.x, thisObj.localScale.y, thisObj.localScale.z);
                thisObj.localScale = Vector3.MoveTowards(thisObj.localScale, scale, growSpeed);

                break;
            case ScaleAxis.Y:
                scale = new Vector3(thisObj.localScale.x, scale.y, thisObj.localScale.z);
                thisObj.localScale = Vector3.MoveTowards(thisObj.localScale, scale, growSpeed);
                break;
            case ScaleAxis.Z:
                scale = new Vector3(thisObj.localScale.x, thisObj.localScale.y, scale.z);
                thisObj.localScale = Vector3.MoveTowards(thisObj.localScale, scale, growSpeed);
                break;

        }
        
    }

    private void Shrink()
    {
        float distance = Vector3.Distance(player.position, thisObj.position);
        Vector3 scale =  baseScale * distance/ shrinkScaleMultiplier;
        if (scale.magnitude>maxScale.magnitude)
        {
            scale = Vector3.ClampMagnitude(scale, maxScale.magnitude);
        }


        switch (scaleAxis)
        {
            case ScaleAxis.XYZ:
                thisObj.localScale = Vector3.MoveTowards(thisObj.localScale, scale, growSpeed);
                break;

            case ScaleAxis.X:
                scale= new Vector3(scale.x, thisObj.localScale.y, thisObj.localScale.z);
                thisObj.localScale = Vector3.MoveTowards(thisObj.localScale, scale, growSpeed);

                break;
            case ScaleAxis.Y:
                scale = new Vector3(thisObj.localScale.x, scale.y, thisObj.localScale.z);
                thisObj.localScale = Vector3.MoveTowards(thisObj.localScale, scale, growSpeed);
                break;
            case ScaleAxis.Z:
                scale= new Vector3(thisObj.localScale.x, thisObj.localScale.y, scale.z);
                thisObj.localScale = Vector3.MoveTowards(thisObj.localScale, scale, growSpeed);
                break;

        }


    }



}
