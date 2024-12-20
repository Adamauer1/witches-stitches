using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

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
        //SpawnPlayer();
    }

    private void Start(){
        // GameObject crystal = GameObject.FindWithTag("Crystal");
        // Debug.Log(crystal.name);
    }

    private void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        Debug.Log($"Scene {scene.name} loaded.");
        // Call your SpawnPlayer function
        // upgradeController = GameObject.FindGameObjectWithTag("Upgrade").GetComponent<UpgradeController>();
        // Debug.Log(upgradeController);
        if (SceneManager.GetActiveScene().buildIndex != 3){
            SpawnPlayer();
        }
    }

    public void ResetGame(){
        // SetDefaultPlayerData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Reset Level");
        //SpawnPlayer();
        // playerHealth.currentHealth = playerHealth.maxHealth;
        playerHealth.SetMaxHealth();
        // SetDefaultPlayerData();
        
    }

    public void SpawnCrystal(){
        GameObject crystal = GameObject.FindWithTag("Crystal");
        crystal.GetComponent<BoxCollider2D>().enabled = true;
        crystal.GetComponent<SpriteRenderer>().enabled = true;
    }

    public IEnumerator NextLevel(int levelIndex){
        // player.SetDoubleJump(true);
        player.SetCanInput(false);
        if (levelIndex == 3){
            upgradeController.upgradeSelected = true;
        }
        else{
            upgradeController.gameObject.SetActive(true);
        }
        // upgradeController
        yield return new WaitUntil(() => upgradeController.upgradeSelected);
        upgradeController.upgradeSelected = false;
        upgradeController.gameObject.SetActive(false);
        SceneManager.LoadScene(levelIndex);
        if (levelIndex < 3){
            //SpawnPlayer();
        }
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
