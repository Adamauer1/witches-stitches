using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{

    [Header("Input")]
    private bool canInput = true;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 9.5f;
    [SerializeField] private float iceFriction = 0.4f;
    private Vector2 movementInput;

    [Header("Ground Check")]
    // [SerializeField] private Collider2D feet;
    private bool isOnIce = false;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.49f, 0.03f);
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool isGrounded;


    [Header("Jump")]
    [SerializeField] private bool doubleJumpActive = false;
    [SerializeField] private bool canDoubleJump = false;
    [SerializeField] private float jumpHeight = 6.5f;
    private bool triggerJump;
    private bool triggerJumpCut;
    [SerializeField] private float jumpTimeToApex = 0.5f;
    private float jumpForce;
    [SerializeField] private float jumpBuffer = 0.1f;
    private float jumpBufferCounter = 0;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private bool isJumpFalling = false;
    private float coyoteTimer = 0;
    [SerializeField] private float coyoteTime = 0.1f;
    
    
    [Header("Gravity")]
    private bool isFalling;
    [SerializeField] private float maxFallSpeed = 18f;
    private float gravityStrength;
    private float gravityScale;
    [SerializeField] private float jumpCutGravityMult = 3.5f;

    [Header("Attack")]
    [SerializeField] private float attackCoolDown = 1f;
    [SerializeField] private float attackDamage = 1f;
    private float attackTimer = 0f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform attackTransform;
    [SerializeField] private float attackRange = 1.5f;
    // private RaycastHit2D[] targetsHit;
    private Collider2D[] targetsHit;
    private bool triggerAttack = false;
    private Animator animator;

    private Rigidbody2D rb;
    private bool canAttack = true;

    private static PlayerController instance;

    private void Awake (){
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
        rb = GetComponent<Rigidbody2D>();

        gravityStrength = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);
        
        jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;
        
        gravityScale = gravityStrength / Physics2D.gravity.y;
        animator = GetComponent<Animator>();
        canAttack = true;
    }

    private void Update(){
            
            // if (triggerAttack && attackTimer > attackCoolDown){
            if (triggerAttack && canAttack){
                triggerAttack = false;
                // Debug.Log("Attack Triggered");
                attackTimer = 0;
                // Attack();
                canAttack = false;
                animator.SetTrigger("attack");
                animator.SetBool("isFalling", false);
                animator.SetBool("isJumping", false);
            }

            attackTimer += Time.deltaTime;

            // counts up when in the falling state
            if (!isJumping && !isJumpFalling && !isGrounded){
                canDoubleJump = true && doubleJumpActive;
                coyoteTimer += Time.deltaTime;
            }
            else {
                coyoteTimer = 0;
            }

            // allows for a jump buffer if about to land
            if (triggerJump){
                jumpBufferCounter += Time.deltaTime;
                if (jumpBufferCounter > jumpBuffer){
                    triggerJump = false;
                    jumpBufferCounter = 0;
                }
            }

            // change state of player to jump falling (different gravity than regular falling)
            if (isJumping && rb.velocity.y < 0){
                isJumping = false;
                isJumpFalling = true;
                triggerJumpCut = false;
                isFalling = true;
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", true);
                canAttack = true;
            }

            // Updates all of the players gravity depending on their current state
            UpdateGravity();
    }

    private void FixedUpdate(){
        // Checking if player is touching the ground
        GroundCheck();

        // Applying the movement to the player
        Movement();

        // Applys the jump action to the player
        Jump();

    }

    // Updates all of the players gravity depending on their current state
    private void UpdateGravity(){
            // cuts the jump if the player releases the button before the apex is reached
			if (triggerJumpCut)
			{
				SetGravityScale(gravityScale * jumpCutGravityMult);
				rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
			}
			else if ((isJumping || isJumpFalling) && Mathf.Abs(rb.velocity.y) < 0)
			{
				SetGravityScale(gravityScale * 1);
                
			}
			else if (rb.velocity.y < 0)
			{
				SetGravityScale(gravityScale * 2);
				rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
                isFalling = true;
                //animator.SetBool("isFalling", true);
                // Debug.Log("t");
                
			}
			else
			{
				SetGravityScale(gravityScale);
                // Debug.Log("Test");
                
			}
    }

    public void SetGravityScale(float scale)
	{
		rb.gravityScale = scale;
	}

    private void Movement()
    {
        if (movementInput.x == 0){
            animator.SetBool("isWalking", false);
        }
        else {
            animator.SetBool("isWalking", true);
            canAttack = true;
        }
        float targetVelocity = movementInput.x * moveSpeed;
        targetVelocity = Mathf.Lerp(rb.velocity.x, targetVelocity, 1);

        float speedDif = targetVelocity - rb.velocity.x;

        float acceleration = moveSpeed;

        if (isOnIce){
            if (movementInput.x == 0){
                acceleration = moveSpeed * iceFriction;
            }
        }
        // else {
        //     acceleration = moveSpeed;
        // }

        // float movement = isOnIce ? speedDif * moveSpeed * iceFriction : speedDif * moveSpeed;
        // change to only flip if needed
        // make sure to flip child as well
        if (movementInput.x < 0f){
            transform.localScale = new Vector3(-1,1,1);
        }
        else if (movementInput.x > 0){
            // gameObject.GetComponent<SpriteRenderer>().flipX = false;
            transform.localScale = new Vector3(1,1,1);
        }
        float movement = speedDif * acceleration;
        rb.AddForce(movement*Vector2.right, ForceMode2D.Force);
    }

    // Applys the jump action to the player
    private void Jump(){
        if (triggerJump){
            if (isGrounded  || (coyoteTimer > 0.05f && coyoteTimer < coyoteTime) || canDoubleJump){
                triggerJump = false;
                // jumpCharges--;
                if (!isGrounded  || (coyoteTimer > coyoteTime)){
                    canDoubleJump = false;
                }
                else {
                    canDoubleJump = true && doubleJumpActive;
                }
                coyoteTimer = 0;
                float force = jumpForce;
                if (rb.velocity.y < 0)
		        	force -= rb.velocity.y;
                rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
                animator.SetTrigger("jump");
                animator.SetBool("isJumping", true);
                canAttack = true;
                isJumping = true;
            }
        }
    }

    private void Attack(){
        // attackTransform.GetComponent<SpriteRenderer>().enabled = true;
        // targetsHit = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f, enemyLayer);
        //targetsHit = Physics2D.BoxCastAll(attackTransform.position, new Vector2(1.8f, 2), transform.right, 0f, enemyLayer);
        targetsHit = Physics2D.OverlapBoxAll(attackTransform.position, new Vector2(1.5f, 2), 0, enemyLayer);
        // Debug.Log("Test");
        for (int i = 0; i < targetsHit.Length; i++){
            // Debug.Log(targetsHit[i].collider.gameObject);
            //IDamageable damageable = targetsHit[i].collider.gameObject.GetComponent<IDamageable>();
            IDamageable damageable = targetsHit[i].gameObject.GetComponent<IDamageable>();

            damageable?.Damage(attackDamage);
        }
        StartCoroutine(ResetAttack());
    }

    private IEnumerator ResetAttack(){
        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
    }

    // private void UpdateAnimation(){

    // }

    // Handles the input for the movement of the player
    public void HandleMove(InputAction.CallbackContext context){
        if (canInput){
            movementInput = new Vector2(context.ReadValue<Vector2>().x, rb.velocity.y);
        } else{
            movementInput = new Vector2(0, rb.velocity.y);
        }
    }

    // Handles the input for the jump of the player
    public void HandleJump(InputAction.CallbackContext context){
        if (context.performed && canInput){
            triggerJump = true;
        }
        // if the jump button is released early
        if (context.canceled && canInput){
            triggerJumpCut = true;
        }
    }

    public void HandleAttack(InputAction.CallbackContext context){
        if (context.performed && canInput && canAttack){
            triggerAttack = true;
        }
    }

    // Checks if the player is touching the ground
    private void GroundCheck(){
        // if (feet.IsTouchingLayers(LayerMask.GetMask("Ground"))) return true;
        // return false;
        
        // Casts a box of size groundCheckSize at the posistion of groundCheckPoint
        // player is touching the ground if the box overlaps with a collider that has the groundLayer layer 
        if (Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer))
			{
                if (isFalling){
                    animator.SetBool("isFalling", false);
                   // animator.SetBool("isLanding", true);
                    animator.SetTrigger("Landing");
                    canAttack = true;
                    isFalling = false;
                }
                
                isFalling = false;
                isGrounded = true;
                canDoubleJump = false;
                isJumpFalling = false;
                isJumping = false;
                triggerJumpCut = false;
                animator.SetBool("isJumping", false);
                // Debug.Log("Is Grounded");
            }	
            else {
                isGrounded = false;
            }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ice"))
        {
            isOnIce = true;
            // iceFriction = 0.4f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ice"))
        {
            isOnIce = false;
            // iceFriction = 1f;
        }
    }

    public void SetCanInput(bool canInput){
        this.canInput = canInput;
    }

    public void SetDoubleJump(bool doubleJump){
        this.doubleJumpActive = doubleJump;
    }

    public void SetPlayerData(PlayerData playerData){
        attackCoolDown = playerData.attackCoolDown;
        attackDamage = playerData.attackDamage;
        attackRange = playerData.attackRange;
        //canDoubleJump = playerData.canDoubleJump;
    }

    public void ApplyDamageUpgrade(){
        attackDamage += 3;
    }


    private void OnDrawGizmos(){
        //Gizmos.DrawLine(attackTransform, origin + direction * distance);
        Gizmos.DrawWireCube(attackTransform.position, new Vector3(1.5f,2,0));
    }

}
