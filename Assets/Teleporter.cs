using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AudioManager audioManager;
public class Teleporter : MonoBehaviour
{
    //private void Aake(){
     //   audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    //}

    [SerializeField] private Transform destination;
    //audioManager.PlaySFX(audioManager.teleport);
    public Transform GetDestination(){
        
        return destination;
    }
}
