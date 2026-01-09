
using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    public UnityEvent OnItemAdded;

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
    }

    public void AddItem(Item item)
    {
        for (int i = 0; i < maxSlots; i++)
        {
            if (inventoryItems[i] == null)
            {
                inventoryItems[i] = item;

                // === ЛОГИКА ВЫВОДА ТЕГА ===
                string tagDisplay = string.IsNullOrEmpty(item.itemTypeTag) ? "неизвестный тип" : item.itemTypeTag;
                Debug.Log($"Предмет \"{item.itemName}\" с типом \"{tagDisplay}\" добавлен в слот {i}");
                // ===========================

                OnItemAdded.Invoke();
                return;
            }
        }
        Debug.Log("Инвентарь полон!");
    }

    public Item[] GetItems() => inventoryItems;
}