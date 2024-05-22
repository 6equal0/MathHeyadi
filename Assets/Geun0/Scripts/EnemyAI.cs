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

    [SerializeField] private Vector2 mapCenter;
    [SerializeField] private Vector2 mapSize;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerTest>();
        target = transform.position;
        states = State.IDLE;
    }

    private void Update()
    {
        // agent.destination = target;

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
        target = transform.position;

        //현우가 시야각 만들면 적용
        if(Vector2.Distance(transform.position, player.transform.position) <= chaseRange)
        {
            states = State.CHASE;
        }
    }

    private void ChaseUpdate()
    {
        target = player.transform.position;

        if(Vector2.Distance(transform.position, player.transform.position) > patrolRange)
        {
            target = transform.position;
            states = State.PATROL;
        }
    }

    private void PatrolUpdate()
    {
        if (Vector2.Distance(transform.position, target) <= 0.7f)
        {
            Debug.Log("목적지에 도착");
            target = new Vector3(Random.Range(-(mapSize.x / 2), mapSize.x / 2), -(mapSize.y / 2), mapSize.y / 2);
            Debug.Log(target);
            Debug.Log(agent.destination);
            Debug.Log(Vector2.Distance(transform.position, target));

        }
        else
        {
            Debug.Log("목적지로 가는중");
        }

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
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(mapCenter, mapSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(target, 1);
        Gizmos.DrawWireSphere(transform.position, patrolRange);
    }
}
