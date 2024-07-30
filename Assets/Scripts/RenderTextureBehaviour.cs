using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextureBehaviour : MonoBehaviour
{
    public CustomRenderTexture renderTexture;
    
    private int indexer = 0;


    private void Start()
    {
        renderTexture.Release();
        renderTexture.width = 30;
        renderTexture.height = 17;
        indexer++;
    }
    public void ChangeResolution()
    {   if(indexer < 5)
        {
            renderTexture.Release();
            renderTexture.width = (int)(renderTexture.width*2) ;
            renderTexture.height = (int)(renderTexture.width*2);
            if (indexer==4)
            {
                renderTexture.width = 1920;
                renderTexture.height = 1080;
            }
            indexer++;
        }
        
    }
   
}
