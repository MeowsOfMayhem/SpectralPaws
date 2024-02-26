using UnityEngine;

public class Dummy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;

            // Check if the dummy is dead
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        isDead = true;
        // You can add death animations, effects, or other logic here
        Debug.Log("Dummy died!");
        Destroy(gameObject); // Destroy the dummy GameObject
    }
}
