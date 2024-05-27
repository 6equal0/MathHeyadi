using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject exit;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(moveHorizontal, moveVertical).normalized * moveSpeed * Time.deltaTime;
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == exit)
        {
            Debug.Log("Exit");
            Timer timer = FindObjectOfType<Timer>();
            Debug.Log("Timer를 찾았습니다");
            if (timer != null)
            {
                timer.GameClear();
            }
            SceneManager.LoadScene("GameClear");
        }
    }
}
