using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioLight : MonoBehaviour {
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
            yield return new WaitForSeconds(.5f);
            light.enabled = !light.enabled;
        }
    }
}
