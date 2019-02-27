using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Arm : MonoBehaviour {

    public float AnimSpeed;
    public float AnimStartPos;

    public SkeletonAnimation skeletonAnimation;
    // Spine.AnimationState and Spine.Skeleton are not Unity-serialized objects. You will not see them as fields in the inspector.
    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;

    // Use this for initialization
    void Awake () {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update () {

    }

    public void SetAnim(string anim)
    {
        anim = anim;
    }
}
