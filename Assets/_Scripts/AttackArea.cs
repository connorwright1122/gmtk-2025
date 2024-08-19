using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private List<I_Damageable> _damageablesInRange = new List<I_Damageable>();
    private List<I_Knockable> _knockablesInRange = new List<I_Knockable>();


    private void OnTriggerEnter(Collider other)
    {
        
        var damageable = other.GetComponent<I_Damageable>();
        if (damageable != null)
        {
            damageable.InRange();
            _damageablesInRange.Add(damageable);
            Debug.Log("New damageable");
        }

        var knockable = other.GetComponent<I_Knockable>();
        if (knockable != null)
        {
            //knockable.InRange();
            _knockablesInRange.Add(knockable);
            //Debug.Log("New damageable");
            //knockable.HandleKnockback(-);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var damageable = other.GetComponent<I_Damageable>();
        if (damageable != null && _damageablesInRange.Contains(damageable))
        {
            damageable.OutOfRange();
            _damageablesInRange.Remove(damageable);
        }

        var knockable = other.GetComponent<I_Knockable>();
        if (knockable != null && _knockablesInRange.Contains(knockable))
        {
            //damageable.OutOfRange();
            _knockablesInRange.Remove(knockable);
        }
    }

    public List<I_Damageable> GetDamageablesInRange()
    {
        return _damageablesInRange;
    }

    public List<GameObject> GetDamageablesObjects()
    {
        List<GameObject> list = new List<GameObject>();
        foreach (var damageable in _damageablesInRange)
        {
            list.Add(damageable.GetGameObject());
        }
        return list;
    }

    public List<I_Knockable> GetKnockablesInRange()
    {
        return _knockablesInRange;
    }

    public void RemoveDamageable(I_Damageable damageable)
    {
        if (damageable != null && _damageablesInRange.Contains(damageable))
        {
            _damageablesInRange.Remove(damageable);
        }
    }

    public int GetCount()
    {
        return _damageablesInRange.Count; 
    }

}
