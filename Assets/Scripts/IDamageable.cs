public interface IDamageable
{
    public float Health { get; protected set; }

    public void Damage();
}