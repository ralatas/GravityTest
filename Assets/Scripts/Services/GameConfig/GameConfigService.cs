using GravityTest.Scripts.Models;
using UnityEngine;

namespace GravityTest.Scripts.Services
{
    public sealed class GameConfigService : IGameConfigService
    {
        private const string ResourcePath = "GameConfig";
        private GameConfig _cachedConfig;

        public GameConfig Load()
        {
            if (_cachedConfig != null)
            {
                return _cachedConfig;
            }

            _cachedConfig = Resources.Load<GameConfig>(ResourcePath);

            if (_cachedConfig != null)
            {
                return _cachedConfig;
            }

            _cachedConfig = ScriptableObject.CreateInstance<GameConfig>();
            Debug.LogWarning($"GameConfig not found in Resources/{ResourcePath}. Default ScriptableObject instance was created.");
            return _cachedConfig;
        }
    }
}
