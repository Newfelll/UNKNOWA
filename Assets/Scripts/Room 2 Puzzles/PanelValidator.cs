using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelValidator : MonoBehaviour
{
    public List<ShadowObjControl> panelObjects;
    public Renderer indicator;
    public bool isSolved = false;
    public GameEvent onShadowSolved;


    public void CheckAndValidate()
    {
        foreach (ShadowObjControl obj in panelObjects)
        {
            if (!obj.onCorrectPoint)
            {
                return;
            }

        }

        indicator.material.EnableKeyword("_EMISSION");
        SFXSoundManager.Instance.PlayCorrectSFX();
        isSolved = true;
        onShadowSolved.TriggerEvent();

    }




}
