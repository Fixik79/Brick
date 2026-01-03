using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName; // Основное имя
    public Sprite icon;
    public string description;
    public bool isStackable; // Основное поле для стакинга
    // Убрал дублированные amount и stackable (isStackable уже есть)
}