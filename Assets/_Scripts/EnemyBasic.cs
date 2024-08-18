using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;


public class EnemyBasic : MonoBehaviour
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


    private enum EnemyState 
    {
        Walking,
        Ragdoll
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        //NavMeshAgent agent = GetComponent<NavMeshAgent>();
        //agent.destination = goal.position;
        isWalking = true;
        _animator = GetComponent<Animator>();
        Debug.Log(_animator);
    }

    private void Update()
    {
        /*
        switch (currentState)
        {
            case EnemyState.Walking:
                WalkingBehavior();
                break;
            case EnemyState.Ragdoll:
                RagdollBehavior();
                break;
        }
        */
    }

    private void FixedUpdate()
    {
        //GetComponent<NavMeshAgent>().destination = player.transform.position;
        if (!isBouncing)
        {
            GoToPlayer();
        }
    }

    private void GoToPlayer()
    {
        agent.destination = player.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //float bounce = 6f; //amount of force to apply
            agent.enabled = false;
            rb.AddForce(collision.contacts[0].normal * bounceForce);
            isBouncing = true;
            Invoke("StopBounce", 1.6f);
            //Invoke("StopBounce", 1.6f);
            //_animator.Play("Stumble");
            _animator.SetBool("Stumble", true);
            //_animator.SetBool("Stumble", false);
            //Debug.Log(;
            collision.gameObject.GetComponent<PlayerMovementController>().Knockback(-collision.contacts[0].normal);
        }
    }

    private void StopBounce()
    {
        isBouncing = false;
        agent.enabled = true;
        _animator.SetBool("Stumble", false);
        //_animator.Play("StandUp");
    }

    private void WalkingBehavior()
    {
        GoToPlayer();
    }

    private void RagdollBehavior()
    {

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
}
