
using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    public UnityEvent OnItemAdded;
    public UnityEvent OnItemUsed; // Новое событие при использовании предмета

    [SerializeField] private int maxSlots = 10;
    private Item[] inventoryItems;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        inventoryItems = new Item[maxSlots];
        OnItemAdded = new UnityEvent();
        OnItemUsed = new UnityEvent();
    }

    public void AddItem(Item item)
    {
        for (int i = 0; i < maxSlots; i++)
        {
            if (inventoryItems[i] == null)
            {
                inventoryItems[i] = item;

                string tagDisplay = string.IsNullOrEmpty(item.itemTypeTag) ? "неизвестный тип" : item.itemTypeTag;
                Debug.Log($"Предмет \"{item.itemName}\" с типом \"{tagDisplay}\" добавлен в слот {i}");

                OnItemAdded.Invoke();
                return;
            }
        }
        Debug.Log("Инвентарь полон!");
    }

    // === НОВЫЙ МЕТОД ДЛЯ ИСПОЛЬЗОВАНИЯ ЕДЫ ===
    public bool UseFoodItem()
    {
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] != null && inventoryItems[i].itemTypeTag == "food")
            {
                Debug.Log($"Использован предмет: {inventoryItems[i].itemName}. Здоровье восстановлено.");

                inventoryItems[i] = null; // Удаляем предмет
                OnItemUsed.Invoke();     // Сигнализируем об изменении

                return true;
            }
        }

        Debug.Log("Нет доступной еды для использования.");
        return false;
    }
    // ======================================

    public Item[] GetItems() => inventoryItems;
}