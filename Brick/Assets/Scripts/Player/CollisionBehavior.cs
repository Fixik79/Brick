using UnityEngine;

public class CollisionBehavior : MonoBehaviour
{
    private ICollisionAbility[] _collisionAbilities; // Массив способностей (через GetComponents)

private void Start()
{
    _collisionAbilities = GetComponents<ICollisionAbility>(); // Получаем все способности на этом объекте

    if (PlayerInventory.Instance == null)
    {
        Debug.LogError("CollisionBehavior: PlayerInventory.Instance не найден!");
    }
}

private void OnTriggerEnter(Collider other)
{
    // Вызываем способности (например, наносить урон врагу)
    if (_collisionAbilities.Length > 0)
    {
        foreach (var ability in _collisionAbilities)
        {
            ability.UseAbility(other.gameObject);
        }
    }

    // Подбираем предмет, используя Singleton
    var itemPickup = other.GetComponent<ItemPickup>();
    if (itemPickup != null && PlayerInventory.Instance != null)
    {
        itemPickup.PickUp(PlayerInventory.Instance);
        Destroy(other.gameObject); // Уничтожаем объект предмета после подбора
    }
}
}