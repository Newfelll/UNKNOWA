using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
   
    

    public void OpenLevel(int LevelID)
    {
       
        FindAnyObjectByType<LevelLoader>().LoadLevel(LevelID);
    }


    public void Exit()
    {
        Application.Quit();
    }
}
