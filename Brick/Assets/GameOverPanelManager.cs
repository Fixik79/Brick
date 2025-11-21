using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelManager : MonoBehaviour
{
    [Header("Buttons (assign in prefab's Inspector)")]
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;

    private void Awake()
    {
        Debug.Log("GameOverPanelManager Awake.");
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(OnRestartClicked);
            Debug.Log("Restart button listener added.");
        }
        else
        {
            Debug.LogError("RestartButton is null! Assign in Inspector.");
        }

        if (menuButton != null)
        {
            menuButton.onClick.AddListener(OnMenuClicked);
            Debug.Log("Menu button listener added.");
        }
        else
        {
            Debug.LogError("MenuButton is null! Assign in Inspector.");
        }
    }

    private void OnRestartClicked()
    {
        Debug.Log("Restart button clicked.");
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartCurrentLevel();
        }
        else
        {
            Debug.LogError("GameManager.Instance not found!");
            // Fallback
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            Time.timeScale = 1f;
        }
    }

    private void OnMenuClicked()
    {
        Debug.Log("Menu button clicked.");
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadMenu();
        }
        else
        {
            Debug.LogError("GameManager.Instance not found!");
            // Fallback
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
            Time.timeScale = 1f;
        }
    }

    private void OnDestroy()
    {
        if (restartButton != null)
        {
            restartButton.onClick.RemoveListener(OnRestartClicked);
        }
        if (menuButton != null)
        {
            menuButton.onClick.RemoveListener(OnMenuClicked);
        }
    }
}