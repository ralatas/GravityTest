using UnityEngine;

namespace GravityTest.Scripts.Models
{
    public readonly struct PathSample
    {
        public PathSample(Vector2 point, Vector2 tangent, Vector2 outwardNormal)
        {
            Point = point;
            Tangent = tangent.normalized;
            OutwardNormal = outwardNormal.normalized;
        }

        public Vector2 Point { get; }
        public Vector2 Tangent { get; }
        public Vector2 OutwardNormal { get; }
    }
}
