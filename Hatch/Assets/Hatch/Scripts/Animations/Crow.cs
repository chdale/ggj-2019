using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Crow : MonoBehaviour {

    public float LifeTime;
    public float LifeTimeCounter;
    public float AnimDuration;
    public float AnimCounter;
    public float Speed;
    public Vector3 Direction;

    SkeletonAnimation skeletonAnimation;
    // Spine.AnimationState and Spine.Skeleton are not Unity-serialized objects. You will not see them as fields in the inspector.
    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;


	// Use this for initialization
	void Start () {
        LifeTimeCounter = 0;
        AnimDuration = 0.8f;
        AnimCounter = 0.8f;
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
    }
	
	// Update is called once per frame
	void Update () {
        AnimCounter += Time.deltaTime;
        LifeTimeCounter += Time.deltaTime;
        transform.position += Direction * Speed;
        if (AnimCounter > AnimDuration)
        {
            AnimCounter = 0;

            var animationRange = Random.Range(0f, 1f);

            if (animationRange > 0.5f)
            {
                spineAnimationState.SetAnimation(0, "flap", true);
            }
            else
            {
                spineAnimationState.SetAnimation(0, "idle", true);
            }
        }
        //if (LifeTimeCounter > LifeTime)
        //{
        //    Object.Destroy(this.gameObject);
        //}
    }
}
