using System.Collections;
using UnityEngine;

public class PlatformFolder : MonoBehaviour, ICorruptible
{
    [SerializeField] private bool canBeCorrupted;
    [SerializeField] private FolderTypeSo folderType;

    private GameManager gameManager;

    private SpriteRenderer sprite;

    private void Start()
    {
        gameManager = GameManager.Instance;
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = folderType.folderSprite;
    }

    public bool IsCorrupted { get; private set; }

    public void Corrupt()
    {
        if (!canBeCorrupted || IsCorrupted) return;

        IsCorrupted = true;
        sprite.sprite = folderType.corruptedFolderSprite;
        GameManager.Instance.AddRage(folderType.rageAmount);


        if (folderType.timeUntilFix <= 0) return;

        StartCoroutine(FixCorruption());
    }

    private IEnumerator FixCorruption()
    {
        yield return new WaitForSeconds(folderType.timeUntilFix / folderType.timeUntilFix *
                                        gameManager.GetCurrentLevel().fixMultiplier);

        IsCorrupted = false;
        sprite.sprite = folderType.folderSprite;


        gameManager.SetRageValue(gameManager.GetRageValue() - folderType.rageAmount);
    }
}