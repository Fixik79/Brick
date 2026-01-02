using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item[] items; // Массив возможных предметов (в Inspector: добавьте Item-ассеты)

    public void PickUp(PlayerInventory inventory)
    {
        if (items.Length > 0)
        {
            Item randomItem = items[Random.Range(0, items.Length)]; // Рандомный выбор
            inventory.AddItem(randomItem); // Добавляем в инвентарь
        }
        else
        {
            Debug.LogWarning("ItemPickup: Нет предметов в массиве!");
        }
    }
}