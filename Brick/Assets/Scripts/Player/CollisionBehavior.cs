using UnityEngine;

public class CollisionBehavior : MonoBehaviour
{
    private ICollisionAbility[] _collisionAbilities;
    private PlayerInventory _inventory;  // Добавляем ссылку на инвентарь (предполагаем, что он на том же объекте)

    private void Start()
    {
        // Получаем все компоненты, реализующие интерфейс ICollisionAbility
        _collisionAbilities = GetComponents<ICollisionAbility>();

        // Ищем инвентарь на этом же объекте (или можем передать его извне)
        _inventory = GetComponent<PlayerInventory>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Существующая логика: проверяем и используем способности
        if (_collisionAbilities.Length > 0)
        {
            foreach (var ability in _collisionAbilities)
            {
                ability.UseAbility(collision.gameObject);
            }
        }

        // Новая логика: проверяем, есть ли на столкнувшемся объекте компонент ItemPickup для подбора
        var itemPickup = collision.gameObject.GetComponent<ItemPickup>();
        if (itemPickup != null)
        {
            itemPickup.PickUp(_inventory);  // Собираем предмет
        }
    }
}

