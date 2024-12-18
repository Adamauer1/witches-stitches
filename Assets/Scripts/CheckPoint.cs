using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private bool hit = false;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collider){
        //temp
        if (collider.gameObject.CompareTag("Player")){
            //StartCoroutine(GameManager.instance.NextLevel(nextLevelIndex));
            // GameManager.instance.LoadLevel(nextLevelIndex);
            if (!hit){
                hit = true;
                GameManager.instance.UpdateSpawnPointLocation(this.transform.position);
                spriteRenderer.color = Color.green;
            }
        }
    }
}
