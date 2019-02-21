using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LightParticle : MonoBehaviour {
    public Light lightComp;
    public float rate;
    public float brightness;

    // Use this for initialization
    void Start () {
        lightComp = GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update () {
        lightComp.intensity = brightness * Mathf.Sin(Time.time * rate);
	}
}
