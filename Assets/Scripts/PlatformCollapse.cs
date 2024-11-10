using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollapse : MonoBehaviour
{

    [SerializeField] private float collapseDelay = 0.5f;
    [SerializeField] private float respawnDelay = 1.5f;  
    // private float collapseDelayTimer = 0f;
    // private float respawnTimer = 0f;       
    // private bool playerTriggered = false;
    // private bool isCollapsed = false;
    private Collider2D platformCollider;
    private SpriteRenderer platformRenderer;
    private Collider2D platformTrigger;

    void Awake()
    {
        platformCollider = transform.parent.Find("Test").GetComponent<BoxCollider2D>();
        platformRenderer = transform.parent.Find("Test").GetComponent<SpriteRenderer>();
        platformTrigger = GetComponent<BoxCollider2D>();
    }

    // void Update(){
    //     if (playerTriggered){
    //         collapseDelayTimer += Time.deltaTime;
    //         if (collapseDelayTimer >= collapseDelay){
    //             CollapsePlatform();
    //         }
    //     }
    //     if (isCollapsed){
    //         respawnTimer += Time.deltaTime;
    //         if (respawnTimer >= collapseDelay){
    //             RespawnPlatform();
    //         }
    //     }
    // }

    // private void CollapsePlatform(){
    //     isCollapsed = true;
    //     PlatformEnabled(false);
    //     collapseDelayTimer = 0;
    //     playerTriggered= false;

    // }

    // private void RespawnPlatform(){
    //     isCollapsed = false;
    //     respawnTimer = 0;
    //     PlatformEnabled(true);
    // }

    private void PlatformEnabled(bool enabled){
        platformCollider.enabled = enabled;
        platformRenderer.enabled = enabled;
        platformTrigger.enabled = enabled;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            // playerTriggered = true;
            StartCoroutine(Collapse());
        }
    }


    private IEnumerator Collapse(){
        yield return new WaitForSeconds(collapseDelay);

        PlatformEnabled(false);

        yield return new WaitForSeconds(respawnDelay);

        PlatformEnabled(true);
    }
}