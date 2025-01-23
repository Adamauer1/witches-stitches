using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealth = 3f;
    [SerializeField] float coinAward;
    [SerializeField] private AudioClip deathSound;
    private AudioSource audioSource;
    private float currentHealth;
    private Animator animator;
    private bool canTakeDamage;
    public bool isDead = false;
    private void Awake(){
        currentHealth = maxHealth;
         animator = GetComponent<Animator>();
         canTakeDamage = true;
         audioSource = GetComponent<AudioSource>();
    }
    public void Damage(float damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        canTakeDamage = false;
        if (currentHealth <= 0 && !isDead)
        {
            //canTakeDamage = false;
            isDead = true;
            animator.SetTrigger("Death");
            audioSource.PlayOneShot(deathSound);
            return;
        }
        animator.SetTrigger("Hurt");
        // canTakeDamage = false;
    }

    public void Die(){
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.AddCoins(coinAward);
        Destroy(gameObject);
    }

    private void ResetCanDamage(){
        canTakeDamage = true;
    }
}
