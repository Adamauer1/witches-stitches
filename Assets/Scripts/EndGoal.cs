using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoal : MonoBehaviour
{
    [SerializeField] int nextLevelIndex;
    [SerializeField] private string crystalName;

    private void OnTriggerEnter2D(Collider2D collider){
        //temp
        if (collider.gameObject.CompareTag("Player")){
            //StartCoroutine(GameManager.instance.NextLevel(nextLevelIndex));
            GameManager.instance.UpdateCrystalCheck(crystalName);
            // GameManager.instance.LoadLevel(nextLevelIndex);
            
        }
    }
}
