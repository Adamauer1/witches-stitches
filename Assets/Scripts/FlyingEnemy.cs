using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Animations;

public class FlyingEnemy : MonoBehaviour, IEnemy
{
    private Transform player;
    [SerializeField] private float shootingRange = 5f;
    [SerializeField] private float chaseRange = 15f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float fireRate = 3f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask contactLayer;
    private float nextFireTime = 0f;

    public AIPath aIPath;

    public AIDestinationSetter aIDestinationSetter;
    private Animator animator;
    private bool canAttack;

    private void Awake(){
        aIPath = GetComponent<AIPath>();
        aIDestinationSetter = GetComponent<AIDestinationSetter>();
    }
    private void Start(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //aIPath = GetComponent<AIPath>();
        aIDestinationSetter.target = player;
        animator = GetComponent<Animator>();
        canAttack = true;
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
            aIPath.enabled = false;
            animator.SetBool("IsFlying", false);
        }
        else if (distanceToPlayer > shootingRange && distanceToPlayer < chaseRange){
            // MoveTowardsPlayer();
            aIPath.enabled = true;
            animator.SetBool("IsFlying", true);
        }
        else if (distanceToPlayer < shootingRange && CanSeePlayer()) {
            aIPath.enabled = false;
            animator.SetBool("IsFlying", false);
            if (canAttack){
                canAttack = false;
                animator.SetTrigger("Attack");
            }
        }
    }

    private bool CanSeePlayer(){
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, shootingRange, contactLayer);
        if (hit.collider != null && hit.collider.CompareTag("Player")){
            // Debug.Log(hit);
            return true;
        }
        return false;
    }

    // manual movement to player
    // private void MoveTowardsPlayer(){
    //     // Vector2 direction = (player.position - transform.position).normalized;
    //     transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

    // }

    private void Shoot(){
        // if (Time.time > nextFireTime){
            // nextFireTime = Time.time + fireRate;
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Vector2 direction = (player.position - firePoint.position).normalized;
            projectile.GetComponent<Rigidbody2D>().velocity =  moveSpeed * 3 * direction;
            StartCoroutine(ResetAttack());
        // }
    }

    private IEnumerator ResetAttack(){
        yield return new WaitForSeconds(fireRate);
        canAttack = true;
    }

    public void Move()
    {
        throw new System.NotImplementedException();
    }

    public void Attack()
    {
        throw new System.NotImplementedException();
    }

}
