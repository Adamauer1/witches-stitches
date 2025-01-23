using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PortalTransport : MonoBehaviour, IInteractables
{
    [SerializeField] int nextLevelIndex;
    [SerializeField] TextMeshProUGUI interactionDisplay;
    [SerializeField] private Image backgroundImage;
    //private bool playerInRange;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Vector3 spawnPointPosition;
    [SerializeField] private String text;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        // interactionDisplay.gameObject.SetActive(false);
        backgroundImage.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D coll){
        //temp
        if (coll.gameObject.CompareTag("Player")){
            interactionDisplay.gameObject.SetActive(true);
            backgroundImage.enabled = true;
            interactionDisplay.text = text;
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
        backgroundImage.enabled = false;
        playerController.SetPlayerInteract(false);

    }

    public void HandleInteract()
    {
        GameManager.instance.LoadLevel(nextLevelIndex, spawnPointPosition);
    }
}
