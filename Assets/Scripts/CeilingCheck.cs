using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingCheck : MonoBehaviour
{
    private Rigidbody2D rbPlayer;

    private PlayerController characterController;

    private void Start()
    {
        rbPlayer = GetComponentInParent<Rigidbody2D>();
        characterController = GetComponentInParent<PlayerController>();
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == 3)
    //    {
    //        characterController.SetMoveVelocity(-1);
    //        //rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, -1);
    //    }
    //}
}
