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
    [SerializeField] private Vector3 spawnPointPosition;
    [SerializeField] private AudioClip[] audioClips;
    private AudioSource audioSource;
    // private PlayerData playerData;
    [SerializeField] public Dictionary<string, bool> crystalCheck;

    private void Awake(){
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
        crystalCheck = new Dictionary<string, bool>()
        {
            {"Fire", false},
            {"Ice", false}
        };
        // SetDefaultPlayerData();
        //SpawnPlayer();
        audioSource = GetComponent<AudioSource>();
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
        if (scene.buildIndex == 1)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerFlappyController>().enabled = false;
            player.GetComponent<PlayerController>().enabled = true;
            
        }
        else if (scene.buildIndex == 2)
        {
            audioSource.Stop();
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }
        else if (scene.buildIndex == 3)
        {
            audioSource.Stop();
            audioSource.clip = audioClips[1];
            audioSource.Play();
        }
        else if (scene.buildIndex == 5)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<PlayerFlappyController>().enabled = true;
            StartCoroutine(player.GetComponent<PlayerFlappyController>().StartFlappyGame());
        }
        // Call your SpawnPlayer function
        // upgradeController = GameObject.FindGameObjectWithTag("Upgrade").GetComponent<UpgradeController>();
        // Debug.Log(upgradeController);
        if (SceneManager.GetActiveScene().buildIndex != 4){
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

    public void LoadLevel(int levelIndex){
        SceneManager.LoadScene(levelIndex);
        // SpawnPlayer();
    }

    


    private void SpawnPlayer(){
        //player.transform.position = GameObject.Find("SpawnPoint").transform.position;
        player.transform.position = spawnPointPosition;
        player.SetCanInput(true);
    }

    public void UpdateSpawnPointLocation(Vector3 newPoint){
        spawnPointPosition = newPoint;
    }

    public void UpdateCrystalCheck(string crystal)
    {
        crystalCheck[crystal] = true;
        LoadLevel(1);
    }

    // public void DisplayFlappyMenu()
    // {
    //     
    // }

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
