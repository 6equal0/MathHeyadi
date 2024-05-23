using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private PlayerTest player;
    private CircleCollider2D col;
    private Sight2D sight;
    private Vector3 target;
    private bool isParalized = false;
    private bool cantParalized = false;
    private float tempParalizedTime;
    private float tempCantParalizedTime;

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

    [SerializeField] private float cantParalizedTime = 1.5f;

    [SerializeField] private float chaseSpeed = 6f;
    [SerializeField] private float patrolSpeed = 3f;

    [SerializeField] private GameObject sightObject;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerTest>();
        col = GetComponent<CircleCollider2D>();
        sight = GetComponentInChildren<Sight2D>();
        target = transform.position;
        states = State.IDLE;
    }

    private void Update()
    {
        /*Vector3 direction = agent.desiredVelocity;
        float angle = Mathf.Atan2(direction.y - transform.position.y, direction.x - transform.position.x) * Mathf.Rad2Deg;
        sightObject.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);*/

        Vector3 dir = agent.velocity.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        sightObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        agent.destination = target;
        InputTest();

        if (cantParalized)
        {
            tempCantParalizedTime -= Time.deltaTime;

            if(tempCantParalizedTime < 0)
            {
                cantParalized = false;
            }
        }

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
        if (!isParalized && !cantParalized)
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
            transform.GetComponent<SpriteRenderer>().DOColor(chaseColor, 0.2f).SetEase(Ease.OutQuint);
            states = State.CHASE;
        }
    }

    private void ChaseUpdate()
    {
        target = player.transform.position;
        agent.speed = chaseSpeed;

        if(Vector2.Distance(transform.position, player.transform.position) > patrolRange)
        {
            transform.GetComponent<SpriteRenderer>().DOColor(patrolColor, 0.2f).SetEase(Ease.OutSine);
            states = State.PATROL;
        }
    }

    private void PatrolUpdate()
    {
        agent.speed = patrolSpeed;

        if (Vector2.Distance(transform.position, target) <= 0.7f)
        {
            target = new Vector3(Random.Range(-(mapSize.x / 2), mapSize.x / 2), Random.Range(-(mapSize.y / 2), mapSize.y / 2), 0);
        }

        if (Vector2.Distance(transform.position, player.transform.position) <= patrolRange)
        {
            transform.GetComponent<SpriteRenderer>().DOColor(chaseColor, 0.2f).SetEase(Ease.OutQuint);
            states = State.CHASE;
        }
    }

    private void Paralized1Update()
    {
        if (isParalized)
        {
            tempParalizedTime -= Time.deltaTime;

            if (tempParalizedTime < 0)
            {
                isParalized = false;

                transform.GetComponent<SpriteRenderer>().color = patrolColor;

                cantParalized = true;
                tempCantParalizedTime = cantParalizedTime;

                col.enabled = true;

                states = State.PATROL;
            }
        }
        else
        {
            isParalized = true;
            tempParalizedTime = paralizedTime1;
            target = transform.position;
            col.enabled = false;
            transform.GetComponent<SpriteRenderer>().color = paralizedColor;
        }
    }

    private void Paralized2Update()
    {
        if (isParalized)
        {
            tempParalizedTime -= Time.deltaTime;

            if (tempParalizedTime < 0)
            {
                isParalized = false;

                transform.GetComponent<SpriteRenderer>().color = patrolColor;

                cantParalized = true;
                tempCantParalizedTime = cantParalizedTime;

                col.enabled = true;

                states = State.PATROL;
            }
        }
        else
        {
            isParalized = true;
            tempParalizedTime = paralizedTime2;
            target = transform.position;
            col.enabled = false;
            transform.GetComponent<SpriteRenderer>().color = paralizedColor;
        }
    }

    private void Paralized3Update()
    {
        if (isParalized)
        {
            tempParalizedTime -= Time.deltaTime;

            if (tempParalizedTime < 0)
            {
                isParalized = false;

                transform.GetComponent<SpriteRenderer>().color = patrolColor;

                cantParalized = true;
                tempCantParalizedTime = cantParalizedTime;

                col.enabled = true;

                states = State.PATROL;
            }
        }
        else
        {
            isParalized = true;
            tempParalizedTime = paralizedTime3;
            target = transform.position;
            col.enabled = false;
            transform.GetComponent<SpriteRenderer>().color = paralizedColor;
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
