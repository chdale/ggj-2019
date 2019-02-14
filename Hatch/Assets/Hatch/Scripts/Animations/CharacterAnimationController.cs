using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour {

    // Animation

    [SpineAnimation]
    public string currentAnimation;

    public SkeletonAnimation skeletonAnimation;
    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;
    private string currentAnimationState = "idle";

    private void Awake()
    {
        skeletonAnimation = GameObject.Find("PlayerAnim").GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
    }


    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Walk()
    {

    }
}
