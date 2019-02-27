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
            var sceneTrigger = SceneTrigger.GetComponent<RockFall>();
            sceneTrigger.RockSceneStart();
            sceneTrigger.GetComponent<Collider2D>().enabled = false;
            
            var skeletonAnimation = Player.GetComponent<SkeletonAnimation>();
            var spineAnimationState = skeletonAnimation.AnimationState;
            spineAnimationState.SetAnimation(1, "trip", false);
        }
    }
}
