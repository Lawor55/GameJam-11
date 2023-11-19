using UnityEngine;

public class Fist : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.SetFist(this);
    }

    public void Punch()
    {
        animator.SetBool("HasWon", true);
    }
}