using Assets.Hatch.Scripts.Enumerations;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

namespace Assets.Hatch.Scripts.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Game State Data")]
    public class GameStateDatabase : ScriptableObject
    {
        [SerializeField]
        private GameState_GameStateData _gameStateDictionary;

        [System.Serializable]
        public class GameState_GameStateData : SerializableDictionaryBase<GameState, LevelRequirement> { }
    }
}
