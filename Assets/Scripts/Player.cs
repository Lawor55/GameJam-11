    using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask corruptibleLayerMask;

    private readonly int maxHealth = 3;

    private Controlls controlls;
    private int currentHealth;

    private GameManager gameManager;
    public static Player Instance { get; private set; }

    private Animator animator;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null) Debug.LogWarning("There is more than one Player");
        Instance = this;

        currentHealth = maxHealth;

        //create instant of the wrapper class for our controlls
        controlls = new Controlls();
    }

    public void Start()
    {
        controlls.PlayerControlls.Enable();
        gameManager = GameManager.Instance;
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        HandleCrouch();
        CheckPause();
    }

    //activates the movement map when this script gets enabled
    private void OnEnable()
    {
        controlls.PlayerControlls.Enable();
    }

    //deactivates the movement map when this script gets disabled
    private void OnDisable()
    {
        controlls.PlayerControlls.Disable();
    }

    private void CheckPause()
    {
        if (controlls.PlayerControlls.Pause.WasPressedThisFrame()) gameManager.PauseGame(!gameManager.IsPaused());
    }

    public void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log(currentHealth);
        audioSource.Play();

        if (currentHealth <= 0)
        {
            gameManager.GameOver();
            currentHealth = maxHealth;

        }

    }

    public int GetHealth()
    {
        return currentHealth;
    }


    private void HandleCrouch()
    {
        if (!controlls.PlayerControlls.Sting.IsPressed()) return;
        Animation();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 2f, corruptibleLayerMask);

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