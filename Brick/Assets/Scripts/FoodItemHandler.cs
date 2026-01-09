using UnityEngine;

public class FoodItemHandler : MonoBehaviour
{
    [SerializeField] private float healthRestoreAmount = 25f; // Сколько здоровья даёт еда
    private Health playerHealth;

    private void Start()
    {
        playerHealth = FindObjectOfType<Health>();
        if (playerHealth == null)
            Debug.LogError("FoodItemHandler: Не найден компонент Health у игрока!");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4)) // Клавиша 4
        {
            if (PlayerInventory.Instance != null && PlayerInventory.Instance.UseFoodItem())
            {
                if (playerHealth != null)
                {
                    playerHealth.PlusDamage(healthRestoreAmount); // Восстанавливаем здоровье
                }
            }
        }
    }
}
