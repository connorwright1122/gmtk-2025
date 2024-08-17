using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour, I_Damageable
{
    public float currentHealth;
    public float maxHealth;
    public bool _isDestroyed;
    public SO_Destructable destructableStats;
    public Slider healthbar;
    public bool _inRange;
    private CinemachineImpulseSource _impulseSource;


    private void Start()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        
        maxHealth = destructableStats.health;
        currentHealth = maxHealth;
        healthbar = GetComponentInChildren<Slider>();
        healthbar.maxValue = maxHealth;
        healthbar.value = maxHealth;
        healthbar.gameObject.SetActive(false);
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //healthbar.value = currentHealth / maxHealth;
        healthbar.value = currentHealth;
        CameraShakeManager.instance.CameraShake(_impulseSource);
        if (currentHealth <= 0)
        {
            _isDestroyed = true;
            //DestroyBuilding();
        }
    }

    public void DestroySelf()
    {
        // Play destruction animation or sound here
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    public bool IsDestroyed() { return _isDestroyed; }

    public SO_Destructable GetDestructableStats() { return destructableStats; }

    public float GetSizeIncrease()
    {
        return destructableStats.sizeIncreaseAmount;
    }

    public void InRange()
    {
        //if (!_inRange) 
        //{ 
        healthbar.gameObject.SetActive(true);
        //}
    }

    public void OutOfRange()
    {
        //if (_inRange)
        //{
            healthbar.gameObject.SetActive(false);
        //}
    }

    public bool IsInRange()
    {
        return _inRange;
    }

    //public bool 
}
