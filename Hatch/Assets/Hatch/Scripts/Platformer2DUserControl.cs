using Spine.Unity;
using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof(PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        private float h;
        private bool ableToJump;

        // Animation

        [SpineAnimation]
        public string idleAnimationName;
        [SpineAnimation]
        public string walkAnimationName;
        [SpineAnimation]
        public string jumpAnimationName;

        public SkeletonAnimation skeletonAnimation;
        public Spine.AnimationState spineAnimationState;
        public Spine.Skeleton skeleton;
        private string currentAnimationState = "idle";


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
            AnimationAwake();
        }

        private void AnimationAwake()
        {
            skeletonAnimation = GameObject.Find("PlayerAnim").GetComponent<SkeletonAnimation>();
            spineAnimationState = skeletonAnimation.AnimationState;
            skeleton = skeletonAnimation.Skeleton;
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = Input.GetKey(KeyCode.Space);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                m_Character.ClimbLedge();
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                m_Character.DismountLedge();
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                h = -1.0f;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                h = 1.0f;
            }
            else
            {
                h = 0.0f;
            }
            // Pass all parameters to the character control script.

            m_Character.Move(h, crouch, m_Jump);
            AnimationUpdate();
            m_Jump = false;
        }

        void AnimationUpdate()
        {
            if (!m_Character.IsGrounded())
            {
                SetAnimationState(0, jumpAnimationName, true);
            }
            else if (Math.Abs(h) > 0)
            {
                SetAnimationState(0, walkAnimationName, true);
            }
            else
            {
                SetAnimationState(0, idleAnimationName, true);
            }
        }
        void SetAnimationState(int trackindex, string animationName, bool loop)
        {
            if (currentAnimationState != animationName)
            {
                skeletonAnimation = GameObject.Find("PlayerAnim").GetComponent<SkeletonAnimation>();
                spineAnimationState = skeletonAnimation.AnimationState;
                spineAnimationState.SetAnimation(0, animationName, true);
                currentAnimationState = animationName;
            }
        }
    }
}
