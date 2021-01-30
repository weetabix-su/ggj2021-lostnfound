using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceNew : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public bool actived = false;


    /// <summary>
    /// Should Be Implemented In Character, Inheriting
    /// </summary>
    public float movementSpeed;
    public Animator animator;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        //agent.speed = GetMovementSpeed();
        agent.speed = movementSpeed;
    }

    private void Start()
    {
        Queue.OnEnd += SetActived;
        QueueOnCol.OnCol += SetActived;
    }

    private void OnEnable()
    {
        actived = false;
    }

    private void OnDestroy()
    {
        Queue.OnEnd -= SetActived;
        QueueOnCol.OnCol -= SetActived;
    }

    private void Update()
    {
        if (actived)
        {
            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            //if (!_isDead)
            //{
            if (!playerInSightRange && !playerInAttackRange) Idling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
            //}

            //animator.SetFloat("Speed", agent.velocity.magnitude / agent.speed);
        }
    }

    private void Idling()
    {
        return;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        Debug.Log("3");
        agent.SetDestination(transform.position);
        actived = false;
        LevelController.instance.LevelRetry();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SetActived();
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void SetActived()
    {
        actived = true;
    }
}