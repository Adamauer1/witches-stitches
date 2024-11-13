using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 5f;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float iFrames = 0.3f;
    private bool canTakeDamage = true;
    private PlayerController playerController;
    private float currentHealth;
    private void Awake(){
        currentHealth = maxHealth;
        playerController = GetComponent<PlayerController>();
    }

    private void Start(){
        healthText.text = currentHealth.ToString();
    }

    public void Damage(float damage){
        if (canTakeDamage){
            currentHealth -= damage;
            if (currentHealth <= 0){
                currentHealth = 0;
                healthText.text = currentHealth.ToString();
                StartCoroutine(Die());
            }
            else{
                healthText.text = currentHealth.ToString();
                StartCoroutine(RunIFrame());
            }
        }
        
    }

    public IEnumerator Die(){
        playerController.SetCanInput(false);
        yield return new WaitForSeconds(0.5f);
        playerController.SetCanInput(true);
        GameManager.instance.ResetGame();
    }

    public IEnumerator RunIFrame(){
        canTakeDamage = false;
        yield return new WaitForSeconds(iFrames);
        canTakeDamage = true;
    }
}
