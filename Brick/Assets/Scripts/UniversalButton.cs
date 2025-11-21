using UnityEngine;
using UnityEngine.SceneManagement;

public class UniversalButton : MonoBehaviour
{

    public void StartGame()
    {
        Debug.Log("Кнопка Play: Запуск новой игры");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartNewGame();
        }
        else
        {
            Debug.LogError("GameManager Instance не найден! Загружаю сцену напрямую.");
            SceneManager.LoadScene("Level1");
            Time.timeScale = 1f;
        }
    }

    public void Restart()
    {
        Debug.Log("Кнопка Restart: Перезапуск уровня с сбросом");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartCurrentLevel();
        }
        else
        {
            Debug.LogError("GameManager Instance не найден! Перезагружаю сцену напрямую.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1f;
        }
    }


    public void LoadMenu()
    {
        Debug.Log("Кнопка Menu: Загрузка меню");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadMenu();
        }
        else
        {
            Debug.LogError("GameManager Instance не найден! Загружаю меню напрямую.");
            SceneManager.LoadScene("Menu");
            Time.timeScale = 1f;
        }
    }
}
