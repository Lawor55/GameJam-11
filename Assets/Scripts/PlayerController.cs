using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")] [SerializeField] [Range(0.1f, 10)]
    private float moveSpeed = 3;

    [SerializeField] [Range(1, 10)] private float jumpHeight = 3;

    //[SerializeField] [Range(1, 10)] private float airTimeTillStop = 5;
    [SerializeField] private float gravity = -9.81f;

    [Header("Ground and Ceiling Check Settings")]
    [SerializeField] bool showCheckZones = true;
    //[SerializeField] private Vector2 checkSize = new Vector2();
    [SerializeField] [Range(0.1f, 2f)] private float checkWidth;
    [SerializeField] [Range(0.1f, 2f)] private float checkHeight;
    [SerializeField] private Transform ceilingCheckPosition;
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private LayerMask groundLayerMask;

    private Controlls actions;
    private Rigidbody2D rbPlayer;
    private Vector2 moveVelocity;
    private bool isGrounded = false;


    private void Awake()
    {
        //create instant of the wrapper class for our controlls
        actions = new Controlls();
    }

    // Start is called before the first frame update
    private void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();
    }

    //activates the movement map when this script gets enabled
    private void OnEnable()
    {
        actions.PlayerControlls.Enable();
    }

    //deactivates the movement map when this script gets disabled
    private void OnDisable()
    {
        actions.PlayerControlls.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
            //Debug.Log("Is Grounded");
            isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Groundcheck();
        Movement();
        CeilingCheck();
    }

    private void Groundcheck()
    {
        if (Physics2D.OverlapBox(groundCheckPosition.position, new Vector2(checkWidth, checkHeight), 0, groundLayerMask))
        {
            Debug.Log("Is Grounded");
            isGrounded = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (showCheckZones)
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.green;
            Gizmos.DrawCube(groundCheckPosition.position, new Vector2(checkWidth, checkHeight));
            Gizmos.DrawCube(ceilingCheckPosition.position, new Vector2(checkWidth, checkHeight));
        }
    }

    private void Movement()
    {
        //sideways movement
        moveVelocity.x = actions.PlayerControlls.Movement.ReadValue<float>() * moveSpeed;
        //vertical movement. formula to calculate the jump acceleration based on gravity and the desired jump height
        if (actions.PlayerControlls.Jump.IsPressed() && isGrounded)
        {
            Debug.Log("Jump");
            moveVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            isGrounded = false;
        }

        /*
        if (!isGrounded && !actions.PlayerControlls.Movement.IsPressed())
        {
            moveVelocity.x = Mathf.Lerp(xVelocity, 0, airTimeTillStop);
        }
        */

        if (!isGrounded) moveVelocity.y += gravity * Time.deltaTime;

        //applies combined movement velocity
        rbPlayer.velocity = moveVelocity;
        //Debug.Log("Move Velocity: " + moveVelocity);
        //xVelocity = moveVelocity.x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            //Debug.Log("Is Grounded");
            isGrounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
}
