using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoal : MonoBehaviour
{
    [SerializeField] int nextLevelIndex;

    private void OnTriggerEnter2D(Collider2D collider){
        //temp
        if (collider.gameObject.CompareTag("Player")){
            StartCoroutine(GameManager.instance.NextLevel(nextLevelIndex));
        }
    }
}
