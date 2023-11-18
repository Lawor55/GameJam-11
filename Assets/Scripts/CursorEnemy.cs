using UnityEngine;

public class CursorEnemy : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float followSpeed;


    // Update is called once per frame
    private void Update()
    {
        TrackPlayer();
    }

    private void TrackPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);
    }
}