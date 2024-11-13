using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollapse : MonoBehaviour
{

    [SerializeField] private float collapseDelay = 0.5f;
    [SerializeField] private float respawnDelay = 1.5f;  

    private Collider2D platformCollider;
    private SpriteRenderer platformRenderer;
    private Collider2D platformTrigger;

    void Awake()
    {
        platformCollider = transform.parent.Find("Platform").GetComponent<BoxCollider2D>();
        platformRenderer = transform.parent.Find("Platform").GetComponent<SpriteRenderer>();
        platformTrigger = GetComponent<BoxCollider2D>();
    }

    private void PlatformEnabled(bool enabled){
        platformCollider.enabled = enabled;
        platformRenderer.enabled = enabled;
        platformTrigger.enabled = enabled;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Collapse());
        }
    }


    private IEnumerator Collapse(){
        // Debug.Log("Collapse");
        yield return new WaitForSeconds(collapseDelay);

        PlatformEnabled(false);

        yield return new WaitForSeconds(respawnDelay);

        PlatformEnabled(true);
    }
}