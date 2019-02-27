using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamBubbleAnimationController : MonoBehaviour {

    Material SharedMaterial;
    public float AnimationDampening = 0.05f;
    public float AnimationOffset = 1.5f;
    public float PositionMultiplier = 3f;
    Vector3 OrigPos;

    public GameObject Particles;

	void Start () {
        SharedMaterial = gameObject.GetComponent<Renderer>().sharedMaterial;
        OrigPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    void Update () {
        var maskX = (float)Math.Cos(Time.time * AnimationOffset) * AnimationDampening;
        var maskY = (float)Math.Sin(Time.time) * AnimationDampening;
        SharedMaterial.SetTextureOffset("_Mask", new Vector2(maskX, maskY));
        SharedMaterial.SetTextureScale("_Mask", new Vector2(1+maskX, 1+maskY));
        gameObject.transform.position = new Vector3(OrigPos.x + (maskX * PositionMultiplier), 
                                                    OrigPos.y + (maskY * PositionMultiplier),
                                                    OrigPos.z);
    }
    public void OpenBubbleStart(float duration = 0.8f)
    {
        this.StartCoroutine(OpenBubble(duration));
    }

    public IEnumerator OpenBubble(float duration = 0.8f)
    {
        var pitchCurve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1.0f, 90.0f);
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            var t = pitchCurve.Evaluate(elapsed / duration);
            t = t / (pitchCurve.Evaluate(1));
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, Mathf.Lerp(0, 1, t));
            Particles.transform.localScale = new Vector3(transform.localScale.z, transform.localScale.x, transform.localScale.y); 
            elapsed += Time.deltaTime;
            yield return null;
        }
        yield break;
    }
    public void CloseBubbleStart(float duration = 0.8f)
    {
        this.StartCoroutine(CloseBubble(duration));
    }

    public IEnumerator CloseBubble(float duration = 0.8f)
    {
        var pitchCurve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1.0f, 90.0f);
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            var t = pitchCurve.Evaluate(elapsed / duration);
            t = t / (pitchCurve.Evaluate(1));
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, Mathf.Lerp(1, 0, t));
            Particles.transform.localScale = new Vector3(transform.localScale.z, transform.localScale.x, transform.localScale.y);
            elapsed += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
        yield break;
    }
}
