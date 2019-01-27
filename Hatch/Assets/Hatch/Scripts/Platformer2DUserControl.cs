using Spine.Unity;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof(PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private GameController gameController;
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
        [SpineAnimation]
        public string climbAnimationName;
        [SpineAnimation]
        public string hangAnimationName;
        [SpineAnimation]
        public string hangIdleAnimationName;
        [SpineAnimation]
        public string hurtIdleAnimationName;
        [SpineAnimation]
        public string hurtStandAnimationName;

        public SkeletonAnimation skeletonAnimation;
        public Spine.AnimationState spineAnimationState;
        public Spine.Skeleton skeleton;
        private string currentAnimationState = "idle";
        private float climbDuration = 2.5f;
        private float climbStart = -2.5f;
        private bool isClimbing = false;

        private GameObject interactText;


        private void Awake()
        {
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
            m_Character = GetComponent<PlatformerCharacter2D>();
            GameEventManager.OnEntered += SetInteractTextActive;
            GameEventManager.OnExited += SetInteractTextInactive;
            interactText = transform.GetChild(0).gameObject;

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
                m_Jump = Input.GetKeyDown(KeyCode.Space);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (m_Character.IsHanging())
                {
                    climbStart = Time.time;
                    
                }
                m_Character.ClimbLedge();
                
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                m_Character.DismountLedge();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                gameController.InteractEvent();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameController.EscapeFunctionsEvent();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameController.NextDialogueEvent();
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
            if (climbStart + climbDuration > Time.time)
            {
                SetAnimationState(0, climbAnimationName, false);
            }
            else if (m_Character.IsHanging())
            {
                SetAnimationState(0, hangIdleAnimationName, true);
            }
            else if(!m_Character.IsGrounded())
            {
                SetAnimationState(0, jumpAnimationName, true);
            }
            else if (Math.Abs(h) > 0 && !m_Character.noMoveyMrMan)
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
                if (currentAnimationState == climbAnimationName)
                {
                    m_Character.ClimbCorrection();
                }
                skeletonAnimation = GameObject.Find("PlayerAnim").GetComponent<SkeletonAnimation>();
                spineAnimationState = skeletonAnimation.AnimationState;
                spineAnimationState.SetAnimation(0, animationName, true);
                currentAnimationState = animationName;
            }
        }

        private void SetInteractTextInactive()
        {
            interactText.SetActive(false);
        }

        private void SetInteractTextActive()
        {
            interactText.SetActive(true);
        }
    }
}
