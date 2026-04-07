using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GravityTest.Scripts.Views
{
    public sealed class MobileControlsView : MonoBehaviour
    {
        [SerializeField] private Button leftButton;
        [SerializeField] private Button rightButton;
        [SerializeField] private Button jumpButton;

        private bool _jumpQueued;
        private bool _isLeftPressed;
        private bool _isRightPressed;
        private PointerHandler _leftPointerHandler;
        private PointerHandler _rightPointerHandler;

        public bool IsLeftPressed => _isLeftPressed;
        public bool IsRightPressed => _isRightPressed;

        private void OnEnable()
        {
            BindButtons();
        }

        private void OnDisable()
        {
            UnbindButtons();
            _isLeftPressed = false;
            _isRightPressed = false;
            _jumpQueued = false;
        }

        private void BindButtons()
        {
            if (leftButton == null || rightButton == null || jumpButton == null)
            {
                Debug.LogError("MobileControlsView is missing button references.", this);
                return;
            }

            _leftPointerHandler = GetOrAddPointerHandler(leftButton.gameObject);
            _rightPointerHandler = GetOrAddPointerHandler(rightButton.gameObject);

            _leftPointerHandler.PressedChanged += OnLeftPressedChanged;
            _rightPointerHandler.PressedChanged += OnRightPressedChanged;
            jumpButton.onClick.AddListener(OnJumpClicked);
        }

        private void UnbindButtons()
        {
            if (leftButton == null || rightButton == null || jumpButton == null)
            {
                return;
            }

            if (_leftPointerHandler != null)
            {
                _leftPointerHandler.PressedChanged -= OnLeftPressedChanged;
            }

            if (_rightPointerHandler != null)
            {
                _rightPointerHandler.PressedChanged -= OnRightPressedChanged;
            }

            jumpButton.onClick.RemoveListener(OnJumpClicked);
        }

        private static PointerHandler GetOrAddPointerHandler(GameObject target)
        {
            if (target.TryGetComponent<PointerHandler>(out var handler))
            {
                return handler;
            }

            return target.AddComponent<PointerHandler>();
        }

        private void OnLeftPressedChanged(bool isPressed)
        {
            _isLeftPressed = isPressed;
        }

        private void OnRightPressedChanged(bool isPressed)
        {
            _isRightPressed = isPressed;
        }

        private void OnJumpClicked()
        {
            _jumpQueued = true;
        }

        public bool ConsumeJump()
        {
            if (!_jumpQueued)
            {
                return false;
            }

            _jumpQueued = false;
            return true;
        }

        private sealed class PointerHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
        {
            public event System.Action<bool> PressedChanged;

            public void OnPointerDown(PointerEventData eventData)
            {
                PressedChanged?.Invoke(true);
            }

            public void OnPointerUp(PointerEventData eventData)
            {
                PressedChanged?.Invoke(false);
            }

            public void OnPointerExit(PointerEventData eventData)
            {
                PressedChanged?.Invoke(false);
            }
        }
    }
}
