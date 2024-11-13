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
    [SerializeField] private Collider2D feet;
    private bool isOnIce = false;
    public Transform groundCheckPoint;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.49f, 0.03f);
    public LayerMask groundLayer;
    public bool isGrounded;


    [Header("Jump")]
    [SerializeField] private float jumpHeight = 6.5f;
    private bool triggerJump;
    private bool triggerJumpCut;
    [SerializeField] private float jumpTimeToApex = 0.5f;
    private float jumpForce;
    [SerializeField] private float jumpBuffer = 0.1f;
    private float jumpBufferCounter = 0;
    public bool isJumping = false;
    public bool isJumpFalling = false;
    private float coyoteTimer = 0;
    [SerializeField] private float coyoteTime = 0.1f;
    
    
    [Header("Gravity")]
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
    private RaycastHit2D[] targetsHit;
    private bool triggerAttack = false;

    private Rigidbody2D rb;

    private void Awake (){
        rb = GetComponent<Rigidbody2D>();

        gravityStrength = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);
        
        jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;
        
        gravityScale = gravityStrength / Physics2D.gravity.y;
    }

    private void Update(){
            
            if (triggerAttack && attackTimer > attackCoolDown){
                triggerAttack = false;
                Debug.Log("Attack Triggered");
                attackTimer = 0;
                Attack();
            }

            // //temp
            // if (attackTimer > attackCoolDown){
            //     attackTransform.GetComponent<SpriteRenderer>().enabled = false;

            // }

            attackTimer += Time.deltaTime;

            // counts up when in the falling state
            if (!isJumping && !isJumpFalling && !isGrounded){
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
            if (isGrounded  || (coyoteTimer > 0.05f && coyoteTimer < coyoteTime)){
                triggerJump = false;
                coyoteTimer = 0;
                float force = jumpForce;
                if (rb.velocity.y < 0)
		        	force -= rb.velocity.y;
                rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);    
                isJumping = true;
            }
        }
    }

    private void Attack(){
        // attackTransform.GetComponent<SpriteRenderer>().enabled = true;
        targetsHit = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f, enemyLayer);
        // Debug.Log("Test");
        for (int i = 0; i < targetsHit.Length; i++){
            // Debug.Log(targetsHit[i].collider.gameObject);
            IDamageable damageable = targetsHit[i].collider.gameObject.GetComponent<IDamageable>();

            if (damageable != null){
                damageable.Damage(attackDamage);
            }
        }
    }

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
        if (context.performed && canInput){
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
                isGrounded = true;
                isJumpFalling = false;
                isJumping = false;
                triggerJumpCut = false;
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

}
