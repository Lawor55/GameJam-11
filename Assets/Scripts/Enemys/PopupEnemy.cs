using UnityEngine;

public class PopupEnemy : BaseEnemy
{
    [SerializeField] private bool isMoving;
    [SerializeField] private float speed;

    private Bounds bounds;
    private Vector3 moveDir;

    private void Start()
    {
        player = Player.Instance;

        bounds = Background.Instance.GetBounds();
        moveDir = new Vector3(1, -1, 0);
    }

    private void Update()
    {
        if (isMoving) MovePopup();
    }

    private void MovePopup()
    {
        Vector3 nextPos = transform.position + moveDir * (speed * Time.deltaTime);

        float xMin = bounds.min.x;
        float yMin = bounds.min.y;

        float xMax = bounds.max.x;
        float yMax = bounds.max.y;


        switch (nextPos)
        {
            case var _ when nextPos.x > xMax || nextPos.x < xMin:
                moveDir.x *= -1;
                break;

            case var _ when nextPos.y > yMax || nextPos.y < yMin:
                moveDir.y *= -1;
                Debug.Log(moveDir);
                break;
        }

        transform.position += moveDir * (speed * Time.deltaTime);
    }
}