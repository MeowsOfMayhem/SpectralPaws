using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    public static EnemyCounter Instance; // Singleton instance

    public int enemyCount = 0; // Make enemyCount public

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncrementEnemyCount()
    {
        enemyCount++;
    }

    public void DecrementEnemyCount()
    {
        enemyCount--;
        if (enemyCount <= 0)
        {
            // No enemies left, destroy the door
            DestroyDoor();
        }
    }

    private void DestroyDoor()
    {
        // Implement door destruction logic here
    }

    public void EnemyKilled()
    {
        // Print console statement when an enemy is killed
        Debug.Log("An enemy has been killed!");
    }
}
