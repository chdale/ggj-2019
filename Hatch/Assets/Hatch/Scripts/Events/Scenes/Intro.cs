using Assets.Hatch.Scripts.Events.Scenes.SceneData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spine.Unity;
using UnityEngine;
using System.Collections;

namespace Assets.Hatch.Scripts.Events.Scenes
{
    public class Intro : Scene
    {
        public GameObject SubwayFlashPos;
        public GameObject PlayerIntroCamPos;
        public GameObject PlayerIntroEndCamPos;
        public GameObject MysteryManPos;
        public GameObject MysteryManCamPos;
        public GameObject MysteryManCamEndPos;
        public GameObject MysteryMan;
        public GameObject MysteryManIntroDialogueObject;
        public GameObject Medic;
        public GameObject MedicIntroDialogueObject;
        public GameObject PlayerPuppet;
        public GameObject DreamBubble;
        public GameObject Crows;
        public GameObject Arms;
        public GameObject Teeth1;
        public GameObject Teeth2;
        public GameObject BeginGamePos;
        public GameObject PlayerEndPos;
        public AudioSource fullIntroSfx;


        private CharacterAnimationController MysteryManController;
        private CharacterAnimationController MedicController;
        private MysteryManIntro MysteryManIntroDialogue;

        public override void InitScene()
        {
            foreach (KeyValuePair<string, Action> sceneEvent in GetSceneEventMethods(this))
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
            StartCoroutine(DelayAudio(fullIntroSfx, 1f));
            SetFader("closed");
        }

        [SceneEvent]
        public void SubwayCarFlashStart()
        {
            var subwayCarFlashLR = LevelRequirements.PresetLevelRequirements["SubwayCarFlash"];
            subwayCarFlashLR.defaultCameraPosition = SubwayFlashPos.transform.position;
            SetCamera(subwayCarFlashLR);
            SetFader("fadeFastOpen");
            StaticEvent.CameraShake(3, 0.15f);
        }
        [SceneEvent]
        public void SubwayCarFlashEnd()
        {
            SetFader("fadeFastClose");
        }

        [SceneEvent]
        public void PlayerSceneStart()
        {
            var playerIntroLR = LevelRequirements.PresetLevelRequirements["PlayerIntro"];
            playerIntroLR.defaultCameraPosition = PlayerIntroCamPos.transform.position;
            SetCamera(playerIntroLR);
            SetFader("fadeSlowOpen");

            SetCameraLerp(PlayerIntroCamPos.transform.position, PlayerIntroEndCamPos.transform.position, 6);
        }
        [SceneEvent]
        public void PlayerSceneEnd()
        {
            SetFader("fadeSlowClose");
        }

