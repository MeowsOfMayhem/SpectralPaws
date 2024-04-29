using UnityEngine;

public class CubeDestroyer : MonoBehaviour
{
    private void Start()
    {
        // Check enemy count when the cube is created
        CheckEnemyCount();
    }

    private void CheckEnemyCount()
    {
        // Access enemy count directly from the EnemyCounter singleton instance
        int enemyCount = EnemyCounter.Instance.enemyCount;

        // Check if there are any enemies left in the room
        if (enemyCount <= 0)
        {
            // If no enemies are left, destroy the cube
            Destroy(gameObject);
        }
    }
}
