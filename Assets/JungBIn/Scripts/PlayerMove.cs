using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private GameObject lightFlicker;

    [SerializeField] private int startHealth = 100;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float accelerationSpeed = 9f;

    [SerializeField] string gameOverSceneName;
    [SerializeField] string exitSceneName;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private int currentHealth;
    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        currentHealth = startHealth;
        moveSpeed = walkSpeed;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            TakeDamage(10);
        }

        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * moveSpeed;

        rb.velocity = moveVelocity;
    }

    public void SpeedUp(float speedUp)
    {
        walkSpeed += speedUp;
        accelerationSpeed += speedUp;

        moveSpeed += speedUp;
    }

    public void TakeDamage(int damage)
    {
        healthBar.value = currentHealth;
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            //¡◊¿Ω
            SceneManager.LoadScene(gameOverSceneName);
        }

        if(currentHealth * 100 / startHealth <= 40)
        {
            Debug.Log(currentHealth * 100 / startHealth);
            lightFlicker.GetComponent<LightFlicker>().batteryLow = true;
        }

        StartCoroutine(Acceleration());
    }

    IEnumerator Acceleration()
    {
        WaitForSeconds wfs = new WaitForSeconds(0.01f);

        moveSpeed = accelerationSpeed;
        yield return new WaitForSeconds(0.5f);

        while(moveSpeed >= walkSpeed)
        {
            moveSpeed -= 0.1f;
            yield return wfs;
        }

        moveSpeed = walkSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Exit"))
        {
            //≈ª√‚
            SceneManager.LoadScene(exitSceneName);
        }
    }
}
