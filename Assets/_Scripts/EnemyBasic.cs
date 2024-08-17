using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyBasic : MonoBehaviour
{
    public Transform goal;
    private GameObject player;
    private NavMeshAgent agent;
    private bool isBouncing;
    private Rigidbody rb;
    public float bounceForce;

    public bool isWalking;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        //NavMeshAgent agent = GetComponent<NavMeshAgent>();
        //agent.destination = goal.position;
        isWalking = true;
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
            Invoke("StopBounce", 5f);

        }
    }

    private void StopBounce()
    {
        isBouncing = false;
        agent.enabled = true;
    }


}
