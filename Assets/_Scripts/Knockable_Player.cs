using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockable_Player : MonoBehaviour, I_Knockable
{
    private PlayerMovementController _controller;

    private void Start()
    {
        _controller = GetComponent<PlayerMovementController>();
    }

    public void HandleKnockback(Vector3 direction)
    {
        //_controller.Knockback(direction, n);
    }

    
}
