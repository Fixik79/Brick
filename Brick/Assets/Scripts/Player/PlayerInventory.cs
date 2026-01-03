
using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }
    // Убрали индекс из события для упрощения
    public UnityEvent OnItemAdded;

    // Максимум слотов, настраивается в Inspector
    [SerializeField] private int maxSlots;
    // Массив предметов
    private Item[] inventoryItems;

    private void Awake()
    {
        // Гарантируем только один экземпляр
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        inventoryItems = new Item[maxSlots];
        OnItemAdded = new UnityEvent();
    }

    // Добавляет предмет в первый свободный слот
    public void AddItem(Item item)
    {
        for (int i = 0; i < maxSlots; i++)
        {
            if (inventoryItems[i] == null)
            {
                inventoryItems[i] = item;
                // Вызываем событие без индекса (теперь просто оповещаем о добавлении)
                OnItemAdded.Invoke();
                Debug.Log($"Предмет {item.itemName} добавлен в слот {i}"); // Исправил на itemName
                return;
            }
        }
        Debug.Log("Инвентарь полон!");
    }

    // Возвращает массив предметов (для UI)
    public Item[] GetItems() => inventoryItems;
}