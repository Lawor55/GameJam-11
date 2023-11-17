using UnityEngine;

public class Player : MonoBehaviour
{
    private void Start()
    {
    }

    private void Update()
    {
        HandleCrouch();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, -Vector2.up);
    }

    private void HandleCrouch()
    {
        if (!Input.GetKeyDown(KeyCode.S)) return;


        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1f);

        if (!hit) return;

        ICorruptible corruptible = hit.transform.GetComponent<ICorruptible>();

        if (corruptible == null)
        {
            Debug.Log("Not a corruptible");
            return;
        }

        ;

        corruptible.Corrupt();
    }
}