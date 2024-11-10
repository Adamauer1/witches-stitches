using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void Awake(){
    }

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Player")){
            PlayerHealth player = collider.gameObject.GetComponent<PlayerHealth>();
            player.Damage(1);

        };
        Destroy(gameObject);
    }

}
