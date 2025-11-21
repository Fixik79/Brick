using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private const int NUM_LEVELS = 2;

    private Ball ball;
    private Paddle paddle;
    private Brick[] bricks;

    public int level { get; private set; } = 1;
    public int score { get; private set; } = 0;
    public int lives { get; private set; } = 3;

    [Header("Сцены")]
    [SerializeField] private string menuSceneName = "Menu";

    [Header("UI")]
    [SerializeField] private GameObject gameOverCanvasPrefab;  
    private GameObject gameOverCanvas;
    private GameObject gameOverPanel;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            
            if (gameOverCanvasPrefab != null)
            {
                gameOverCanvas = Instantiate(gameOverCanvasPrefab);
                DontDestroyOnLoad(gameOverCanvas);
                gameOverPanel = gameOverCanvas.transform.Find("GameOverPanel")?.gameObject;
                if (gameOverPanel != null) gameOverPanel.SetActive(false);
            }
            else
            {
                Debug.LogError("Game Over Canvas Prefab не назначен в Inspector!");
            }

            FindSceneReferences();
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void FindSceneReferences()
    {
        ball = FindObjectOfType<Ball>();
        paddle = Object.FindObjectOfType<Paddle>();
        bricks = FindObjectsOfType<Brick>();
    }

    private void LoadLevel(int level)
    {
        this.level = level;

        if (level > NUM_LEVELS)
        {
            LoadLevel(1);
        }
        else
        {
            SceneManager.sceneLoaded += OnLevelLoaded;
            SceneManager.LoadScene($"Level{level}");
        }
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
        FindSceneReferences();
    }

    public void OnBallMiss()
    {
        lives--;

        if (lives > 0)
        {
            ResetLevel();
        }
        else
        {
            GameOver();
        }
    }

    private void ResetLevel()
    {
        ball?.ResetBall();
        paddle?.ResetPaddle();
    }

    private void GameOver()
    {
        Debug.Log("Game Over! Остановка времени и показ панели.");
        Time.timeScale = 0f;
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Debug.Log("Панель активирована: " + gameOverPanel.activeSelf);
        }
        else
        {
            Debug.LogError("GameOverPanel отсутствует! Проверьте инстанцирование Canvas и структуру префаба (Panel внутри Canvas с именем 'GameOverPanel').");
        }
    }

    private void NewGame()
    {
        score = 0;
        lives = 3;
        level = 1;

        Time.timeScale = 1f;
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        LoadLevel(1);
    }

    public void OnBrickHit(Brick brick)
    {
        score += brick.points;

        if (Cleared())
        {
            LoadLevel(level + 1);
        }
    }

    private bool Cleared()
    {
        for (int i = 0; i < bricks.Length; i++)
        {
            if (bricks[i].gameObject.activeInHierarchy && !bricks[i].unbreakable)
            {
                return false;
            }
        }

        return true;
    }

    
    public void ReloadCurrentLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene.StartsWith("Level"))
        {
            Debug.Log($"Перезагрузка уровня {level} с текущими значениями (lives: {lives}, score: {score})");
            ResetLevel();
            Time.timeScale = 1f;
            if (gameOverPanel != null) gameOverPanel.SetActive(false);
            SceneManager.sceneLoaded += OnLevelLoaded;
            SceneManager.LoadScene(currentScene);
        }
        else
        {
            Debug.LogWarning("ReloadCurrentLevel работает только в уровнях.");
        }
    }

  
    public void RestartCurrentLevel()
    {
        Debug.Log($"Рестарт уровня после Game Over: сброс lives=3, сохранение score={score} и level={level}, перезагрузка уровня");
        lives = 3; 
        ResetLevel();
        Time.timeScale = 1f;  
        if (gameOverPanel != null) gameOverPanel.SetActive(false);  
        SceneManager.sceneLoaded += OnLevelLoaded;  
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  
    }

    public void LoadMenu()
    {
        Debug.Log("Загрузка меню");
        Time.timeScale = 1f;
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        SceneManager.sceneLoaded += OnMenuLoaded;
        SceneManager.LoadScene(menuSceneName);
    }

    private void OnMenuLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnMenuLoaded;
        ball = null;
        paddle = null;
        bricks = null;
    }

    public void StartNewGame()
    {
        NewGame();
    }
}
