using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DamageUpgrade : MonoBehaviour, IPlayerUpgrade, IInteractables
{
    public string upgradeLabel = "buy Damage Upgrade";
    private float cost = 5;
    [SerializeField] TextMeshProUGUI interactionDisplay;
    [SerializeField] private Image backgroundImage;
    //private bool playerInRange;
    [SerializeField] private PlayerController playerController;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        interactionDisplay.gameObject.SetActive(false);
        backgroundImage.enabled = false;
    }

    public float GetUpgradeCost()
    {
        return 1f;
    }
    public void ApplyUpgrade(){
        //PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.SpendCoins(cost);
        playerController.ApplyDamageUpgrade();
        //GetComponentInParent<UpgradeController>().upgradeSelected = true;
    }
    private void OnTriggerEnter2D(Collider2D coll){
        //temp
        if (coll.gameObject.CompareTag("Player")){
            interactionDisplay.gameObject.SetActive(true);
            backgroundImage.enabled = true;
            interactionDisplay.text = "Press E to buy Damage Upgrade";
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
        if (playerController.GetCoinAmount() >= cost)
        {
            ApplyUpgrade();
        }
    }
}
