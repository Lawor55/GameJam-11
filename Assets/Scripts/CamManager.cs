using UnityEngine;

public class CamManager : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private Camera renderCam;
    [SerializeField] private Camera inGameCam;

    private bool isInGame;

    private void Awake()
    {
        GameManager.Instance.SetCamManager(this);
        // TODO: Debug only:
        SetInGame(true);
    }

    public bool GetInGame()
    {
        return isInGame;
    }

    public void SetInGame(bool inGame)
    {
        isInGame = inGame;

        mainCam.enabled = !inGame;
        renderCam.enabled = !inGame;
        inGameCam.enabled = inGame;
    }
}