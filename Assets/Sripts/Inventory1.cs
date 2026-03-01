using System.Collections.Generic;
using UnityEngine;

public class Inventory1 : MonoBehaviour
{
    public List<ItemData> items = new List<ItemData>();

    public void Add(ItemData item)
    {
        if (item == null) return;
        items.Add(item);
        Debug.Log("Picked up: " + item.itemName);
        if (item == null)
        {
            Debug.LogError("Add() получи NULL item! Провери ItemPickup.item в Inspector.");
            return;
        }

        items.Add(item);
        Debug.Log($"Added: {item.itemName} | Count: {items.Count}");
    }
    
}