using UnityEngine;

public class WindowPlatform : MonoBehaviour, ICorruptible
{
    private void Start()
    {
        IsCorrupted = false;
    }

    public bool IsCorrupted { get; private set; }

    public void Corrupt()
    {
        if (IsCorrupted) return;
        IsCorrupted = true;

        CloseWindow();
    }

    private void CloseWindow()
    {
        gameObject.SetActive(false);
    }
}