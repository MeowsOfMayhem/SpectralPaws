using UnityEngine;
using System;
public class Dummy : MonoBehaviour
{
    public Action<Dummy> OnDeath;
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
            
            print(transform.parent.gameObject.tag);
            
            // Check if the dummy is dead
            if (currentHealth <= 0)
            {
                Die();

                if(transform.parent.gameObject.tag =="Boss")
                {
                    if (PlayerMovement2.instance.currentRoom.boss > 0)
                    {
                        PlayerMovement2.instance.currentRoom.boss--;
                        PlayerMovement2.instance.WonGame();
                    }
                }

                if (transform.parent.gameObject.tag == "Enemy")
                {
                    if (PlayerMovement2.instance.currentRoom.wrogowie > 0)
                    {
                        PlayerMovement2.instance.currentRoom.wrogowie--;
                        PlayerMovement2.instance.currentRoom.unlockDoor();
                    }
                }
            }
        }
    }

    void Die()
    {
        isDead = true;
        // You can add death animations, effects, or other logic here
        Debug.Log("Dummy died!");
        transform.parent.gameObject.SetActive(false);
        OnDeath?.Invoke(this);
        Destroy(transform.parent.gameObject, 0.5f); // Destroy the dummy GameObject
    }
}
