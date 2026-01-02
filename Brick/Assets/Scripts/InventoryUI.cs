using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform slotsParent; // Родительский объект для слотов (перетащите объект "SlotsParent")

    private Slot[] slots; // МАССИВ слотов UI (единственное поле с этим именем)

    private void Start()
    {
        if (PlayerInventory.Instance == null)
        {
            Debug.LogError("InventoryUI: PlayerInventory.Instance не найден! Убедитесь, что PlayerInventory на сцене.");
            return;
        }

        slots = slotsParent.GetComponentsInChildren<Slot>(); // Получаем все слоты (без неоднозначности, возвращает массив Slot[])

        PlayerInventory.Instance.OnItemAddedAtIndex.AddListener(UpdateSingleSlot); // Подписываемся на событие добавления

        UpdateSlots(); // Запускаем UI
    }

    // Обновляет все слоты (использует локальную переменную для безопасности)
    private void UpdateSlots()
    {
        if (slots == null || PlayerInventory.Instance == null) return;
        Item[] items = PlayerInventory.Instance.GetItems();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetItem(items[i]); // Без конфликтов — slots[i] однозначно
        }
    }

    // Обновляет один слот по индексу
    private void UpdateSingleSlot(int index)
    {
        if (slots != null && index < slots.Length && PlayerInventory.Instance != null)
        {
            slots[index].SetItem(PlayerInventory.Instance.GetItems()[index]);
        }
    }
}