using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoal : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;

    private void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.CompareTag("Player")){
            // StartCoroutine(collider.gameObject.GetComponent<PlayerHealth>().Die());
            GameManager.instance.ResetGame();
        }
    }
}
