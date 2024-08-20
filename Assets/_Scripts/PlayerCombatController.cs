using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using TMPro;

public class PlayerCombatController : MonoBehaviour
{

#if ENABLE_INPUT_SYSTEM
    private PlayerInput _playerInput;
#endif
    private Animator _animator;
    private CharacterController _controller;
    private StarterAssetsInputs _input;

    [SerializeField]
    public bool _canAttack = true;
    public float _timeSinceLastAttack;
    public float _primaryCooldown = 1f;

    private bool _isPrimaryCooldown;


    public AttackArea _attackArea;
    public ParticleSystem _meleeParticle;

    public float lerpDuration = 1f;

    public float maxSize = 10f;
    private Vector3 maxSizeVector; 

    public TMP_Text _sizeText;


    void Start()
    {
        _input = StarterAssetsInputs.Instance;
#if ENABLE_INPUT_SYSTEM
        _playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif
        _attackArea = GetComponentInChildren<AttackArea>();
        _meleeParticle = GetComponentInChildren<ParticleSystem>();
        _animator = GetComponent<Animator>();

        maxSizeVector = new Vector3(maxSize, maxSize, maxSize);
    }

    void Update()
    {
        PrimaryCheck();
    }

    private void PrimaryCheck()
    {
        if (CanAttack())
        {
            if (_input.primary)
            {
                Debug.Log("Attacked1");
                PrimaryOther();
                Invoke("Primary", 1f);
            }
        }
        _input.primary = false;
    }

    private void Primary()
    {
        List<I_Damageable> originalList = _attackArea.GetDamageablesInRange();
        List<I_Damageable> deepCopiedList = new List<I_Damageable>(originalList);


        foreach (var damageable in deepCopiedList)
        {
            damageable.TakeDamage(10);
            if (damageable.IsDestroyed())
            {
                IncreaseSizeAndPower(damageable.GetSizeIncrease());
                damageable.DestroySelf();
                _attackArea.RemoveDamageable(damageable);
                break;
            }
            Debug.Log("Attacked damageable");
        }
        /*
        foreach (var knockable in _attackArea.GetKnockablesInRange())
        {
            //knockable.TakeDamage(10);
            knockable.HandleKnockback(knockable.gameObject.transform.forward * 1000);
            
            Debug.Log("Attacked knockable");
        }
        */
        /*
        _timeSinceLastAttack = 0f;
        //_meleeParticle.Play();
        //_animator.Play("PrimaryAttack");
        _animator.SetBool("PrimaryAttack", true);
        StartCoroutine(PrimaryCooldown());
        */
    }

    private void PrimaryOther()
    {
        _timeSinceLastAttack = 0f;
        //_meleeParticle.Play();
        //_animator.Play("PrimaryAttack");
        _animator.SetBool("PrimaryAttack", true);
        StartCoroutine(PrimaryCooldown());
    }

    private bool CanAttack()
    {
        if (!_isPrimaryCooldown)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private IEnumerator PrimaryCooldown()
    {
        _isPrimaryCooldown = true;
        yield return new WaitForSeconds(_primaryCooldown);
        _isPrimaryCooldown = false;
        _animator.SetBool("PrimaryAttack", false);
    }

    private void IncreaseSizeAndPower(float sizeIncreaseAmount)
    {
        //transform.localScale += new Vector3(sizeIncrease, sizeIncrease, sizeIncrease);
        //transform.localScale += new Vector3(1, 1, 1);
        // Increase knockback power here (example, you can implement your own knockback logic)
        // knockbackPower += knockbackIncrease;
        //Vector3 newScale = transform.localScale + new Vector3(1, 1, 1);
        Vector3 newScale = transform.localScale + new Vector3(sizeIncreaseAmount, sizeIncreaseAmount, sizeIncreaseAmount);
        StartCoroutine(LerpScale(newScale, lerpDuration));

    }

    private IEnumerator LerpScale(Vector3 targetScale, float duration)
    {
        Vector3 startScale = transform.localScale;
        float timeElapsed = 0;

        while (timeElapsed < duration && transform.localScale.x < maxSize)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, timeElapsed / duration);
            timeElapsed += Time.deltaTime;

            double sizeVar = System.Math.Round(transform.localScale.x, 2) * 10;
            _sizeText.text = sizeVar.ToString() + "M";

            float newSpeed = 1f - (this.transform.localScale.x / maxSize);
            newSpeed = Mathf.Clamp(newSpeed, .3f, 1f);
            _animator.SetFloat("MotionSpeed2", newSpeed);

            yield return null; // Wait for the next frame
        }

        if (targetScale.x < maxSize)
        {
            transform.localScale = targetScale; // Ensure the final scale is set
        }
        else
        {
            transform.localScale = maxSizeVector;
        }

        //float newSpeed = 1f - (this.transform.localScale.x * .1f);
        float newSpeed2 = 1f - (this.transform.localScale.x / maxSize);
        newSpeed2 = Mathf.Clamp(newSpeed2, .2f, 1f);
        _animator.SetFloat("MotionSpeed2", newSpeed2);

        double sizeVar1 = System.Math.Round(transform.localScale.x, 2) * 10;
        if (transform.localScale.x >= maxSize)
        {
            _sizeText.text = sizeVar1.ToString() + "M - MAX!";
        } else
        {
            _sizeText.text = sizeVar1.ToString() + "M";
        }
        
        //sizeVar1 = Mathf
    }




}
