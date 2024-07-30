using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.StickyNote;

public class ShadowObjControl : MonoBehaviour
{   
    public GameObject shadowObj;
    public List<Transform> points;
    [SerializeField] private int currentPoint;
    [SerializeField] private int correctPoint;
    public bool onCorrectPoint;
    [SerializeField] private bool reverse;
    [SerializeField] private bool moving;
    private Animator anim;

    [SerializeField] private float animationTime = 0.5f;
    private float timer = 0f;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (currentPoint == 3)
        {
            reverse = true;
        }
    }

    // Update is called once per frame
   



    public void MoveObject()
    {   if (!moving&&!GetComponentInParent<PanelValidator>().isSolved)
        {
            

            anim.SetTrigger("Press");

            SFXSoundManager.Instance.PlayButtonSFX();
            if (reverse)
            {
                currentPoint--;
                if (currentPoint <= 0)
                {
                   
                    reverse = false;
                }
            }
            else
            {
                currentPoint++;
                if (currentPoint >= points.Count-1)
                {
                   
                    reverse = true;
                }

            }

            StartCoroutine(Move());
        }
    }


    IEnumerator Move()
    {   moving = true;
        Vector3 desiredPoint = new Vector3(shadowObj.transform.position.x, shadowObj.transform.position.y, points[currentPoint].transform.position.z);

        while (timer < animationTime)
        {


            shadowObj.transform.position = Vector3.Lerp(shadowObj.transform.position, desiredPoint, timer/animationTime);

            timer += Time.deltaTime;
            yield return null;
        }
         timer = 0f;

        moving = false;

        if (currentPoint == correctPoint)
        {
            onCorrectPoint = true;
        }
       else
        {
            onCorrectPoint = false;
        }

        GetComponentInParent<PanelValidator>().CheckAndValidate();
    }
}
