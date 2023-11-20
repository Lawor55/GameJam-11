using System.Collections;
using UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CamManager camManager;
    [SerializeField] private LevelSo[] levelArray;
    [SerializeField] private GameObject pauseMenuPrefab;
    [SerializeField] private string mainMenuName;
    [SerializeField] private GameObject endScreen;
    private LevelSo currentLevel;
    private float rageValue;

    private Fist fist;
    private bool isPaused;
    private GameObject pauseMenu;
    private bool isGameOver;

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
        SceneManager.LoadScene(mainMenuName);
        currentLevel = null;
    }

    public LevelSo[] GetLevels()
    {
        return levelArray;
    }

    public void SetFist(Fist fist)
    {
        this.fist = fist;
    }

    public void FreezeTime(bool freeze)
    {
        Time.timeScale = freeze ? 0 : 1;
    }

    public void PauseGame(bool pause)
    {
        isPaused = pause;

        if (isGameOver) return;

        if (pause)
        {
            FreezeTime(true);
            pauseMenu = Instantiate(pauseMenuPrefab);
            PauseMenu component = pauseMenu.GetComponent<PauseMenu>();
            component.SetLevel(currentLevel);

            return;
        }

        Destroy(pauseMenu);
        FreezeTime(false);
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
        SceneManager.LoadScene(level.sceneName);
        FreezeTime(false);
        currentLevel = level;
        rageValue = 0;
        isGameOver = false;
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

        if (rageValue >= currentLevel.rageNeeded) FinishLevel();
    }

    public void AddRage(float newRageValue)
    {
        SetRageValue(rageValue + newRageValue);
    }

    private void FinishLevel()
    {
        if (isGameOver)
        {
            return;
        }

        camManager.SetInGame(false);
        fist.Punch();
        StartCoroutine(WaitForAnimation());

        Debug.Log("Level Done");
        isGameOver = true;
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSecondsRealtime(3f);

        EndScreen screen = Instantiate(endScreen).GetComponent<EndScreen>();
        FreezeTime(true);
        screen.SetHasWon(true);
        screen.SetLevel(currentLevel);
    }

    public void GameOver()
    {
        if (isGameOver)
        {
            return;
        }

        FreezeTime(true);
        EndScreen screen = Instantiate(endScreen).GetComponent<EndScreen>();
        screen.SetHasWon(false);
        screen.SetLevel(currentLevel);
        isGameOver = true;
    }

    public LevelSo GetCurrentLevel()
    {
        return currentLevel;
    }

    public void SpecificScene(string specificLevelName)
    {
        SceneManager.LoadScene(specificLevelName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}