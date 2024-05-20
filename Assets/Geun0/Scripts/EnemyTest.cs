using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTest : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 target;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        target = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        agent.destination = target;

    }
}
