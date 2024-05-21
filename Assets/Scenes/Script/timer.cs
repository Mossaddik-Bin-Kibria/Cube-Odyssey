using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour
{
    private float currentTime ;
    public float targetTime ; // Set the time after which the game should stop
    public TMP_Text stopwatchText;
    public GameObject gameOverPanel;
    public GameObject restartButton;

    //public TMP_Text gameText; 

    // Start is called before the first frame update
    void Start()
    {
       currentTime= targetTime;
       stopwatchText.text = FormatTime(currentTime);
       gameOverPanel.SetActive(false);
        // gameText.gameObject.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
           currentTime -= Time.deltaTime;

            stopwatchText.text = FormatTime(currentTime);

            // Check if the target time is reached
            if (FormatTime(currentTime)=="00:00")
            {
                 // Stop the game or perform other actions
                // gameText.gameObject.SetActive(true);
                // gameText.text="Game Over";
                Time.timeScale = 0f; // Stop the game
                SceneManager.LoadSceneAsync(2);

                // gameOverPanel.SetActive(true);
                // Debug.Break();  
            }
    }
    // Format time as minutes:seconds
    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // public void RestartGame()
    // {
    //     Debug.Log(FormatTime(currentTime));
    //     Time.timeScale = 1f;
    //     SceneManager.LoadScene("prototype"); 
    // }

    
}
