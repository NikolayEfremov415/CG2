using System.Text;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory1 inventory;
    public TMP_Text text;

    private void Update()
    {
        if (inventory == null || text == null) return;

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Inventory:");

        foreach (var item in inventory.items)
            sb.AppendLine("- " + item.itemName);

        text.text = sb.ToString();
    }
}