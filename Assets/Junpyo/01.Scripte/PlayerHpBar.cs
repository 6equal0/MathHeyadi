using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Scrollbar healthBar;
    public int maxHealth = 100;
    public GameObject enemy;

    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == enemy)
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        UpdateHealthBar();

        if (currentHealth == 0)
        {
            Debug.Log("플레이어 사망");
        }
    }

    void UpdateHealthBar()
    {
        healthBar.size = (float)currentHealth / maxHealth;
        healthBar.value = currentHealth / (float)maxHealth * 100;
    }
}
