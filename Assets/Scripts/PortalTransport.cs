using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTransport : MonoBehaviour, IInteractables
{
    [SerializeField] int nextLevelIndex;
    [SerializeField] GameObject interactionDisplay;
    //private bool playerInRange;
    [SerializeField] private PlayerController playerController;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D coll){
        //temp
        if (coll.gameObject.CompareTag("Player")){
            interactionDisplay.SetActive(true);
            playerController.SetPlayerInteract(true);
            playerController.SetInteractingGameObject(gameObject);
            //StartCoroutine(GameManager.instance.NextLevel(nextLevelIndex));
            // GameManager.instance.LoadLevel(nextLevelIndex);
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (!coll.gameObject.CompareTag("Player"))
        {
            return;
        }
        interactionDisplay.SetActive(false);
        playerController.SetPlayerInteract(false);

    }

    public void HandleInteract()
    {
        GameManager.instance.LoadLevel(nextLevelIndex);
    }
}
