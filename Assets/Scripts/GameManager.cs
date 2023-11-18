using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null) Debug.LogWarning("There is more than one GameManager!");
        Instance = this;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}