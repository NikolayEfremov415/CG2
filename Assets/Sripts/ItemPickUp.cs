using UnityEngine;


public class ItemPickup : MonoBehaviour
{
    public ItemData item;
    public KeyCode pickupKey = KeyCode.E;

    private Inventory1 playerInventory;
    private bool playerInRange;

    private void Update()
    {
        if (playerInRange && playerInventory != null && Input.GetKeyDown(pickupKey))
        {
            playerInventory.Add(item);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var inv = other.GetComponent<Inventory1>();
        if (inv != null)
        {
            playerInventory = inv;
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var inv = other.GetComponent<Inventory1>();
        if (inv != null)
        {
            playerInRange = false;
            playerInventory = null;
        }
    }
}