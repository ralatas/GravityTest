using UnityEngine;

namespace GravityTest.Scripts.Views
{
    public sealed class PlayerView : MonoBehaviour
    {
        public void Initialize(Vector2 size)
        {
            transform.localScale = new Vector3(size.x, size.y, 1f);
        }

        public void Render(Vector2 position, Vector2 upDirection)
        {
            transform.position = position;
            transform.up = upDirection;
        }
    }
}
