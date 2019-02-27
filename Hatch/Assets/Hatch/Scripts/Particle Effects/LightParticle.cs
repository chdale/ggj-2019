using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LightParticle : MonoBehaviour
{
    public Light LightComp;
    public float Rate;
    public float MaxBrightness;
    public float MinBrightness;

    // Use this for initialization
    void Start()
    {
        LightComp = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        LightComp.intensity = MaxBrightness * Math.Abs(Mathf.Sin(Time.time * Rate)) + MinBrightness;
    }
}