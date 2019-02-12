using System.Collections;
using UnityEngine;

public class Blink : MonoBehaviour
{
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
            yield return new WaitForSeconds(0.5f);
            light.enabled = !light.enabled;
        }
    }
}
