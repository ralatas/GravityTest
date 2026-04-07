using UnityEngine;

namespace GravityTest.Scripts.Views
{
    public sealed class PlatformView : MonoBehaviour
    {
        public void Initialize(Vector2 position)
        {
            transform.position = position;
        }
    }
}
