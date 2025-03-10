using Circles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic.Match
{
    public class VerificationPoint : MonoBehaviour
    {
        private readonly int _numberIdenticalCircles = 3;

        [SerializeField] private Direction _direction;
        [SerializeField] private float _raycastDistance = 2f;
        [SerializeField] private LayerMask _circleLayer;

        private List<Circle> _lastDetectedCircles = new List<Circle>();
        private bool _isClearingCircles = false;

        public bool IsClearingCircles => _isClearingCircles;

        public List<Circle> CheckForMatch()
        {
            if (_isClearingCircles) return null;

            _lastDetectedCircles = GetCirclesInPoint();

            if (_lastDetectedCircles.Count == _numberIdenticalCircles && AreSameColor(_lastDetectedCircles))
            {
                _isClearingCircles = true;
                StartCoroutine(ResetClearingFlag());
                return _lastDetectedCircles;
            }

            return null;
        }

        public bool IsFull() => _lastDetectedCircles.Count >= _numberIdenticalCircles;

        private IEnumerator ResetClearingFlag()
        {
            yield return new WaitForSeconds(0.5f);
            _isClearingCircles = false;
        }

        private List<Circle> GetCirclesInPoint()
        {
            Vector2 direction = GetDirection();
            Vector2 origin = transform.position;

            RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, _raycastDistance, _circleLayer);
            List<Circle> circles = new List<Circle>();

            foreach (var hit in hits)
            {
                Circle circle = hit.collider.GetComponent<Circle>();

                if (circle != null)
                    circles.Add(circle);
            }

            return circles;
        }

        private bool AreSameColor(List<Circle> circles)
        {
            CircleColor firstColor = circles[0].Color;
            return circles.TrueForAll(circle => circle.Color == firstColor);
        }

        private Vector2 GetDirection()
        {
            switch (_direction)
            {
                case Direction.Right:
                    return Vector2.right;

                case Direction.Top:
                    return Vector2.up;

                case Direction.DiagonalRight:
                    return new Vector2(1, -1).normalized;

                case Direction.DiagonalLeft:
                    return new Vector2(-1, -1).normalized;

                default:
                    return Vector2.zero;
            }
        }
    }
}