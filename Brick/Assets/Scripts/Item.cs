using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;         // Название предмета
    public Sprite icon;             // Иконка
    public string description;      // Описание
    public bool isStackable;        // Можно ли стакать

    [Tooltip("Укажите тип предмета (например: здоровье, патроны, оружие и т.д.)")]
    public string itemTypeTag;      // Тип предмета (новое поле)
}