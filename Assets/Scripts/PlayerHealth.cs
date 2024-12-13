using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] public float maxHealth = 5f;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float iFrames = 0.3f;
    [SerializeField] private PlayerAnimatorController playerAnimatorController;
    public const string DEATH = "Death";
    private Animator animator;
    private bool canTakeDamage = true;
    private PlayerController playerController;
    public float currentHealth;
    public bool deathOver = false;
    private void Awake(){
        currentHealth = maxHealth;
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        playerAnimatorController = GetComponent<PlayerAnimatorController>();
    }

    private void Start(){
        // maxHealth = GameManager.instance.GetPlayerData().maxHealth;
        // Debug.Log(GameManager.instance.GetPlayerData().maxHealth);
        // Debug.Log(maxHealth);
        currentHealth = maxHealth;
        healthText.text = currentHealth.ToString();
    }

    public void Damage(float damage){
        if (canTakeDamage){
            currentHealth -= damage;
            if (currentHealth <= 0){
                currentHealth = 0;
                healthText.text = currentHealth.ToString();
                Debug.Log("DEAD");
                // StartCoroutine(Die());
                Die();

            }
            else{
                healthText.text = currentHealth.ToString();
                //StartCoroutine(RunIFrame());
            }
        }
        
    }

    public bool IsDead(){
        return currentHealth <= 0;
    }

    public void Die(){
        canTakeDamage = false;
        playerController.SetCanInput(false);
        // zero out movement
        // animator.SetTrigger("death");
        playerAnimatorController.ChangeAnimationState(DEATH);
        // yield return new WaitForSeconds(0.5f);
        // while (!deathOver){
        //     yield return null;
        // }
        // yield return new WaitUntil(() => deathOver);
        // GameManager.instance.ResetGame();
        // playerController.SetCanInput(true);
        // deathOver = false;
    }

    public void SetHealth(float health){
        currentHealth = health;
        healthText.text = currentHealth.ToString();
    }

    public void SetMaxHealth(){
        SetHealth(maxHealth);
    }

    // public IEnumerator RunIFrame(){
    //     canTakeDamage = false;
    //     yield return new WaitForSeconds(iFrames);
    //     canTakeDamage = true;
    // }

    public void DeathOver(){
        GameManager.instance.ResetGame();
        playerController.SetCanInput(true);
        deathOver = false;
        canTakeDamage = true;
    }

    public void SetPlayerData(PlayerData playerData){
        currentHealth = playerData.currentHealth;
        maxHealth = playerData.maxHealth;
    }

    public void UpdateHealthText(){
        healthText.text = currentHealth.ToString();
    }
}
