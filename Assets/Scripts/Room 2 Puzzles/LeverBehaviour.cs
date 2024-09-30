using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LeverBehaviour : MonoBehaviour,IInteractable
{
    public GameEvent leverPulled;
    

    

    public bool isPulled = false;
    public float time = 10f;

    

    // Update is called once per frame
   
   


    public void Interact()
    {   if (isPulled)
        {
            return;
        }
        isPulled = true;
        leverPulled.TriggerEvent();
        SFXSoundManager.Instance.PlayButtonSFX();
        StartCoroutine(ResetLever());
    }


    IEnumerator ResetLever()
    {   
        float timer=0;
         Vector3 initialPos = transform.localEulerAngles;
        
         Vector3 targetPos = new Vector3(initialPos.x, initialPos.y, 180);
        

         while (timer < 0.5f)
         {

             transform.localEulerAngles = Vector3.Lerp(initialPos, targetPos, timer / 0.5f);


             
             timer += Time.deltaTime;

             yield return null;
         }
        

        transform.localEulerAngles = targetPos;

         timer = 0;


         
     /*    while (timer < time)
         {

             transform.localEulerAngles = Vector3.Lerp(targetPos, initialPos, timer / time);

             timer+= Time.deltaTime;

             yield return null;
         }
        transform.localEulerAngles = initialPos;*/
         

        
        
       
        

    }


  
}
