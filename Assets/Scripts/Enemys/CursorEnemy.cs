using System.Collections;
using UnityEngine;

public class CursorEnemy : BaseEnemy
{
    [SerializeField] private float followSpeed;
    [SerializeField] private float dashMultiplier;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashCooldownVariation;
    [SerializeField] private float dashDuration;

    private bool dashReady = false;
    private float currentDashDuration;
    private Rigidbody2D rbCursorEnemy;


    private void Start()
    {
        player = Player.Instance;
        rbCursorEnemy = GetComponent<Rigidbody2D>();
        StartCoroutine(DashCooldown());
    }

    private void Update()
    {
        TrackPlayer();
    }


    private void TrackPlayer()
    {
        Vector2 desiredMoveVelocity;

        desiredMoveVelocity = (player.GetPos() - transform.position).normalized;


        if (currentDashDuration >= 0)
        {
            desiredMoveVelocity = desiredMoveVelocity * followSpeed * Time.deltaTime * dashMultiplier;
            currentDashDuration -= Time.deltaTime;
        }
        else
        {
            desiredMoveVelocity = desiredMoveVelocity * followSpeed * Time.deltaTime;
        }

        if (dashReady)
        {
            rbCursorEnemy.velocity = new Vector2();
            dashReady = false;
            StartCoroutine(DashCooldown());
            Debug.Log("Start Cooldown");
        }

        Vector2 deltaVelocity = desiredMoveVelocity - rbCursorEnemy.velocity;

        rbCursorEnemy.AddForce(deltaVelocity);
    }

    private IEnumerator DashCooldown()
    {
        //Debug.Log("Dash cooldown");
        yield return new WaitForSeconds(Random.Range(dashCooldown, dashCooldown + dashCooldownVariation));
        Debug.Log("Dash ready");
        dashReady = true;
        currentDashDuration = dashDuration;
    }
}