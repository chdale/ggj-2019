using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Hatch.Scripts.Events.Scenes.SceneData
{
    public static class LevelRequirements
    {
        public static Dictionary<string, LevelRequirement> PresetLevelRequirements = new Dictionary<string, LevelRequirement>()
        {
            { "SubwayCarFlash", new LevelRequirement(new UnityEngine.Vector2(), new UnityEngine.Vector3(), Level.Hatch, 4) }
        };
    }
}
