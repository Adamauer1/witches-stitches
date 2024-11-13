using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealth = 5f;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float iFrames = 0.3f;
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
        currentHealth -= damage;
        // healthText.text = currentHealth.ToString();
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

    public IEnumerator Die(){
        // Destroy(gameObject);
        playerController.SetCanInput(false);
        yield return new WaitForSeconds(0.5f);
        // currentHealth = maxHealth;
        // healthText.text = currentHealth.ToString();
        // transform.position = spawnPoint.position;
        playerController.SetCanInput(true);
        GameManager.instance.ResetGame();
        //restart level
    }

    public IEnumerator RunIFrame(){
        canTakeDamage = false;
        yield return new WaitForSeconds(iFrames);
        canTakeDamage = true;
    }
}
