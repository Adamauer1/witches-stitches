using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyPipes : MonoBehaviour
{
    [SerializeField] private Transform topPipe;
    [SerializeField] private Transform bottomPipe;
    [SerializeField] private float speed;
    public float gap;

    [SerializeField] private float leftCameraEdge;

    private void Start(){
        topPipe.position += Vector3.up * gap / 2;
        bottomPipe.position += Vector3.down * gap / 2;
        // leftCameraEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    private void Update(){
        transform.position += speed * Time.deltaTime * Vector3.left;

        // if (transform.position.x < leftCameraEdge){
        //     Destroy(gameObject);
        // }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }
        // increase score
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerFlappyController>().IncreaseScoreCount();
    }
}
