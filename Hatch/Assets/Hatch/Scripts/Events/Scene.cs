using Assets.Hatch.Scripts.Events.Scenes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Assets.Hatch.Scripts.Events
{
    public class Scene : MonoBehaviour
    {
        public GameObject Limbo;
        public SceneEventDictionary SceneEvents;
        private static Dictionary<string, Action> eventTable = new Dictionary<string, Action>();

        public void Awake()
        {
            InitScene();
        }

        public virtual void InitScene()
        {

        }

        public void StartScene()
        {
            foreach (KeyValuePair<string, float> sceneEvent in SceneEvents)
            {
                Broadcast(sceneEvent.Key, sceneEvent.Value);
            }
        }
        public void LastScene()
        {
            var lastEvent = SceneEvents.LastOrDefault();
            Broadcast(lastEvent.Key, 0);
        }

        // Adds a delegate to get called for a specific event
        public static void AddHandler(string evnt, Action action)
        {
            if (!eventTable.ContainsKey(evnt)) eventTable[evnt] = action;
            else eventTable[evnt] += action;
        }

        // Fires the event
        public void Broadcast(string evnt, float time)
        {
            evnt = evnt.ToLower();
            if (eventTable[evnt] != null) Invoke(eventTable[evnt], time);
        }

        public void Invoke(Action theDelegate, float time)
        {
            this.StartCoroutine(ExecuteAfterTime(theDelegate, time));
        }

        private IEnumerator ExecuteAfterTime(Action theDelegate, float delay)
        {
            yield return new WaitForSeconds(delay);
            theDelegate();
        }
        public Dictionary<string, Action> GetSceneEventMethods(Scene scene)
        {
            Attribute type = new SceneEventAttribute();
            var result = new Dictionary<string, Action>();
            var sceneType = scene.GetType();
            foreach (var method in sceneType.GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                var attrs = method.GetCustomAttributes(false);
                if (attrs.Any(x => ((Attribute)x).Match(type)))
                {
                    var action = (Action)Delegate.CreateDelegate(typeof(Action), scene, method);
                    result.Add(method.Name.ToLower(), action);

                    //var names = Enum.GetNames(typeof(EVENT));
                    //var name = names.Where(x => x.ToLower() == method.Name.ToLower()).FirstOrDefault();
                    //EVENT eventEnum;
                    //if (!String.IsNullOrEmpty(name))
                    //{
                    //    try
                    //    {
                    //        eventEnum = (EVENT)Enum.Parse(typeof(EVENT), name);
                    //        result.Add(eventEnum, action);

                    //    }
                    //    catch (Exception e)
                    //    {

                    //    }
                    //}
                }
            }
            return result;
        }
        public static void LoadLevel(LevelRequirement levelReq)
        {
            Camera.main.GetComponent<CameraController>().LoadLevel(levelReq);
        }

        public static void SetCamera(LevelRequirement levelReq)
        {
            Camera.main.GetComponent<CameraController>().SetCamera(levelReq);
        }

        public static void SetCameraLerp(Vector3 startPos, Vector3 endPos, float duration)
        {
            Camera.main.GetComponent<CameraController>().CameraLerpStart(startPos, endPos, duration);
        }

        public static void StartDialogue(GameObject dialogueTarget = null)
        {
            GameObject.Find("GameController").GetComponent<GameController>().StartDialogueEvent(dialogueTarget);
        }
        public static void SetFader(string trigger)
        {
            var camController = Camera.main.GetComponent<CameraController>();
            var fader = camController.GetSceneFader();
            fader.SetTrigger(trigger);
        }

        internal IEnumerator DelayAudio(AudioSource audio, float v)
        {
            yield return new WaitForSeconds(v);
            audio.Play();
        }

    }
    public class SceneEventAttribute : Attribute
    {

    }
}
