using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerFlappyController : MonoBehaviour
{
    public bool canInput = false;
    //[SerializeField] private bool isEnabled = false;
    [SerializeField] private float velocity = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private bool freezePlayer = false;
    [SerializeField] private GameObject flappyMenu;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private GameObject flappyHUD;
    public float tilt = 5f;
    private Vector3 direction;

    private Rigidbody2D rb;


    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        flappyMenu.SetActive(false);
    }

    private void OnEnable()
    {
        freezePlayer = true;
        direction = Vector3.zero;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        canInput = true;
        flappyHUD.SetActive(true);
        transform.localScale = new Vector3(1,1,1);

    }

    private void OnDisable(){
        canInput = false;
        flappyHUD.SetActive(false);
        flappyMenu.SetActive(false);
        countdownText.gameObject.SetActive(false);
    }

    private void Update(){
        if (freezePlayer)
        {
            return;
        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;


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
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FlappyGround"))
        {
            freezePlayer = true;
            // GameManager.instance.DisplayFlappyMenu();
            DisplayFlappyMenu();
        }
    }

    public IEnumerator StartFlappyGame()
    {
        int currentCount = 3;
        countdownText.gameObject.SetActive(true);
        while (currentCount > 0)
        {
            countdownText.text = currentCount.ToString();
            yield return new WaitForSeconds(1f);
            currentCount--;
        }

        countdownText.text = "GO";
        yield return new WaitForSeconds(0.5f);
        countdownText.gameObject.SetActive(false);
        freezePlayer = false;
    }

    private void DisplayFlappyMenu()
    {
        flappyMenu.SetActive(true);
    }

    public void ResetFlappyGame()
    {
        // freezePlayer = false;
        flappyMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        StartCoroutine(StartFlappyGame());
    }

    public void ReturnToHome()
    {
        GameManager.instance.LoadLevel(1);
        
    }
}
