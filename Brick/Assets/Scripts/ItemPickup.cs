using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    // Массив возможных предметов (в Inspector: добавьте Item-ассеты)
    public Item[] items;

    public void PickUp(PlayerInventory inventory)
    {
        if (items.Length > 0)
        {
            // Рандомный выбор
            Item randomItem = items[Random.Range(0, items.Length)];
            // Добавляем в инвентарь
            inventory.AddItem(randomItem);
            // Уничтожаем объект сразу после подбора (перенесено сюда для надежности)
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("ItemPickup: Нет предметов в массиве!");
        }
    }
}