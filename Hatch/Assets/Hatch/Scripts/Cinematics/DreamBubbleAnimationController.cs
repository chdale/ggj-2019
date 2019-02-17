using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamBubbleAnimationController : MonoBehaviour {

    Material SharedMaterial;
    public float AnimationDampening = 0.05f;
    public float PositionMultiplier = 3f;
    Vector3 OrigPos;

	void Start () {
        SharedMaterial = gameObject.GetComponent<Renderer>().sharedMaterial;
        OrigPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    void Update () {
        var maskX = (float)Math.Cos(Time.time) * AnimationDampening;
        var maskY = (float)Math.Sin(Time.time) * AnimationDampening;
        SharedMaterial.SetTextureOffset("_Mask", new Vector2(maskX, maskY));
        SharedMaterial.SetTextureScale("_Mask", new Vector2(1+maskX, 1+maskY));
        gameObject.transform.position = new Vector3(OrigPos.x + (maskX * PositionMultiplier), 
                                                    OrigPos.y + (maskY * PositionMultiplier),
                                                    OrigPos.z);
    }
}
