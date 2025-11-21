using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UniversalButton : MonoBehaviour
{
    // Метод для кнопки "Play" (загрузка первого уровня и сброс игры)
    public void StartGame()
    {
        Debug.Log("Кнопка Play: Запуск новой игры");
        // Через GameManager для правильного сброса:
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartNewGame();  // Сброс всего (score=0, lives=3, level=1)
        }
        else
        {
            Debug.LogError("GameManager Instance не найден! Загружаю сцену напрямую.");
            SceneManager.LoadScene("Level1");  // Резерв: замените на имя сцены
            Time.timeScale = 1f;
        }
    }

    // Метод для кнопки "Restart" (перезапуск текущего уровня с сбросом жизней/счёта после GameOver)
    public void Restart()
    {
        Debug.Log("Кнопка Restart: Перезапуск уровня с сбросом");
        // Через GameManager для правильного рестарта:
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartCurrentLevel();  // Новый метод: сброс lives=3, score=0, сброс уровня
        }
        else
        {
            Debug.LogError("GameManager Instance не найден! Перезагружаю сцену напрямую.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1f;
        }
    }

    // Метод для кнопки "Menu" (возврат в главное меню)
    public void LoadMenu()
    {
        Debug.Log("Кнопка Menu: Загрузка меню");
        // Через GameManager для правильного выхода:
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadMenu();
        }
        else
        {
            Debug.LogError("GameManager Instance не найден! Загружаю меню напрямую.");
            SceneManager.LoadScene("Menu");  // Замените на имя сцены меню
            Time.timeScale = 1f;
        }
    }
}
