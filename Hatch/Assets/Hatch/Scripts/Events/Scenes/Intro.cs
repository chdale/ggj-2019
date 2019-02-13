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

        public override void InitScene()
        {
            foreach (KeyValuePair<EVENT, Action> sceneEvent in GetSceneEventMethods(this))
            {
                AddHandler(sceneEvent.Key, sceneEvent.Value);
            }
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
            SetFader("reset");
            SetFader("fadeFastClose");
        }
    }
}
