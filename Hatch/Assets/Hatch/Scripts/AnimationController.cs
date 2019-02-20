/******************************************************************************
 * Spine Runtimes Software License v2.5
 *
 * Copyright (c) 2013-2016, Esoteric Software
 * All rights reserved.
 *
 * You are granted a perpetual, non-exclusive, non-sublicensable, and
 * non-transferable license to use, install, execute, and perform the Spine
 * Runtimes software and derivative works solely for personal or internal
 * use. Without the written permission of Esoteric Software (see Section 2 of
 * the Spine Software License Agreement), you may not (a) modify, translate,
 * adapt, or develop new applications using the Spine Runtimes or otherwise
 * create derivative works or improvements of the Spine Runtimes or (b) remove,
 * delete, alter, or obscure any trademarks or any copyright, trademark, patent,
 * or other intellectual property or proprietary rights notices on or in the
 * Software, including any copy thereof. Redistributions in binary or source
 * form must include this license and terms.
 *
 * THIS SOFTWARE IS PROVIDED BY ESOTERIC SOFTWARE "AS IS" AND ANY EXPRESS OR
 * IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
 * MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO
 * EVENT SHALL ESOTERIC SOFTWARE BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
 * PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES, BUSINESS INTERRUPTION, OR LOSS OF
 * USE, DATA, OR PROFITS) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER
 * IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 *****************************************************************************/

using UnityEngine;
using System.Collections.Generic;
using Spine.Unity;

namespace Spine.Unity.Examples
{
    public class AnimationController : MonoBehaviour
    {

        #region Inspector
        // [SpineAnimation] attribute allows an Inspector dropdown of Spine animation names coming form SkeletonAnimation.
        [SpineAnimation]
        public string[] triggeredRestState;
        [SpineAnimation]
        public string[] triggerAnimation;
        [SpineAnimation]
        public string[] untriggerAnimation;
        [SpineAnimation]
        public string[] untriggeredRestState;
        public float[] animationLength;
        public bool isTriggered = false;
        public bool isFinished = false;
        public bool isToggleable;
        public bool lastState;
        public bool isDualStateTransition = true;
        #endregion

        SkeletonAnimation skeletonAnimation;

        // Spine.AnimationState and Spine.Skeleton are not Unity-serialized objects. You will not see them as fields in the inspector.
        public Spine.AnimationState spineAnimationState;
        public Spine.Skeleton skeleton;

        private float introTime = 0f;
        private float outroTime = 0f;
        private bool outro = false;
        private bool intro = false;

        void Start()
        {
            // Make sure you get these AnimationState and Skeleton references in Start or Later.
            // Getting and using them in Awake is not guaranteed by default execution order.

            skeletonAnimation = GetComponent<SkeletonAnimation>();
            spineAnimationState = skeletonAnimation.AnimationState;
            skeleton = skeletonAnimation.Skeleton;

            foreach (var animation in untriggeredRestState)
            {
                spineAnimationState.SetAnimation(0, animation, true);
            }
        }

        public void TriggerAnimationsToggle(bool state = true)
        {
            lastState = isTriggered;
            isTriggered = state;
        }

        private void Update()
        {
            if (isDualStateTransition)
            {
                DualStateTransitionAnimation();
            }
        }

        private void DualStateTransitionAnimation()
        {
            if (outro)
            {
                outroTime -= Time.deltaTime;
            }
            if (intro)
            {
                introTime -= Time.deltaTime;
            }

            if (isTriggered && lastState && outro && outroTime <= 0)
            {
                foreach (var animation in triggeredRestState)
                {
                    spineAnimationState.SetAnimation(0, animation, true);
                    outro = false;
                }
            }
            else if (isTriggered && !lastState)
            {
                foreach (var animation in triggerAnimation)
                {
                    spineAnimationState.SetAnimation(0, animation, false);
                    outro = true;
                    outroTime = animationLength[1];
                }
                lastState = isTriggered;
            }
            else if (!isTriggered && lastState)
            {
                foreach (var animation in untriggerAnimation)
                {
                    spineAnimationState.SetAnimation(0, animation, false);
                    intro = true;
                    introTime = animationLength[0];
                }
                lastState = isTriggered;
            }
            else if (!isTriggered && !lastState && intro && introTime <= 0)
            {
                foreach (var animation in untriggeredRestState)
                {
                    spineAnimationState.SetAnimation(0, animation, true);
                    intro = false;
                }
            }
        }
    }
}
