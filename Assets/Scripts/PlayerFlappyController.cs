using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFlappyController : MonoBehaviour
{
    public bool canInput = false;
    //[SerializeField] private bool isEnabled = false;
    [SerializeField] private float velocity = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float gravity = -9.8f;
    public float tilt = 5f;
    private Vector3 direction;

    private Rigidbody2D rb;


    private void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable(){
        direction = Vector3.zero;
        canInput = true;

    }

    private void OnDisable(){
        canInput = false;
    }

    private void Update(){
        // Apply gravity and update the position
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        // Tilt the bird based on the direction
        Vector3 rotation = transform.eulerAngles;
        rotation.z = direction.y * tilt;
        transform.eulerAngles = rotation;
    }

    private void FixedUpdate(){
        transform.rotation = Quaternion.Euler(0, 0, rotationSpeed * rb.velocity.y);
    }

    public void HandleJump(InputAction.CallbackContext context){
        if (context.performed && canInput){
            //triggerJump = true;
            // rb.velocity = Vector2.up * velocity;
            direction = Vector3.up * velocity;
        }
    }
}
