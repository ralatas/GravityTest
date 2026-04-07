using GravityTest.Scripts.Models;
using GravityTest.Scripts.Views;
using UnityEngine;

namespace GravityTest.Scripts.Services
{
    public sealed class MobileInputService : IInputService
    {
        private readonly MobileControlsView _controlsView;

        public MobileInputService(MobileControlsView controlsView)
        {
            _controlsView = controlsView;
        }

        public InputFrame ReadInput()
        {
            var move = 0f;

            if (_controlsView.IsLeftPressed)
            {
                move -= 1f;
            }

            if (_controlsView.IsRightPressed)
            {
                move += 1f;
            }

            var jump = _controlsView.ConsumeJump();
            return new InputFrame(Mathf.Clamp(move, -1f, 1f), jump);
        }
    }
}
