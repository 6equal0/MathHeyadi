using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float x, y;

        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        Vector3 pos = new Vector3(x, y).normalized;

        transform.position += pos * moveSpeed * Time.deltaTime;
    }
}
