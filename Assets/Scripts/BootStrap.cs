using UnityEngine;
using UnityEngine.SceneManagement;

public class BootStrap : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("MainMenu");
    }
}