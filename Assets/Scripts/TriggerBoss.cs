using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoss : MonoBehaviour
{   
    public BoxCollider2D boxCollider2D;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            GetComponentInParent<SpriteRenderer>().enabled = true;
            boxCollider2D.enabled = true;
        }
    }
}
