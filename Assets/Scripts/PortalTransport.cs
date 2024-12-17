using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTransport : MonoBehaviour
{
    [SerializeField] int nextLevelIndex;
    [SerializeField] GameObject interactionDisplay;
    private bool playerInRange;
    private void OnTriggerEnter2D(Collider2D collider){
        //temp
        if (collider.gameObject.CompareTag("Player")){
            interactionDisplay.SetActive(true);
            //StartCoroutine(GameManager.instance.NextLevel(nextLevelIndex));
            // GameManager.instance.LoadLevel(nextLevelIndex);
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            interactionDisplay.SetActive(false);
        }
    }
    
    public void On
}
