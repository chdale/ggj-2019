using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        private float h;
        private bool ableToJump;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
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
            m_Jump = false;
        }

        //private void OnCollisionStay2D(Collision2D collision)
        //{
        //    if (collision.gameObject.layer == 8)
        //    {
        //        ableToJump = true;
        //    }
        //    else
        //    {
        //        ableToJump = false;
        //    }
        //}

        //private void OnCollisionExit2D(Collision2D collision)
        //{
        //    ableToJump = false;
        //}
    }
}
