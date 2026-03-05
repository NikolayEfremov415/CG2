using UnityEngine;

public class SimpleDestructible : MonoBehaviour
{
    public int health = 1;
    public TrashManager trashManager; // Плъзни TrashManager тук в Inspector-а

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            // Преди да се изтрие, казваме на мениджъра да увеличи брояча
            if (trashManager != null)
            {
                trashManager.CollectTrash();
            }

            Destroy(gameObject);
        }
    }
}