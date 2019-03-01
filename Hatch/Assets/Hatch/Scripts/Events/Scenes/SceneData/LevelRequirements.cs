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
            { "SubwayCarFlash", new LevelRequirement(new UnityEngine.Vector2(), new UnityEngine.Vector3(), Level.Hatch, 4) },
            { "PlayerIntro", new LevelRequirement(new UnityEngine.Vector2(), new UnityEngine.Vector3(), Level.Hatch, 0.5f) },
            { "DogDomain", new LevelRequirement(new UnityEngine.Vector2(), new UnityEngine.Vector3(), Level.Hatch, 8f) },
            { "PitFall", new LevelRequirement(new UnityEngine.Vector2(), new UnityEngine.Vector3(), Level.Hatch, 3f) },
            { "MysteryManIntro", new LevelRequirement(new UnityEngine.Vector2(), new UnityEngine.Vector3(), Level.Hatch, 5f) },
            { "BeginGame", new LevelRequirement(new UnityEngine.Vector2(0f, -22.83f), new UnityEngine.Vector3(2.17f, -21.73f, -10f), Level.HatchInterior, 5f) }
        };
    }
}
