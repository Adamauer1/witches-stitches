using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealth = 3f;
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
        if (currentHealth <= 0){
            animator.SetTrigger("Death");
        }
        animator.SetTrigger("Hurt");
        // canTakeDamage = false;
    }

    private void Die(){
        Destroy(gameObject);
    }

    private void ResetCanDamage(){
        canTakeDamage = true;
    }
}
