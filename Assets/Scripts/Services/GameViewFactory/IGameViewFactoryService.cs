using GravityTest.Scripts.Views;
using UnityEngine;

namespace GravityTest.Scripts.Services
{
    public interface IGameViewFactoryService
    {
        PlatformView CreatePlatform(Transform root);
        PlayerView CreatePlayer(Transform root);
        MobileControlsView CreateMobileControls(RectTransform root);
    }
}
