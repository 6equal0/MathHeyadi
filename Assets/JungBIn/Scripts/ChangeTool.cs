using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTool : MonoBehaviour
{
    [SerializeField] private GameObject handLight;
    [SerializeField] private GameObject laser;

    private bool isSwitching = false;

    void Start()
    {
        handLight.SetActive(true);
        laser.SetActive(false);
    }


    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (!isSwitching)
            {
                SwitchToLaser();
            }
        }
        else
        {
            if (isSwitching)
            {
                SwitchToHandlight();
            }
        }
    }

    void SwitchToLaser()
    {
        handLight.SetActive(false);
        laser.SetActive(true);
        isSwitching = true;
    }

    void SwitchToHandlight()
    {
        handLight.SetActive(true);
        laser.SetActive(false);
        isSwitching = false;
    }
}