        [SceneEvent]
        public void MysteryManSceneStart()
        {
            var mysteryManLR = LevelRequirements.PresetLevelRequirements["MysteryManIntro"];
            mysteryManLR.defaultCameraPosition = MysteryManCamPos.transform.position;
            SetCamera(mysteryManLR);
            SetFader("fadeSlowBlinkOpen");
            StaticEvent.CameraShake(10, 0.01f);

            MysteryMan.transform.position = MysteryManPos.transform.position;
        }
        [SceneEvent]
        public void MysteryManEnter()
        {
            SetFader("haze");
            MysteryManController.Walk(4, new Vector3() { x = 1, y = 0, z = 0 }, 0.02f, .7f);
        }
        [SceneEvent]
        public void OpenBubble()
        {
            DreamBubble.SetActive(true);
            DreamBubble.GetComponent<DreamBubbleAnimationController>().OpenBubbleStart();
        }
        [SceneEvent]
        public void CrowsStart()
        {

            Crows.GetComponent<CrowsEmitter>().CrowsSceneStart();
        }
        [SceneEvent]
        public void CrowsEnd()
        {
            Crows.GetComponent<CrowsEmitter>().CrowsSceneEnd();
        }
        [SceneEvent]
        public void ArmsStart()
        {
            Arms.GetComponent<ArmsEmitter>().ArmsSceneStart();
        }
        [SceneEvent]
        public void ArmsEnd()
        {
            Arms.GetComponent<ArmsEmitter>().ArmsSceneEnd();
        }
        [SceneEvent]
        public void TeethStart()
        {
            var teeth1 = Teeth1.GetComponent<SkeletonAnimation>();
            teeth1.AnimationState.SetAnimation(1, "bite", false);
            var teeth2 = Teeth2.GetComponent<SkeletonAnimation>();
            teeth2.AnimationState.SetAnimation(1, "bite", false);
        }
        [SceneEvent]
        public void TeethEnd()
        {

        }
        [SceneEvent]
        public void CloseBubble()
        {
            DreamBubble.GetComponent<DreamBubbleAnimationController>().CloseBubbleStart();
        }
        [SceneEvent]
        public void MysteryManWave()
        {
            MysteryManController.Wave();
        }
        [SceneEvent]
        public void MysteryManSnap1()
        {
            MysteryManController.Snap();
            //StaticEvent.CameraShake(3, 0.025f);
        }
        [SceneEvent]
        public void MysteryManSnap2()
        {
            MysteryManController.Snap();
            //StaticEvent.CameraShake(3, 0.025f);
        }
        [SceneEvent]
        public void MysteryManSnap3()
        {
            MysteryManController.Snap();
            //StaticEvent.CameraShake(3, 0.025f);
        }
        [SceneEvent]
        public void MysteryManSnap4()
        {
            MysteryManController.Snap();
            //StaticEvent.CameraShake(3, 0.025f);
        }
        [SceneEvent]
        public void MemoryFocus()
        {
            SetCameraLerp(MysteryManCamPos.transform.position, MysteryManCamEndPos.transform.position, 3);
        }
        [SceneEvent]
        public void MemoryBlur()
        {
            SetCameraLerp(MysteryManCamEndPos.transform.position, MysteryManCamPos.transform.position, 3);
        }
        [SceneEvent]
        public void MysteryManDialogue1()
        {
            MysteryManIntroDialogue.StartDialogue(MysteryMan);
        }
        [SceneEvent]
        public void MysteryManDialogue2()
        {
            MysteryManIntroDialogue.NextDialogue();
        }
        [SceneEvent]
        public void MysteryManDialogue3()
        {
            MysteryManIntroDialogue.NextDialogue();
        }
        [SceneEvent]
        public void MysteryManDialogue4()
        {
            MysteryManIntroDialogue.NextDialogue();
        }
        [SceneEvent]
        public void MysteryManDialogue5()
        {
            MysteryManIntroDialogue.NextDialogue();
        }
        [SceneEvent]
        public void MysteryManDialogue6()
        {
            MysteryManIntroDialogue.NextDialogue();
        }
        [SceneEvent]
        public void MysteryManDialogue7()
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
            MysteryManController.Walk(4, new Vector3() { x = -1, y = 0, z = 0 }, 0.02f, .7f);
        }
        [SceneEvent]
        public void MysteryManSceneEnd()
        {
            SetFader("closed");
        }
        [SceneEvent]
        public void MedicIntroStart()
        {
            var player = GameObject.FindGameObjectWithTag("Player").transform;
            player.position = PlayerEndPos.transform.position;
            var beginGameLR = LevelRequirements.PresetLevelRequirements["BeginGame"];
            beginGameLR.defaultCameraPosition = BeginGamePos.transform.position;
            SetCamera(beginGameLR);
            SetFader("fadeSlowOpen");
        }
        [SceneEvent]
        public void MedicIntroStartDialogue()
        {
            var medicDialogue = MedicIntroDialogueObject.GetComponent<MedicIntro>();
            medicDialogue.StartDialogue(Medic.gameObject);
            MysteryMan.transform.position = Limbo.transform.position;
            PlayerPuppet.transform.position = Limbo.transform.position;
        }
        [SceneEvent]
        public void End()
        {
            MysteryMan.transform.position = Limbo.transform.position;
            PlayerPuppet.transform.position = Limbo.transform.position;


        }
    }
}
