using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float rageValue;
    public static GameManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null) Debug.LogWarning("There is more than one GameManager!");
        Instance = this;
    }

    public float GetRageValue()
    {
        return rageValue;
    }

    public float GetLevelMaxRage()
    {
        return 100;
    }

    public void SetRageValue(float newRageValue)
    {
        rageValue = newRageValue;

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
}