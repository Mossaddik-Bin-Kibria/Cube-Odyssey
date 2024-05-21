using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameover : MonoBehaviour
{
    public void RestartGame()
{
    Time.timeScale = 1f; // Reset the time scale

    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
}
