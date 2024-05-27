using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    public Slider healthBar;
    public int startingHealth = 100;
    public int maxHealth = 200;
    public int recoveryRate = 5;
    public int recoveryInterval = 10;

    private int currentHealth;

    void Start()
    {
        currentHealth = startingHealth;
        healthBar.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        healthBar.value = currentHealth;
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("플레이어 사망");
        }
    }
}