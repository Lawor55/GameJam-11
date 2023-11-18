using UnityEngine;

public class CursorEnemy : BaseEnemy
{
    [SerializeField] private float followSpeed;


    private void Start()
    {
        player = Player.Instance;
    }

    private void Update()
    {
        TrackPlayer();
    }


    private void TrackPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.GetPos(), followSpeed * Time.deltaTime);
    }
}