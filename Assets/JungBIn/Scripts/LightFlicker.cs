using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    public Light2D light2D;
    public float flickerSpeed = 1f; // ±ôºý ¼Óµµ
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    void Start()
    {
        if(light2D == null)
        {
            light2D = GetComponent<Light2D>();
        }
    }

    void Update()
    {
        float intensity = minIntensity + (maxIntensity - minIntensity) * 0.5f * (1 + Mathf.Sin(Time.time * flickerSpeed));
        light2D.intensity = intensity;
    }
}
