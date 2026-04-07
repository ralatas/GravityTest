using GravityTest.Scripts.Services;
using GravityTest.Scripts.Views;

namespace GravityTest.Scripts.Presenters
{
    public sealed class PlayerPresenter
    {
        private readonly PlayerView _playerView;
        private readonly PlayerMotorService _playerMotorService;
        private readonly IInputService _inputService;

        public PlayerPresenter(PlayerView playerView, PlayerMotorService playerMotorService, IInputService inputService)
        {
            _playerView = playerView;
            _playerMotorService = playerMotorService;
            _inputService = inputService;
        }

        public void Tick(float deltaTime)
        {
            var inputFrame = _inputService.ReadInput();
            var sample = _playerMotorService.Tick(inputFrame, deltaTime);
            _playerView.Render(sample.Point, sample.OutwardNormal);
        }
    }
}
