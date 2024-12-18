using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealth = 10f;
    [SerializeField] float coinAward;
    private float currentHealth;
    private Animator animator;
    private bool canTakeDamage;
    private void Awake(){
        currentHealth = maxHealth;
         animator = GetComponent<Animator>();
         canTakeDamage = true;
    }
    public void Damage(float damage){
        Debug.Log(currentHealth);
        currentHealth -= damage;
        canTakeDamage = false;
        if (currentHealth <= 0){
            animator.SetTrigger("Death");
        }
        else {
            animator.SetTrigger("Hurt");
        }
        // canTakeDamage = false;
    }

    private void Die(){
        GameManager.instance.SpawnCrystal();
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.AddCoins(coinAward);
        Destroy(gameObject);
    }

    private void ResetCanDamage(){
        canTakeDamage = true;
    }
}
