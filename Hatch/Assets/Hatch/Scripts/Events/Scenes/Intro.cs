using Assets.Hatch.Scripts.Events.Scenes.SceneData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Hatch.Scripts.Events.Scenes
{
    public class Intro : Scene
    {
        public GameObject SubwayFlashPos;
        public GameObject MysteryManPos;
        public GameObject MysteryManCamPos;

        public override void InitScene()
        {
            foreach (KeyValuePair<EVENT, Action> sceneEvent in GetSceneEventMethods(this))
            {
                AddHandler(sceneEvent.Key, sceneEvent.Value);
            }
        }
        [SceneEvent]
        public void StartGame()
        {
            SetFader("closed");
        }
        [SceneEvent]
        public void SubwayCarFlashStart()
        {
            Console.WriteLine("SubwayCarFlashStart");
            var subwayCarFlashLR = LevelRequirements.PresetLevelRequirements["SubwayCarFlash"];
            subwayCarFlashLR.defaultCameraPosition = SubwayFlashPos.transform.position;
            SetCamera(subwayCarFlashLR);
            SetFader("fadeFastOpen");
        }
        [SceneEvent]
        public void SubwayCarFlashEnd()
        {
            Console.WriteLine("SubwayCarFlashEnd");
            SetFader("opened");
            SetFader("fadeFastClose");
        }
        [SceneEvent]
        public void MysteryManSceneStart()
        {
            var mysteryManLR = LevelRequirements.PresetLevelRequirements["MysteryManIntro"];
            mysteryManLR.defaultCameraPosition = MysteryManCamPos.transform.position;
            SetCamera(mysteryManLR);
            SetFader("closed");
            SetFader("fadeSlowBlinkOpen");

        }
        [SceneEvent]
        public void MysteryManEnter()
        {
            SetFader("haze");
        }
        [SceneEvent]
        public void MysteryManDialogue()
        {

        }
        [SceneEvent]
        public void MysteryManExit()
        {
            SetFader("fadeSlowBlinkClose");
        }
        [SceneEvent]
        public void MysteryManSceneEnd()
        {
            SetFader("closed");
        }
    }
}
