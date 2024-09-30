using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelValidator : MonoBehaviour
{
    public List<ShadowObjControl> panelObjects;
    public Renderer indicator;
    public bool isSolved = false;
    public GameEvent onShadowSolved;

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

    public void CheckAndValidate()
    {
        foreach (ShadowObjControl obj in panelObjects)
        {
            if (!obj.onCorrectPoint)
            {
                return;
            }

        }
        Mpb.SetColor("_EmissionColor", Color.green * 3);
        indicator.SetPropertyBlock(Mpb);
        SFXSoundManager.Instance.PlayCorrectSFX();
        isSolved = true;
        onShadowSolved.TriggerEvent();

    }




}
