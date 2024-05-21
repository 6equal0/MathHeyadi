using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private PlayerTest player;
    private Vector3 target;

    private enum State
    {
        IDLE,
        CHASE,
        PATROL,
        PARALIZED,
    }

    [SerializeField] private State states;

    [SerializeField] private float chaseRange = 2.5f;
    [SerializeField] private float patrolRange = 4f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerTest>();
        target = transform.position;
        states = State.IDLE;
    }

    private void Update()
    {
        switch (states)
        {
            case State.IDLE:
                IdleUpdate();
                break;
            case State.CHASE:
                ChaseUpdate();
                break;
            case State.PATROL:
                PatrolUpdate();
                break;
            case State.PARALIZED:
                ParalizedUpdate();
                break;
        }
    }

    private void IdleUpdate()
    {
        //현우가 시야각 만들면 적용
        if(Vector2.Distance(transform.position, player.transform.position) <= chaseRange)
        {
            states = State.CHASE;
        }
    }

    private void ChaseUpdate()
    {
        agent.destination = player.transform.position;

        if(Vector2.Distance(transform.position, player.transform.position) > patrolRange)
        {
            states = State.PATROL;
        }
    }

    private void PatrolUpdate()
    {


        if (Vector2.Distance(transform.position, player.transform.position) <= patrolRange)
        {
            states = State.CHASE;
        }
    }

    private void ParalizedUpdate()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
