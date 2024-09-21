using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChannelSwitchButtons : MonoBehaviour, IInteractable
{   
    private Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
   


    public void Interact()
    {
        anim.SetTrigger("Press");
        SFXSoundManager.Instance.PlayButtonSFX();

        transform.parent.GetComponent<LensSwitch>().ChangeChannel(int.Parse(gameObject.name));

    }

}
