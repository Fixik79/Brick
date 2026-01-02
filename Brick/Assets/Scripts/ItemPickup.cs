using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Item item;  // Ссылка на твой ScriptableObject Item (выбери в Inspector)

    // Метод для подбора. Вызывается извне (например, из CollisionBehavior)
    public void PickUp(PlayerInventory inventory)
    {
        if (inventory != null && item != null)
        {
            inventory.AddItem(item);  // Добавляем в инвентарь (используя твой PlayerInventory)
            Destroy(gameObject);      // Удаляем объект из сцены
        }
    }
}
