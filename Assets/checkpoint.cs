using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    PlayerMovement playerMovement;
    // Start is called before the first frame update
    private void Awake()
    {
        playerMovement= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMovement.UpdateCheckpoint(transform.position);
        }
    }

    // Update is called once per frame
    
}
