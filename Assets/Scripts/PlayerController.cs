using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float jumpHeight = 3;
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

    private Controlls controlls;
    private Rigidbody2D rbPlayer;
    private Vector2 moveVelocity;
    private bool isGrounded = true;
    Animator animator;
    //private AudioSource audioSource;
    //[SerializeField] AudioClip audioClipImpact;


    private void Awake()
    {
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

    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Groundcheck();
        Movement();
        Animation();
        CeilingCheck();
    }

    private void Groundcheck()
    {
        if (Physics2D.OverlapBox(groundCheckPosition.position, new Vector2(checkWidth, checkHeight), 0, groundLayerMask) != null)
        {
            //if (!isGrounded)
            //{
            //    audioSource.PlayOneShot(audioClipImpact);
            //}
            //Debug.Log("Is Grounded");
            isGrounded = true;
            moveVelocity = new Vector2(moveVelocity.x, -1);
        }
        else
        {
            isGrounded = false;
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
        moveVelocity.x = controlls.PlayerControlls.Movement.ReadValue<float>() * moveSpeed;
        if (moveVelocity.x != 0)
        {
            animator.SetBool("isWalking",true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        //vertical movement. formula to calculate the jump acceleration based on gravity and the desired jump height
        if (controlls.PlayerControlls.Jump.IsPressed() && isGrounded)
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

        if (!isGrounded)
        {
            moveVelocity.y += gravity * Time.deltaTime;
        }

        //applies combined movement velocity
        rbPlayer.velocity = moveVelocity;
        //Debug.Log("Move Velocity: " + moveVelocity);
        //xVelocity = moveVelocity.x;
    }

    private void Animation()
    {
        if (!animator.GetBool("jump") && controlls.PlayerControlls.Jump.WasPerformedThisFrame())
        {
            animator.SetTrigger("jump");
        }

        if (!isGrounded)
        {
            animator.SetBool("inAir", true);
        }
        else
        {
            animator.SetBool("inAir", false);
        }
    }

    private void CeilingCheck()
    {
        if (Physics2D.OverlapBox(ceilingCheckPosition.position, new Vector2(checkWidth, checkHeight), 0, groundLayerMask))
        {
            //Debug.Log("Ceiling Bonk");
            moveVelocity = new Vector2(moveVelocity.x, -1);
        }
    }

    //public void SetMoveVelocity(float newMoveVelocity)
    //{
    //    moveVelocity.y = newMoveVelocity;
    //}
}
