using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoal : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;

    private void OnTriggerEnter2D(Collider2D collider){
        //temp
        if (collider.gameObject.CompareTag("Player")){
            GameManager.instance.ResetGame();
        }
    }
}