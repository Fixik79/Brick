using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ItemAddedEvent : UnityEvent<Item> { }

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; } // Singleton-паттерн: единичный доступ
    public Action OnInventoryChanged { get; internal set; }

    [SerializeField] private int maxSlots = 4; // Максимум слотов, настраивается в Inspector
    private Item[] inventoryItems; // Массив предметов
    public UnityEvent<int> OnItemAddedAtIndex; // Событие: предмет добавлен, передаёт индекс слота

    private void Awake()
    {
        // Гарантируем только один экземпляр
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Уничтожаем дубликаты, если появятся
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Опционально: сохраняет инвентарь между сценами

        inventoryItems = new Item[maxSlots];
        OnItemAddedAtIndex = new UnityEvent<int>();
    }

    // Добавляет предмет в первый свободный слот
    public void AddItem(Item item)
    {
        for (int i = 0; i < maxSlots; i++)
        {
            if (inventoryItems[i] == null)
            {
                inventoryItems[i] = item;
                OnItemAddedAtIndex.Invoke(i); // Уведомляем UI о новом слоте
                Debug.Log($"Предмет {item.name} добавлен в слот {i}");
                return;
            }
        }
        Debug.Log("Инвентарь полон!");
    }

    // Возвращает массив предметов (для UI)
    public Item[] GetItems() => inventoryItems;

    internal void UseItem(Item item)
    {
        throw new NotImplementedException();
    }
}