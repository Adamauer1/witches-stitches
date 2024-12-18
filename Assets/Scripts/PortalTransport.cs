using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PortalTransport : MonoBehaviour, IInteractables
{
    [SerializeField] int nextLevelIndex;
    [SerializeField] TextMeshProUGUI interactionDisplay;
    //private bool playerInRange;
    [SerializeField] private PlayerController playerController;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D coll){
        //temp
        if (coll.gameObject.CompareTag("Player")){
            interactionDisplay.gameObject.SetActive(true);
            interactionDisplay.text = "Press E to enter";
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

        interactionDisplay.text = "";
        interactionDisplay.gameObject.SetActive(false);
        playerController.SetPlayerInteract(false);

    }

    public void HandleInteract()
    {
        GameManager.instance.LoadLevel(nextLevelIndex);
    }
}
