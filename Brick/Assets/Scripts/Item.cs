
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName = "New Item";
    public Sprite icon;
    public bool isDefaultItem = false;

    // Новое поле: true для расходников (удаляются после использования), false для предметов вроде оружия
    public bool isConsumable = false;

    // Опционально: добавь описание для лучшей читабельности
    public string description = "Item description";
}