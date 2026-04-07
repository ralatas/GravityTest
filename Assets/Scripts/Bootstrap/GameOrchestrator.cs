using GravityTest.Scripts.Models;
using GravityTest.Scripts.Presenters;
using GravityTest.Scripts.Services;
using GravityTest.Scripts.Views;
using UnityEngine;

namespace GravityTest.Scripts.Bootstrap
{
    public sealed class GameOrchestrator : MonoBehaviour
    {
        [SerializeField] private Transform worldRoot;
        [SerializeField] private RectTransform uiRoot;

        private PlayerPresenter _playerPresenter;

        private void Awake()
        {
            if (worldRoot == null)
            {
                Debug.LogError("GameOrchestrator is missing worldRoot reference.", this);
                enabled = false;
                return;
            }

            if (uiRoot == null)
            {
                Debug.LogError("GameOrchestrator is missing uiRoot reference.", this);
                enabled = false;
                return;
            }

            ServiceLocator.Register<IGameConfigService>(new GameConfigService());
            var config = ServiceLocator.Resolve<IGameConfigService>().Load();
            ServiceLocator.Register<IGameViewFactoryService>(new GameViewFactoryService(config));
            ServiceLocator.Register(new RectangleGravityPathService(config));
            ServiceLocator.Register(new PlayerMotorService(
                config,
                ServiceLocator.Resolve<RectangleGravityPathService>()));

            var viewFactory = ServiceLocator.Resolve<IGameViewFactoryService>();
            var platformView = viewFactory.CreatePlatform(worldRoot);
            var playerView = viewFactory.CreatePlayer(worldRoot);
            var mobileControlsView = viewFactory.CreateMobileControls(uiRoot);

            ServiceLocator.Register<IInputService>(new InputService(mobileControlsView));

            _playerPresenter = new PlayerPresenter(
                playerView,
                ServiceLocator.Resolve<PlayerMotorService>(),
                ServiceLocator.Resolve<IInputService>());
            _playerPresenter.Tick(0f);
        }

        private void Update()
        {
            _playerPresenter?.Tick(Time.deltaTime);
        }
    }
}
