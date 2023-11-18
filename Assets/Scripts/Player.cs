using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask corruptibleLayerMask;

    private readonly int maxHealth = 3;
    private int currentHealth;

    private GameManager gameManager;
    public static Player Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) Debug.LogWarning("There is more than one Player");
        Instance = this;

        currentHealth = maxHealth;
    }

    public void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        HandleCrouch();
    }

    public void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log(currentHealth);

        if (currentHealth <= 0) gameManager.GameOver();
    }

    private void HandleCrouch()
    {
        if (!Input.GetKeyDown(KeyCode.S)) return;


        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1f, corruptibleLayerMask);

        if (!hit) return;

        Debug.Log(hit.collider.name);

        ICorruptible corruptible = hit.transform.GetComponent<ICorruptible>();

        if (corruptible == null)
        {
            Debug.Log("Not a corruptible");
            return;
        }

        corruptible.Corrupt();
    }

    public Vector3 GetPos()
    {
        return transform.position;
    }
}