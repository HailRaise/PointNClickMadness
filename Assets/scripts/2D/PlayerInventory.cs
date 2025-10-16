using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<string> items = new List<string>();

    public void AddItem(string itemName)
    {
        if (!items.Contains(itemName))
        {
            items.Add(itemName);
            Debug.Log($"[Inventory] Added: {itemName}");
        }
        else
        {
            Debug.Log($"[Inventory] {itemName} already in inventory");
        }
    }

    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }
}
