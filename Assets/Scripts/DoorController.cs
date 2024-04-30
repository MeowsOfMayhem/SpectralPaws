using UnityEngine;
using System.Collections.Generic;

public class DoorController : MonoBehaviour
{
    public GameObject door; // Reference to the door object

    [SerializeField]
    private List<Dummy> enemies = new();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<Dummy>();
            enemy.OnDeath += RemoveEnemy;
            enemies.Add(enemy);
            Debug.Log("Enemy entered the collider.");

            // An enemy entered the trigger collider
            // Implement any other behavior you want here
        }
    }

    private void OnTriggerExit(Collider other)
    {
            Debug.Log("Enemy exited the collider.");
        if (other.CompareTag("Enemy"))
        {
            // An enemy exited the trigger collider
        }
    }

    private void RemoveEnemy(Dummy enemy)
    {
            enemies.Remove(enemy);
            enemy.OnDeath -= RemoveEnemy;

            if (!AreEnemiesPresent())
            {
                Debug.Log("No enemies left, destroying the door.");
                // No enemies left in the collider, destroy the door
                Destroy(door);
            }

    }
    private bool AreEnemiesPresent()
    {
        return enemies.Count > 0;
    }
}
