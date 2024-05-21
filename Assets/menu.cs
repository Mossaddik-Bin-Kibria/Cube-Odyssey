using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public void PlayGame()
        {
            SceneManager.LoadSceneAsync(1);
        }   

    public void QuitGame()
        {
            Application.Quit();

            // If running in the Unity editor, stop playing the scene
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        }  
    public void GoLevel2()
        {
        Time.timeScale = 1f; // Reset the time scale
        SceneManager.LoadSceneAsync(4);

        }  

    public void GoLevel3()
        {
            Debug.Log("error");
        Time.timeScale = 1f; // Reset the time scale
        SceneManager.LoadSceneAsync(6);

        } 
}
