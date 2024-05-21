using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveGoal : MonoBehaviour
{
    [SerializeField] private int maxPoint; 

    
    public PlayerMovement playerMovement;

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        
        if (playerMovement.totalScore == maxPoint)
        {
            gameObject.SetActive(true);
        }
    }
}
