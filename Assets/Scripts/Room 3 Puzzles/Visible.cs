using UnityEngine;

public class Visible : MonoBehaviour
{
    public Camera mainCamera;
    public Transform player;

    private Transform thisObj;
    private bool isObjectInView;

    [SerializeField]private float scaleMultiplier=1;


    Vector3 baseScale;
    void Start()
    {
        thisObj = transform;
        baseScale = thisObj.localScale;
    }

    void Update()
    {
        CheckVisibility();


        if (isObjectInView)
        {
            thisObj.localScale = scaleMultiplier * baseScale/Vector3.Distance(player.position, thisObj.position);
        }
    }


    private void CheckVisibility()
    {
       
        
            Vector3 screenPoint = mainCamera.WorldToViewportPoint(thisObj.position);

            bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

            if (onScreen != isObjectInView)
            {
                isObjectInView = onScreen;
                if (isObjectInView)
                {
                    Debug.Log("Object is in camera view.");
                }
                else
                {
                    Debug.Log("Object is not in camera view.");
                }
            }
        
    }


    private void Scaling()
    {

    }
}
