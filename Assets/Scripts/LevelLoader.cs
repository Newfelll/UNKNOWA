using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1.5f;
    public GameObject endText;
    

    private void Start()
    {
       
    }

    public void LoadLevel(int LevelID)
        {
            
        
            Time.timeScale = 1;
            StartCoroutine(LoadLevelCoroutine(LevelID));
        }


    IEnumerator LoadLevelCoroutine(int LevelID)
    {
        if (LevelID == 4)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
            endText.SetActive(true);
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(0);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
        else
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);

            SceneManager.LoadScene(LevelID);
            transition.SetTrigger("End");
        }
    }


    
}
