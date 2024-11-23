using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    //[SerializeField] PlayerHealth playerHealth;
    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerHealth playerHealth;
    private PlayerData playerData;

    private void Awake(){
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    // private void Start(){
    //     player = 
    // }

    public void ResetGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Reset Level");
        SetDefaultPlayerData();
        
    }

    public void SetDefaultPlayerData(){
        playerData.attackCoolDown = 0;
        playerData.attackDamage = 0;
        playerData.attackRange = 0;
        playerData.canDoubleJump = false;
        playerData.currentHealth = 4;
        playerData.maxHealth = 4;
        player.SetPlayerData(playerData);
        playerHealth.SetPlayerData(playerData);
    }

}
