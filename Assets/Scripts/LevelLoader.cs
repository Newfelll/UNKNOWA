using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1.5f;
   

    // Update is called once per frame
    
        public void LoadLevel(int LevelID)
        {
            
        
            Time.timeScale = 1;
            StartCoroutine(LoadLevelCoroutine(LevelID));
        }


    IEnumerator LoadLevelCoroutine(int LevelID)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadSceneAsync(LevelID);
        transition.SetTrigger("End");
    }
}
