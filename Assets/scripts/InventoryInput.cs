using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    public int selectedSlot = 0; // Номер выбранной ячейки (0-8)

    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = InventoryManager.instance;
        
    }

    void Update()
    {
        // --- Выбор ячейки с помощью клавиш 1-9 ---
        if (Input.GetKeyDown(KeyCode.Alpha1)) selectedSlot = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) selectedSlot = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) selectedSlot = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4)) selectedSlot = 3;
        if (Input.GetKeyDown(KeyCode.Alpha5)) selectedSlot = 4;
        // ... добавьте больше, если у вас больше ячеек ...
        // Ало
        // --- Выброс предмета на клавишу G ---
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log($"Попытка выбросить из слота {selectedSlot}. Всего предметов: {inventoryManager.items.Count}");
            DropSelectedItem();
        }
    }

    void DropSelectedItem()
    {
        // Проверяем, есть ли вообще что-то в выбранной ячейке
        if (selectedSlot < inventoryManager.items.Count)
        {
            // Получаем данные о предмете, который хотим выбросить
            ItemData itemToDrop = inventoryManager.items[selectedSlot];

            // Создаем префаб этого предмета перед игроком
            // transform.position - это позиция игрока
            // Quaternion.identity - без вращения
            Instantiate(itemToDrop.itemPrefab, transform.position + Vector3.right, Quaternion.identity);

            // Удаляем предмет из инвентаря
            inventoryManager.RemoveItemByIndex(selectedSlot);
        }
    }
}