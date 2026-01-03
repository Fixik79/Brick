using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image icon;
    public Text itemName;

    public void SetItem(Item item)
    {
        if (item != null)
        {
            icon.sprite = item.icon;
            icon.enabled = true;
            itemName.text = item.itemName; // Исправлено: itemName вместо item.name
        }
        else
        {
            icon.sprite = null;
            icon.enabled = false;
            itemName.text = "";
        }
    }
}
