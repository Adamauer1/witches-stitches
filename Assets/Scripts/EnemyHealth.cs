using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealth = 3f;
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
        currentHealth -= damage;
        canTakeDamage = false;
        if (currentHealth <= 0)
        {
            //canTakeDamage = false;
            animator.SetTrigger("Death");
        }
        animator.SetTrigger("Hurt");
        // canTakeDamage = false;
    }

    private void Die(){
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.AddCoins(coinAward);
        Destroy(gameObject);
    }

    private void ResetCanDamage(){
        canTakeDamage = true;
    }
}
