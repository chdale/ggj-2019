using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
        [SerializeField] private float grabDelayTime = .2f;                 // The amount of seconds after releasing a ledge before you can grab again
        [SerializeField] private float characterHeightDistance = 1.088f;    // Character distance above ground
        [SerializeField] private float climbAnimationDelay = 2.5f;          // The amount of time for the character to climb a ledge

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        //private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        private bool canGrab = true; // Determines where or not you can grab a ledge for climbing
        private Transform ledgeTransform; // The transform for the ledge you are grabbing
        private bool isHanging; //Determines whether or not you are hanging from a ledge
        public bool preventMovement = false;

        private void Awake()
        {
            Subscribe();
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            //m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
            //m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            //m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }

        private void Subscribe()
        {
            GameEventManager.OnEntered += EnableInteractText;
            GameEventManager.OnExited += DisableInteractText;
            GameController.InteractActive += EnableInteractText;
            GameController.InteractInactive += DisableInteractText;
            GameController.FinishModal += DisableInteractText;
            GameController.CancelJump += DelayMovement;
            GameController.StopPlayer += StopMovement;
            GameController.StartPlayer += StartMovement;
        }

        public void Move(float move, bool crouch, bool jump)
        {
            if (!m_Rigidbody2D.isKinematic && !preventMovement)
            {// If crouching, check to see if the character can stand up
                if (!crouch /*&& m_Anim.GetBool("Crouch")*/)
                {
                    // If the character has a ceiling preventing them from standing up, keep them crouching
                    if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                    {
                        crouch = true;
                    }
                }

                // Set whether or not the character is crouching in the animator
                //m_Anim.SetBool("Crouch", crouch);

                //only control the player if grounded or airControl is turned on
                if (m_Grounded || m_AirControl)
                {
                    // Reduce the speed if crouching by the crouchSpeed multiplier
                    move = (crouch ? move * m_CrouchSpeed : move);

                    // The Speed animator parameter is set to the absolute value of the horizontal input.
                    //m_Anim.SetFloat("Speed", Mathf.Abs(move));

                    // Move the character
                    m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

                    // If the input is moving the player right and the player is facing left...
                    if (move > 0 && !m_FacingRight)
                    {
                        // ... flip the player.
                        Flip();
                    }
                    // Otherwise if the input is moving the player left and the player is facing right...
                    else if (move < 0 && m_FacingRight)
                    {
                        // ... flip the player.
                        Flip();
                    }
                }
                // If the player should jump...
                if (m_Grounded && jump /*&& m_Anim.GetBool("Ground")*/)
                {
                    // Add a vertical force to the player.
                    m_Grounded = false;
                    //m_Anim.SetBool("Ground", false);
                    m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
                    m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                }
            }
        }

        public void DismountLedge()
        {
            if (isHanging)
            {
                StartCoroutine(SetUngrabbable(grabDelayTime));
            }
        }

        public void ClimbLedge()
        {
            if (isHanging)
            {
                StartCoroutine(BeginClimb(ledgeTransform));
            }
        }

        public void ClimbCorrection()
        {
            transform.position = new Vector2(ledgeTransform.position.x, ledgeTransform.position.y + characterHeightDistance);
            m_Rigidbody2D.isKinematic = false;
        }

        private IEnumerator BeginClimb(Transform ledge)
        {
            isHanging = false;
            canGrab = false;
            float prevGrav = m_Rigidbody2D.gravityScale;
            m_Rigidbody2D.isKinematic = true;
            m_Rigidbody2D.gravityScale = 0;
            yield return new WaitForSeconds(climbAnimationDelay);
            m_Rigidbody2D.gravityScale = prevGrav;
            canGrab = true;
        }

        private IEnumerator SetUngrabbable(float time)
        {
            isHanging = false;
            canGrab = false;
            m_Rigidbody2D.isKinematic = false;
            yield return new WaitForSeconds(time);
            canGrab = true;
        }

        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            Vector3 textScale = transform.GetChild(0).localScale;
            theScale.x *= -1;
            textScale.x *= -1;
            transform.localScale = theScale;
            transform.GetChild(0).localScale = textScale;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (m_Rigidbody2D.velocity.y < 0 && other.tag == "Ledge" && canGrab)
            {
                m_Rigidbody2D.velocity = new Vector2(0f, 0f);
                m_Rigidbody2D.isKinematic = true;
                ledgeTransform = other.transform;
                isHanging = true;
            }
        }

        private void EnableInteractText()
        {
            transform.GetChild(0).gameObject.SetActive(true);

        }

        private void DisableInteractText()
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        private void DelayMovement()
        {
            StartCoroutine(DelayMovementRoutine());
        }

        private IEnumerator DelayMovementRoutine()
        {
            preventMovement = true;
            yield return new WaitForSeconds(.1f);
            preventMovement = false;
        }

        private void StopMovement()
        {
            preventMovement = true;
        }

        private void StartMovement()
        {
            preventMovement = false;
        }

        public bool IsGrounded()
        {
            return m_Grounded;
        }

        public bool IsHanging()
        {
            return isHanging;
        }
    }
}
