using UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float rageValue;
    [SerializeField] private CamManager camManager;
    [SerializeField] private LevelSo[] levelArray;
    [SerializeField] private GameObject pauseMenuPrefab;
    [SerializeField] private SceneAsset mainMenu;

    private LevelSo currentLevel;
    private bool isPaused;
    private GameObject pauseMenu;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(this);


        if (Instance != null)
        {
            Debug.LogWarning("There is more than one GameManager!");
            Destroy(gameObject);
        }

        Instance = this;


        if (camManager != null) camManager.SetInGame(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu.name);
        currentLevel = null;
    }

    public LevelSo[] GetLevels()
    {
        return levelArray;
    }

    public void PauseGame(bool pause)
    {
        isPaused = pause;

        if (pause)
        {
            Time.timeScale = 0;
            pauseMenu = Instantiate(pauseMenuPrefab);
            PauseMenu component = pauseMenu.GetComponent<PauseMenu>();
            component.SetLevel(currentLevel);

            return;
        }

        Destroy(pauseMenu);
        Time.timeScale = 1;
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public void SetCamManager(CamManager camManager)
    {
        this.camManager = camManager;
    }

    public void SetLevel(LevelSo level)
    {
        SceneManager.LoadScene(level.scene.name);
        currentLevel = level;
    }

    public float GetRageValue()
    {
        return rageValue;
    }

    public float GetLevelMaxRage()
    {
        return currentLevel.rageNeeded;
    }

    public void SetRageValue(float newRageValue)
    {
        rageValue = newRageValue;

        if (rageValue >= 100) FinishLevel();
    }

    public void AddRage(float newRageValue)
    {
        rageValue += newRageValue;

        if (rageValue >= 100) FinishLevel();
    }

    private void FinishLevel()
    {
        Debug.Log("Level Done");
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }

    public LevelSo GetCurrentLevel()
    {
        return currentLevel;
    }
}