using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float shootingRange = 5f;
    [SerializeField] private float chaseRange = 15f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float fireRate = 3f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    private float nextFireTime = 0f;

    public AIPath aIPath;
    private void Start(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (player == null){
            return;
        }
        //depending on default direction of the sprite
        if (aIPath.desiredVelocity.x >= 0.01f){
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (aIPath.desiredVelocity.x <= -0.01) {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > chaseRange){

        }
        else if (distanceToPlayer > shootingRange && distanceToPlayer < chaseRange){
            // MoveTowardsPlayer();
        }
        else {
            Shoot();
        }
    }

    private void MoveTowardsPlayer(){
        // Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

    }

    private void Shoot(){
        if (Time.time > nextFireTime){
            nextFireTime = Time.time + fireRate;
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Vector2 direction = (player.position - firePoint.position).normalized;
            projectile.GetComponent<Rigidbody2D>().velocity = direction * moveSpeed;
        }
    }
}
