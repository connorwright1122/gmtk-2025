public interface I_Damageable
{
    public void TakeDamage(int damageAmount);
    public bool IsDestroyed();
    public SO_Destructable GetDestructableStats();

    public float GetSizeIncrease();

    public void DestroySelf();

    public void InRange();
    public void OutOfRange();
    public bool IsInRange();
}
