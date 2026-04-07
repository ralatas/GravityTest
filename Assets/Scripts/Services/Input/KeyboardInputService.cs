using GravityTest.Scripts.Models;
using UnityEngine;

namespace GravityTest.Scripts.Services
{
    public sealed class KeyboardInputService : IInputService
    {
        public InputFrame ReadInput()
        {
            var move = 0f;

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                move -= 1f;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                move += 1f;
            }

            var jumpPressed =
                Input.GetKeyDown(KeyCode.Space) ||
                Input.GetKeyDown(KeyCode.W) ||
                Input.GetKeyDown(KeyCode.UpArrow);

            return new InputFrame(Mathf.Clamp(move, -1f, 1f), jumpPressed);
        }
    }
}
