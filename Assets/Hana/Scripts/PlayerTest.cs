using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public float moveSpeed = 10f;

    void Start()
    {
        transform.position = new Vector3(-7f, -1.5f, 0f);
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 v = new Vector3(x, y, 0);

        transform.position += v.normalized * moveSpeed * Time.deltaTime; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("ClearLine"))
        {
            Debug.Log("Hit");
            //Clear 패널 연결
        }
    }
}
