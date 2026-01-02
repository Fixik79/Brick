using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();  // Список всех предметов в инвентаре

    public int MaxCapacity = 10;  // Максимум предметов (можно изменить или убрать для бесконечного инвентаря)

    // Событие для уведомления UI или других систем о изменениях (опционально, но полезно для обновления интерфейса)
    public System.Action OnInventoryChanged;

    // Метод добавления предмета в инвентарь
    public bool AddItem(Item item)
    {
        if (item == null)
        {
            Debug.LogWarning("Попытка добавить null предмет!");
            return false;
        }

        if (items.Count >= MaxCapacity)
        {
            Debug.LogWarning("Инвентарь полон! Не могу добавить: " + item.itemName);
            return false;
        }

        // Проверяем, есть ли уже такой предмет (для уникальности, если нужно)
        if (items.Contains(item))
        {
            Debug.LogWarning("Предмет уже в инвентаре: " + item.itemName);
            return false;  // Или можно добавить стекинг: item.StackSize++
        }

        items.Add(item);
        Debug.Log("Добавлен предмет в инвентарь: " + item.itemName);

        // Вызываем событие для обновления UI (если есть)
        OnInventoryChanged?.Invoke();

        return true;
    }

    // Метод удаления предмета (полезно для использования или продажи)
    public bool RemoveItem(Item item)
    {
        if (items.Remove(item))
        {
            Debug.Log("Удалён предмет из инвентаря: " + item.itemName);
            OnInventoryChanged?.Invoke();
            return true;
        }
        Debug.LogWarning("Предмет не найден в инвентаре: " + item.itemName);
        return false;
    }

    // Метод получения всех предметов (для UI или проверки)
    public List<Item> GetItems()
    {
        return new List<Item>(items);  // Возвращаем копию, чтобы не модифицировать оригинал
    }

    // Метод поиска конкретного предмета (по имени или типу)
    public Item FindItem(string itemName)
    {
        return items.Find(i => i.itemName == itemName);
    }

    // Метод использования предмета (если Item реализует IUsable или подобное; пока базовый)
    public void UseItem(Item item)
    {
        if (items.Contains(item))
        {
            // Здесь можно добавить логику использования, например:
            // if (item is Weapon) { EquipWeapon((Weapon)item); }
            Debug.Log("Использован предмет: " + item.itemName);

            // Автоматически удаляем после использования (для расходников; для оружия — нет)
            if (item.isConsumable)  // Теперь поле доступно
            {
                RemoveItem(item);
            }
        }
        else
        {
            Debug.LogWarning("Предмет не в инвентаре: " + item.itemName);
        }
    }
}