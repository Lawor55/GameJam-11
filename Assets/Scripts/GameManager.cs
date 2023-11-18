using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float rageValue;
    [SerializeField] private CamManager camManager;
    [SerializeField] private LevelSo[] levelArray;

    private LevelSo currentLevel;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(this);


        if (Instance != null) Debug.LogWarning("There is more than one GameManager!");
        Instance = this;


        if (camManager != null) camManager.SetInGame(true);
    }

    public LevelSo[] GetLevels()
    {
        return levelArray;
    }

    public void SetLevel(LevelSo level)
    {
        SceneManager.LoadSceneAsync(level.scene.name);
        Debug.Log("heyyyyyyy");
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