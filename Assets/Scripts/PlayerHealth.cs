using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealth = 5f;
    [SerializeField] TextMeshProUGUI healthText;
    private float currentHealth;
    private void Awake(){
        currentHealth = maxHealth;
    }

    private void Start(){
        healthText.text = currentHealth.ToString();
    }

    public void Damage(float damage){
        currentHealth -= damage;
        healthText.text = currentHealth.ToString();
        if (currentHealth <= 0){
            Die();
        }
    }

    private void Die(){
        Destroy(gameObject);
        //restart level
    } 
}
