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
    [SerializeField] private UpgradeController upgradeController;
    public int test = 5;
    // private PlayerData playerData;

    private void Awake(){
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
        // SetDefaultPlayerData();
        SpawnPlayer();
    }

    // private void Start(){
    //     SetDefaultPlayerData();
    // }

    public void ResetGame(){
        // SetDefaultPlayerData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Reset Level");
        SpawnPlayer();
        // SetDefaultPlayerData();
        
    }

    public IEnumerator NextLevel(){
        // player.SetDoubleJump(true);
        upgradeController.gameObject.SetActive(true);
        player.SetCanInput(false);
        // upgradeController
        yield return new WaitUntil(() => upgradeController.upgradeSelected);
        upgradeController.upgradeSelected = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SpawnPlayer();
    }


    private void SpawnPlayer(){
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
        player.SetCanInput(true);
    }

    // public void SetDefaultPlayerData(){
    //     playerData.attackCoolDown = 0;
    //     playerData.attackDamage = 0;
    //     playerData.attackRange = 0;
    //     playerData.canDoubleJump = false;
    //     playerData.currentHealth = 4;
    //     playerData.maxHealth = 4;
    //     Debug.Log("load player data");
    //     Debug.Log(playerData);
    //     // player.SetPlayerData(playerData);
    //     // playerHealth.SetPlayerData(playerData);
    // }

    // public PlayerData GetPlayerData(){
    //     return playerData;
    // }

}
