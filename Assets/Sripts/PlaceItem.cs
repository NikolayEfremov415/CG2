using UnityEngine;

public class PlaceItem : MonoBehaviour
{
    public ItemData requiredItem;          // кой предмет трябва да имаш
    public KeyCode placeKey = KeyCode.F;   
    public SpriteRenderer placedRenderer;  // къде да се покаже картинката

    private Inventory1 playerInventory;
    private bool playerInRange;
    private bool alreadyPlaced;

    private void Start()
    {
        if (placedRenderer != null)
            placedRenderer.enabled = false; // скрий, докато не поставиш
    }

    private void Update()
    {
        if (!playerInRange || playerInventory == null || alreadyPlaced) return;

        if (Input.GetKeyDown(placeKey))
        {
            if (requiredItem == null)
            {
                Debug.LogError("requiredItem е None! Сложи ItemData в Inspector.");
                return;
            }

            if (!playerInventory.Has(requiredItem))
            {
                Debug.Log("Нямаш нужния предмет: " + requiredItem.itemName);
                return;
            }

            // махни предмета от инвентара
            playerInventory.Remove(requiredItem);

            // покажи изображението в света
            if (placedRenderer != null)
            {
                placedRenderer.sprite = requiredItem.icon;
                placedRenderer.enabled = true;
            }

            alreadyPlaced = true;
            Debug.Log("Постави: " + requiredItem.itemName);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var inv = other.GetComponentInParent<Inventory1>();
        if (inv != null)
        {
            playerInventory = inv;
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var inv = other.GetComponentInParent<Inventory1>();
        if (inv != null)
        {
            playerInRange = false;
            playerInventory = null;
        }
    }
}