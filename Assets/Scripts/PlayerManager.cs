using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{


    private void OnDisable()
    {
        
    }

    private void OnDestroy()
    {
       //QuitGame(); 
       //ReloadScene();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
           ReloadScene();
        }
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
