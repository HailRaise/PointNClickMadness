using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Singleton - чтобы иметь доступ к инвентарю из любого другого скрипта
    public static InventoryManager instance;

    public List<ItemData> items = new List<ItemData>();
    public int inventorySize = 12; // Максимальный размер инвентаря

    // Делегат и событие для обновления UI при изменении инвентаря
    public delegate void OnInventoryChanged();
    public event OnInventoryChanged onInventoryChangedCallback;


    void Awake()
    {
        // Настройка Singleton
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of InventoryManager found!");
            return;
        }
        instance = this;
    }

    // Метод для добавления предмета
    public bool AddItem(ItemData item)
    {
        if (items.Count >= inventorySize)
        {
            Debug.Log("Inventory is full.");
            return false; // Не удалось добавить, так как инвентарь полон
        }

        items.Add(item);

        // Вызываем событие, чтобы UI обновился
        if (onInventoryChangedCallback != null)
        {
            onInventoryChangedCallback.Invoke();
        }

        return true;
    }

    // Метод для удаления предмета
    public void RemoveItem(ItemData item)
    {
        items.Remove(item);

        // Вызываем событие, чтобы UI обновился
        if (onInventoryChangedCallback != null)
        {
            onInventoryChangedCallback.Invoke();
        }
    }



    // ДОБАВЬТЕ ЭТОТ НОВЫЙ МЕТОД
    public void RemoveItemByIndex(int index)
    {
        // Проверяем, что такой индекс вообще существует в списке
        if (index < items.Count)
        {
            items.RemoveAt(index);

            // Вызываем событие, чтобы UI обновился
            if (onInventoryChangedCallback != null)
            {
                onInventoryChangedCallback.Invoke();
            }
        }
    }

public bool HasItem(ItemData itemToCheck)
    {
        return items.Contains(itemToCheck);
    }
}

