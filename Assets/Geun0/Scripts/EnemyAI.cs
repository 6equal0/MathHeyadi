using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private PlayerTest player;
    private Vector3 target;
    private bool isParalized = false;
    private float tempParalizedTime;
    private float standardParalizedTime = 10f;

    private enum State
    {
        IDLE,
        CHASE,
        PATROL,
        PARALIZED1,
        PARALIZED2,
        PARALIZED3,
    }

    [SerializeField] private State states;

    [SerializeField] private float chaseRange = 2.5f;
    [SerializeField] private float patrolRange = 4f;

    [SerializeField] private Vector2 mapCenter;
    [SerializeField] private Vector2 mapSize;

    [SerializeField] private Color chaseColor;
    [SerializeField] private Color patrolColor;
    [SerializeField] private Color paralizedColor;

    [SerializeField] private float paralizedTime1 = 0.5f;
    [SerializeField] private float paralizedTime2 = 1.5f;
    [SerializeField] private float paralizedTime3 = 3f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerTest>();
        target = transform.position;
        states = State.IDLE;
    }

    private void Update()
    {
        agent.destination = target;

        InputTest();

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
            case State.PARALIZED1:
                Paralized1Update();
                break;
            case State.PARALIZED2:
                Paralized2Update();
                break;
            case State.PARALIZED3:
                Paralized3Update();
                break;
        }
    }

    private void InputTest()
    {
        if (!isParalized)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                states = State.PARALIZED1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                states = State.PARALIZED2   ;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                states = State.PARALIZED3;
            }
        }

    }

    private void IdleUpdate()
    {
        target = transform.position;

        //현우가 시야각 만들면 적용
        if(Vector2.Distance(transform.position, player.transform.position) <= chaseRange)
        {
            states = State.CHASE;
            transform.GetComponent<SpriteRenderer>().DOColor(chaseColor, 1f).SetEase(Ease.OutQuint);
        }
    }

    private void ChaseUpdate()
    {
        target = player.transform.position;

        if(Vector2.Distance(transform.position, player.transform.position) > patrolRange)
        {
            states = State.PATROL;
            transform.GetComponent<SpriteRenderer>().DOColor(patrolColor, 1f).SetEase(Ease.OutSine);
        }
    }

    private void PatrolUpdate()
    {
        if (Vector2.Distance(transform.position, target) <= 0.7f)
        {
            target = new Vector3(Random.Range(-(mapSize.x / 2), mapSize.x / 2), Random.Range(-(mapSize.y / 2), mapSize.y / 2), 0);
        }

        if (Vector2.Distance(transform.position, player.transform.position) <= patrolRange)
        {
            transform.GetComponent<SpriteRenderer>().DOColor(chaseColor, 1f).SetEase(Ease.OutQuint);
            states = State.CHASE;
        }
    }

    private void Paralized1Update()
    {
        if (isParalized)
        {
            tempParalizedTime -= (standardParalizedTime / paralizedTime1);

            if(tempParalizedTime < 0)
            {
                isParalized = false;
                tempParalizedTime = standardParalizedTime;

                states = State.PATROL;
                transform.GetComponent<SpriteRenderer>().DOColor(patrolColor, 1f).SetEase(Ease.OutSine);
            }
        }
        else
        {
            isParalized = true;
            agent.destination = transform.position;
            transform.GetComponent<SpriteRenderer>().DOColor(paralizedColor, 0.5f).SetEase(Ease.OutQuint);
        }
    }

    private void Paralized2Update()
    {
        if (isParalized)
        {
            tempParalizedTime -= (standardParalizedTime / paralizedTime2);

            if (tempParalizedTime < 0)
            {
                isParalized = false;
                tempParalizedTime = standardParalizedTime;

                states = State.PATROL;
                transform.GetComponent<SpriteRenderer>().DOColor(patrolColor, 1f).SetEase(Ease.OutSine);
            }
        }
        else
        {
            isParalized = true;
            agent.destination = transform.position;
            transform.GetComponent<SpriteRenderer>().DOColor(paralizedColor, 0.5f).SetEase(Ease.OutSine);
        }
    }

    private void Paralized3Update()
    {
        if (isParalized)
        {
            tempParalizedTime -= (standardParalizedTime / paralizedTime3);

            if (tempParalizedTime < 0)
            {
                isParalized = false;
                tempParalizedTime = standardParalizedTime;

                states = State.PATROL;
                transform.GetComponent<SpriteRenderer>().DOColor(patrolColor, 1f).SetEase(Ease.OutSine);
            }
        }
        else
        {
            isParalized = true;
            agent.destination = transform.position;
            transform.GetComponent<SpriteRenderer>().DOColor(paralizedColor, 0.5f).SetEase(Ease.OutSine);
        }
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
