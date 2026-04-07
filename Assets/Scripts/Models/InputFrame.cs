namespace GravityTest.Scripts.Models
{
    public readonly struct InputFrame
    {
        public InputFrame(float move, bool jumpPressed)
        {
            Move = move;
            JumpPressed = jumpPressed;
        }

        public float Move { get; }
        public bool JumpPressed { get; }
    }
}
