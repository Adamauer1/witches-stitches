using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxMoveSpeed = 10f;           
    [SerializeField] private float deceleration = 0.95f;  
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Collider2D feet;
    private Rigidbody2D rb;
    private bool isOnIce = false;
    private Vector2 movementInput;

    private void Awake (){
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate(){
        ApplyMovement();
    }

    private void ApplyMovement()
    {
       if ((isOnIce || !IsGrounded()) && movementInput.x == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * deceleration, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(movementInput.x * moveSpeed, rb.velocity.y);
        }
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxMoveSpeed);
    }

    public void HandleMove(InputAction.CallbackContext context){
        movementInput = new Vector2(context.ReadValue<Vector2>().x, rb.velocity.y);
    }

    public void HandleJump(InputAction.CallbackContext context){
        if (IsGrounded()){
            if(context.performed){
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); 
            }
            if(context.canceled){
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f); 
            }
        }
    }

    private bool IsGrounded(){
        if (feet.IsTouchingLayers(LayerMask.GetMask("Ground"))) return true;
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ice"))
        {
            isOnIce = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ice"))
        {
            isOnIce = false;
        }
    }
}
