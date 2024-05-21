using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
 
void start() {
 
}
private void OnTriggerEnter2D(Collider2D collision){
    if (collision.CompareTag("flag")){
            Debug.Log("Player touched the flag. Game Over or Next Level.");
            
            // Example: Pausing the game. Replace or extend this with your actual game over logic
            Time.timeScale = 0;
    }    
}
}
