using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform endTransform;
    public float speed = 2f;
    private Vector3 target;
    private bool isMoving;

    private void Start(){
        target = endTransform.position;
        isMoving = true;
    }

    private void Update(){
        if (isMoving){
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, target) < 0.01f){
                StartCoroutine(FlipDirection());
                // target = target == startTransform.position ? endTransform.position : startTransform.position; 
            }
        }
    }

    private IEnumerator FlipDirection(){
        isMoving = false;
        yield return new WaitForSeconds(1f);
        target = target == startTransform.position ? endTransform.position : startTransform.position; 
        isMoving = true;
    }
    
}