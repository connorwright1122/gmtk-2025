using Cinemachine;
using JetBrains.Annotations;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;


public class EnemyBasic : MonoBehaviour, I_Knockable
{
    public Transform goal;
    private GameObject player;
    private NavMeshAgent agent;
    private bool isBouncing;
    private Rigidbody rb;
    public float bounceForce;

    public bool isWalking;

    public float _timeToWakeUp;

    private EnemyState currentState;

    private Animator _animator;

    //public GameObject[]

    public AttackArea attackArea;
    public AttackArea buildingArea;

    private bool _isPrimaryCooldown;
    private float _timeSinceLastAttack;
    public float primaryCooldown = 1f;

    public GameObject nearestBuilding = null;
    public float lerpDuration = 1f;

    private CinemachineImpulseSource _impulseSource;

    private float prevSpeed;

    public GameObject followThis;

    public GameObject center;

    private bool _hasTargetedPlayer;

    public float knockbackStrength = 1f;


    private enum EnemyState 
    {
        FindingPlayer,
        //Ragdoll,
        FindingBuilding,
        Stumble,
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        //followThis = player.GetComponent<FollowThis>().gameObject;
        rb = GetComponent<Rigidbody>();
        //NavMeshAgent agent = GetComponent<NavMeshAgent>();
        //agent.destination = goal.position;
        isWalking = true;
        _animator = GetComponent<Animator>();
        Debug.Log(_animator);
        currentState = EnemyState.FindingBuilding;
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        prevSpeed = agent.speed;
    }

    private void Update()
    {
        //Debug.Log(currentState);
        
        
    }

    private void FixedUpdate()
    {
        //GetComponent<NavMeshAgent>().destination = player.transform.position;
        /*
        if (!isBouncing)
        {
            GoToPlayer();
        }
        */
        switch (currentState)
        {
            case EnemyState.FindingBuilding:
                FindBuildingBehavior();
                break;
            case EnemyState.FindingPlayer:
                FindPlayerBehavior();
                break;
            case EnemyState.Stumble:
                StumbleBehavior();
                break;
        }
    }

