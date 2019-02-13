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
        public SceneEventDictionary SceneEvents;
        public enum EVENT { TrainFlash };

        public virtual void InitScene()
        {

        }

        public void StartScene()
        {
            foreach (KeyValuePair<EVENT, float> sceneEvent in SceneEvents)
            {
                Broadcast(sceneEvent.Key, sceneEvent.Value);
            }
        }

        private static Dictionary<EVENT, Action> eventTable
             = new Dictionary<EVENT, Action>();

        // Adds a delegate to get called for a specific event
        public static void AddHandler(EVENT evnt, Action action)
        {
            if (!eventTable.ContainsKey(evnt)) eventTable[evnt] = action;
            else eventTable[evnt] += action;
        }

        // Fires the event
        public void Broadcast(EVENT evnt, float time)
        {
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
        public Dictionary<EVENT, Action> GetSceneEventMethods(Scene scene)
        {
            Attribute type = new SceneEventAttribute();
            var result = new Dictionary<EVENT, Action>();
            var sceneType = scene.GetType();
            foreach (var method in sceneType.GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                var attrs = method.GetCustomAttributes(false);
                if (attrs.Any(x => ((Attribute)x).Match(type)))
                {
                    var action = (Action)Delegate.CreateDelegate(typeof(Action), method);
                    var names = Enum.GetNames(typeof(EVENT));
                    var name = names.Where(x => x.ToLower() == method.Name.ToLower()).FirstOrDefault();;
                    EVENT eventEnum;
                    if (!String.IsNullOrEmpty(name))
                    {
                        try
                        {
                            eventEnum = (EVENT)Enum.Parse(typeof(EVENT), name);
                            result.Add(eventEnum, action);

                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
            }
            return result;
        }

    }
    public class SceneEventAttribute : Attribute
    {

    }
}
