using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Restart : MonoBehaviour
{
  public void RestartGame()
{
    Time.timeScale = 1f; // Reset the time scale
    SceneManager.LoadSceneAsync(1);

    // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}

}

