using GravityTest.Scripts.Models;
using UnityEngine;

namespace GravityTest.Scripts.Services
{
    public sealed class PlayerMotorService
    {
        private readonly GameConfig _config;
        private readonly RectangleGravityPathService _pathService;
        private PlayerState _state;

        public PlayerMotorService(GameConfig config, RectangleGravityPathService pathService)
        {
            _config = config;
            _pathService = pathService;
            _state = new PlayerState
            {
                DistanceOnPath = 0f,
                JumpOffset = 0f,
                JumpVelocity = 0f,
                IsGrounded = true
            };
        }

        public PlayerState CurrentState => _state;

        public PathSample Tick(InputFrame inputFrame, float deltaTime)
        {
            _state.DistanceOnPath -= inputFrame.Move * _config.MoveSpeed * deltaTime;

            if (inputFrame.JumpPressed && _state.IsGrounded)
            {
                _state.JumpVelocity = _config.JumpSpeed;
                _state.IsGrounded = false;
            }

            if (!_state.IsGrounded || _state.JumpOffset > 0f)
            {
                _state.JumpVelocity -= _config.Gravity * deltaTime;
                _state.JumpOffset += _state.JumpVelocity * deltaTime;

                if (_state.JumpOffset <= 0f)
                {
                    _state.JumpOffset = 0f;
                    _state.JumpVelocity = 0f;
                    _state.IsGrounded = true;
                }
            }

            var sample = _pathService.Evaluate(_state.DistanceOnPath);
            return new PathSample(sample.Point + sample.OutwardNormal * _state.JumpOffset, sample.Tangent, sample.OutwardNormal);
        }
    }
}
