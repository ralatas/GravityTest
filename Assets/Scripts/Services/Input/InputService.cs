using GravityTest.Scripts.Models;
using GravityTest.Scripts.Views;

namespace GravityTest.Scripts.Services
{
    public sealed class InputService : IInputService
    {
        private readonly IInputService[] _inputServices;

        public InputService(MobileControlsView controlsView)
        {
            _inputServices = new IInputService[]
            {
                new KeyboardInputService(),
                new MobileInputService(controlsView)
            };
        }

        public InputFrame ReadInput()
        {
            var move = 0f;
            var jumpPressed = false;

            for (var i = 0; i < _inputServices.Length; i++)
            {
                var frame = _inputServices[i].ReadInput();
                move += frame.Move;
                jumpPressed |= frame.JumpPressed;
            }

            return new InputFrame(UnityEngine.Mathf.Clamp(move, -1f, 1f), jumpPressed);
        }
    }
}
