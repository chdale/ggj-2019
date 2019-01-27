using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFX : MonoBehaviour {
    private SpriteRenderer light;

    void Start()
    {
        light = GetComponent<SpriteRenderer>();
        StartCoroutine(Flash());
    }

    public IEnumerator Flash()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(.1f, .9f));
            light.enabled = !light.enabled;
        }
    }
}
