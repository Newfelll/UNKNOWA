using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InteractionBehaviour : MonoBehaviour
{
    public Transform cam;
    public Image reticle;
    public Sprite interactSprite;
    public Sprite defaultSprite;



    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private float interactionDistance = 3f;


    RaycastHit hit;
    private GameObject hitObject;
    private MelodyPuzzleManage MelodyPuzzleManage;
    private ShadowObjControl ShadowObjControl;
    void Start()
    {
        reticle.sprite = defaultSprite;
    }

    // Update is called once per frame
    void Update()
    {   

        if (Physics.Raycast(cam.position, cam.forward, out hit, interactionDistance, interactionLayer))
        {
            reticle.sprite = interactSprite;

            hitObject = hit.collider.gameObject;
            if (Input.GetMouseButtonDown(0))
            {
                hitObject.GetComponent<IInteractable>().Interact();
                
            }
            

        }
        else reticle.sprite = defaultSprite;
    }
}
