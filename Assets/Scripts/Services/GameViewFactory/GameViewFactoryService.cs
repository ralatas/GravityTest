using GravityTest.Scripts.Models;
using GravityTest.Scripts.Views;
using UnityEngine;

namespace GravityTest.Scripts.Services
{
    public sealed class GameViewFactoryService : IGameViewFactoryService
    {
        private readonly GameConfig _config;

        public GameViewFactoryService(GameConfig config)
        {
            _config = config;
        }

        public PlatformView CreatePlatform(Transform root)
        {
            var platformView = InstantiatePrefab(_config.PlatformViewPrefab, root);
            platformView.Initialize(_config.PlatformCenter);
            return platformView;
        }

        public PlayerView CreatePlayer(Transform root)
        {
            var playerView = InstantiatePrefab(_config.PlayerViewPrefab, root);
            playerView.Initialize(_config.PlayerSize);
            return playerView;
        }

        public MobileControlsView CreateMobileControls(RectTransform root)
        {
            return InstantiatePrefab(_config.MobileControlsViewPrefab, root);
        }

        private static TView InstantiatePrefab<TView>(TView prefab, Transform parent) where TView : Object
        {
            if (prefab == null)
            {
                throw new MissingReferenceException($"Prefab reference for type {typeof(TView).Name} is missing in GameConfig.");
            }

            return Object.Instantiate(prefab, parent);
        }
    }
}
