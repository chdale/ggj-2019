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
        public GameObject MysteryMan;
        public GameObject Medic;
        public GameObject MysteryManIntroDialogueObject;

        private CharacterAnimationController MysteryManController;
        private CharacterAnimationController MedicController;
        private MysteryManIntro MysteryManIntroDialogue;

        public override void InitScene()
        {
            foreach (KeyValuePair<EVENT, Action> sceneEvent in GetSceneEventMethods(this))
            {
                AddHandler(sceneEvent.Key, sceneEvent.Value);
            }
            MysteryManController = MysteryMan.GetComponent<CharacterAnimationController>();
            MedicController = Medic.GetComponent<CharacterAnimationController>();
            MysteryManIntroDialogue = MysteryManIntroDialogueObject.GetComponent<MysteryManIntro>();
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
            MysteryMan.transform.position = MysteryManPos.transform.position;
            MysteryManController.Walk(2, new Vector3() { x = 1, y = 0, z = 0 });
        }
        [SceneEvent]
        public void MysteryManDialogue1()
        {
            MysteryManIntroDialogue.StartDialogue(MysteryMan);
        }
        [SceneEvent]
        public void MysteryManWave()
        {
            MysteryManController.Wave();
        }
        [SceneEvent]
        public void MysteryManDialogue2()
        {
            MysteryManIntroDialogue.NextDialogue();
        }
        [SceneEvent]
        public void MysteryManDialogueEnd()
        {
            MysteryManIntroDialogue.EndDialogue();
        }
        [SceneEvent]
        public void MysteryManExit()
        {
            SetFader("fadeSlowBlinkClose");
            var mysteryMan = MysteryMan.GetComponent<CharacterAnimationController>();
            MysteryManController.Walk(2, new Vector3() { x = -1, y = 0, z = 0 });
        }
        [SceneEvent]
        public void MysteryManSceneEnd()
        {
            SetFader("closed");
        }
    }
}
