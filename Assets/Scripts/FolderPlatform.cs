using UnityEngine;

public class PlatformFolder : MonoBehaviour, ICorruptible
{
    [SerializeField] private bool canBeCorrupted;
    [SerializeField] private FolderTypeSo folderType;

    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = folderType.folderSprite;
    }

    public bool IsCorrupted { get; private set; }

    public void Corrupt()
    {
        if (!canBeCorrupted) return;

        IsCorrupted = true;
        sprite.sprite = folderType.corruptedFolderSprite;
    }
}