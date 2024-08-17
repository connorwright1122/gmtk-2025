public interface I_Damageable
{
    public void TakeDamage(int damageAmount);
    public bool IsDestroyed();
    public SO_Destructable GetDestructableStats();

    public float GetSizeIncrease();
}
