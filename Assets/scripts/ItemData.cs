using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName = "New Item";
    public Sprite icon = null;
    public string description = "Item Description";

    // ДОБАВЬТЕ ЭТУ СТРОКУ
    public GameObject itemPrefab; // Префаб объекта, который появится в мире
}