using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private float rageValue;
    public static GameManager Instance { get; private set; }

    private AudioSource audioSource;
    [SerializeField] private AudioClip audioClipStart;
    [SerializeField] private AudioClip audioClipDeath;


    private void Awake()
    {
        if (Instance != null) Debug.LogWarning("There is more than one GameManager!");
        Instance = this;

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            audioSource.PlayOneShot(audioClipStart);
        }
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
        audioSource.PlayOneShot(audioClipDeath);
        Debug.Log("Game Over");
    }
}