using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private List<I_Damageable> _damageablesInRange = new List<I_Damageable>();

    private void OnTriggerEnter(Collider other)
    {
        
        var damageable = other.GetComponent<I_Damageable>();
        if (damageable != null)
        {
            _damageablesInRange.Add(damageable);
            Debug.Log("New damageable");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var damageable = other.GetComponent<I_Damageable>();
        if (damageable != null && _damageablesInRange.Contains(damageable))
        {
            _damageablesInRange.Remove(damageable);
        }
    }

    public List<I_Damageable> GetDamageablesInRange()
    {
        return _damageablesInRange;
    }
}
