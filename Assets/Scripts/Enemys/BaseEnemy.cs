using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected int damageAmount;
    protected Player player;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log($"Hit {other}");
        if (other.transform.GetComponent<Player>() == null) return;

        DamagePlayer();
    }

    protected virtual void DamagePlayer()
    {
        player.Damage(damageAmount);
    }
}