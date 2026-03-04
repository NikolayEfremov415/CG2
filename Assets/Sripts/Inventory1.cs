using System.Collections.Generic;
using UnityEngine;

public class Inventory1 : MonoBehaviour
{
    public List<ItemData> items = new List<ItemData>();

    public void Add(ItemData item)
    {
        if (item == null)
        {
            Debug.LogError("Add() получи NULL item! Провери ItemPickup.item в Inspector.");
            return;
        }

        items.Add(item);
        Debug.Log($"Added: {item.itemName} | Count: {items.Count}");
    }
    public bool Has(ItemData item)
    {
        return item != null && items.Contains(item);
    }

    public bool Remove(ItemData item)
    {
        if (item == null) return false;
        bool removed = items.Remove(item);
        if (removed) Debug.Log($"Removed: {item.itemName} | Count: {items.Count}");
        return removed;
    }
}