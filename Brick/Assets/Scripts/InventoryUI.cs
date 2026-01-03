using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    // Родительский объект для слотов (перетащите объект "SlotsParent")
    [SerializeField] private Transform slotsParent;

    // МАССИВ слотов UI (единственное поле с этим именем)
    private Slot[] slots;

    private void Start()
    {
        if (PlayerInventory.Instance == null)
        {
            Debug.LogError("InventoryUI: PlayerInventory.Instance не найден! Убедитесь, что PlayerInventory на сцене.");
            return;
        }
        // Получаем все слоты
        slots = slotsParent.GetComponentsInChildren<Slot>();
        // Подписываемся на событие добавления (теперь без индекса)
        PlayerInventory.Instance.OnItemAdded.AddListener(UpdateSlots); // Вызываем UpdateSlots напрямую
        // Запускаем UI
        UpdateSlots();
    }

    // Обновляет все слоты (единственный метод обновления)
    private void UpdateSlots()
    {
        if (slots == null || PlayerInventory.Instance == null) return;
        Item[] items = PlayerInventory.Instance.GetItems();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetItem(items[i]);
        }
    }

    // Убрали UpdateSingleSlot, так как теперь обновляем весь UI
}