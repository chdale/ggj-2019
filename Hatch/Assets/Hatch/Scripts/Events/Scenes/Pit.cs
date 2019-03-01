using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Hatch.Scripts.Events.Scenes.SceneData;
using Spine.Unity;
using UnityEngine;

namespace Assets.Hatch.Scripts.Events.Scenes
{
    public class Pit : Scene
    {
        public GameObject SceneTrigger;
        public GameObject Player;
        public GameObject FallingCameraPos;

        private MysteryManIntro MysteryManIntroDialogue;

        public override void InitScene()
        {
            foreach (KeyValuePair<string, Action> sceneEvent in GetSceneEventMethods(this))
            {
                AddHandler(sceneEvent.Key, sceneEvent.Value);
            }
        }
        [SceneEvent]
        public void StartRockFall()
        {
            SetFader("opened");
            var sceneTrigger = SceneTrigger.GetComponent<RockFall>();
            sceneTrigger.RockSceneStart();
            
            // var skeletonAnimation = Player.GetComponent<SkeletonAnimation>();
            // var spineAnimationState = skeletonAnimation.AnimationState;
            // spineAnimationState.SetAnimation(1, "trip", false);
        }
        [SceneEvent]
        public void DisableCollider()
        {
            var sceneTrigger = SceneTrigger.GetComponent<RockFall>();
            sceneTrigger.GetComponent<Collider2D>().enabled = false;
        }
        [SceneEvent]
        public void FadeOut()
        {
            SetFader("fadeFastClose");
        }
        [SceneEvent]
        public void FadeOut2()
        {
            SetFader("fadeFastClose");
        }
        [SceneEvent]
        public void FadeIn()
        {
            SetFader("fadeFastOpen");
        }
        [SceneEvent]
        public void SetFallingCinematic()
        {
            var fallingGameLR = LevelRequirements.PresetLevelRequirements["PitFall"];
            Player.SetActive(false);
            fallingGameLR.defaultCameraPosition = FallingCameraPos.transform.position;
            SetCamera(fallingGameLR);
        }
    }
}
