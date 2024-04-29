using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject door; // Reference to the door object

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy entered the collider.");
            // An enemy entered the trigger collider
            // Implement any other behavior you want here
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy exited the collider.");
            // An enemy exited the trigger collider
            if (!EnemiesArePresent())
            {
                Debug.Log("No enemies left, destroying the door.");
                // No enemies left in the collider, destroy the door
                Destroy(door);
            }
        }
    }

    private bool EnemiesArePresent()
    {
        // Check if there are still enemies in the trigger collider
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                return true;
            }
        }
        return false;
    }
}
