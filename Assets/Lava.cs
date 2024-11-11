using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision){
        Debug.Log("Touched Lava");
        if(collision.gameObject.CompareTag("Player")){
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
            player.Damage(100);

        };
    }
}