    private void GoToPlayer()
    {
        if (!isBouncing)
        {
            agent.destination = player.transform.position;

            if (attackArea.GetCount() > 0)
            {
                PrimaryCheck();
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //float bounce = 6f; //amount of force to apply
            /*
            agent.enabled = false;
            //rb.AddForce(collision.contacts[0].normal * bounceForce);
            isBouncing = true;
            Invoke("StopBounce", 1.6f);
            //Invoke("StopBounce", 1.6f);
            //_animator.Play("Stumble");
            _animator.SetBool("Stumble", true);
            */
            Debug.Log("collided with player");
            //HandleKnockback(collision.contacts[0].normal * bounceForce);

            /*
            Vector3 enemyBounce = collision.contacts[0].normal * collision.gameObject.transform.localScale.x / this.transform.localScale.x * bounceForce;
            Debug.Log("enemy bounces " + enemyBounce.x);
            HandleKnockback(enemyBounce);
            
            Vector3 knockbackDirection = -collision.contacts[0].normal * this.transform.localScale.x / collision.gameObject.transform.localScale.x * bounceForce;
            knockbackDirection.y = 0; // Flatten the Y component
            Debug.Log("player bounces " + knockbackDirection.x);
            collision.gameObject.GetComponent<PlayerMovementController>().Knockback(knockbackDirection.normalized);
            */
            //Vector3 enemyBounce = collision.gameObject.transform.forward * (collision.gameObject.transform.localScale.x / this.transform.localScale.x) * bounceForce;

            /*
            Vector3 enemyBounce = collision.gameObject.transform.forward * (collision.gameObject.transform.localScale.x / this.transform.localScale.x) * bounceForce;
            this.knockbackStrength = (collision.gameObject.transform.localScale.x / this.transform.localScale.x);
            Debug.Log("player scale " + collision.gameObject.transform.localScale.x);
            Debug.Log("enemy bounces " + enemyBounce.x);
            HandleKnockback(enemyBounce);

            Vector3 knockbackDirection = -collision.gameObject.transform.forward * (this.transform.localScale.x / collision.gameObject.transform.localScale.x) * bounceForce;
            knockbackDirection.y = 0; // Flatten the Y component
            Debug.Log("player bounces " + knockbackDirection.x);
            float newKnockbackStrength = this.transform.localScale.x / collision.gameObject.transform.localScale.x;
            collision.gameObject.GetComponent<PlayerMovementController>().Knockback(knockbackDirection.normalized, newKnockbackStrength);
            */

            Vector3 directionToPlayer = (collision.gameObject.transform.position - this.transform.position).normalized;
            directionToPlayer.y = 0;

            Vector3 enemyBounce = -directionToPlayer * (collision.gameObject.transform.localScale.x / this.transform.localScale.x) * bounceForce;
            this.knockbackStrength = (collision.gameObject.transform.localScale.x / this.transform.localScale.x);
            Debug.Log("player scale " + collision.gameObject.transform.localScale.x);
            Debug.Log("enemy bounces " + enemyBounce.x);
            HandleKnockback(enemyBounce);

            Vector3 knockbackDirection = directionToPlayer * (this.transform.localScale.x / collision.gameObject.transform.localScale.x) * bounceForce;
            knockbackDirection.y = 0; // Flatten the Y component
            Debug.Log("player bounces " + knockbackDirection.x);
            float newKnockbackStrength = this.transform.localScale.x / collision.gameObject.transform.localScale.x;
            collision.gameObject.GetComponent<PlayerMovementController>().Knockback(knockbackDirection.normalized, newKnockbackStrength);

            //collision.gameObject.GetComponent<PlayerMovementController>().Knockback(-collision.contacts[0].normal);
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            /*
            Debug.Log("player collision trigger");
            HandleKnockback(other.contacts[0].normal * bounceForce);
            //_animator.SetBool("Stumble", false);
            //Debug.Log(;

            Vector3 knockbackDirection = -collision.contacts[0].normal;
            knockbackDirection.y = 0; // Flatten the Y component

            collision.gameObject.GetComponent<PlayerMovementController>().Knockback(knockbackDirection.normalized);
            */
            Debug.Log("player collision trigger");
            //agent.enabled = false;
            //currentState = EnemyState.Stumble;

            //old - good
            /*
            HandleKnockback(-transform.forward);

            Vector3 knockbackDirection = transform.forward;
            knockbackDirection.y = 0; // Flatten the Y component
            other.gameObject.GetComponent<PlayerMovementController>().Knockback(knockbackDirection.normalized);
            */

            /*
            Vector3 enemyBounce = -transform.forward * (other.gameObject.transform.localScale.x / this.transform.localScale.x) * bounceForce;
            this.knockbackStrength = (other.gameObject.transform.localScale.x / this.transform.localScale.x);
            Debug.Log("enemy bounces " + enemyBounce.x);
            HandleKnockback(enemyBounce);

            Vector3 knockbackDirection = transform.forward * (this.transform.localScale.x / other.gameObject.transform.localScale.x) * bounceForce;
            knockbackDirection.y = 0; // Flatten the Y component
            Debug.Log("player bounces " + knockbackDirection.x);
            float newKnockbackStrength = this.transform.localScale.x / other.gameObject.transform.localScale.x;

            other.gameObject.GetComponent<PlayerMovementController>().Knockback(knockbackDirection.normalized, newKnockbackStrength);
            */
            Vector3 directionToPlayer = (other.gameObject.transform.position - this.transform.position).normalized;
            directionToPlayer.y = 0;

            Vector3 enemyBounce = -directionToPlayer * (other.gameObject.transform.localScale.x / this.transform.localScale.x) * bounceForce;
            this.knockbackStrength = (other.gameObject.transform.localScale.x / this.transform.localScale.x);
            Debug.Log("enemy bounces " + enemyBounce.x);
            HandleKnockback(enemyBounce);

            Vector3 knockbackDirection = directionToPlayer * (this.transform.localScale.x / other.gameObject.transform.localScale.x) * bounceForce;
            knockbackDirection.y = 0; // Flatten the Y component
            Debug.Log("player bounces " + knockbackDirection.x);
            float newKnockbackStrength = this.transform.localScale.x / other.gameObject.transform.localScale.x;

            other.gameObject.GetComponent<PlayerMovementController>().Knockback(knockbackDirection.normalized, newKnockbackStrength);

            //collision.gameObject.GetComponent<PlayerMovementController>().Knockback(knockbackDirection.normalized)
            //_hasTargetedPlayer = true;
        }
    }
    

    private void StopBounce()
    {
        isBouncing = false;
        agent.enabled = true;
        _animator.SetBool("Stumble", false);
        currentState = EnemyState.FindingPlayer;
        //_animator.Play("StandUp");
    }

    private void FindPlayerBehavior()
    {
        GoToPlayer();
    }

    

    private void FindBuildingBehavior()
    {
        if (nearestBuilding == null)
        {
            FindNearestObject();
        } else
        {
            /*
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                agent.updateRotation = true;
                //insert your rotation code here
            }
            else
            {
                agent.updateRotation = true;
            }
            */
            FaceTarget(nearestBuilding.transform.position);

            //if close enough, attack building
            if (attackArea.GetDamageablesObjects().Contains(nearestBuilding))
            {
                //Debug.Log("now attack building");
                PrimaryCheck();
            }
        }
        
    }

    private void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10f);
    }

    public void TriggerRagdoll()
    {
        agent.enabled = false;
    }

    private void OnFootstep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            /*
            if (FootstepAudioClips.Length > 0)
            {
                var index = Random.Range(0, FootstepAudioClips.Length);
                AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(this.transform.position), FootstepAudioVolume);
            }
            */
        }
    }

    public void HandleKnockback(Vector3 direction)
    {
        CameraShakeManager.instance.CameraShake(_impulseSource);
        agent.enabled = false;
        //rb.AddForce(direction);
        StartCoroutine(ApplyKnockback(direction));
        isBouncing = true;
        Invoke("StopBounce", 1.6f);
        _animator.SetBool("Stumble", true);

        
    }

    private IEnumerator ApplyKnockback(Vector3 knockbackDirection)
    {
        //agent.enabled = false;

        float timeElapsed = 0f;
        Vector3 initialPosition = transform.position;
        _animator.SetBool("Stumble", true);

        float knockbackCheckDistance = .6f * this.transform.localScale.x; // Define the distance to check for collisions

        bool noWall = true;
        int buildingLayerMask = LayerMask.GetMask("Building");

        //knockbackStrength *= decayFactor;
        float decayFactor = 0.95f;

        while (timeElapsed < 1.5f && noWall)
        {
            // Calculate knockback movement
            Vector3 knockbackMovement = knockbackDirection * knockbackStrength * Time.deltaTime;
            Vector3 raycastStartPos = transform.position + Vector3.up * 0f;
            raycastStartPos.y = .02f;
            //raycastStartPos
            //knockbackStrength *= decayFactor;

            // Apply knockback movement
            //transform.position += knockbackMovement;
            //_controller.Move(knockbackMovement);
            Debug.DrawRay(raycastStartPos, knockbackDirection * knockbackCheckDistance, Color.red);

            // Perform a raycast to check if there's an obstacle within the knockbackCheckDistance
            /*
            if (!Physics.Raycast(raycastStartPos, knockbackDirection, knockbackCheckDistance, buildingLayerMask))
            {
                // Apply knockback movement if no obstacle is detected within the specified distance
                noWall = false;
                //transform.position += knockbackMovement;
            }
            transform.position += knockbackMovement;
            */
            if (Physics.Raycast(raycastStartPos, knockbackDirection, out RaycastHit hit, knockbackCheckDistance, buildingLayerMask))
            {
                // Log the name of the object that the raycast hit
                Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
                noWall = false;
            }
            else
            {
                // Apply knockback movement if no obstacle is detected within the specified distance
                transform.position += knockbackMovement;
            }


            timeElapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        _animator.SetBool("Stumble", false);
        agent.enabled = true;
    }

    private void FindNearestObject()
    {
        float distance = 0f;
        float nearestDistance = 1000f;
        
        List<GameObject> buildingObjects = buildingArea.GetDamageablesObjects();
        foreach (GameObject buildingObject in buildingObjects)
        {
            distance = Vector3.Distance(this.transform.position, buildingObject.transform.position);
            if (distance < nearestDistance)
            {
                nearestBuilding = buildingObject;
                nearestDistance = distance;
            }
        }

        Invoke("CheckNoTarget", .5f);
        /*
        if (nearestBuilding != null)
        {
            agent.destination = nearestBuilding.transform.position;
        } else
        {
            //agent.destination = Vector3.zero;
            //GoToPlayer();
            //currentState = EnemyState.FindingPlayer;
            Debug.Log("No target");
        }
        */
    }

    private void CheckNoTarget()
    {
        //return (nearestBuilding == null);
        if (nearestBuilding != null)
        {
            agent.destination = nearestBuilding.transform.position;
        }
        else
        {
            /*
            if (agent.destination != Vector3.zero)
            {
                agent.destination = Vector3.zero;
            }
            */
            //agent.destination = Vector3.zero;
            Debug.Log("no target");
            if (!isBouncing)
            {
                agent.destination = player.transform.position;
            }
            //GoToPlayer();
            //currentState = EnemyState.FindingPlayer;
            //Debug.Log("No target");
            //agent.destination = followThis.transform.position;

        }
    }

    private void AttackBuilding()
    {
        PrimaryCheck();
    }





    
    private void PrimaryCheck()
    {
        if (CanAttack())
        {
            //if (_input.primary)
            //{
                //Debug.Log("Attacked1");
                PrimaryOther();
                Invoke("Primary", 1f);
            //}
        }
        //_input.primary = false;
    }

    private void Primary()
    {
        foreach (var damageable in attackArea.GetDamageablesInRange())
        {
            damageable.TakeDamage(10);
            if (damageable.IsDestroyed())
            {
                IncreaseSizeAndPower(damageable.GetSizeIncrease());
                damageable.DestroySelf();
                attackArea.RemoveDamageable(damageable);
                buildingArea.RemoveDamageable(damageable);
                nearestBuilding = null;
                Debug.Log(nearestBuilding);
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
        prevSpeed = agent.speed;
        agent.speed = .1f;
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
        yield return new WaitForSeconds(primaryCooldown);
        _isPrimaryCooldown = false;
        _animator.SetBool("PrimaryAttack", false);
        agent.speed = prevSpeed;
    }

    private void IncreaseSizeAndPower(float sizeIncreaseAmount)
    {
        
        Vector3 newScale = transform.localScale + new Vector3(sizeIncreaseAmount, sizeIncreaseAmount, sizeIncreaseAmount);
        StartCoroutine(LerpScale(newScale, lerpDuration));
        Debug.Log("NEW SCALE ENEMY " + newScale.x);

    }

    private IEnumerator LerpScale(Vector3 targetScale, float duration)
    {
        Vector3 startScale = transform.localScale;
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        transform.localScale = targetScale; // Ensure the final scale is set

    }

    private void StumbleBehavior()
    {
        agent.enabled = false;
    }
}
