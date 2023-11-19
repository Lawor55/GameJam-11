using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask corruptibleLayerMask;

    private readonly int maxHealth = 3;
    private int currentHealth;

    private GameManager gameManager;
    public static Player Instance { get; private set; }

    private Controlls controlls;

    private Animator animator;

    private void Awake()
    {
        if (Instance != null) Debug.LogWarning("There is more than one Player");
        Instance = this;

        currentHealth = maxHealth;

        //create instant of the wrapper class for our controlls
        controlls = new Controlls();
    }

    //activates the movement map when this script gets enabled
    void OnEnable()
    {
        controlls.PlayerControlls.Enable();
    }
    //deactivates the movement map when this script gets disabled
    void OnDisable()
    {
        controlls.PlayerControlls.Disable();
    }

    public void Start()
    {
        gameManager = GameManager.Instance;
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        HandleCrouch();
        Animation();
    }

    public void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log(currentHealth);

        if (currentHealth <= 0) gameManager.GameOver();
    }

    private void HandleCrouch()
    {
        if (!controlls.PlayerControlls.Sting.IsPressed()) return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1f, corruptibleLayerMask);

        if (!hit) return;

        Debug.Log(hit.collider.name);

        ICorruptible corruptible = hit.transform.GetComponent<ICorruptible>();

        if (corruptible == null)
        {
            Debug.Log("Not a corruptible");
            return;
        }

        ;

        corruptible.Corrupt();
    }

    private void Animation()
    {
        if (!animator.GetBool("stab") && controlls.PlayerControlls.Sting.WasPerformedThisFrame())
        {
            animator.SetTrigger("stab");
        }
    }

    public Vector3 GetPos()
    {
        return transform.position;
    }
}