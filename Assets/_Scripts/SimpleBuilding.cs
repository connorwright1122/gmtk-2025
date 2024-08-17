using UnityEngine;

public class Building : MonoBehaviour, I_Damageable
{
    public float currentHealth;
    public bool _isDestroyed;
    public SO_Destructable destructableStats;

    private void Start()
    {
        currentHealth = destructableStats.health;

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            _isDestroyed = true;
            DestroyBuilding();
        }
    }

    public void DestroyBuilding()
    {
        // Play destruction animation or sound here
        //Destroy(gameObject);
    }

    public bool IsDestroyed() { return _isDestroyed; }
}
