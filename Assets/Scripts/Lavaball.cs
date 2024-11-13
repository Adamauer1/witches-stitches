using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lavaball : MonoBehaviour
{
    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform endTransform;
    [SerializeField] private float directionDelay = 1f;
    [SerializeField] private float speed = 5f;
    private Vector3 target;
    private bool isMoving;
    private float currentSpeed;

    private void Start(){
        target = endTransform.position;
        isMoving = true;
    }

    private void Update(){
        if (isMoving){
            float distanceToTarget = Vector3.Distance(transform.position, endTransform.position);
            if (distanceToTarget < 1f){
                currentSpeed = Mathf.Lerp(speed/4, speed, distanceToTarget / 1f);
            }
            else{
                currentSpeed = speed;
            }
            transform.position = Vector3.MoveTowards(transform.position, target, currentSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, target) < 0.01f){
                target = target == startTransform.position ? endTransform.position : startTransform.position; 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Player")){
            PlayerHealth player = collider.gameObject.GetComponent<PlayerHealth>();
            player.Damage(2);

        };
    }
}
