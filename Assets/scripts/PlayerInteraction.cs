using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Здесь будет храниться ссылка на предмет, который находится в зоне досягаемости
    private ItemPickup itemInRange;

    void Update()
    {
        // Проверяем, нажата ли клавиша 'E' и есть ли предмет для подбора
        if (Input.GetKeyDown(KeyCode.E) && itemInRange != null)
        {
            PickUpItem();
        }
    }

    // Этот метод вызывается, когда игрок входит в триггер-коллайдер другого объекта
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, является ли объект предметом, который можно подобрать
        ItemPickup item = other.GetComponent<ItemPickup>();
        if (item != null)
        {
            // Запоминаем этот предмет, так как он теперь в нашей зоне
            itemInRange = item;
            Debug.Log("Вошел в зону предмета: " + item.itemData.itemName);
            // Сюда можно добавить логику для отображения подсказки, например, "[E] Подобрать"
        }
    }

    // Этот метод вызывается, когда игрок покидает триггер-коллайдер
    private void OnTriggerExit2D(Collider2D other)
    {
        // Проверяем, не ушли ли мы от предмета, который запомнили ранее
        ItemPickup item = other.GetComponent<ItemPickup>();
        if (item != null && item == itemInRange)
        {
            // Очищаем ссылку, так как предмет больше не в зоне досягаемости
            itemInRange = null;
            Debug.Log("Покинул зону предмета");
            // Здесь можно скрыть подсказку
        }
    }

    private void PickUpItem()
    {
        // Добавляем предмет в инвентарь через InventoryManager
        bool wasPickedUp = InventoryManager.instance.AddItem(itemInRange.itemData);

        // Если предмет был успешно добавлен
        if (wasPickedUp)
        {
            // Уничтожаем игровой объект предмета со сцены
            Destroy(itemInRange.gameObject);
            // Очищаем ссылку на всякий случай
            itemInRange = null; 
        }
    }
}