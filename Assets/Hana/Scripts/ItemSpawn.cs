using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] GameObject itemSpeedPrefab;

    void Start()
    {
        Spawner();
    }

    private void Update()
    {
        
    }

    private void Spawner()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childSpawnPoint = transform.GetChild(i);
            GameObject item = Instantiate(itemSpeedPrefab, childSpawnPoint.position, Quaternion.identity);
        }
    }
        
}
