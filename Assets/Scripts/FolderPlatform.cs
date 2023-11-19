using System.Collections;
using UnityEngine;

public class PlatformFolder : MonoBehaviour, ICorruptible
{
    [SerializeField] private bool canBeCorrupted;
    [SerializeField] private FolderTypeSo folderType;

    private GameManager gameManager;


    private SpriteRenderer sprite;
    private float timeInterval;

    private void Start()
    {
        gameManager = GameManager.Instance;
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = folderType.folderSprite;
    }


    private void LateUpdate()
    {
        // ones per in seconds
        timeInterval += Time.deltaTime;
        if (timeInterval >= 1 && IsCorrupted)
        {
            timeInterval = 0;
            // Performance friendly code here

            RageTick();
        }
    }


    public bool IsCorrupted { get; private set; }

    public void Corrupt()
    {
        if (!canBeCorrupted || IsCorrupted) return;

        IsCorrupted = true;
        sprite.sprite = folderType.corruptedFolderSprite;


        if (folderType.timeUntilFix <= 0) return;


        StartCoroutine(FixCorruption());
    }

    private void RageTick()
    {
        GameManager.Instance.AddRage(folderType.rageAmount);
    }

    private IEnumerator FixCorruption()
    {
        // / folderType.timeUntilFix * gameManager.GetCurrentLevel().fixMultiplier
        yield return new WaitForSeconds(folderType.timeUntilFix);

        IsCorrupted = false;
        sprite.sprite = folderType.folderSprite;

        gameManager.SetRageValue(gameManager.GetRageValue() - folderType.rageAmount * folderType.timeUntilFix / 2);
    }
}