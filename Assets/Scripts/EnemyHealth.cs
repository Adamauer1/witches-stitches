using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealth = 3f;
    private float currentHealth;
    private void Awake(){
        currentHealth = maxHealth;
    }
    public void Damage(float damage){
        currentHealth -= damage;
        if (currentHealth <= 0){
            Die();
        }
    }

    private void Die(){
        Destroy(gameObject);
    }
}
