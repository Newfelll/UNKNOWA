using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractionBehaviour : MonoBehaviour
{
    public Transform cam;



    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private float interactionDistance = 3f;


    RaycastHit hit;
    private GameObject hitObject;
    private MelodyPuzzleManage MelodyPuzzleManage;
    private ShadowObjControl ShadowObjControl;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   // For one click interactions

      /*  if (Physics.Raycast(cam.position, cam.forward, out hit, interactionDistance, interactionLayer))
        {
            hitObject = hit.collider.gameObject;
            if (hitObject.CompareTag("Shadow"))
            {
                ShadowObjControl = hitObject.GetComponent<ShadowObjControl>();


                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Pressed");
                    ShadowObjControl.MoveObject();

                    ;

                }
            }
            else if (hitObject.CompareTag("Rgb"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    hitObject.GetComponent<ChannelSwitchButtons>().ButtonPush();

                }
            }
            else
            {
                MelodyPuzzleManage = hitObject.GetComponentInParent<MelodyPuzzleManage>();
                // MelodyPuzzleManage.HiglightSelection();


                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Pressed");
                    MelodyPuzzleManage.PressNote(hitObject);

                    // MelodyPuzzleManage.test(hitObject);

                }
            }

        }else if (hitObject != null)
        {
           //MelodyPuzzleManage.ResetLook();
            hitObject = null;
            MelodyPuzzleManage = null;
        }*/

        if (Physics.Raycast(cam.position, cam.forward, out hit, interactionDistance, interactionLayer))
        {


            hitObject = hit.collider.gameObject;
            if (Input.GetMouseButtonDown(0))
            {
                hitObject.GetComponent<IInteractable>().Interact();
                
            }
            

        }
    }
}
