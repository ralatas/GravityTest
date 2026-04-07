using GravityTest.Scripts.Views;
using UnityEngine;

namespace GravityTest.Scripts.Models
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "GravityTest/Game Config")]
    public sealed class GameConfig : ScriptableObject
    {
        [SerializeField] private Vector2 platformSize = new(6f, 3.5f);
        [SerializeField] private Vector2 platformCenter = Vector2.zero;
        [SerializeField] private float surfaceOffset = 0.45f;
        [SerializeField] private float cornerRadius = 0.65f;
        [SerializeField] private float moveSpeed = 4.5f;
        [SerializeField] private float jumpSpeed = 5.5f;
        [SerializeField] private float gravity = 15f;
        [SerializeField] private Vector2 playerSize = new(0.7f, 0.9f);
        [SerializeField] private PlatformView platformViewPrefab;
        [SerializeField] private PlayerView playerViewPrefab;
        [SerializeField] private MobileControlsView mobileControlsViewPrefab;

        public Vector2 PlatformSize => platformSize;
        public Vector2 PlatformCenter => platformCenter;
        public float SurfaceOffset => surfaceOffset;
        public float CornerRadius => cornerRadius;
        public float MoveSpeed => moveSpeed;
        public float JumpSpeed => jumpSpeed;
        public float Gravity => gravity;
        public Vector2 PlayerSize => playerSize;
        public PlatformView PlatformViewPrefab => platformViewPrefab;
        public PlayerView PlayerViewPrefab => playerViewPrefab;
        public MobileControlsView MobileControlsViewPrefab => mobileControlsViewPrefab;
    }
}
