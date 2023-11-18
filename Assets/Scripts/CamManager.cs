using UnityEngine;

public class CamManager : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private Camera renderCam;
    [SerializeField] private Camera inGameCam;

    private bool isInGame;

    public bool GetInGame()
    {
        return isInGame;
    }

    public void SetInGame(bool inGame)
    {
        isInGame = inGame;

        mainCam.gameObject.SetActive(!inGame);
        renderCam.gameObject.SetActive(!inGame);
        inGameCam.gameObject.SetActive(inGame);
    }
}