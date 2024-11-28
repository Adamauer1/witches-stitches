using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoss : MonoBehaviour, IEnemy
{
private Transform player;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float chaseRange = 15f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float attackRate = 3f;
    // [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask contactLayer;
    [SerializeField] private Transform attackTransform;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float attackDamage;
    private float nextAttackTime = 0f;
    private Animator animator;
    private Collider2D[] targetsHit;
    public bool canAttack;

    // public AIPath aIPath;
    private void Start(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        canAttack = true;
    }
    void Update()
    {
        if (player == null){
            return;
        }
        Move();
        //depending on default direction of the sprite
        // if (aIPath.desiredVelocity.x >= 0.01f){
        //     transform.localScale = new Vector3(-1f, 1f, 1f);
        // }
        // else if (aIPath.desiredVelocity.x <= -0.01) {
        //     transform.localScale = new Vector3(1f, 1f, 1f);
        // }

    }

    private bool CanSeePlayer(){
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, attackRange, contactLayer);
        if (hit.collider != null && hit.collider.CompareTag("Player")){
            // Debug.Log(hit);
            return true;
        }
        return false;
    }

    // manual movement to player
    private void MoveTowardsPlayer(){
        Vector2 direction = (player.position - transform.position).normalized;
        if (direction.x >= 0.01f){
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (direction.x <= -0.01) {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

    }

    public void Move()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > chaseRange){
            animator.SetBool("IsWalking", false);
            // aIPath.enabled = false;

        }
        else if (distanceToPlayer > attackRange && distanceToPlayer < chaseRange){
            MoveTowardsPlayer();
            animator.SetBool("IsWalking", true);
            // aIPath.enabled = true;
        }
        else if (distanceToPlayer < attackRange) {
            animator.SetBool("IsWalking", false);
            //aIPath.enabled = false;
            //Attack();
            // Debug.Log(canAttack);
            if (canAttack){
                // StartCoroutine(TriggerAttack());
                canAttack = false;
                animator.SetTrigger("Attack");
            }
        }
    }

    public IEnumerator TriggerAttack(){
        yield return new WaitForSeconds(1);
        canAttack = false;
        animator.SetTrigger("Attack");
    }
    public void Attack()
    {
        // Debug.Log("Attacking");
        targetsHit = Physics2D.OverlapBoxAll(attackTransform.position, new Vector2(5f, 5f), 0, enemyLayer);
        for (int i = 0; i < targetsHit.Length; i++){
            // Debug.Log(targetsHit[i].collider.gameObject);
            //IDamageable damageable = targetsHit[i].collider.gameObject.GetComponent<IDamageable>();
            IDamageable damageable = targetsHit[i].gameObject.GetComponent<IDamageable>();

            damageable?.Damage(attackDamage);
        }
        canAttack = true;
    }

    private void ResetAttack(){
        //StartCoroutine(CanAttackAgain());
        canAttack = true;
    }

    private IEnumerator CanAttackAgain(){
        yield return new WaitForSeconds(2);
        canAttack = true;
    }

    private void OnDrawGizmos(){
        //Gizmos.DrawLine(attackTransform, origin + direction * distance);
        Gizmos.DrawWireCube(attackTransform.position, new Vector3(5f,5f,0));
    }
}
