using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    private Transform player;

    public float escapeRange;

    public float patrolRadius;

    public LayerMask layerMask;

    private float patrolTimer;

    public float patrolDuration;

    public bool isEscaping;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        Vector3 newPos = RandomNavSphere(transform.position, patrolRadius, layerMask);
        navMeshAgent.SetDestination(newPos);
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Running", navMeshAgent.velocity.magnitude);
        
        if (!isEscaping)
        {
            navMeshAgent.speed = 4f;
            navMeshAgent.acceleration = 8;
            Patrol();
        }
        else
        {
            navMeshAgent.speed = 8;
            navMeshAgent.acceleration = 5;
        }

        if (Vector3.Distance(transform.position, player.transform.position) < escapeRange)
        {
            if (!isEscaping)
                navMeshAgent.ResetPath();
            isEscaping = true;
            Escape();
        }
        else
        {
            isEscaping = false;
        }
    }

    void Escape()
    {
        Vector3 dir = transform.position - player.transform.position;
        Vector3 newPos = transform.position + dir.normalized;
        navMeshAgent.SetDestination(newPos);
    }

    void Patrol()
    {
        patrolTimer += Time.deltaTime;
        if (patrolTimer > patrolDuration)
        {
            patrolTimer = 0;
            Vector3 newPos = RandomNavSphere(transform.position, patrolRadius, layerMask);
            navMeshAgent.SetDestination(newPos);
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}