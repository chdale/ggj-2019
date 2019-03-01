using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using System;
using Assets.Hatch.Scripts.Events.Scenes.SceneData;

namespace Assets.Hatch.Scripts.Events.Scenes
{
    public class SceneController : MonoBehaviour
    {
        // Controls
        public bool EnableCinematics;
        public GameObject PlayerSpawn;
        public GameObject Player;

        // Scenes
        public GameObject Intro;
        private GameController gameController;

        private void Start()
        {
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
            if (gameController.currentGameState >= Enumerations.GameState.Intro)
            {
                EnableCinematics = false;
            }

            var intro = Intro.GetComponent<Intro>();

            if (EnableCinematics)
            {
                this.StartCoroutine(SceneStart(intro.StartScene));
            }
            else
            {
                this.StartCoroutine(SceneStart(intro.LastScene));
                Player.transform.position = PlayerSpawn.transform.position;

            }
        }
        public IEnumerator SceneStart(Action action, float delay = 1f)
        {
            float elapsed = 0.0f;

            while (elapsed < delay)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }
            action.Invoke();

            yield break;
        }
    }
}