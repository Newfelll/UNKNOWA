using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyPad : MonoBehaviour,IInteractable
{
    public MelodyPuzzleManage melodyPuzzleManage;

    void Start()
    {
        melodyPuzzleManage = GetComponentInParent<MelodyPuzzleManage>();
    }
    public void Interact()
    {
        melodyPuzzleManage.PressNote(this.gameObject);
    }


   
}
