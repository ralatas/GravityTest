using GravityTest.Scripts.Models;
using UnityEngine;

namespace GravityTest.Scripts.Services
{
    public sealed class RectangleGravityPathService
    {
        private readonly Vector2 _center;
        private readonly float _outerHalfWidth;
        private readonly float _outerHalfHeight;
        private readonly float _cornerRadius;
        private readonly float _horizontalLength;
        private readonly float _verticalLength;
        private readonly float _quarterArcLength;

        public RectangleGravityPathService(GameConfig config)
        {
            _center = config.PlatformCenter;
            _outerHalfWidth = config.PlatformSize.x * 0.5f + config.SurfaceOffset;
            _outerHalfHeight = config.PlatformSize.y * 0.5f + config.SurfaceOffset;
            _cornerRadius = Mathf.Min(config.CornerRadius, _outerHalfWidth, _outerHalfHeight);
            _horizontalLength = Mathf.Max(0.01f, 2f * (_outerHalfWidth - _cornerRadius));
            _verticalLength = Mathf.Max(0.01f, 2f * (_outerHalfHeight - _cornerRadius));
            _quarterArcLength = Mathf.PI * 0.5f * _cornerRadius;
            Perimeter = 2f * (_horizontalLength + _verticalLength) + 4f * _quarterArcLength;
        }

        public float Perimeter { get; }

        public PathSample Evaluate(float distance)
        {
            distance = Repeat(distance, Perimeter);

            if (distance <= _horizontalLength)
            {
                return BuildLinear(
                    new Vector2(-_outerHalfWidth + _cornerRadius + distance, -_outerHalfHeight),
                    Vector2.right,
                    Vector2.down);
            }

            distance -= _horizontalLength;

            if (distance <= _quarterArcLength)
            {
                return BuildArc(
                    new Vector2(_outerHalfWidth - _cornerRadius, -_outerHalfHeight + _cornerRadius),
                    -90f,
                    distance / _quarterArcLength * 90f);
            }

            distance -= _quarterArcLength;

            if (distance <= _verticalLength)
            {
                return BuildLinear(
                    new Vector2(_outerHalfWidth, -_outerHalfHeight + _cornerRadius + distance),
                    Vector2.up,
                    Vector2.right);
            }

            distance -= _verticalLength;

            if (distance <= _quarterArcLength)
            {
                return BuildArc(
                    new Vector2(_outerHalfWidth - _cornerRadius, _outerHalfHeight - _cornerRadius),
                    0f,
                    distance / _quarterArcLength * 90f);
            }

            distance -= _quarterArcLength;

            if (distance <= _horizontalLength)
            {
                return BuildLinear(
                    new Vector2(_outerHalfWidth - _cornerRadius - distance, _outerHalfHeight),
                    Vector2.left,
                    Vector2.up);
            }

            distance -= _horizontalLength;

            if (distance <= _quarterArcLength)
            {
                return BuildArc(
                    new Vector2(-_outerHalfWidth + _cornerRadius, _outerHalfHeight - _cornerRadius),
                    90f,
                    distance / _quarterArcLength * 90f);
            }

            distance -= _quarterArcLength;

            if (distance <= _verticalLength)
            {
                return BuildLinear(
                    new Vector2(-_outerHalfWidth, _outerHalfHeight - _cornerRadius - distance),
                    Vector2.down,
                    Vector2.left);
            }

            distance -= _verticalLength;

            return BuildArc(
                new Vector2(-_outerHalfWidth + _cornerRadius, -_outerHalfHeight + _cornerRadius),
                180f,
                distance / _quarterArcLength * 90f);
        }

        private PathSample BuildLinear(Vector2 point, Vector2 tangent, Vector2 outwardNormal)
        {
            return new PathSample(_center + point, tangent, outwardNormal);
        }

        private PathSample BuildArc(Vector2 arcCenter, float startAngle, float deltaAngle)
        {
            var angle = (startAngle + deltaAngle) * Mathf.Deg2Rad;
            var outward = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            var point = _center + arcCenter + outward * _cornerRadius;
            var tangent = new Vector2(-outward.y, outward.x);
            return new PathSample(point, tangent, outward);
        }

        private static float Repeat(float value, float length)
        {
            if (length <= 0f)
            {
                return 0f;
            }

            value %= length;
            return value < 0f ? value + length : value;
        }
    }
}
