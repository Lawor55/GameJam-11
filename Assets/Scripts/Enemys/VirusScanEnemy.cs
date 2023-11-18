using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusScanEnemy : BaseEnemy
{
    [Header("Virus Scan Settings")]
    [SerializeField] [Range(3,50)] private float turningSpeed;

    private Transform virusScanOrigin;

    private void Start()
    {
        virusScanOrigin = GetComponentInParent<Transform>().parent;
        player = Player.Instance;
    }

    private void Update()
    {
        virusScanOrigin.transform.Rotate(new Vector3(0, 0, turningSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Hit {collision}");
        if (collision.transform.GetComponent<Player>() == null) return;

        DamagePlayer();
    }
}
