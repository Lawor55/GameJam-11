using UnityEngine;

public class PlatformFolder : MonoBehaviour, ICorruptible
{
    [SerializeField] private bool canBeCorrupted;

    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public bool IsCorrupted { get; private set; }

    public void Corrupt()
    {
        if (!canBeCorrupted) return;

        IsCorrupted = true;
        sprite.color = Color.red;
    }
}