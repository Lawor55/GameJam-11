using UnityEngine;

public class Background : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public static Background Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) Debug.LogWarning("There is more than one Player");
        Instance = this;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public Bounds GetBounds()
    {
        return spriteRenderer.bounds;
    }
}