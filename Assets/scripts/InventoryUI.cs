using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic; // Добавляем для использования List

public class InventoryUI : MonoBehaviour
{
    // Перетащите сюда ваш префаб InventorySlot_Prefab
    public GameObject inventorySlotPrefab; 
    
    // Родительский объект для слотов (наша HotbarPanel)
    // Мы можем назначить его в инспекторе, или он может найти сам себя
    private Transform itemsParent;          

    private InventoryManager inventoryManager;
    private List<Image> itemIcons = new List<Image>(); // Используем List вместо массива для гибкости

    void Start()
    {
        inventoryManager = InventoryManager.instance;
        // Подписываемся на событие изменения инвентаря
        inventoryManager.onInventoryChangedCallback += UpdateUI;

        itemsParent = transform; // Так как скрипт висит на HotbarPanel, она и будет родителем

        InitializeInventoryUI();
        UpdateUI(); // Первоначальное обновление, чтобы скрыть иконки
    }

    // Создаем слоты при старте
    void InitializeInventoryUI()
    {
        for (int i = 0; i < inventoryManager.inventorySize; i++)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, itemsParent);
            // Находим иконку именно в дочерних объектах созданного слота
            // Имя "ItemIcon" должно точно совпадать с тем, что вы задали в префабе!
            Image icon = slot.transform.Find("ItemIcon").GetComponent<Image>();
            itemIcons.Add(icon);
        }
    }

    // Обновляем отображение инвентаря
    void UpdateUI()
    {
        // Проходимся по всем предметам в инвентаре
        for (int i = 0; i < itemIcons.Count; i++)
        {
            // Если предмет в инвентаре существует для этого слота
            if (i < inventoryManager.items.Count)
            {
                itemIcons[i].sprite = inventoryManager.items[i].icon;
                itemIcons[i].color = new Color(1, 1, 1, 1); // Делаем иконку полностью видимой
                itemIcons[i].enabled = true;
            }
            else
            {
                // Если предмета нет, очищаем и скрываем иконку
                itemIcons[i].sprite = null;
                itemIcons[i].color = new Color(1, 1, 1, 0); // Делаем иконку полностью прозрачной
                itemIcons[i].enabled = false;
            }
        }
    }
}