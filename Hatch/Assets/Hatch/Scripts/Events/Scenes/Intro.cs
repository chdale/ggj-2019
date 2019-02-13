using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Hatch.Scripts.Events.Scenes
{
    public class Intro : Scene
    {
        public override void InitScene()
        {
            foreach (KeyValuePair<EVENT, Action> sceneEvent in GetSceneEventMethods(this))
            {
                AddHandler(sceneEvent.Key, TrainFlash);
            }
        }
        [SceneEvent]
        public void TrainFlash()
        {

        }
    }
}
