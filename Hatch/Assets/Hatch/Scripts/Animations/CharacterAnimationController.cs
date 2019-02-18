using Spine.Unity;
using System;
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
    private Vector3 characterDirection;
    private float animationTime = 0;
    private float animationTimeCounter = 0;
    private float walkSpeed = 0.035f;
    private bool m_FacingRight = true;

    private void Awake()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
    }


    void Start () {
        characterDirection = new Vector3() { x = 1, y = 0, z = 0 };
    }

    // Update is called once per frame
    void Update () {
        if (characterDirection.x > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (characterDirection.x < 0 && m_FacingRight)
        {
            Flip();
        }
    }

    public void Wave()
    {
        spineAnimationState = skeletonAnimation.AnimationState;
        spineAnimationState.SetAnimation(1, "wave", false);
    }

    public void Snap()
    {
        spineAnimationState = skeletonAnimation.AnimationState;
        spineAnimationState.SetAnimation(1, "snap", false);
    }

    public void Walk(float duration = 1, Vector3 direction = new Vector3(), float speed = 0.035f, float animSpeed = 1)
    {
        if (Math.Abs(direction.x) < 0)
        {
            characterDirection = new Vector3() { x = 1, y = 0, z = 0 };
        } else
        {
            characterDirection = direction;
        }
        animationTimeCounter = 0;
        animationTime = duration;
        spineAnimationState = skeletonAnimation.AnimationState;
        Debug.Log("walk");
        spineAnimationState.SetAnimation(0, "walk", true).TimeScale = animSpeed;

        this.StartCoroutine(Walking(speed));
        this.StartCoroutine(Idle(duration));
    }
    public IEnumerator Walking(float speed)
    {
        while (animationTime  > animationTimeCounter)
        {
            this.transform.position += (this.characterDirection * speed);
            animationTimeCounter += Time.deltaTime;
            yield return null;
        }
        Debug.Log("walk end");
        animationTimeCounter = 0;
        animationTime = 0;
        yield break;
    }
    public IEnumerator Idle(float duration)
    {
        yield return new WaitForSeconds(duration);
        spineAnimationState = skeletonAnimation.AnimationState;
        spineAnimationState.SetAnimation(0, "idle", false);
        yield break;
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private IEnumerator ExecuteAfterTime(Action theDelegate, float delay)
    {
        yield return new WaitForSeconds(delay);
        theDelegate();
    }
}
