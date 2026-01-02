using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image icon; // Иконка предмета (в Inspector: перетащите дочерний Image)
    public Text itemName; // Текст имени (в Inspector: перетащите дочерний Text)

    public void SetItem(Item item)
    {
        if (item != null)
        {
            icon.sprite = item.icon; // Устанавливаем иконку
            icon.enabled = true; // Показываем
            itemName.text = item.name; // Устанавливаем имя
        }
        else
        {
            icon.sprite = null;
            icon.enabled = false; // Скрываем пустой слот
            itemName.text = "";
        }
    }
}
